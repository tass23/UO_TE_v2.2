
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Misc
{
	public abstract class VinePlacement
	{
		public static bool AllowPlayerYards = true;
		public static bool AllowAllHouseTiles = true;
		public static bool AllowAllYardTiles = true;

		public const int m_VinePrice = 250;

		public static int[] FarmTiles = new int[]
		{
			0x009, 0x00A,
			0x00C, 0x00E,
			0x013, 0x015,
			0x150, 0x155,
			0x15A, 0x15C
		};

		public static int[] HouseTiles = new int[]
		{
			0x31F4, 0x31F5,
			0x31F6, 0x31F7,
			0x515, 0x516,
			0x517, 0x518,
			0x31F4, 0x31F9,
			0x31FA, 0x31FB
		};

		public static int[] DirtTiles = new int[]
		{
			0x071, 0x07C,
			0x165, 0x174,
			0x1DC, 0x1EF,
			0x306, 0x31F,
			0x08D, 0x0A7,
			0x2E5, 0x305,
			0x777, 0x791,
			0x98C, 0x9BF,
		};

		public static int[] GroundTiles = new int[]
		{
			0x003, 0x006,
			0x033, 0x03E,
			0x078, 0x08C,
			0x0AC, 0x0DB,
			0x108, 0x10B,
			0x14C, 0x174,
			0x1A4, 0x1A7,
			0x1B1, 0x1B2,
			0x26E, 0x281,
			0x292, 0x295,
			0x355, 0x37E,
			0x3CB, 0x3CE,
			0x547, 0x5A6,
			0x5E3, 0x618,
			0x66B, 0x66E,
			0x6A1, 0x6C2,
			0x6DE, 0x6E1,
			0x73F, 0x742,
		};

		public static bool ValidatePlacement( Point3D loc, Mobile m_From, object m_Obj )
		{
			Map map = m_From.Map;
			if( map == null )
				return false;

			if ( !map.CanFit( loc, 20 ) )
				return false;

			if ( m_From.AccessLevel == AccessLevel.Player )
			{
				return ValidatePlayerPlacement( loc, m_From, map, m_Obj );
			}
			else
			{
				return ValidateAdminPlacement( loc, m_From, map, m_Obj );
			}
		}

		public static bool ValidateAdminPlacement( Point3D loc, Mobile m_From, Map m_Map, object o )
		{
			LandTarget land = o as LandTarget;
			BaseHouse house = BaseHouse.FindHouseAt( loc, m_Map, 20 );

			if ( land == null && house == null )
			{
				return false;
			}
			return true;
		}

		public static bool ValidatePlayerPlacement( Point3D loc, Mobile m_From, Map m_Map, object o )
		{
			BaseHouse house = BaseHouse.FindHouseAt( m_From.Location, m_Map, 20 );
			if( house == null || !house.IsOwner( m_From ) )
			{
				m_From.SendMessage( "You must be standing in your house to place this");
				return false;
			}

			if ( !AllowPlayerYards )
			{
				if ( !CheckHouse( m_From, loc, m_Map, 20 ) )
				{
					m_From.SendMessage( "The grapevines must be placed inside your house." );
					return false;
				}
				else
				{
                    if (!AllowAllHouseTiles && (!ValidateHouseTiles(m_Map, loc.X, loc.Y) && !ValidateVinyardPlot( m_Map, loc.X, loc.Y )))
					{
						m_From.SendMessage( "The grapevines must be placed on dirt tiles or a vinyard ground addon." );
						return false;
					}
				}
			}
			else
			{
				if ( CheckHouse( m_From, loc, m_Map, 20 ) )
				{
                    if (!AllowAllHouseTiles && (!ValidateHouseTiles(m_Map, loc.X, loc.Y) && !ValidateVinyardPlot(m_Map, loc.X, loc.Y)) )
					{
						m_From.SendMessage( "Grapevines placed inside your house must be placed on dirt tiles or a vinyard ground addon." );
						return false;
					}
				}
				else
				{
					if( loc.Y > m_From.Location.Y+5 || loc.Y < m_From.Location.Y-5 )
					{
						m_From.SendMessage( "This is outside of your yard. Please re-try the placement" );
						return false;
					}

					if( loc.X > m_From.Location.X+5 || loc.X < m_From.Location.X-5 )
					{
						m_From.SendMessage( "This is outside of your yard. Please re-try the placement" );
						return false;
					}

					if ( !AllowAllYardTiles )
					{
						if ( !ValidateFarmLand( m_Map, loc.X, loc.Y) && !ValidateDirt( m_Map, loc.X, loc.Y) && !ValidateGround( m_Map, loc.X, loc.Y) )
						{
							m_From.SendMessage( "Grapevines must be placed on dirt, ground, or farm tiles." );
							return false;
						}
					}
				}
			}
			return true;
		}

		public static bool CheckHouse( Mobile from, Point3D p, Map map, int height )
		{
			if ( from == null || from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			BaseHouse house = BaseHouse.FindHouseAt( p, map, height );

			if ( house == null || !house.IsOwner( from ) )
				return false;

			return true;
		}

        public static bool ValidateVinyardPlot(Map map, int x, int y)
        {
            bool ground = false;

            IPooledEnumerable eable = map.GetItemsInBounds(new Rectangle2D(x, y, 1, 1));
            foreach (Item item in eable)
            {
                if (item.ItemID == 0x32C9 || item.ItemID == 0x31F4)
                    ground = true;
            }
            eable.Free();

            if (!ground)
            {
                StaticTile[] tiles = map.Tiles.GetStaticTiles(x, y);
                for (int i = 0; i < tiles.Length; ++i)
                {
                    if ((tiles[i].ID & 0x3FFF) == 0x32C9 || (tiles[i].ID & 0x3FFF) == 0x31F4)
                        ground = true;
                }
            }

            return ground;
        }

		public static bool ValidateFarmLand( Map map, int x, int y )
		{
			int tileID = map.Tiles.GetLandTile( x, y ).ID & 0x3FFF;
			bool ground = false;

			for ( int i = 0; !ground && i < FarmTiles.Length; i += 2 )
				ground = ( tileID >= FarmTiles[i] && tileID <= FarmTiles[i + 1] );

			return ground;
		}

		public static bool ValidateHouseTiles( Map map, int x, int y )
		{
			bool ground = false;

			StaticTile[] tiles = map.Tiles.GetStaticTiles( x, y, true );

			for ( int i = 0; i < tiles.Length; ++i )
			{
				StaticTile t = tiles[i];
				int tileID = t.ID & 0x3FFF;

				for ( int j = 0; !ground && j < HouseTiles.Length; j += 2 )
				{
					ground = ( tileID >= HouseTiles[j] && tileID <= HouseTiles[j + 1] );
				}
			}

			return ground;
		}

		public static bool ValidateDirt( Map map, int x, int y )
		{
			int tileID = map.Tiles.GetLandTile( x, y ).ID & 0x3FFF;
			bool ground = false;

			for ( int i = 0; !ground && i < DirtTiles.Length; i += 2 )
				ground = ( tileID >= DirtTiles[i] && tileID <= DirtTiles[i + 1] );

			return ground;
		}

		public static bool ValidateGround( Map map, int x, int y )
		{
			int tileID = map.Tiles.GetLandTile( x, y ).ID & 0x3FFF;
			bool ground = false;

			for ( int i = 0; !ground && i < GroundTiles.Length; i += 2 )
				ground = ( tileID >= GroundTiles[i] && tileID <= GroundTiles[i + 1] );

			return ground;
		}

		public static bool PayForVine( Mobile m_From )
		{
			if ( Banker.Withdraw( m_From, m_VinePrice ) )
			{
				m_From.SendLocalizedMessage( 1060398, m_VinePrice.ToString() );

				return true;
			}
			else
			{
				Item[] Gold = m_From.Backpack.FindItemsByType( typeof( Gold ) );
				if( m_From.Backpack.ConsumeTotal( typeof( Gold ), m_VinePrice ) )
				{
					m_From.SendMessage( m_VinePrice.ToString()+" gold has been removed from your pack" );

					return true;
				}
			}

			return false;
		}

		public static bool RefundForVine( Mobile m_From )
		{
			Container c = m_From.Backpack;
			Gold t = new Gold( ( m_VinePrice ) );

			if( c.TryDropItem( m_From, t, true ) )
			{
				m_From.SendMessage( "You have been refunded "+m_VinePrice.ToString()+" gold for the deleted vine." );

				return true;
			}
			else
			{
				t.Delete();
				m_From.SendMessage("For some reason, the refund didn't work!  Please page a GM");

				return false;
			}
		}
	}
}