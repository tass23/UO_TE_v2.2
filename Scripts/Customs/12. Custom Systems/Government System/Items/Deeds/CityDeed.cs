using Server;
using System;
using Server.Items;
using Server.Multis;
using Server.Regions;
using Server.Mobiles;
using Server.Targeting;
using System.Collections;

namespace Server.Items
{
	public class CityPlacementTarget : MultiTarget
	{
		private CityDeed m_Deed;

		public CityPlacementTarget( CityDeed deed ) : base( deed.MultiID, deed.Offset )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object o )
		{
			IPoint3D ip = o as IPoint3D;

			if ( ip != null )
			{
				if ( ip is Item )
					ip = ((Item)ip).GetWorldTop();

				Point3D p = new Point3D( ip );

				Region reg = Region.Find( new Point3D( p ), from.Map );

				if ( from.AccessLevel >= AccessLevel.GameMaster || reg.AllowHousing( from, p ) )
				{
					if ( PlayerGovernmentSystem.CheckIfInIlsh( from ) )
					{
						from.SendMessage( "You cannot place this structure on this facet." );
					}
					else if ( m_Deed.Type == CivicStrutureType.FieldStoneCityHall || m_Deed.Type == CivicStrutureType.SandstoneCityHall || m_Deed.Type == CivicStrutureType.NecroCityHall || m_Deed.Type == CivicStrutureType.MarbleCityHall || m_Deed.Type == CivicStrutureType.StoneCityHall || m_Deed.Type == CivicStrutureType.WoodCityHall || m_Deed.Type == CivicStrutureType.AsianCityHall || m_Deed.Type == CivicStrutureType.PlasterCityHall )
					{
						if ( PlayerGovernmentSystem.CheckIfCanBeMayor( from ) )
						{
							PlayerGovernmentSystem.PlaceCityHall( from, p, m_Deed );
						}
						else
						{
							from.SendMessage( "You lack the required skill to become a mayor at this time, You need at least 35.0 forensics." );
						}
					}
					else
					{
						PlayerGovernmentSystem.PlaceCivic( from, p, m_Deed );
					}
				}
				else if ( reg is TreasureRegion )
					from.SendLocalizedMessage( 1043287 ); // The house could not be created here.  Either something is blocking the house, or the house would not be on valid terrain.
				else
					from.SendLocalizedMessage( 501265 ); // Housing can not be created in this area.
			}
		}
	}

	public abstract class CityDeed : Item
	{
		private int m_MultiID;
		private Point3D m_Offset;
		private CivicStrutureType m_Type;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MultiID
		{
			get{ return m_MultiID; }
			set{ m_MultiID = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Offset
		{
			get{ return m_Offset; }
			set{ m_Offset = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CivicStrutureType Type
		{
			get{ return m_Type; }
			set{ m_Type = value; }
		}

		public CityDeed( int id, Point3D offset ) : base( 0x14F0 )
		{
			Weight = 1.0;
			LootType = LootType.Newbied;

			m_MultiID = id & 0x3FFF;
			m_Offset = offset;
		}

		public CityDeed( Serial serial ) : base( serial )
		{
		}

		public void FinishPlacement( Mobile from, Point3D p )
		{
			if ( Type == CivicStrutureType.FieldStoneCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );
				
				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList(); 
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;
				
				
				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				FieldstoneCityHallAddon building = new FieldstoneCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 3, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X, p.Y - 14, p.Z + 17 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 1, p.Y - 14, p.Z + 17 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				SandstoneCityHallAddon building = new SandstoneCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 3, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 4, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				MarbleCityHallAddon building = new MarbleCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				NecroCityHallAddon building = new NecroCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				PlasterCityHallAddon building = new PlasterCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				WoodCityHallAddon building = new WoodCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				StoneCityHallAddon building = new StoneCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityHall )
			{
				// City Bounds
				int offset = PlayerGovernmentSystem.L1CLOffset;

				Point2D center = new Point2D( p.X - m_Offset.X, p.Y - m_Offset.Y );
				Point2D start = new Point2D( center.X - offset, center.Y - offset );
				Point2D end = new Point2D( center.X + offset, center.Y + offset );
				Rectangle2D box = new Rectangle2D( start, end );
				ArrayList cityCoords = new ArrayList();
				cityCoords.Add( box );

				//Remove List
				ArrayList toDelete = new ArrayList();

				// Add Mayor As Citizen
				ArrayList citizens = new ArrayList();
				citizens.Add( from );

				// Add Sponsor List
				ArrayList sponsor = new ArrayList();

				// Add Ban List
				ArrayList ban = new ArrayList();

				// Add Lockdown List
				ArrayList ld = new ArrayList();

				// Add Waring List
				ArrayList war = new ArrayList();

				// Add Wars Declare List
				ArrayList dwar = new ArrayList();

				// Add Wars Invite List
				ArrayList iwar = new ArrayList();
	
				// Add Allegiances List
				ArrayList allies = new ArrayList();

				// Add Allegiances Declared List
				ArrayList dallies = new ArrayList();

				// Add Allegiances Invited List
				ArrayList iallies = new ArrayList();

				// Add Gardens And Parks
				ArrayList gardens = new ArrayList();
				ArrayList parks = new ArrayList();
				ArrayList vendor = new ArrayList();
				ArrayList addon = new ArrayList();

				// Add Management Stone
				CityManagementStone mStone = new CityManagementStone();

				// Add City Region
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				PlayerCityRegion reg = new PlayerCityRegion( mStone, from.Map, area );
				reg.Register();

				// Setup Stone
				mStone.RegionCoords = area;
				mStone.Coords = cityCoords;
				mStone.CityName = from.Name + "'s City";
				mStone.Level = 1;
				mStone.Center = center;
				mStone.Mayor = from;
				mStone.PCRegion = reg;
				mStone.Citizens = citizens;
				mStone.Banned = ban;
				mStone.isLockedDown = ld;
				mStone.Sponsored = sponsor;
				mStone.Vendors = vendor;
				mStone.AddOns = addon;

				mStone.Waring = war;
				mStone.WarsDeclared = dwar;
				mStone.WarsInvited = iwar;

				mStone.Allegiances = allies;
				mStone.AllegiancesDeclared = dallies;
				mStone.AllegiancesInvited = iallies;

				mStone.Gardens = gardens;
				mStone.Parks = parks;

				// Setup Mayor
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = mStone;
				pm.CityTitle = "Mayor";
				pm.ShowCityTitle = true;

				AsianCityHallAddon building = new AsianCityHallAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 2, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 1, p.Y - 1, p.Z + 7 ), from.Map );

				// Add Voters List
				ArrayList voters = new ArrayList();

				CityVotingStone vStone = new CityVotingStone();
				vStone.Voters = voters;
				vStone.Stone = mStone;
				mStone.VoteStone = vStone;

				toDelete.Add( vStone );

				vStone.MoveToWorld( new Point3D( p.X - 5, p.Y - 14, p.Z + 7 ), from.Map );

				toDelete.Add( d1 );
				toDelete.Add( d2 );
				toDelete.Add( building );

				mStone.toDelete = toDelete;

				mStone.MoveToWorld( new Point3D( p.X - 6, p.Y - 14, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityHealer )
			{
				WoodCityHealerAddon building = new WoodCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;
				

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterCityHealer )
			{
				PlasterCityHealerAddon building = new PlasterCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityHealer )
			{
				StoneCityHealerAddon building = new StoneCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				sign.Name = "City Healer";

				pm.City.toDelete.Add( sign );

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.FieldStoneCityHealer )
			{
				FieldstoneCityHealerAddon building = new FieldstoneCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityHealer )
			{
				SandstoneCityHealerAddon building = new SandstoneCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 2, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityHealer )
			{
				MarbleCityHealerAddon building = new MarbleCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 2, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityHealer )
			{
				NecroCityHealerAddon building = new NecroCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;

					//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;
				
				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 2, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityHealer )
			{
				AsianCityHealerAddon building = new AsianCityHealerAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasHealer = true;

				CityResurrectionStone ank = new CityResurrectionStone();
				ank.Sign = sign;
				pm.City.ResStone = ank;
				
				//Initialize the Ghost Record
				Hashtable table = new Hashtable();
				ank.Ghosts = table;

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 2, p.Y - 2, p.Z + 7 ), from.Map );

				ank.MoveToWorld( new Point3D( p.X + 1, p.Y - 1, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( ank );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Healer;

				pm.City.toDelete.Add( sign );

				sign.Name = "City Healer";

				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 10, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityBank )
			{
				WoodCityBankAddon building = new WoodCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityBank )
			{
				StoneCityBankAddon building = new StoneCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 1, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X + 1, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterCityBank )
			{
				PlasterCityBankAddon building = new PlasterCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.FieldStoneCityBank )
			{
				FieldstoneCityBankAddon building = new FieldstoneCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityBank )
			{
				SandstoneCityBankAddon building = new SandstoneCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 1, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X + 1, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityBank )
			{
				MarbleCityBankAddon building = new MarbleCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityBank )
			{
				NecroCityBankAddon building = new NecroCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				bStone.MoveToWorld( new Point3D( p.X + 1, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityBank )
			{
				AsianCityBankAddon building = new AsianCityBankAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasBank = true;

				CityBankStone bStone = new CityBankStone();

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 1, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X, p.Y - 2, p.Z + 7 ), from.Map );

				bStone.MoveToWorld( new Point3D( p.X, p.Y - 8, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( bStone );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Bank;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Bank";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityStable )
			{
				WoodCityStableAddon building = new WoodCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );


				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityStable )
			{
				StoneCityStableAddon building = new StoneCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.FieldStoneCityStable )
			{
				FieldstoneCityStableAddon building = new FieldstoneCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterCityStable )
			{
				PlasterCityStableAddon building = new PlasterCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityStable )
			{
				SandstoneCityStableAddon building = new SandstoneCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 4, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityStable )
			{
				MarbleCityStableAddon building = new MarbleCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 4, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityStable )
			{
				NecroCityStableAddon building = new NecroCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 4, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityStable )
			{
				AsianCityStableAddon building = new AsianCityStableAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasStable = true;

				CityStableStone sStone = new CityStableStone();
				sStone.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 7 ), from.Map );
				sStone.Sign = sign;
				sign.toDelete.Add( sStone );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X + 2, p.Y - 2, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X + 3, p.Y - 2, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Stable;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Stables";
				sign.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityTavern )
			{
				WoodCityTavernAddon building = new WoodCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityTavern )
			{
				StoneCityTavernAddon building = new StoneCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 6, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterCityTavern )
			{
				PlasterCityTavernAddon building = new PlasterCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.FieldStoneCityTavern )
			{
				FieldstoneCityTavernAddon building = new FieldstoneCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityTavern )
			{
				SandstoneCityTavernAddon building = new SandstoneCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityTavern )
			{
				MarbleCityTavernAddon building = new MarbleCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityTavern )
			{
				NecroCityTavernAddon building = new NecroCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityTavern )
			{
				AsianCityTavernAddon building = new AsianCityTavernAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasTavern = true;

				Spawner spwn = new Spawner( "Barkeeper" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 3;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				BaseDoor d1 = new StrongWoodDoor( DoorFacing.WestCW );
				BaseDoor d2 = new StrongWoodDoor( DoorFacing.EastCCW );
				
				d1.Link = d2;
				d2.Link = d1;

				d1.MoveToWorld( new Point3D( p.X - 5, p.Y - 1, p.Z + 7 ), from.Map );
				d2.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 7, p.Z + 7 ), from.Map );

				sign.toDelete.Add( d1 );
				sign.toDelete.Add( d2 );
				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Tavern;

				pm.City.toDelete.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Tavern";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodCityMoongate )
			{
				WoodCityMoongateAddon building = new WoodCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z -6 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneCityMoongate )
			{
				StoneCityMoongateAddon building = new StoneCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z -6 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneCityMoongate )
			{
				SandstoneCityMoongateAddon building = new SandstoneCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z -6 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleCityMoongate )
			{
				MarbleCityMoongateAddon building = new MarbleCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z -6 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroCityMoongate )
			{
				NecroCityMoongateAddon building = new NecroCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.ItemID = 3026;
				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y, p.Z + 10 ), from.Map );
			}
			else if ( Type == CivicStrutureType.AsianCityMoongate )
			{
				AsianCityMoongateAddon building = new AsianCityMoongateAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				CivicSign sign = new CivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;

				pm.City.HasMoongate = true;

				Spawner spwn = new Spawner( "PublicMoongate" );
				spwn.Count = 1;
				spwn.Running = true;
				spwn.HomeRange = 0;
				spwn.MinDelay = TimeSpan.FromMinutes( 1 );
				spwn.MaxDelay = TimeSpan.FromMinutes( 2 );

				spwn.MoveToWorld( new Point3D( p.X, p.Y - 5, p.Z + 5 ), from.Map );

				sign.toDelete.Add( spwn );
				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Moongate;

				pm.City.toDelete.Add( sign );
				pm.City.MoongateLocation = spwn.Location;

				sign.Name = "City Moongate";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z -6 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SmallCityGarden )
			{
				SmallCityGardenAddon building = new SmallCityGardenAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 1;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Garden;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Small Garden";
				sign.MoveToWorld( new Point3D( p.X - 3, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MedCityGarden )
			{
				MedCityGardenAddon building = new MedCityGardenAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 2;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Garden;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Medium Garden";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y - 1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.LargeCityGarden )
			{
				LargeCityGardenAddon building = new LargeCityGardenAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 3;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Garden;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Large Garden";
				sign.MoveToWorld( new Point3D( p.X - 8, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SmallCityPark )
			{
				SmallCityParkAddon building = new SmallCityParkAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 4;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Park;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Small Park";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MedCityPark )
			{
				MedCityParkAddon building = new MedCityParkAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 5;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Park;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Medium Park";
				sign.MoveToWorld( new Point3D( p.X - 7, p.Y, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.LargeCityPark )
			{
				LargeCityParkAddon building = new LargeCityParkAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );

				PlayerMobile pm = (PlayerMobile)from;

				//Remove List
				ArrayList toDelete = new ArrayList();

				RecCivicSign sign = new RecCivicSign();
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.CityLevel = 6;

				sign.toDelete.Add( building );

				sign.Type = CivicSignType.Park;

				pm.City.toDelete.Add( sign );
				pm.City.Gardens.Add( sign );

				sign.ItemID = 3026;
				sign.Name = "City Large Park";
				sign.MoveToWorld( new Point3D( p.X - 8, p.Y - 1, p.Z + 7 ), from.Map );
			} 
			else if ( Type == CivicStrutureType.AsianMarket )
			{
				AsianCityMarketAddon building = new AsianCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			
			else if ( Type == CivicStrutureType.FieldstoneMarket )
			{
				FieldstoneCityMarketAddon building = new FieldstoneCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.SandstoneMarket )
			{
				SandstoneCityMarketAddon building = new SandstoneCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.MarbleMarket )
			{
				MarbleCityMarketAddon building = new MarbleCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.NecroMarket )
			{
				NecroCityMarketAddon building = new NecroCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.WoodMarket )
			{
				WoodCityMarketAddon building = new WoodCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.StoneMarket )
			{
				StoneCityMarketAddon building = new StoneCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			else if ( Type == CivicStrutureType.PlasterMarket )
			{
				PlasterCityMarketAddon building = new PlasterCityMarketAddon();
				building.MoveToWorld( new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z ), from.Map );
				PlayerMobile pm = (PlayerMobile)from;
				CivicSign sign = new CivicSign();				
				
				Point3D center = new Point3D( p.X - m_Offset.X, p.Y - m_Offset.Y, p.Z - m_Offset.Z );
				Point3D start = new Point3D( center.X - 4, center.Y - 4, center.Z );
				Point3D end = new Point3D( center.X + 6, center.Y + 6, center.Z );
				Rectangle2D box = new Rectangle2D( start, end );
				Rectangle3D[] area = PlayerGovernmentSystem.FormatRegion( box );
				CityLandLord lord = new CityLandLord( pm.City, area, sign, p, from.Map  );
				
							
				//Remove List
				ArrayList toDelete = new ArrayList();
				
				sign.Stone = pm.City;
				sign.toDelete = toDelete;
				sign.Stone.HasMarket = true;
				
				sign.toDelete.Add( building );
				sign.LandlordRemove = lord;

				sign.Type = CivicSignType.Market;

				pm.City.toDelete.Add( sign );
						
				

				sign.ItemID = 3026;
				sign.Name = "City Market";
				sign.MoveToWorld( new Point3D( p.X - 4, p.Y -1, p.Z + 7 ), from.Map );
			}
			

			this.Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
			
			writer.Write( (int) m_Type );
			
			writer.Write( m_Offset );

			writer.Write( m_MultiID );

			
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_Type = (CivicStrutureType)reader.ReadInt();
					goto case 1;
				}
				case 1:
				{
					m_Offset = reader.ReadPoint3D();
					goto case 0;
				}
				case 0:
				{
					m_MultiID = reader.ReadInt();
					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.Target = new CityPlacementTarget( this );
		}
	}
}
