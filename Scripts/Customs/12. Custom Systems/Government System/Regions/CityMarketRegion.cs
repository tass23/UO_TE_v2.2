using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Regions
{
	public class CityMarketRegion : GuardedRegion
	{
		public static readonly int MarketPriority = Region.DefaultPriority + 1;
		private CityManagementStone m_Stone;
		private CityLandLord m_LandLord;
		private CivicSign m_Sign;
		
		public CivicSign Sign
		{
			get{ return m_Sign; }
		}		
		
		public CityManagementStone Stone
		{
			get{ return m_Stone; }
		}
		public CityLandLord LandLord
		{
			get{ return m_LandLord; }
		}
		
		public static string PickRegionName()  //Dissallows duplicate region names
		{
			string name = "CityMarketRegion";
			int rndm = Utility.Random( 100000 );
			name += rndm.ToString();
			return name;
					
		}
		
		public CityMarketRegion( CityManagementStone stone, CityLandLord lord, Map map, Rectangle3D[] area, CivicSign sign ) : base( PickRegionName(), map, MarketPriority, area )
		{
			m_Stone = stone;
			m_LandLord = lord;
			m_Sign = sign;
		}
		
		public override bool IsDisabled()
		{
			if ( m_Stone.IsGuarded == false )
				return !Disabled;
			else
				return Disabled;
		}
		
		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}
		
		public override bool AllowSpawn()
		{
			return false;
			
		}
		
		/*public override void OnExit( Mobile m )
		{
			m.SendMessage("You have left." );
			base.OnExit( m );
		}
		
		public override void OnEnter( Mobile m )
		{
			m.SendMessage("You have entered." );
			base.OnEnter( m );
		}*/
	}

	
}
