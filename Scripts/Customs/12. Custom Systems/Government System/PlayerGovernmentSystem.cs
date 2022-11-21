using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Mobiles;
using Server.Regions;
using Server.Accounting;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Globalization;

namespace Server
{
	public class PlayerGovernmentSystem
	{
		//System Version
		public static readonly string SystemVersion = "2.3";
		public static string FileName = "Data/GovernmentVersion.xml";
		public static string FileVersion = "0.0";
				
		//Forensics Requirement
		public static bool NeedsForensics = false;
		public static double ForensicsRequirement = 35.0;
		
		// Global Start Timer
		public static readonly TimeSpan StartUpdate = TimeSpan.FromHours( 24.0 ); // Default 1140 = 24 hours

		// City Update Time
		public static readonly TimeSpan CityUpdate = TimeSpan.FromDays( 7.0 ); // Default 10080 = 7 days

		// Mayor Voting Time
		public static readonly TimeSpan VoteUpdate = TimeSpan.FromDays( 14.0 ); // Default 20160 = 14 Days

		// Staring Treasury Amount RECOMMENDED: At least 20k so if player forgets to add, and dont lose on first update.
		public static int TreasuryAmount = 20000; // Default 20000

		// Level 1 City Limit Offset // See Docs On How To Set this.
		public static int L1CLOffset = 25; // 50x50 Area

		// Level 2 City Limit Offset // See Docs On How To Set this.
		public static int L2CLOffset = 38; // 75x75 Area (Is really 76x76)

		// Level 3 City Limit Offset // See Docs On How To Set this.
		public static int L3CLOffset = 50; // 100x100 Area

		// Level 4 City Limit Offset // See Docs On How To Set this.
		public static int L4CLOffset = 75; // 150x150 Area

		// Level 5 City Limit Offset // See Docs On How To Set this.
		public static int L5CLOffset = 88; // 175x175 Area (Is really 176x176)

		// Level 6 City Limit Offset // See Docs On How To Set this.
		public static int L6CLOffset = 100; // 200x200 Area

		// Set the amount of tiles the system checks for cities in range. // See Docs On How To Set This.
		public static int CityRangeOffset = 100; // 200x200 Area

		// Number of Max Lockdowns per City, Per Level
		public static int Level1LD = 10; // Default 10
		public static int Level2LD = 50; // Default 50
		public static int Level3LD = 100; // Default 100
		public static int Level4LD = 200; // Default 200
		public static int Level5LD = 300; // Default 300
		public static int Level6LD = 500; // Default 500

		// Number of citizens needed for each city level.
		public static int Level1 = 12; // Default 20 12
		public static int Level2 = 20; // Default 40 20
		public static int Level3 = 35; // Default 60 35
		public static int Level4 = 45; // Default 80 45
		public static int Level5 = 50; // Default 100 50
		public static int Level6 = 56; // Default 200 55

		/*
		* Member Rules
		*
		* As long as a member has a house withing the city limits they can bacome an member of the city.
		* Member cannot be a member of another city. Unless multi houses per account is enabled.
		* However the same member cannot be a member of 2 differant cities.
		* A member can join as many charactors to the city from thier account as they wish.
		* Each charactor from the members account counts as a member to the citys total population.
		* So in theory one account counts as 6 members to the city. (If 6 chars are enabled.) & if all are joined.
		*/

		// Title for cities at each level.
		public static string Title1 = "outpost";
		public static string Title2 = "village";
		public static string Title3 = "township";
		public static string Title4 = "city";
		public static string Title5 = "metropolis";
		public static string Title6 = "empire";

		// Enable city placement for Ilshenar (Note: need to let players place houses as well in ilsh.)
		public static bool EnableIlshenar = false;

		// Max amounts of cities per map.
		public static int MaxCitiesForFelucca = 25;
		public static int MaxCitiesForTrammel = 25;
		public static int MaxCitiesForIlshenar = 0; //No Housing In Ilshenar.. However can be changed.
		public static int MaxCitiesForMalas = 0;
		public static int MaxCitiesForTokuno = 0;

		//Max number of citizens per city
		public static int MaxCitizensPerCity = 300;

		//Max number of banned players NOTE: if 0 will disable city banning.
		public static int MaxBannedPerCity = 50;
		
		public static void Initialize()
		{
			CheckCitySystemVersion();
		}
		
		
		public static void PlaceCityHall( Mobile from, Point3D p, CityDeed deed )
		{
			if ( CheckPlacement( from, p, deed.MultiID, deed.Offset ) )
			{
				if ( !CheckCitiesInRange( from, p ) )
				{
					deed.FinishPlacement( from, p );
					from.SendGump( new CityWarningGump() );
				}
				else
				{
					int offset = CityRangeOffset * 2;

					from.SendMessage( "Another city is to close to place your city hall here." );
					from.SendMessage( 38, "You must be at least {0} yards (Steps) away from any other city in order to place your city hall.", offset );
				}
			}
		}

		public static void PlaceCivic( Mobile from, Point3D p, CityDeed deed )
		{
			if ( CheckPlacement( from, p, deed.MultiID, deed.Offset ) )
			{
				deed.FinishPlacement( from, p );
			}
		}

		public static bool CheckPlacement( Mobile from, Point3D p, int multiId, Point3D offset )
		{
			ArrayList toMove;
			Point3D center = new Point3D( p.X - offset.X, p.Y - offset.Y, p.Z - offset.Z );
			HousePlacementResult res = HousePlacement.Check( from, multiId, center, out toMove );

			if ( res == HousePlacementResult.Valid )
			{
				return true;
			}
			else if ( res == HousePlacementResult.BadItem || res == HousePlacementResult.BadLand || res == HousePlacementResult.BadStatic || res == HousePlacementResult.BadRegionHidden )
			{
				from.SendLocalizedMessage( 1043287 ); // The house could not be created here.  Either something is blocking the house, or the house would not be on valid terrain.
				return false;
			}
			else if ( res == HousePlacementResult.NoSurface )
			{
				from.SendMessage( "The house could not be created here.  Part of the foundation would not be on any surface." );
				return false;
			}
			else if ( res == HousePlacementResult.BadRegion )
			{
				from.SendLocalizedMessage( 501265 ); // Housing cannot be created in this area.
				return false;
			}
			else
			{
				return false;
			}
		}

		public static bool CheckIfInIlsh( Mobile from )
		{
			if ( from.Map == Map.Ilshenar && EnableIlshenar == false )
				return true;
			
			return false;
		}

		public static bool CheckIfCanBeMayor( Mobile from )
		{
			if ( NeedsForensics )
				if ( from.Skills[SkillName.Forensics].Base >= 35.0 )
				return true;
				else return false;
			else
				return true;

		}

		public static bool CheckCitiesInRange( Mobile from, Point3D p )
		{
			// Very Speical Thanks To ArteGordon For His Help On This Bool

			Map map = from.Map;

			int offset = CityRangeOffset;
			int offset2 = offset * 3;

			int x1 = p.X - offset;
			int y1 = p.Y - offset;
			int width = offset2;
			int height = offset2;

			int depth = p.Z;

			for( int x = x1; x <= x1 + width; x += Map.SectorSize )
			{
				for( int y = y1; y <= y1 + height; y += Map.SectorSize )
				{
					Sector s = map.GetSector( new Point3D( x, y, depth ) );
		
					foreach ( RegionRect rect in s.RegionRects )
					{
						Region r = rect.Region;
						if ( r is GuardedRegion || r is PlayerCityRegion ) 
							return true;
						
					}
				}
			}	

			return false;
		}

		public static bool CheckIfMayor( Mobile from )
		{

			foreach ( Item item in World.Items.Values )
			{
				if ( item is CityManagementStone )
				{
					CityManagementStone stone = (CityManagementStone)item;
					if ( stone.Mayor == from )
						return true;
				}
			}

			return false;
		}

		public static bool CheckIfCitizen( Mobile from )
		{

			foreach ( Item item in World.Items.Values )
			{
				if ( item is CityManagementStone )
				{
					CityManagementStone stone = (CityManagementStone)item;
					foreach ( Mobile m in stone.Citizens )
					{
						if ( from == m )
							return true;
					}
				}
			}

			return false;
		}

		public static List<BaseHouse> GetAllHouses( Mobile m ) // thanks to bripbrip
		{
			List<BaseHouse> allHouses = new List<BaseHouse>();
			
			Account a = m.Account as Account;

			if ( a == null )
				return allHouses;
			

			for ( int i = 0; i < a.Length; ++i )
			{
				Mobile mob = a[i];

				if ( mob != null )
					allHouses.AddRange( BaseHouse.GetHouses( mob ) );
			}
			return allHouses;
		}
		
	
		public static bool CheckIfSpecificHouseInCity( BaseHouse h, Region region )
		{
			
			if ( h != null )
			{
						int X = h.Sign.X;
						int Y = h.Sign.Y + 1;
						int Z = h.Sign.Z;
						
						
						Point3D hsp = new Point3D( X, Y, Z );
						Map hsm = h.Sign.Map;

						Region reg = Region.Find( hsp, hsm );
						
						if( reg == region )
							return true;
			}
			return false;
		}
		
		
		public static bool CheckIfHouseInCity( Mobile from, Region region )
		{
			if ( from == null )
				return false;

			if ( region == null )
				return false;
			List<BaseHouse> list = BaseHouse.GetHouses( from );
			if ( list != null )
			{
				for ( int i = 0; i < list.Count; i++ )
				{
					BaseHouse h = list[i];
					if ( h != null )
					{
						int X = h.Sign.X;
						int Y = h.Sign.Y + 1;
						int Z = h.Sign.Z;
						
						
						Point3D hsp = new Point3D( X, Y, Z );
						Map hsm = h.Sign.Map;

						Region reg = Region.Find( hsp, hsm );
						
						if( reg == region )
							return true;
						else if ( list.Count > 1 && i < (list.Count - 1) )
							continue;
						else
						{
							
							return false;
						}
					}
				  }
				}
				
					foreach ( BaseHouse h in GetAllHouses( from ) )
					{
						if ( h != null )
						{
							int X = h.Sign.X;
							int Y = h.Sign.Y + 1;
							int Z = h.Sign.Z;
							
							
							Point3D hsp = new Point3D( X, Y, Z );
							Map hsm = h.Sign.Map;

							Region reg = Region.Find( hsp, hsm );
							if ( reg == region )
							{
								Mobile owner = h.Owner;
								Account acct = (Account)from.Account;
								Account acct2 = (Account)owner.Account;
								if ( acct == null || acct2 == null )
									return false;
								else if ( acct == acct2 )
									return true;
								else
									continue;
								
							}
							else
								continue;
						}
						else
						{
							//from.SendMessage( "No House" );
							return false;
						}
					}
					//from.SendMessage( "Out of Loop no houses" );
					return false;
					
				
				
		}

		public static bool CheckMapCityLimit( Mobile from )
		{
			ArrayList count = new ArrayList();

			foreach ( Item item in World.Items.Values )
			{
				if ( item is CityManagementStone )
				{
					if ( item.Map == from.Map )
						count.Add( item );
				}
			}

			if ( from.Map == Map.Felucca )
			{
				if ( count.Count >= MaxCitiesForFelucca )
					return true;
			}
			else if ( from.Map == Map.Trammel )
			{
				if ( count.Count >= MaxCitiesForTrammel )
					return true;
			}
			else if ( from.Map == Map.Ilshenar )
			{
				if ( count.Count >= MaxCitiesForIlshenar )
					return true;
			}
			else if ( from.Map == Map.Malas )
			{
				if ( count.Count >= MaxCitiesForMalas )
					return true;
			}
			else if ( from.Map == Map.Tokuno )
			{
				if ( count.Count >= MaxCitiesForTokuno )
					return true;
			}
			else
			{
				return true;
			}

			return false;
		}

		public static bool CheckCityName( string text )
		{

			foreach ( Item item in World.Items.Values )
			{
				if ( item is CityManagementStone )
				{
					CityManagementStone stone = (CityManagementStone)item;

					if ( stone.CityName == text )
						return true;
				}
			}

			return false;
		}

		public static bool CheckIfBanned( Mobile from, Mobile target )
		{
			if ( from is PlayerMobile && target is PlayerMobile )
			{
				if ( target.Region is PlayerCityRegion && from.Region is PlayerCityRegion )
				{
					PlayerMobile pm = (PlayerMobile)from;
					CityManagementStone stone = pm.City;

					if ( stone != null )
					{
						if ( stone.PCRegion == target.Region )
						{
							bool isBanned = false;
							bool isMember = false;

							foreach ( Mobile checkBan in stone.Banned )
							{
								if ( target == checkBan )
									isBanned = true;
							}
	
							foreach ( Mobile checkMem in stone.Citizens )
							{
								if ( from == checkMem )
									isMember = true;
							}

							if ( isBanned == true && isMember == true )
								return true;
						}
					}
				}
			}

			return false;
		}

		public static bool CheckBanLootable( Mobile from, Mobile target )
		{
			if ( from is PlayerMobile && target is PlayerMobile )
			{
				if ( from.Region is PlayerCityRegion )
				{
					PlayerMobile pm = (PlayerMobile)from;
					CityManagementStone stone = pm.City;

					if ( stone != null )
					{
						bool isBanned = false;

						foreach ( Mobile checkBan in stone.Banned )
						{
							if ( target == checkBan )
								isBanned = true;
						}

						if ( isBanned == true )
							return true;
					}
				}
			}

			return false;
		}

		public static bool CheckAtWarWith( Mobile from, Mobile target )
		{
			if ( from is PlayerMobile && target is PlayerMobile )
			{
				PlayerMobile pm1 = (PlayerMobile)from;
				PlayerMobile pm2 = (PlayerMobile)target;
				CityManagementStone fromCity = pm1.City;
				CityManagementStone targCity = pm2.City;

				if ( fromCity != null && targCity != null )
				{
					if ( fromCity.Waring.Contains( targCity ) )
						return true;
				}
			}

			return false;
		}

		public static bool CheckCityAlly( Mobile from, Mobile target )
		{
			if ( from is PlayerMobile && target is PlayerMobile )
			{
				PlayerMobile pm1 = (PlayerMobile)from;
				PlayerMobile pm2 = (PlayerMobile)target;
				CityManagementStone fromCity = pm1.City;
				CityManagementStone targCity = pm2.City;

				if ( fromCity != null )
				{
					if ( fromCity.Citizens.Contains( target ) )
						return true;

					if ( fromCity.Allegiances.Contains( targCity ) )
						return true;
				}
			}

			return false;
		}

		public static bool IsCityLevelReached( Mobile from, int level )
		{
			PlayerMobile pm = (PlayerMobile)from;

			if ( pm.City.Mayor == from )
			{
				if ( pm.City.Level >= level )
					return true;
			}

			return false;
		}

		public static bool IsAtCity( Mobile from, Mobile to )
		{
			PlayerMobile pm = (PlayerMobile)from;
			CityManagementStone city = pm.City;
			Region cityreg = Region.Find( from.Location, from.Map );
			Region targregion = Region.Find( to.Location, to.Map );
			
			if ( city != null && cityreg != null && targregion != null )
			{
				if ( ( cityreg == city.PCRegion ) && ( cityreg == targregion ) )
					return true;
			}
			
			return false;

			
		}
		
		
		public static bool IsAtCity( Mobile from )
		{
			PlayerMobile pm = (PlayerMobile)from;
			CityManagementStone city = pm.City;
			Region cityreg = Region.Find( from.Location, from.Map );

			if ( cityreg != null && city != null )
			{
				if ( cityreg == city.PCRegion )
					return true;
			}
				

			return false;
		}
		
		public static bool IsAtCity( Item item )
		{
			Region reg = Region.Find( item.Location, item.Map );
				
			if ( reg != null )
			{
				if ( reg is PlayerCityRegion )
					return true;
				else
					return false;
			}
			return false;
					
		}
		
			
		
		
		public static bool IsMemberOf( Mobile from, CityManagementStone stone )
		{
			PlayerMobile pm = (PlayerMobile)from;
			if ( pm.City == null )
				return false;
			
			foreach ( Mobile mob in stone.Citizens )
			{
				if ( mob == from )
				{
					return true;
					break;
				}
			}
			return false;
		}
		
		public static Rectangle3D[] FormatRegion( Rectangle2D box )
		{
				Rectangle3D area = Server.Region.ConvertTo3D( box );
				List<Rectangle3D> arealist = new List<Rectangle3D>();
				arealist.Add( area );
				Rectangle3D[] regarea = arealist.ToArray();
				return regarea;
		}
		
		public static bool IsIteminCity( Mobile from, Item item )
		{
			PlayerMobile pm = (PlayerMobile)from;
			if ( pm.City == null )
				return false;
			else
			{
				CityManagementStone stone = pm.City;
				Point3D p = new Point3D( item.X, item.Y, item.Z );
				Region target = Region.Find( p, item.Map );
				if ( stone.PCRegion == target )
					return true;
				else
					return false;
			}
			
			
		}
		
		public static bool AreThereVendors( Mobile from )
		{
			
			if ( from == null )
				return false;
			
			foreach ( BaseHouse h in BaseHouse.GetHouses( from ) )
			{
				if ( h != null )
				{
					if ( h.HasPersonalVendors || h.HasRentedVendors )
						return true;
					else 
						return false;
				}
				
			}
			return false;
		}
		
		public static void CheckCitySystemVersion()
		{
		
			 bool SystemUpgrade = false;
			
			if ( !File.Exists( FileName ) )
				SystemUpgrade = true;
			else
			{
				try
				{
					XmlDocument xml = new XmlDocument();
					xml.Load( FileName );
					
					XmlElement system = xml["Version"];
					FileVersion = system.InnerText;
										
					if ( FileVersion == "0.0" )
					{
						Console.WriteLine( "Government System: Error Loading Version Variable!" );
						return;
					}
					if ( FileVersion != SystemVersion )
						SystemUpgrade = true;
					
					
				}
				catch
				{
					Console.WriteLine( "Error Reading City Version File!" );
					return;
				}
				
			}
			if ( SystemUpgrade )
				Console.WriteLine( "The city system needs to be upgraded, please run the [upgradecitysystem command!" );
			else
				Console.WriteLine( "City system current.  Version {0}", SystemVersion );
				
		}
			
	}
}
