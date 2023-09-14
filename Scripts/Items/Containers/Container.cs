using System;
using Server;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Engines.Craft;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public abstract class FakeContainer : Item, ISecurable
    {
        private SecureLevel m_Level;
        [CommandProperty(AccessLevel.GameMaster)]
        public SecureLevel Level
        {
            get { return m_Level; }
            set { m_Level = value; }
        }
		private bool m_state = false;
        public void ClearReadyState()
        {
            m_state = false;
        }
		
		public override bool IsAccessibleTo( Mobile m )
		{
			if ( !BaseHouse.CheckAccessible( m, this ) )
				return false;

			return base.IsAccessibleTo( m );
		}
		
        protected bool ReadyState()
        {
            bool temp = m_state;
            m_state = true;
            return temp || this.RootParent is Mobile == false;
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            SetSecureLevelEntry.AddTo(from, this, list);
        }
		
		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
		}
       
        // ------ ignore stuff below this line ------------
        public FakeContainer(int item) : base(item) { }
        public FakeContainer(Serial serial) : base(serial) { }
        public override void Serialize(GenericWriter writer) { base.Serialize(writer); }
        public override void Deserialize(GenericReader reader) { base.Deserialize(reader); }
    }
	public abstract class BaseContainer : Container, ICraftable, IEngravable
	{
		public override int DefaultMaxWeight
		{
			get
			{
				if ( IsSecure )
					return 0;

				return base.DefaultMaxWeight;
			}
		}

		#region Mondain's Legacy
		private ClothingQuality m_Quality;
		private CraftResource m_Resource;
		private Mobile m_Crafter;
		private string m_EngravedText;

		[CommandProperty( AccessLevel.GameMaster )]
		public ClothingQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource;	}
			set
			{
				if ( m_Resource != value )
				{
					m_Resource = value;
					Hue = CraftResources.GetHue( m_Resource );

					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string EngravedText
		{
			get{ return m_EngravedText; }
			set{ m_EngravedText = value; InvalidateProperties(); }
		}
		#endregion

		public BaseContainer( int itemID ) : base( itemID )
		{
		}

		public override bool IsAccessibleTo( Mobile m )
		{
			if ( !BaseHouse.CheckAccessible( m, this ) )
				return false;

			return base.IsAccessibleTo( m );
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			if ( this.IsSecure && !BaseHouse.CheckHold( m, this, item, message, checkItems, plusItems, plusWeight ) )
				return false;

			return base.CheckHold( m, item, message, checkItems, plusItems, plusWeight );
		}

		public override bool CheckItemUse( Mobile from, Item item )
		{
			if ( IsDecoContainer && item is BaseBook )
				return true;

			return base.CheckItemUse( from, item );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );
			SetSecureLevelEntry.AddTo( from, this, list );
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{
			if ( !CheckHold( from, dropped, sendFullMessage, true ) )
				return false;
			//FakeContainer
				if (dropped is FakeContainer && RootParent is Mobile)
                (dropped as FakeContainer).ClearReadyState();

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( dropped is VendorRentalContract || ( dropped is Container && ((Container)dropped).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, dropped, false ) )
					return false;
			}

			List<Item> list = this.Items;

			for ( int i = 0; i < list.Count; ++i )
			{
				Item item = list[i];

				if ( !(item is Container) && item.StackWith( from, dropped, false ) )
					return true;
			}

			DropItem( dropped );

			return true;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( !CheckHold( from, item, true, true ) )
				return false;
			//Fakecontainer
			if (item is FakeContainer)
                (item as FakeContainer).ClearReadyState();

			BaseHouse house = BaseHouse.FindHouseAt( this );

			if ( house != null && house.IsLockedDown( this ) )
			{
				if ( item is VendorRentalContract || ( item is Container && ((Container)item).FindItemByType( typeof( VendorRentalContract ) ) != null ) )
				{
					from.SendLocalizedMessage( 1062492 ); // You cannot place a rental contract in a locked down container.
					return false;
				}

				if ( !house.LockDown( from, item, false ) )
					return false;
			}

			item.Location = new Point3D( p.X, p.Y, 0 );
			AddItem( item );

			from.SendSound( GetDroppedSound( item ), GetWorldLocation() );

			return true;
		}

		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			base.UpdateTotal( sender, type, delta );

			if ( type == TotalType.Weight && RootParent is Mobile )
				((Mobile) RootParent).InvalidateProperties();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player || from.InRange( this.GetWorldLocation(), 2 ) || this.RootParent is PlayerVendor )
				Open( from );
			else
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
		}

		public override void AddWeightProperty( ObjectPropertyList list )
		{
			base.AddWeightProperty( list );

			#region Mondain's Legacy
			if ( m_EngravedText != null )
				list.Add( 1072305, m_EngravedText ); // Engraved: ~1_INSCRIPTION~

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			if ( m_Quality == ClothingQuality.Exceptional )
				list.Add( 1060636 ); // exceptional
			#endregion
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			#region Mondain's Legacy
			CraftResourceInfo info = CraftResources.IsStandard( m_Resource ) ? null : CraftResources.GetInfo( m_Resource );

			if ( info != null && info.Number > 0 )
				list.Add( info.Number );
			#endregion
		}

		public virtual void Open( Mobile from )
		{
			DisplayTo( from );
		}

		public BaseContainer( Serial serial ) : base( serial )
		{
		}

		/* Note: base class insertion; we cannot serialize anything here */
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			#region Mondain's Legacy
			writer.Write( 0 ); // version

			writer.Write( (string) m_EngravedText );
			writer.Write( (int) m_Quality );
			writer.Write( (int) m_Resource );
			writer.Write( (Mobile) m_Crafter );
			#endregion
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			#region Mondain's Legacy
			int version = reader.ReadInt();

			m_EngravedText = reader.ReadString();
			m_Quality = (ClothingQuality) reader.ReadInt();
			m_Resource = (CraftResource) reader.ReadInt();
			m_Crafter = reader.ReadMobile();
			#endregion
		}

		#region ICraftable
		public virtual int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (ClothingQuality) quality;

			if ( makersMark )
				Crafter = from;

			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Resources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );

			CraftContext context = craftSystem.GetContext( from );

			if ( context != null && context.DoNotColor )
				Hue = 0;

			return quality;
		}
		#endregion
	}

	public class CreatureBackpack : Backpack	//Used on BaseCreature
	{
		[Constructable]
		public CreatureBackpack( string name )
		{
			GumpID = 0x3C;
			Name = name;
			Layer = Layer.Backpack;
			Hue = 5;
			Weight = 3.0;
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Name != null )
				list.Add( 1075257, Name ); // Contents of ~1_PETNAME~'s pack.
			else
				base.AddNameProperty( list );
		}

		public override void OnItemRemoved( Item item )
		{
			if ( Items.Count == 0 )
				this.Delete();

			base.OnItemRemoved( item );
		}

		public override bool OnDragLift( Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player )
				return true;

			from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			return false;
		}

		public override bool TryDropItem( Mobile from, Item dropped, bool sendFullMessage )
		{
			return false;
		}

		public CreatureBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class StrongBackpack : Backpack	//Used on Pack animals
	{
		[Constructable]
		public StrongBackpack()
		{
			Layer = Layer.Backpack;
			Weight = 13.0;
		}

		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			return base.CheckHold( m, item, false, checkItems, plusItems, plusWeight );
		}

		public override int DefaultMaxWeight{ get{ return 1600; } }

		public override bool CheckContentDisplay( Mobile from )
		{
			object root = this.RootParent;

			if ( root is BaseCreature && ((BaseCreature)root).Controlled && ((BaseCreature)root).ControlMaster == from )
				return true;

			return base.CheckContentDisplay( from );
		}

		#region Mondain's Legacy
		public override void UpdateTotal( Item sender, TotalType type, int delta )
		{
			base.UpdateTotal( sender, type, delta );

			if ( type == TotalType.Weight && RootParent is Mobile )
				((Mobile) RootParent).InvalidateProperties();
		}
		#endregion

		public StrongBackpack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 )
				Weight = 13.0;
		}
	}

	public class Backpack : BaseContainer, IDyable
	{
		[Constructable]
		public Backpack() : base( 0xE75 )
		{
			GumpID = 0x3C;
			Layer = Layer.Backpack;
			Weight = 3.0;
		}

		public override int DefaultMaxWeight {
			get {
				if ( Core.ML ) {
					Mobile m = ParentEntity as Mobile;
					if ( m != null && m.Player && m.Backpack == this ) {
						return 550;
					} else {
						return base.DefaultMaxWeight;
					}
				} else {
					return base.DefaultMaxWeight;
				}
			}
		}

		public Backpack( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && ItemID == 0x9B2 )
				ItemID = 0xE75;
		}
	}

	public class Pouch : TrapableContainer
	{
		[Constructable]
		public Pouch() : base( 0xE79 )
		{
			GumpID = 0x3D;
			Weight = 1.0;
		}

		public Pouch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public abstract class BaseBagBall : BaseContainer, IDyable
	{
		public BaseBagBall( int itemID ) : base( itemID )
		{
			GumpID = 0x3D;
			Weight = 1.0;
		}

		public BaseBagBall( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SmallBagBall : BaseBagBall
	{
		[Constructable]
		public SmallBagBall() : base( 0x2256 )
		{
		}

		public SmallBagBall( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LargeBagBall : BaseBagBall
	{
		[Constructable]
		public LargeBagBall() : base( 0x2257 )
		{
		}

		public LargeBagBall( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Bag : BaseContainer, IDyable
	{
		[Constructable]
		public Bag() : base( 0xE76 )
		{
			GumpID = 0x3D;
			Weight = 2.0;
		}

		public Bag( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted ) return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
	
	
	
	public class Barrel : BaseContainer
	{
		[Constructable]
		public Barrel() : base( 0xE77 )
		{
			GumpID = 0x3E;
			Weight = 25.0;
		}

		public Barrel( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 0.0 )
				Weight = 25.0;
		}
	}

	public class Keg : BaseContainer
	{
		[Constructable]
		public Keg() : base( 0xE7F )
		{
			GumpID = 0x3E;
			Weight = 15.0;
		}

		public Keg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PicnicBasket : BaseContainer
	{
		[Constructable]
		public PicnicBasket() : base( 0xE7A )
		{
			GumpID = 0x3F;
			Weight = 2.0; // Stratics doesn't know weight
		}

		public PicnicBasket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class Basket : BaseContainer
	{
		[Constructable]
		public Basket() : base( 0x990 )
		{
			GumpID = 0x3F;
			Weight = 1.0; // Stratics doesn't know weight
		}

		public Basket( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x9AA, 0xE7D )]
	public class WoodenBox : LockableContainer
	{
		[Constructable]
		public WoodenBox() : base( 0x9AA )
		{
			GumpID = 0x43;
			Weight = 4.0;
		}

		public WoodenBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	[Furniture]
	[Flipable( 0x9A9, 0xE7E )]
	public class SmallCrate : LockableContainer
	{
		[Constructable]
		public SmallCrate() : base( 0x9A9 )
		{
			GumpID = 0x44;
			Weight = 2.0;
		}

		public SmallCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 4.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3F, 0xE3E )]
	public class MediumCrate : LockableContainer
	{
		[Constructable]
		public MediumCrate() : base( 0xE3F )
		{
			GumpID = 0x44;
			Weight = 2.0;
		}

		public MediumCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 6.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0xE3D, 0xE3C )]
	public class LargeCrate : LockableContainer
	{
		[Constructable]
		public LargeCrate() : base( 0xE3D )
		{
			GumpID = 0x44;
			Weight = 1.0;
		}

		public LargeCrate( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 8.0 )
				Weight = 1.0;
		}
	}

	[DynamicFliping]
	[Flipable( 0x9A8, 0xE80 )]
	public class MetalBox : LockableContainer
	{
		[Constructable]
		public MetalBox() : base( 0x9A8 )
		{
			GumpID = 0x4B;
		}

		public MetalBox( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 3 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x9AB, 0xE7C )]
	public class MetalChest : LockableContainer
	{
		private static int[] bg_IDs = new int[]
		{
			0x49, 0x4A
		};
			
		[Constructable]
		public MetalChest() : base( 0x9AB )
		{
			GumpID = Utility.RandomList( bg_IDs );
		}

		public MetalChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0xE41, 0xE40 )]
	public class MetalGoldenChest : LockableContainer
	{
		[Constructable]
		public MetalGoldenChest() : base( 0xE41 )
		{
			GumpID = 0x42;
		}

		public MetalGoldenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0xe43, 0xe42 )]
	public class WoodenChest : LockableContainer
	{
		[Constructable]
		public WoodenChest() : base( 0xe43 )
		{
			GumpID = 0x42;
			Weight = 2.0;
		}

		public WoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Weight == 15.0 )
				Weight = 2.0;
		}
	}

	[Furniture]
	[Flipable( 0x280B, 0x280C )]
	public class PlainWoodenChest : LockableContainer
	{
		[Constructable]
		public PlainWoodenChest() : base( 0x280B )
		{
			GumpID = 0x42;
		}

		public PlainWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280D, 0x280E )]
	public class OrnateWoodenChest : LockableContainer
	{
		[Constructable]
		public OrnateWoodenChest() : base( 0x280D )
		{
			GumpID = 0x42;
		}

		public OrnateWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x280F, 0x2810 )]
	public class GildedWoodenChest : LockableContainer
	{
		[Constructable]
		public GildedWoodenChest() : base( 0x280F )
		{
			GumpID = 0x42;
		}

		public GildedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[Furniture]
	[Flipable( 0x2811, 0x2812 )]
	public class WoodenFootLocker : LockableContainer
	{
		[Constructable]
		public WoodenFootLocker() : base( 0x2811 )
		{
			GumpID = 0x10B;
		}

		public WoodenFootLocker( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
			
			if ( version < 2 )
				GumpID = 0x10B;
		}
	}

	[Furniture]
	[Flipable( 0x2813, 0x2814 )]
	public class FinishedWoodenChest : LockableContainer
	{
		[Constructable]
		public FinishedWoodenChest() : base( 0x2813 )
		{
			GumpID = 0x10D;
		}

		public FinishedWoodenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 15 )
				Weight = -1;
		}
	}

	[DynamicFliping]
	[Flipable( 0x4102, 0x4106 )]
	public class PirateChest : LockableContainer
	{
		[Constructable]
		public PirateChest() : base( 0x4102 )
		{
			GumpID = 0x3C;
			Name = "Pirate Chest";
		}

		public PirateChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}
	
	[DynamicFliping]
	[Flipable( 0x4025, 0x4026 )]
	public class GargoyleChest : LockableContainer
	{
		[Constructable]
		public GargoyleChest() : base( 0x4025 )
		{
			GumpID = 0x43;
			Name = "Gargoyle Chest";
		}

		public GargoyleChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && Weight == 25 )
				Weight = -1;
		}
	}
}