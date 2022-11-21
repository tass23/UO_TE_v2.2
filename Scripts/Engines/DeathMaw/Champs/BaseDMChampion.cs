using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.DMChamps;
using System.Collections.Generic;

namespace Server.Mobiles
{
	public abstract class BaseDMChampion : BaseCreature
	{
		public BaseDMChampion( AIType aiType ) : this( aiType, FightMode.Closest )
		{
		}

		public BaseDMChampion( AIType aiType, FightMode mode ) : base( aiType, mode, 18, 1, 0.1, 0.2 )
		{
		}

		public BaseDMChampion( Serial serial ) : base( serial )
		{
		}

        public abstract Type[] UniqueArtifacts { get; }
        public abstract Type[] SharedArtifacts { get; }
        public abstract Type[] DecorationArtifacts { get; }
		public abstract Type[] TokenArtifacts { get ; }
        public abstract MonsterStatuetteType[] StatueTypes { get; }

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

        public Item GetArtifact()
        {
            double random = Utility.RandomDouble();

            if (random < 0.60)
            {
                double random1 = Utility.Random(29);
                if (random1 <= 2)
                    return CreateArtifact(UniqueArtifacts);
                else if (random1 >= 3 && random1 <= 8)
                    return CreateArtifact(SharedArtifacts);
                else if (random1 >= 9 && random1 <= 12)
                    return CreateArtifact(DecorationArtifacts);
				else
					return CreateArtifact(TokenArtifacts);
            }
            return null;
        }

        public Item CreateArtifact(Type[] list)
        {
            if (list.Length == 0)
                return null;

            int random = Utility.Random(list.Length);

            Type type = list[random];

            Item artifact = Loot.Construct(type);

            if (artifact is MonsterStatuette && StatueTypes.Length > 0)
            {
                ((MonsterStatuette)artifact).Type = StatueTypes[Utility.Random(StatueTypes.Length)];
                ((MonsterStatuette)artifact).LootType = LootType.Regular;
            }

            return artifact;
        }

		public override bool OnBeforeDeath()
		{
			if ( !NoKillAwards )
			{
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
				List<DamageStore> rights = BaseCreature.GetLootingRights( this.DamageEntries, this.HitsMax );
				List<Mobile> toGive = new List<Mobile>();

				for ( int i = rights.Count - 1; i >= 0; --i )
				{
					DamageStore ds = rights[i];

					if ( ds.m_HasRight )
						toGive.Add( ds.m_Mobile );
				}
			}

			base.OnDeath( c );
			if ( Utility.RandomDouble() < 0.001 )
			c.DropItem( new DiscountCoupon() );
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

				Gold g = new Gold( 50, 150 );
				
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