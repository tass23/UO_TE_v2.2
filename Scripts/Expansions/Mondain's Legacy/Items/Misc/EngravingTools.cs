using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Gumps;
using Server.Multis;

namespace Server.Items
{
	interface IEngravable
	{
		string EngravedText{ get; set; }
	}

	public class BaseEngravingTool : Item, IUsesRemaining
	{		
		public virtual Type[] Engraves{ get{ return null; } }
		
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get { return m_UsesRemaining; }
			set { m_UsesRemaining = value; InvalidateProperties(); }
		}
		
		public virtual bool ShowUsesRemaining
		{ 
			get{ return true; } 
			set{}
		} 
		
		[Constructable]
		public BaseEngravingTool( int itemID ) : this( itemID, 1 )
		{
		}
	
		[Constructable]
		public BaseEngravingTool( int itemID, int uses ) : base( itemID )
		{
			Weight = 1.0;
			Hue = 0x48D;
			
			m_UsesRemaining = uses;
		}

		public BaseEngravingTool( Serial serial ) : base( serial )
		{
		}
		
		public bool CheckItem( Item item )
		{
			if ( Engraves == null || item == null )
				return false;
				
			Type type = item.GetType();
				
			for ( int i = 0; i < Engraves.Length; i ++ )
			{			
				if ( type == Engraves[ i ] || type.IsSubclassOf( Engraves[ i ] ) )
					return true;
			}
			
			return false;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			base.OnDoubleClick( from );
			
			if ( !from.NetState.SupportsExpansion( Expansion.ML ) )
			{
				from.SendLocalizedMessage( 1072791 ); // You must upgrade to Mondain's Legacy in order to use that item.				
				return;
			}
			
			if ( m_UsesRemaining > 0 )
			{
				from.SendLocalizedMessage( 1072357 ); // Select an object to engrave.
				from.Target = new InternalTarget( this );
			}
			else
				from.SendLocalizedMessage( 1042544 ); // This item is out of charges.
		}
		
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			
			if ( ShowUsesRemaining )
				list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~			
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_UsesRemaining );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			
			m_UsesRemaining = reader.ReadInt();
		}
		
		private class InternalTarget : Target
		{
			private BaseEngravingTool m_Tool;
		
			public InternalTarget( BaseEngravingTool tool ) : base( 2, true, TargetFlags.None )
			{
				m_Tool = tool;
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Tool == null || m_Tool.Deleted )
					return;
					
				if ( targeted is Item )
				{
					Item item = (Item) targeted;
					
					if ( IsValid( item, from ) )
					{
						if ( item is IEngravable && m_Tool.CheckItem( item ) )
						{
							from.CloseGump( typeof( InternalGump ) );
							from.SendGump( new InternalGump( m_Tool, item ) );
						}
						else
							from.SendLocalizedMessage( 1072309 ); // The selected item cannot be engraved by this engraving tool.
					}
					else
						from.SendLocalizedMessage( 1072310 ); // The selected item is not accessible to engrave.
				}
				else
					from.SendLocalizedMessage( 1072309 ); // The selected item cannot be engraved by this engraving tool.
			}
			
			private bool IsValid( Item item, Mobile m )
			{
				if ( BaseHouse.CheckAccessible( m, item ) )
					return true;
				else if ( item.Movable && !item.IsLockedDown && !item.IsSecure )
					return true;
					
				return false;				
			}
			
			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				from.SendLocalizedMessage( 1072310 ); // The selected item is not accessible to engrave.
			}
		}
		
		private class InternalGump : Gump
		{
			private BaseEngravingTool m_Tool;
			private Item m_Target;
		
			private enum Buttons
			{
				Cancel,
				Okay,
				Text
			}
		
			public InternalGump( BaseEngravingTool tool, Item target ) : base( 0, 0 )
			{
				m_Tool = tool;
				m_Target = target;
			
				Closable = true;
				Disposable = true;
				Dragable = true;
				Resizable = false;
			
				AddPage( 0 );
				
				AddBackground( 50, 50, 400, 300, 0xA28 );
				AddHtmlLocalized( 50, 70, 400, 20, 1072359, 0x0, false, false );
				AddHtmlLocalized( 75, 95, 350, 145, 1072360, 0x0, true, true );				
				
				AddButton( 125, 300, 0x81A, 0x81B, (int) Buttons.Okay, GumpButtonType.Reply, 0 );
				AddButton( 320, 300, 0x819, 0x818, (int) Buttons.Cancel, GumpButtonType.Reply, 0 );
				
				AddImageTiled( 75, 245, 350, 40, 0xDB0 );
				AddImageTiled( 76, 245, 350, 2, 0x23C5 );
				AddImageTiled( 75, 245, 2, 40, 0x23C3 );
				AddImageTiled( 75, 285, 350, 2, 0x23C5 );
				AddImageTiled( 425, 245, 2, 42, 0x23C3 );
				
				AddTextEntry( 78, 245, 345, 40, 0x0, (int) Buttons.Text, "" );
			}
			
			public override void OnResponse( Server.Network.NetState state, RelayInfo info )
			{		
				if ( m_Tool == null || m_Tool.Deleted || m_Target == null || m_Target.Deleted )
					return;
			
				if ( info.ButtonID == (int) Buttons.Okay )
				{
					TextRelay relay = info.GetTextEntry( (int) Buttons.Text );
					
					if ( relay != null )
					{
						if ( relay.Text == null || relay.Text.Equals( "" ) )
						{
							((IEngravable)m_Target).EngravedText = null;
							state.Mobile.SendLocalizedMessage( 1072362 ); // You remove the engraving from the object.
						}
						else
						{
							if ( relay.Text.Length > 40 )
								((IEngravable)m_Target).EngravedText = relay.Text.Substring( 0, 40 );
							else
								((IEngravable)m_Target).EngravedText = relay.Text;
						
							state.Mobile.SendLocalizedMessage( 1072361 ); // You engraved the object.	
							m_Target.InvalidateProperties();						
							m_Tool.UsesRemaining -= 1;
							m_Tool.InvalidateProperties();
						
							if ( m_Tool.UsesRemaining < 1 )
							{
								m_Tool.Delete();
								state.Mobile.SendLocalizedMessage( 1044038 ); // You have worn out your tool!
							}
						}
					}
				}
			}
		}
	}
		
	public class LeatherContainerEngraver : BaseEngravingTool
	{
		public override int LabelNumber{ get{ return 1072152; } } // leather container engraving tool
		
		public override Type[] Engraves{ get{ return new Type[]
		{
			typeof( Pouch ), typeof( Backpack ), typeof( Bag )
		}; } }
		
		[Constructable]
		public LeatherContainerEngraver() : base( 0xF9D, 1 )
		{			
		}

		public LeatherContainerEngraver( Serial serial ) : base( serial )
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
	
	public class WoodenContainerEngraver : BaseEngravingTool
	{
		public override int LabelNumber{ get{ return 1072153; } } // wooden container engraving tool
		
		public override Type[] Engraves{ get{ return new Type[]
		{
			typeof( WoodenBox ), typeof( LargeCrate ), typeof( MediumCrate ), 
			typeof( SmallCrate ), typeof( WoodenChest ), typeof( EmptyBookcase ),
			typeof( Armoire ), typeof( FancyArmoire ), typeof( PlainWoodenChest ), 	
			typeof( OrnateWoodenChest ), typeof( GildedWoodenChest ), typeof( WoodenFootLocker ),
			typeof( FinishedWoodenChest ), typeof( TallCabinet ), typeof( ShortCabinet ), 
			typeof( RedArmoire ), typeof( CherryArmoire ), typeof( MapleArmoire ), 
			typeof( ElegantArmoire ), typeof( Keg ), typeof( SimpleElvenArmoire ),
			typeof( DecorativeBox ), typeof( FancyElvenArmoire ), typeof( RarewoodChest ), typeof( BaseInstrument )					
		}; } }
		
		[Constructable]
		public WoodenContainerEngraver() : base( 0x1026, 1 )
		{			
		}

		public WoodenContainerEngraver( Serial serial ) : base( serial )
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
	
	public class MetalContainerEngraver : BaseEngravingTool
	{
		public override int LabelNumber{ get{ return 1072154; } } // metal container engraving tool
		
		public override Type[] Engraves{ get{ return new Type[]
		{
			typeof( ParagonChest ), typeof( MetalChest )
		}; } }
		
		[Constructable]
		public MetalContainerEngraver() : base( 0x1EB8, 1 )
		{			
		}

		public MetalContainerEngraver( Serial serial ) : base( serial )
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
	
	public class FoodEngraver : BaseEngravingTool
	{
		public override int LabelNumber{ get{ return 1072951; } } // food decoration tool
	
		public override Type[] Engraves{ get{ return new Type[]
		{
			typeof( Cake ), typeof( CheesePizza ), typeof( SausagePizza ),
			typeof( Cookies )
		}; } }
		
		[Constructable]
		public FoodEngraver() : base( 0x1BD1, 1 )
		{			
		}

		public FoodEngraver( Serial serial ) : base( serial )
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
	
	public class SpellbookEngraver : BaseEngravingTool
	{
		public override int LabelNumber{ get{ return 1072151; } } // spellbook engraving tool
		
		public override Type[] Engraves{ get{ return new Type[]
		{
			typeof( Spellbook )
		}; } }
		
		[Constructable]
		public SpellbookEngraver() : base( 0xFBF, 1 )
		{			
		}

		public SpellbookEngraver( Serial serial ) : base( serial )
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
}