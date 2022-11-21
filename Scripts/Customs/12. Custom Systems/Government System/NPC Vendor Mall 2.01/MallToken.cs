using System;
using Server;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{

	public enum MallDuration
	{
		OneWeek,
		TwoWeeks,
		ThreeWeeks,
		FourWeeks
	}
	

	public class VendorMallToken : Item
	{
		private BaseHouse m_house;
		private Mobile m_owner;
		private int m_rent;
		private MallDuration m_duration;
						
		public BaseHouse House
		{
			get{ return m_house; }
			set{ m_house = value; }
		}
		
		public Mobile Owner
		{
			get{ return m_owner; }
			set{ m_owner = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Rent
		{
			get{ return m_rent; }
			set{ m_rent= value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public MallDuration Duration
		{
			get{ return m_duration; }
			set{ m_duration = value; }
		}
		
		
		[Constructable]
		public VendorMallToken( BaseHouse dwelling, MallDuration howlong, int rentcost, Mobile whoowns ) : base( 0xF8E )
		{
			
			if ( dwelling != null )
			{
				Name = String.Format( "A vendor mall token for {0}", dwelling.Sign.GetName() );
				Hue = 1168;
			}
			m_house = dwelling;
			m_duration = howlong;
			m_rent = rentcost;
			m_owner = whoowns;
			
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else 
			{
				BaseHouse building = BaseHouse.FindHouseAt( from );
				if ( building == m_house && this.Owner == from )
				{
					VendorRentalDuration m_Expire;
					m_Expire = VendorRentalDuration.Instances[(int)m_duration];
					PlayerVendor vendor = new RentedVendor( m_owner, m_house, m_Expire, m_rent, true, m_rent );
					vendor.MoveToWorld( from.Location, from.Map );
					this.Delete();
				}
				else
					from.SendMessage( "You may only use this in a vendor mall you have paid for a slot in!" );
			}
		}
		
		public VendorMallToken( Serial serial ) : base( serial )
		{
		}
		
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( (BaseHouse) m_house );
			writer.Write( (Mobile) m_owner );
			writer.Write( (int) m_rent );
			writer.Write( (int) m_duration );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_house = reader.ReadItem() as BaseHouse;
			m_owner = reader.ReadMobile();
			m_rent = reader.ReadInt();
			m_duration = (MallDuration)reader.ReadInt();
					
		}
	
	}
	
	
	
}
