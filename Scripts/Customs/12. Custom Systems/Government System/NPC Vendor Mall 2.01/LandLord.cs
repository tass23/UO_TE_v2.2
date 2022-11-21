using System;
using System.Collections.Generic;
using System.Collections;
using Server;
using Server.Items;
using Server.Multis;
using Server.Targeting;
using Server.Misc;


namespace Server.Mobiles
{
	public abstract class BaseLandLord : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		private BaseHouse m_Godhouse;
		private int m_RentCost = 250;
		private MallDuration m_duration;
		
		public static void Initialize()
		{
			ClearMallVendors();
		}
		
				
		[CommandProperty(AccessLevel.GameMaster)]
		public MallDuration Duration
		{
			get{ return m_duration; }
			set{ m_duration = value; }
		}
		
		[CommandProperty(AccessLevel.GameMaster)]
		public int RentCost
		{
			get { return m_RentCost;}
			set { m_RentCost = value;}
		}
				
		public BaseHouse Godhouse
		{
			get { return m_Godhouse;}
			set { m_Godhouse = value;}
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( this.Godhouse != null && from.AccessLevel >= AccessLevel.GameMaster )
				from.SendMessage( "This landlord has already been assigned!" );
						
			else if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.SendMessage( "Target the house sign of the house to assign this landlord to." );
				from.Target = new InternalTarget( this );
			}
			else
				base.OnDoubleClick( from );
		}
		
		public override void InitSBInfo()
		{
		}
		
			
		public virtual void SayPriceTo( Mobile m )
		{
			SayTo( m, String.Format( "The rent payment is {0} gold pieces.", RentCost ) );
		}
		
		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 4 ) )
				return true;
			
			return base.HandlesOnSpeech( from );
		}
		
		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;
			
			if ( !e.Handled && from is PlayerMobile && from.InRange( this.Location, 4 ))
			{
				if ( e.HasKeyword( 0x0004 ) ) // *join*
				{
					SayPriceTo( from );
					
					e.Handled = true;
				}
			}
			
			base.OnSpeech( e );
		}
		
		public override bool OnGoldGiven( Mobile from, Gold dropped )
		{
			if ( m_Godhouse == null )
			{
				from.SendMessage( "No house has been assigned yet." );
				return false;
			}
			
			if (  dropped.Amount == RentCost )
			{
				
				VendorMallToken token = new VendorMallToken( m_Godhouse, m_duration, RentCost, from );
				from.AddToBackpack( token );
				SayTo( from, "You have purchased a spot for your vendor.  Stand in the place you wish to place them and doubleclick the token." );
				return true;
			}
			else 
			{
				SayTo( from, "That's not the right amount!" );
				SayPriceTo( from );
				return false;
			}
			
			
		
		}
		
		public BaseLandLord( string title ) : base( title )
		{
			Title = String.Format( "the {0} ", title );
			
		}
		
		public BaseLandLord( Serial serial ) : base( serial )
		{
		}
		
		public static void ClearMallVendors()
		{
			ArrayList list = new ArrayList( World.Mobiles.Values );
			int count = 0;
			foreach ( Mobile mob in list )
			{
				if ( mob is RentedVendor )
				{
					RentedVendor vendor = (RentedVendor)mob;
					Mobile landlord = vendor.Landlord;
					if ( landlord is BaseLandLord )
					{
						vendor.RentalGold = 0;
						count += 1;
					}
				}
			}
			Console.WriteLine( "{0} mall vendors cleared", count );
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( (int) m_RentCost);		
			writer.Write( (BaseHouse) m_Godhouse);	
			writer.Write( (int) m_duration);
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_RentCost = reader.ReadInt();
			m_Godhouse = reader.ReadItem() as BaseHouse;
			m_duration = (MallDuration)reader.ReadInt();
				
			
		}
		
		
		private class InternalTarget : Target
		{
			private BaseLandLord m_landlord;
			public BaseHouse m_Godhouse;
			
			public InternalTarget( BaseLandLord landlord ) : base( 30, false, TargetFlags.None ) // range, allowground, flags
			{
				m_landlord = landlord;
				
			}
			
			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( from == null )
					return;
				
				if ( targeted is HouseSign )
				{
					HouseSign sign = ( HouseSign ) targeted;
				
					if ( sign.Owner == null )
					{
						from.SendMessage( "That house sign does not seem to have a house associated with it." );
					}
					else
					{
						m_Godhouse = sign.Owner;
										
						if ( m_Godhouse.IsOwner( from ) )
						{
							m_Godhouse.Owner = m_landlord;
							m_Godhouse.Public = true;
							m_Godhouse.RestrictDecay = true;
							m_landlord.Direction = from.Direction & Direction.Mask;
							m_landlord.MoveToWorld( from.Location, from.Map );
							m_landlord.Godhouse = m_Godhouse;
							m_landlord.SayTo( from, "Props me and set the terms!" );
						}
						else
							from.SendMessage( "This house is owned by a player and is off limits!" );
					}
				}
				else
					from.SendMessage( "That does not appear to be a house sign." );
			
		  }
	}
	}
	public class LandLord : BaseLandLord
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
	
		[Constructable]
		public LandLord() : base( "LandLord")
		{
			Frozen = true;
		}
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBLandLord() );
		}
		
		public LandLord( Serial serial ) : base( serial )
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
			
			Frozen = true;
			
		}
	}
}

