using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Multis;
using Server.ContextMenus;
using Server.Prompts;
using Server.Items;
using Server.Regions;


namespace Server.Mobiles
{

public class CityRentedVendor : CityPlayerVendor
	{
		private VendorRentalDuration m_RentalDuration;
		private int m_RentalPrice;
		private bool m_LandlordRenew;
		private bool m_RenterRenew;
		private int m_RenewalPrice;
		private CityManagementStone m_Stone;
		private Mobile m_Landlord;
		

		private int m_RentalGold;

		private DateTime m_RentalExpireTime;
		private Timer m_RentalExpireTimer;

		public CityRentedVendor( Mobile landlord, Mobile owner, VendorRentalDuration duration, int rentalPrice, bool landlordRenew, int rentalGold, CityManagementStone stone ) : base( owner, stone )
		{
			m_RentalDuration = duration;
			m_RentalPrice = m_RenewalPrice = rentalPrice;
			m_LandlordRenew = landlordRenew;
			m_RenterRenew = false;
			m_Stone = stone;
			m_Landlord = landlord;
			

			m_RentalGold = rentalGold;

			m_RentalExpireTime = DateTime.Now + duration.Duration;
			m_RentalExpireTimer = new RentalExpireTimer( this, duration.Duration, m_Stone );
			m_RentalExpireTimer.Start();
		}

		public CityRentedVendor( Serial serial ) : base( serial )
		{
		}

		public CityManagementStone Stone
		{
			get{ return m_Stone; }
		}
		
		public VendorRentalDuration RentalDuration
		{
			get{ return m_RentalDuration; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RentalPrice
		{
			get{ return m_RentalPrice; }
			set{ m_RentalPrice = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool LandlordRenew
		{
			get{ return m_LandlordRenew; }
			set{ m_LandlordRenew = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool RenterRenew
		{
			get{ return m_RenterRenew; }
			set{ m_RenterRenew = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Renew
		{
			get{ return LandlordRenew && RenterRenew;  }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RenewalPrice
		{
			get{ return m_RenewalPrice; }
			set{ m_RenewalPrice = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int RentalGold
		{
			get{ return m_RentalGold; }
			set{ m_RentalGold = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime RentalExpireTime
		{
			get{ return m_RentalExpireTime; }
		}

		public override bool IsOwner( Mobile m )
		{
			return m == Owner || m.AccessLevel >= AccessLevel.GameMaster;
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Landlord
		{
			get{ return m_Landlord; }
			
		}

		public static bool IsLegitmateVendor( Mobile from, Mobile m )
		{
			PlayerMobile pm = (PlayerMobile)from;
			
			
			if ( ( m is CityRentedVendor || m is CityPlayerVendor ) &&  pm.City != null )
			{
				CityManagementStone stone = pm.City;
				Region r = m.Region;
				Region reg = from.Region;
				
				if ( r is CityMarketRegion )
				{
					CityMarketRegion region = (CityMarketRegion)r;
					if ( region.Stone == stone )
						return true;
				}
				else if ( reg is CityMarketRegion )
				{
					CityMarketRegion region = (CityMarketRegion)reg;
					if ( region.Stone == stone && ((CityPlayerVendor)m).IsVendorInTown() )
						return true;
				}
					
				return false;
				
			}
			else
				return false;
		}
		
				
		
		public bool IsLandlord( Mobile m )
		{
			if ( m == Landlord )
				return true;
			else
				return false;
		}

		public void ComputeRentalExpireDelay( out int days, out int hours )
		{
			TimeSpan delay = RentalExpireTime - DateTime.Now;

			if ( delay <= TimeSpan.Zero )
			{
				days = 0;
				hours = 0;
			}
			else
			{
				days = delay.Days;
				hours = delay.Hours;
			}
		}

		public void SendRentalExpireMessage( Mobile to )
		{
			int days, hours;
			ComputeRentalExpireDelay( out days, out hours );

			to.SendLocalizedMessage( 1062464, days.ToString() + "\t" + hours.ToString() ); // The rental contract on this vendor will expire in ~1_DAY~ day(s) and ~2_HOUR~ hour(s).
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			m_RentalExpireTimer.Stop();
		}

		
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			if ( from.Alive )
			{
				if ( IsOwner( from ) )
				{
					list.Add( new ContractOptionsEntry( this ) );
					
				}
				if ( from == m_Stone.Mayor )
				{
					list.Add( new RejectContract( this ) );
				}
				
				
			}

			base.GetContextMenuEntries( from, list );
		}

		private class RejectContract : ContextMenuEntry  
		{
			private CityRentedVendor m_Vendor;
						
			public RejectContract( CityRentedVendor vendor ) : base( 6218 )
			{
				m_Vendor = vendor;
			}
			
			public override void OnClick()
			{
				Mobile owner = m_Vendor.Owner;
				
				m_Vendor.LandlordRenew = false;
				owner.SendMessage( "Your vendor contract has been cancelled, you have until the end of the contract to clean them off!" );				
				
				
			}
		}
		
		
		
		private class ContractOptionsEntry : ContextMenuEntry
		{
			private CityRentedVendor m_Vendor;

			public ContractOptionsEntry( CityRentedVendor vendor ) : base( 6209 )
			{
				m_Vendor = vendor;
			}

			public override void OnClick()
			{
				Mobile from = Owner.From;

				if ( m_Vendor.Deleted || !from.CheckAlive() )
					return;

				if ( m_Vendor.IsOwner( from ) )
				{
					from.CloseGump( typeof( CityRenterVendorRentalGump ) );
					from.SendGump( new CityRenterVendorRentalGump( m_Vendor ) ); 
					m_Vendor.SendRentalExpireMessage( from );
				}
				
			}
		}

		

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version

			writer.Write( (Item) m_Stone );
			writer.Write( (Mobile) m_Landlord );
			writer.WriteEncodedInt( m_RentalDuration.ID );

			writer.Write( (int) m_RentalPrice );
			writer.Write( (bool) m_LandlordRenew );
			writer.Write( (bool) m_RenterRenew );
			writer.Write( (int) m_RenewalPrice );

			writer.Write( (int) m_RentalGold );

			writer.WriteDeltaTime( (DateTime) m_RentalExpireTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			m_Stone = (CityManagementStone)reader.ReadItem();
			m_Landlord = reader.ReadMobile();
			int durationID = reader.ReadEncodedInt();
			if ( durationID < VendorRentalDuration.Instances.Length )
				m_RentalDuration = VendorRentalDuration.Instances[durationID];
			else
				m_RentalDuration = VendorRentalDuration.Instances[0];

			m_RentalPrice = reader.ReadInt();
			m_LandlordRenew = reader.ReadBool();
			m_RenterRenew = reader.ReadBool();
			m_RenewalPrice = reader.ReadInt();

			m_RentalGold = reader.ReadInt();

			m_RentalExpireTime = reader.ReadDeltaTime();

			TimeSpan delay = m_RentalExpireTime - DateTime.Now;
			m_RentalExpireTimer = new RentalExpireTimer( this, delay > TimeSpan.Zero ? delay : TimeSpan.Zero, m_Stone );
			m_RentalExpireTimer.Start();
		}

		private class RentalExpireTimer : Timer
		{
			private CityRentedVendor m_Vendor;
			private CityManagementStone m_Stone;

			public RentalExpireTimer( CityRentedVendor vendor, TimeSpan delay, CityManagementStone stone ) : base( delay, vendor.RentalDuration.Duration )
			{
				m_Vendor = vendor;
				m_Stone = stone;

				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				int renewalPrice = m_Vendor.RenewalPrice;

				if ( m_Vendor.Renew && m_Vendor.HoldGold >= renewalPrice )
				{
					m_Vendor.HoldGold -= renewalPrice;
					m_Stone.CityTreasury+= renewalPrice;
					

					m_Vendor.RentalPrice = renewalPrice;
					

					m_Vendor.m_RentalExpireTime = DateTime.Now + m_Vendor.RentalDuration.Duration;
				}
				else
				{
					m_Vendor.Destroy( false );
				}
			}
		}
	}



public class CityRenterVendorRentalGump : BaseVendorRentalGump 
	{
		private CityRentedVendor m_Vendor;

		public CityRenterVendorRentalGump( CityRentedVendor vendor ) : base(
			GumpType.VendorRenter, vendor.RentalDuration, vendor.RentalPrice, vendor.RenewalPrice,
			vendor.Landlord, vendor.Owner, vendor.LandlordRenew, vendor.RenterRenew, vendor.Renew )
		{
			m_Vendor = vendor;
		}

		protected override bool IsValidResponse( Mobile from )
		{
			return m_Vendor.CanInteractWith( from, true );
		}

		protected override void RenterRenewOnExpiration( Mobile from )
		{
			m_Vendor.RenterRenew = !m_Vendor.RenterRenew;
			

			from.SendGump( new CityRenterVendorRentalGump( m_Vendor ) );
		}
	}
}
