using System;
using Server;
using Server.Items;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;


namespace Server.Mobiles
{
	
public class CityLandLord : BaseLandLord
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }
		private CityManagementStone m_Stone;
		private Rectangle3D[] m_Area;
		public CityMarketRegion m_Region;
		private CivicSign m_Sign;
		private List<Mobile> m_vendorlist;
		
		public List<Mobile> VendorList
		{
			get{ return m_vendorlist; }
			set{ m_vendorlist = value; }
		}
		
		public CivicSign Sign
		{
			get{ return m_Sign; }
		}
		
		[CommandProperty(AccessLevel.Administrator)]
		public CityManagementStone Stone
		{
			get{ return m_Stone; }
		}
		
		public Rectangle3D[] Box
		{
			get{ return m_Area; }
		}
		
		public static Type[] types = new Type[] //Types of random vendors that can be created.
		{
			typeof( Weaver ), typeof( Tailor ), typeof( Blacksmith ), typeof( Butcher ), typeof( Tanner ),
			typeof( Armorer ), typeof( AnimalTrainer ), typeof( Carpenter ), typeof( Mage ), typeof( Tinker ), typeof( Fisherman ),
			typeof( Alchemist ), typeof( Herbalist ), typeof( InnKeeper ), typeof( Veterinarian ), typeof( Shipwright ), typeof( Miner ),
			typeof( Miller ), typeof( Farmer ), typeof( Bard )
		};
				
		[Constructable]
		public CityLandLord( CityManagementStone stone, Rectangle3D[] area, CivicSign sign, Point3D p, Map map ) : base( "Marketkeeper")
		{
			Frozen = true;
			Point3D loc = new Point3D( p.X - 4, p.Y, p.Z );
			Location = loc;
			Direction = Direction.South;
			Map = map;		
			m_Stone = stone;
			m_Area = area;
			m_Sign = sign;
			m_vendorlist = new List<Mobile>();
			
			UpdateMarketRegion();
			CreateRandomVendors( loc, map );
		}
		
		public void UpdateMarketRegion()
		{
			m_Region = new CityMarketRegion( m_Stone, this, this.Map, m_Area, m_Sign );
			m_Region.Register();
		}
		                               
		
		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBCityLandLord() );
		}
		
		public override void SayPriceTo( Mobile m )
		{
			PlayerMobile pm = (PlayerMobile)m;
			if ( pm.City != m_Stone )
			SayTo( m, String.Format( "To rent a spot in this town's market pay me {0} gold.", RentCost ) );
		}
		
		
		
		
		public override void OnDoubleClick( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;
			if ( pm.City != null && pm == pm.City.Mayor && pm.City == m_Stone )
			{
				from.SendGump( new CityMarketGump( this ) );
			}
			else
				SayPriceTo( from );
			
		}
		
		public override bool OnGoldGiven( Mobile from, Gold dropped ) 
		{
				PlayerMobile pm = (PlayerMobile)from;
				if ( m_Stone == pm.City )
				{
					from.SendMessage( "You are a member of the city and do not need to rent a spot to place your vendor!" );
					return false;
				}
				
				
				else if (  dropped.Amount == RentCost )
				{
					
					CityMallToken token = new CityMallToken( this, m_Sign, from, this.Duration, RentCost );
					from.AddToBackpack( token );
					SayTo( from, "You have purchased a spot for your vendor.  Stand in the place you wish to place them and doubleclick the token." );
					this.Stone.CityTreasury += dropped.Amount;
					return true;
				}
				else
				{
					SayTo( from, "That's not the right amount!" );
					SayPriceTo( from );
					return false;
				}
		}
		
		public override void OnDelete() 
		{
			m_Region.Unregister();
			
			if ( m_vendorlist.Count > 0 )
			{
				for ( int i = 0; i < m_vendorlist.Count; i++ )
				{
					Mobile vend = m_vendorlist[i];
					vend.Delete();
				}
				m_vendorlist.Clear();
			}
			
			this.Stone.CheckVendors( false );
			
		}
		
		public void CreateRandomVendors( Point3D p, Map map ) 
		{
			if ( m_vendorlist.Count > 0 )
			{
				for ( int i = 0; i < m_vendorlist.Count; i++ )
				{
					Mobile vend = m_vendorlist[i];
					vend.Delete();
				}
				m_vendorlist.Clear();
			}
			
			int index = Utility.RandomMinMax( 0, types.Length - 1 );
			int index2 = Utility.RandomMinMax( 0, types.Length - 1 );
			while ( index == index2 ) //Make sure you cannot have the same vendor at once
			{
				index2 = Utility.RandomMinMax( 0, types.Length - 1 );
			}
			
			Mobile mob1 = (Mobile)Activator.CreateInstance( types[index] );
			Mobile mob2 = (Mobile)Activator.CreateInstance( types[index2] );
			
			mob1.Frozen = true;
			mob2.Frozen = true;
			mob1.MoveToWorld( new Point3D( p.X + 1, p.Y, p.Z ), map );
			mob2.MoveToWorld( new Point3D( p.X + 2, p.Y, p.Z ), map );
			mob1.Direction = Direction.South;
			mob2.Direction = Direction.South;
			
			m_vendorlist.Add( mob1 );
			m_vendorlist.Add( mob2 );
		}
		
				
		public CityLandLord( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			
			writer.Write( m_vendorlist );
			writer.Write( m_Sign );
			writer.Write( m_Stone );
			Server.Items.CityManagementStone.WriteRect3DArray( writer, m_Area );
			
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			m_vendorlist = reader.ReadStrongMobileList();
			m_Sign = (CivicSign)reader.ReadItem();
			m_Stone = (CityManagementStone)reader.ReadItem();
			m_Area = Server.Items.CityManagementStone.ReadRect3DArray( reader );
			
			Frozen = true;
			
			foreach ( Mobile m in m_vendorlist )
			{
				m.Frozen = true;
			}
			
			UpdateMarketRegion();
		}
	}
}
