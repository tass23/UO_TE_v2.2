using System;
using System.Collections;
using Server.Network;
using Server.Engines.Craft;

namespace Server.Items
{
	public abstract class BaseCraftAle : Item, ICraftable
	{
		private Mobile m_Poisoner;
		private Poison m_Poison;
		private int m_FillFactor;
		private Mobile m_Crafter;
		private AleQuality m_Quality;
		private HopsVariety m_Variety;

		public virtual Item EmptyItem{ get { return null; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Poisoner
		{
			get { return m_Poisoner; }
			set { m_Poisoner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get { return m_Poison; }
			set { m_Poison = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int FillFactor
		{
			get { return m_FillFactor; }
			set { m_FillFactor = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public HopsVariety Variety
		{
			get { return m_Variety; }
			set { if ( m_Variety != value ) { m_Variety = value; InvalidateProperties(); } }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AleQuality Quality
		{
			get{ return m_Quality; }
			set{ m_Quality = value; InvalidateProperties(); }
		}

		public BaseCraftAle( Serial serial ) : base( serial ) { }

		private static void SetSaveFlag( ref SaveFlag flags, SaveFlag toSet, bool setIf )
		{
			if ( setIf ) flags |= toSet;
		}

		private static bool GetSaveFlag( SaveFlag flags, SaveFlag toGet )
		{
			return ( (flags & toGet) != 0 );
		}

		[Flags]
		private enum SaveFlag
		{
			None				= 0x00000000,
			Crafter				= 0x00000001,
			Quality				= 0x00000002,
			Variety				= 0x00000004
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
			SaveFlag flags = SaveFlag.None;
			SetSaveFlag( ref flags, SaveFlag.Crafter, m_Crafter != null );
			SetSaveFlag( ref flags, SaveFlag.Quality, m_Quality != AleQuality.Regular );
			SetSaveFlag( ref flags, SaveFlag.Variety, m_Variety != DefaultVariety );
			writer.WriteEncodedInt( (int) flags );
			if ( GetSaveFlag( flags, SaveFlag.Crafter ) ) writer.Write( (Mobile) m_Crafter );
			if ( GetSaveFlag( flags, SaveFlag.Quality ) ) writer.WriteEncodedInt( (int) m_Quality );
			if ( GetSaveFlag( flags, SaveFlag.Variety ) ) writer.WriteEncodedInt( (int) m_Variety );
			writer.Write( m_Poisoner );
			Poison.Serialize( m_Poison, writer );
			writer.Write( m_FillFactor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			SaveFlag flags = (SaveFlag)reader.ReadEncodedInt();
			if ( GetSaveFlag( flags, SaveFlag.Crafter ) ) m_Crafter = reader.ReadMobile();
			if ( GetSaveFlag( flags, SaveFlag.Quality ) ) m_Quality = (AleQuality)reader.ReadEncodedInt();
			else m_Quality = AleQuality.Regular;
			if ( m_Quality == AleQuality.Low ) m_Quality = AleQuality.Regular;
			if ( GetSaveFlag( flags, SaveFlag.Variety ) ) m_Variety = (HopsVariety)reader.ReadEncodedInt();
			else m_Variety = DefaultVariety;
			if ( m_Variety == HopsVariety.None ) m_Variety = DefaultVariety;
			m_Poisoner = reader.ReadMobile();
			m_Poison = Poison.Deserialize( reader );
			m_FillFactor = reader.ReadInt();
		}

		public virtual HopsVariety DefaultVariety{ get{ return HopsVariety.BitterHops; } }

		public BaseCraftAle( int itemID ) :  base( itemID )
		{
			m_Quality = AleQuality.Regular;
			m_Crafter = null;
			m_Variety = DefaultVariety;
			this.FillFactor = 4;
		}

		public void Drink( Mobile from )
		{
			if ( Thirsty( from, m_FillFactor ) )
			{
				from.PlaySound( Utility.Random( 0x30, 2 ) );
				if ( from.Body.IsHuman && !from.Mounted ) from.Animate( 34, 5, 1, true, false, 0 );
				if ( m_Poison != null ) from.ApplyPoison( m_Poisoner, m_Poison );
				int bac = 5;
				from.BAC += bac;
				if ( from.BAC > 60 ) from.BAC = 60;
				BaseBeverage.CheckHeaveTimer( from );
				this.Consume();
				Item item = EmptyItem;
				if ( item != null ) from.AddToBackpack( item );
			}
		}

		static public bool Thirsty( Mobile from, int fillFactor )
		{
			int iThirst = from.Thirst + fillFactor;
			if ( iThirst >= 20 )
			{
				from.Thirst = 20;
				from.SendMessage( "You manage to drink the beverage, but you are full!" );
			}
			else
			{
				from.Thirst = iThirst;
				if ( iThirst < 5 ) from.SendMessage( "You drink the beverage, but are still extremely thirsty." );
				else if ( iThirst < 10 ) from.SendMessage( "You drink the beverage, and begin to feel more satiated." );
				else if ( iThirst < 15 ) from.SendMessage( "After drinking the beverage, you feel much less thirsty." );
				else from.SendMessage( "You feel quite full after consuming the beverage." );
			}
			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable ) return;
			if ( from.InRange( this.GetWorldLocation(), 1 ) ) Drink( from );
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if (this.Name == null )
			{
				if ( m_Crafter != null ) list.Add( m_Crafter.Name+" Brewery" );
				else list.Add( "Ale" );
			}
			else list.Add (this.Name);
		}

		public override void AddNameProperties( ObjectPropertyList list )
 		{
 			base.AddNameProperties( list );
			string aleType;
			aleType = BrewingResources.GetName( m_Variety );

			if ( m_Quality == AleQuality.Exceptional ) list.Add( 1060847, "Black Label\t{0} Ale", aleType );
 			else list.Add( 1060847, "\t{0}", aleType );
 		}

		public override void OnSingleClick( Mobile from )
		{
			string aleType;

			if (this.Name == null )
			{
				if ( m_Crafter != null ) this.LabelTo( from, "{0} Brewery", m_Crafter.Name );
				else this.LabelTo( from, "Ale" );
			}
			else this.LabelTo( from, "{0}", this.Name );
			aleType = BrewingResources.GetName( m_Variety );
			if ( m_Quality == AleQuality.Exceptional ) this.LabelTo( from, "Black Label {0} Ale", aleType );
 			else this.LabelTo( from, "{0}", aleType );
		}

		#region ICraftable Members
		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (AleQuality)quality;
			if ( Quality == AleQuality.Exceptional ) Crafter = from;
			Item[] items = from.Backpack.FindItemsByType( typeof( BreweryLabelMaker ) );
			if ( items.Length != 0 )
			{
				foreach( BreweryLabelMaker lm in items )
				{
					if (lm.BreweryName != null)
					{
						this.Name = lm.BreweryName;
						break;
					}
				}
			}
			Type resourceType = typeRes;
			if ( resourceType == null ) resourceType = craftItem.Resources.GetAt( 0 ).ItemType;
			Variety = BrewingResources.GetFromType( resourceType );
			CraftContext context = craftSystem.GetContext( from );
			Hue = 0;
			return quality;
		}
		#endregion
	}
}
