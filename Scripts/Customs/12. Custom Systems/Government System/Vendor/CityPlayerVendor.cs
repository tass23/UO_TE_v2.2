using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Prompts;
using Server.Targeting;
using Server.Misc;
using Server.Multis;
using Server.ContextMenus;
using Server.Targets;
using Server.Commands;
using Server.Regions;

namespace Server.Mobiles
{
	public class CityVendorBackpack : VendorBackpack
	{
		public CityVendorBackpack() : base()
		{
			Layer = Layer.Backpack;
			Weight = 1.0;
		}

		
		public override bool CheckHold( Mobile m, Item item, bool message, bool checkItems, int plusItems, int plusWeight )
		{
			return true;
		}

		
		public CityVendorBackpack( Serial serial ) : base( serial )
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
		public class CityPlayerVendor : PlayerVendor
	{

		private Mobile m_Owner;
		private CityManagementStone m_stone;
		private int m_IncomeTax;
		private int m_TaxRate;
		private DateTime m_Die;
		private CityManagementStone m_original;
					
			
			
		public static void Initialize()
		{
			CommandSystem.Register( "MoveVendor", AccessLevel.Player, new CommandEventHandler( MoveVendor_OnCommand ) );
			CommandSystem.Register( "MoveBox", AccessLevel.Player, new CommandEventHandler( MoveVendor_OnCommand) );
		}
		[Usage( "MoveVendor or MoveBox" )]
		[Description( "Moves a City Vendor or resource box")]
		private static void MoveVendor_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			PlayerMobile pm = (PlayerMobile)from;
			
			Region r = from.Region;
			if ( pm.City != null && pm.City.Mayor == pm )
			{
				CityManagementStone stone = pm.City;
				if ( stone.PCRegion == r || ( r is CityMarketRegion && ((CityMarketRegion)r).Stone == stone ) )
				{
					from.SendMessage( "Select the item you wish to move." );
					from.Target = new VendorMoveTarget();
				}
				else
					from.SendMessage( "You may only do this if you are the mayor of this town!");
			}
			else
				from.SendMessage( "You may only do this if you are the mayor of a city" );
		}
		
		private class VendorMoveTarget : Target
		{
			public VendorMoveTarget() : base( -1, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object o )
			{
							
				if ( o is CityPlayerVendor )
				{
					if ( ((CityPlayerVendor)o).City == null )
						from.SendMessage( "You may not move a vendor that is sceduled for termination!" );
					
					Mobile mob = o as Mobile;
					if ( PlayerGovernmentSystem.IsAtCity( from, mob ) || CityRentedVendor.IsLegitmateVendor( from, mob ) )
						from.Target = new MoveVendorTarget( o );
					else
						from.SendMessage( "You may only move vendors that are in the public town areas." );
				}
				else if ( o is CityResourceBox )
				{
					Item item = o as Item;
					if ( PlayerGovernmentSystem.IsAtCity( item ) )
						from.Target = new MoveVendorTarget( o );
					else
						from.SendMessage( "You may only move boxes that are in the public town areas." );
				}
				
				else
					from.SendMessage( "You may only use this to move City Vendors or Town Resource Boxes!" );
			}
		
		}
		
		public CityPlayerVendor( Mobile owner, CityManagementStone stone ): base ( owner, null )
		{
			Owner = owner;
			m_stone = stone;
		}
		
		
		public CityPlayerVendor( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (Item) m_original);
			writer.WriteDeltaTime( m_Die );
			writer.Write( (Item) m_stone );
			writer.Write( (int) m_IncomeTax );
			writer.Write( (int) m_TaxRate );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			switch ( version )
			{
			
				case 1:
					{
						m_original = (CityManagementStone)reader.ReadItem();
						goto case 0;
					}
			
				case 0:
					{
						m_Die = reader.ReadDeltaTime();
						m_stone = (CityManagementStone)reader.ReadItem();
						m_IncomeTax = reader.ReadInt();
						m_TaxRate = reader.ReadInt();
			
						if ( m_stone == null )
						{
							Timer t = new CityVendorDismiss( this, m_Die );
							t.Start();
						}
						
						break;
					}
					
			}
			
			if ( m_original == null )
				m_original = m_stone;
		}
		
		public override void InitOutfit()
		{
		
			Item item = new FancyShirt( Utility.RandomNeutralHue() );
			item.Layer = Layer.InnerTorso;
			AddItem( item );
			AddItem( new LongPants( Utility.RandomNeutralHue() ) ); 
			AddItem( new BodySash( Utility.RandomNeutralHue() ) );
			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new Cloak( Utility.RandomNeutralHue() ) );

			Utility.AssignRandomHair( this );

			Container pack = new CityVendorBackpack();
			pack.Movable = false;
			AddItem( pack );
			
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int IncomeTax
		{
			get{ return m_IncomeTax; }
			set{ m_IncomeTax = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int TaxRate
		{
			get{ return m_TaxRate; }
			set{ m_TaxRate = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public CityManagementStone City
		{
			get{ return m_stone; }
			set{ m_stone = value; }
		}	
		
		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime Die
		{
			get{ return m_Die; }
			set{ m_Die = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster)]
		public CityManagementStone OriginalStone
		{
			get{ return m_original;}
			set{ m_original = value; }
		}
		
		

		public override int ChargePerDay
		{
			get
			{ 
				return 0;
			}
		}

		public override int ChargePerRealWorldDay
		{
			get
			{
				return 0;
			}
		}
		
		public override bool IsOwner( Mobile m )
		{
			if ( m.AccessLevel >= AccessLevel.GameMaster )
				return true;

			
			else
			{
				return m == Owner;
			}
		}
		
		public bool IsVendorInTown()
		{
						
			Region cityreg = Region.Find( this.Location, this.Map );

			if ( cityreg != null && City != null )
			{
				if ( cityreg == City.PCRegion )  
					return true;
			}
				

			return false;
		}
				
		public new void Dismiss( Mobile from )
		{
			Container pack = this.Backpack;

			if ( pack != null && pack.Items.Count > 0 )
			{
				SayTo( from, 1038325 ); // You cannot dismiss me while I am holding your goods.
				return;
			}

			if ( HoldGold > 0 )
			{
				GiveGold( from, HoldGold ); 

				if ( HoldGold > 0 )
					return;
			}

			if ( IncomeTax > 0 && m_original != null )
			{
				m_original.CityTreasury += IncomeTax;
				from.SendMessage( "I have deposited your income tax into the town treasury." );
			}
			
			if ( m_original != null )
				m_original.Vendors.Remove( this ); 
			Destroy( true );
		}
		
		public void Dismiss()
		{
			if ( IncomeTax > 0 && m_original != null )
				m_original.CityTreasury += IncomeTax;
			
			if ( m_original != null )
				m_original.Vendors.Remove( this ); 
			
			Destroy( true );
		}
		
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( e.Handled || !from.Alive || from.GetDistanceToSqrt( this ) > 3 )
				return;
			
			if ( e.HasKeyword( 0x40 ) || (e.HasKeyword( 0x175 ) && WasNamed( e.Speech )) ) // vendor dismiss, *dismiss
			{
				if ( IsOwner( from ) )
				{
					this.Dismiss( from );

					e.Handled = true;
				}
			}
			else
				base.OnSpeech( e );
		}
		
		
		
			
	}
}
