using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public abstract class BaseExpanseBoss : BaseCreature
	{
		public BaseExpanseBoss( AIType aiType ) : this( aiType, FightMode.Closest )
		{
		}

		public BaseExpanseBoss( AIType aiType, FightMode mode ) : base( aiType, mode, 18, 1, 0.1, 0.2 )
		{
		}

		public BaseExpanseBoss( Serial serial ) : base( serial )
		{
		}

		public override bool Unprovokable{ get{ return true; } }
		public virtual double ChangeCombatant{ get{ return 0.3; } }
		
		public virtual bool NoGoodies{ get{ return false; } }

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

		public virtual void PackResources( int amount )
		{
			for ( int i = 0; i < amount; i ++ )
				switch ( Utility.Random( 6 ) )
				{
					case 0: PackItem( new Blight() ); break;
					case 1: PackItem( new Scourge() ); break;
					case 2: PackItem( new Taint() ); break;
					case 3: PackItem( new Putrefication() ); break;
					case 4: PackItem( new Corruption() ); break;
					case 5: PackItem( new Muculent() ); break;
				}
		}
		
		public virtual void PackItems( Item item, int amount )
		{
			for ( int i = 0; i < amount; i ++ )
				PackItem( item );
		}
		
		public virtual Point3D GetSpawnPosition( int range )
		{
			return GetSpawnPosition( Location, Map, range );
		}
		
		public static Point3D GetSpawnPosition( Point3D from, Map map, int range )
		{
			if ( map == null )
				return from;
				
			for ( int i = 0; i < 10; i ++ )
			{
				int x = from.X + Utility.Random( range );
				int y = from.Y + Utility.Random( range );
				int z = map.GetAverageZ( x, y );
				
				if ( Utility.RandomBool() )
					x *= -1;
					
				if ( Utility.RandomBool() )
					y *= -1;
					
				Point3D p = new Point3D( x, y, from.Z );
				
				if ( map.CanSpawnMobile( p ) && map.LineOfSight( from, p ) )
					return p;
				
				p = new Point3D( x, y, z );
					
				if ( map.CanSpawnMobile( p ) && map.LineOfSight( from, p ) )
					return p;
			}
			
			return from;
		}
		
		private PowerScroll CreateRandomPowerScroll()
		{
			int level;
			double random = Utility.RandomDouble();

			if ( 0.05 >= random )
				level = 20;
			else if ( 0.4 >= random )
				level = 15;
			else
				level = 10;

			return PowerScroll.CreateRandomNoCraft( level, level );
		}

		public void GivePowerScrolls()
		{
			//if ( Map != Map.Felucca )
				//return;

			List<Mobile> toGive = new List<Mobile>();
			List<DamageStore> rights = GetLootingRights();

			for ( int i = rights.Count - 1; i >= 0; --i )
			{
				DamageStore ds = rights[i];

				if ( ds.m_HasRight )
					toGive.Add( ds.m_Mobile );
			}

			if ( toGive.Count == 0 )
				return;

			for( int i = 0; i < toGive.Count; i++ )
			{
				Mobile m = toGive[i];

				if( !(m is PlayerMobile) )
					continue;

				bool gainedPath = false;

				int pointsToGain = 800;

				if( VirtueHelper.Award( m, VirtueName.Valor, pointsToGain, ref gainedPath ) )
				{
					if( gainedPath )
						m.SendLocalizedMessage( 1054032 ); // You have gained a path in Valor!
					else
						m.SendLocalizedMessage( 1054030 ); // You have gained in Valor!

					//No delay on Valor gains
				}
			}

			// Randomize
			for ( int i = 0; i < toGive.Count; ++i )
			{
				int rand = Utility.Random( toGive.Count );
				Mobile hold = toGive[i];
				toGive[i] = toGive[rand];
				toGive[rand] = hold;
			}

			for ( int i = 0; i < 6; ++i )
			{
				Mobile m = toGive[i % toGive.Count];

				PowerScroll ps = CreateRandomPowerScroll();

				GivePowerScrollTo( m, ps );
			}
		}

		public static void GivePowerScrollTo( Mobile m, PowerScroll ps )
		{
			if( ps == null || m == null )	//sanity
				return;

			m.SendLocalizedMessage( 1049524 ); // You have received a scroll of power!

			if( !Core.SE || m.Alive )
				m.AddToBackpack( ps );
			else
			{
				if( m.Corpse != null && !m.Corpse.Deleted )
					m.Corpse.DropItem( ps );
				else
					m.AddToBackpack( ps );
			}

			if( m is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)m;

				for( int j = 0; j < pm.JusticeProtectors.Count; ++j )
				{
					Mobile prot = pm.JusticeProtectors[j];

					if( prot.Map != m.Map || prot.Kills >= 5 || prot.Criminal || !JusticeVirtue.CheckMapRegion( m, prot ) )
						continue;

					int chance = 0;

					switch( VirtueHelper.GetLevel( prot, VirtueName.Justice ) )
					{
						case VirtueLevel.Seeker: chance = 60; break;
						case VirtueLevel.Follower: chance = 80; break;
						case VirtueLevel.Knight: chance = 100; break;
					}

					if( chance > Utility.Random( 100 ) )
					{
						PowerScroll powerScroll = new PowerScroll( ps.Skill, ps.Value );

						prot.SendLocalizedMessage( 1049368 ); // You have been rewarded for your dedication to Justice!

						if( !Core.SE || prot.Alive )
							prot.AddToBackpack( powerScroll );
						else
						{
							if( prot.Corpse != null && !prot.Corpse.Deleted )
								prot.Corpse.DropItem( powerScroll );
							else
								prot.AddToBackpack( powerScroll );
						}
					}
				}
			}
		}

		public override bool OnBeforeDeath()
		{
			if ( !NoKillAwards )
			{
				GivePowerScrolls();

				if( NoGoodies )
					return base.OnBeforeDeath();

				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -12; x <= 12; ++x )
					{
						for ( int y = -12; y <= 12; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 12 )
								new GoodiesTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			}

			return base.OnBeforeDeath();
		}

		public override void OnDeath( Container c )
		{
			if ( Map == Map.Felucca )
			{
				//TODO: Confirm SE change or AoS one too?
				List<DamageStore> rights = GetLootingRights();
				List<Mobile> toGive = new List<Mobile>();

				for ( int i = rights.Count - 1; i >= 0; --i )
				{
					DamageStore ds = rights[i];

					if ( ds.m_HasRight )
						toGive.Add( ds.m_Mobile );
				}

				/*
				if ( toGive.Count > 0 )
					toGive[Utility.Random( toGive.Count )].AddToBackpack( new ChampionSkull( SkullType ) );
				else
					c.DropItem( new ChampionSkull( SkullType ) );
				*/
			}

			base.OnDeath( c );
			/*
			if ( Utility.RandomDouble() < 0.001 )
			c.DropItem( new DiscountCoupon() );
			*/
		}

		private class GoodiesTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoodiesTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 25, 50 );
				
				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
	}
}