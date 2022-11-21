using System;
using Server;
using Server.Mobiles;
using Server.Regions;

namespace Server.Items
{

	public class CityMallToken : VendorMallToken
	{
		private CityLandLord m_Lord;
		private CivicSign m_Sign;
		
				
		public CityLandLord Lord
		{
			get{ return m_Lord; }
			
		}
		public CivicSign Sign
		{
			get{ return m_Sign; }
		}
		
		[Constructable]
		public CityMallToken( CityLandLord lord, CivicSign sign, Mobile owner, MallDuration duration, int rent ) : base( null, duration, rent, owner )
		{
			Name = String.Format( "A market token for the city of {0}", sign.Stone.CityName );
			Hue = 1159;			
			m_Lord = lord;
			m_Sign = sign;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				Region r = Region.Find( from.Location, from.Map );
				if ( r is CityMarketRegion )
				{
					CityMarketRegion cityreg = (CityMarketRegion)r;
					if ( cityreg.Sign == this.Sign )
					{
						
						VendorRentalDuration m_Expire;
						m_Expire = VendorRentalDuration.Instances[(int)this.Duration];
						CityRentedVendor vendor = new CityRentedVendor( m_Lord, this.Owner, m_Expire, this.Rent, true, this.Rent, this.Sign.Stone );
						vendor.MoveToWorld( from.Location, from.Map );
						CityManagementStone stone = this.Sign.Stone;
						vendor.TaxRate = stone.IncomeTax;
						stone.Vendors.Add( vendor );
						this.Delete();
					}
					else
						from.SendMessage( "You may only do this in a city market that you purchased a spot in!" );
				}
				else
					from.SendMessage( "You may only use this at a Town Market!" );
			}
		}
		
		public CityMallToken( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( m_Lord );
			writer.Write( m_Sign );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_Lord = (CityLandLord)reader.ReadMobile();
			m_Sign = (CivicSign)reader.ReadItem();
		}
	}
	
	
}
