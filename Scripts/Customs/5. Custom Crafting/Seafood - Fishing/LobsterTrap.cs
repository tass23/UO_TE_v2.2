using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class LobsterTrap : Item
	{	
		
		private static int[] m_WaterTiles = { 0x00A8, 0x00AB, 0x0136, 0x0137 };
		
		private Mobile m_Player;
		
		[Constructable]
		public LobsterTrap() : base( 0x44CF )
		{
			Name = "a lobster trap";
			Weight = 5.0;
			
		}

		public LobsterTrap( Serial serial ) : base( serial )
		{		
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}
			else if ( from.Mounted )
			{
                from.SendMessage("You can't use the trap while riding."); 
				return;
			}			
				
			from.SendMessage( "Where do you wish to use the trap?" ); 
			
			from.BeginTarget( -1, true, TargetFlags.None, new TargetCallback( OnTarget ) );
		}
		
		public void OnTarget( Mobile from, object obj )
		{
			m_Player = from;
			
			if ( Deleted )
				return;

			IPoint3D p3D = obj as IPoint3D;

			if ( p3D == null )
				return;

			Map map = from.Map;

			if ( map == null || map == Map.Internal )
				return;

			int x = p3D.X, y = p3D.Y;

			if ( !from.InRange( p3D, 6 ) )
			{
				from.SendMessage( "You need to be closer to the water to use your trap." ); 
			}
			else if ( FullValidation( map, x, y ) )
			{
				Point3D p = new Point3D( x, y, map.GetAverageZ( x, y ) );

				Movable = false;
				MoveToWorld( p, map );

				from.Animate( 12, 5, 1, true, false, 0 );

				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), TimeSpan.FromSeconds( 1.0 ), 20, new TimerStateCallback( DoEffect ), new object[]{ p, 0 } );

				from.SendMessage( "You plunge the trap into the sea..." ); 
			}
			else
			{
				from.SendMessage( "You can only use this trap in deep water!" ); 
			}
		}
		
		public static bool FullValidation( Map map, int x, int y )
		{
			bool valid = ValidateDeepWater( map, x, y );

			for ( int j = 1, offset = 5; valid && j <= 5; ++j, offset += 5 )
			{
				if ( !ValidateDeepWater( map, x + offset, y + offset ) )
					valid = false;
				else if ( !ValidateDeepWater( map, x + offset, y - offset ) )
					valid = false;
				else if ( !ValidateDeepWater( map, x - offset, y + offset ) )
					valid = false;
				else if ( !ValidateDeepWater( map, x - offset, y - offset ) )
					valid = false;
			}

			return valid;
		}

		private static bool ValidateDeepWater( Map map, int x, int y )
		{
			int tileID = map.Tiles.GetLandTile( x, y ).ID;
			bool water = false;

			for ( int i = 0; !water && i < m_WaterTiles.Length; i += 2 )
				water = ( tileID >= m_WaterTiles[i] && tileID <= m_WaterTiles[i + 1] );

			return water;
		}
		
		private void DoEffect( object state )
		{
			if ( Deleted )
				return;

			object[] states = (object[])state;

			Point3D p = (Point3D)states[0];
			int index = (int)states[1];

			states[1] = ++index;

			if ( index == 1 )
			{
				Effects.SendLocationEffect( p, Map, 0x352D, 16, 4 );
				Effects.PlaySound( p, Map, 0x364 );
			}
			else if ( index <= 10 || index == 20 )
			{
				for ( int i = 0; i < 3; ++i )
				{
					int x, y;

					switch ( Utility.Random( 8 ) )
					{
						default:
						case 0: x = -1; y = -1; break;
						case 1: x = -1; y =  0; break;
						case 2: x = -1; y = +1; break;
						case 3: x =  0; y = -1; break;
						case 4: x =  0; y = +1; break;
						case 5: x = +1; y = -1; break;
						case 6: x = +1; y =  0; break;
						case 7: x = +1; y = +1; break;
					}

					Effects.SendLocationEffect( new Point3D( p.X + x, p.Y + y, p.Z ), Map, 0x352D, 16, 4 );
				}

				Effects.PlaySound( p, Map, 0x364 );

				if ( index == 20 )
					FinishEffect( p );
				else
					this.Z -= 1;
			}
		}
		
		private void FinishEffect( Point3D p )
		{
			Item fish = GiveFish( m_Player.Skills.Fishing.Base / 100 );
			
			if ( fish != null )
			{				
				m_Player.SendMessage( "You empty your catch from the trap." ); 
				fish.MoveToWorld( m_Player.Location, m_Player.Map );
					
				Movable = true;
				
				if ( !m_Player.PlaceInBackpack( this ) )
					MoveToWorld( m_Player.Location, m_Player.Map );
			}
			else if (Utility.RandomDouble() < 0.75) 		
			{
				Movable = true;
				
				if ( !m_Player.PlaceInBackpack( this ) )
					MoveToWorld( m_Player.Location, m_Player.Map );
					
					m_Player.SendLocalizedMessage( 1074487 ); // The creatures are too quick for you!
			}	
			else 
			{
				this.Delete();
				m_Player.SendMessage( "Your trap is destroyed while trying to pull it from the water." );	
			}
		}	
		
		public Item GiveFish( double skill )
		{
			if ( 0.038 * skill > Utility.RandomDouble() ) return new RockLobster();
			if ( 0.040 * skill > Utility.RandomDouble() ) return new RedRockCrab();
			if ( 0.046 * skill > Utility.RandomDouble() ) return new SpineyLobster();
			if ( 0.052 * skill > Utility.RandomDouble() ) return new SnowCrab();
			if ( 0.058 * skill > Utility.RandomDouble() ) return new Lobster();
			if ( 0.062 * skill > Utility.RandomDouble() ) return new BlueCrab();
			if ( 0.064 * skill > Utility.RandomDouble() ) return new Crab();
			if ( 0.076 * skill > Utility.RandomDouble() ) return new Prawn();//need to add to seafood cooking maybe with stat boost 
				
			return null;
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
}