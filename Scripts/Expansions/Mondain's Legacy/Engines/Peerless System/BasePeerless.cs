using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	public class BasePeerless : BaseCreature
	{
		private PeerlessAltar m_Altar;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public PeerlessAltar Altar
		{
			get{ return m_Altar; }
			set{ m_Altar = value; }
		}		
		
		public override bool Unprovokable{ get{ return true; } }
		public virtual double ChangeCombatant{ get{ return 0.3; } }
		
		public BasePeerless( Serial serial ) : base( serial )
		{
		}
		
		public override void OnThink()
		{
			base.OnThink();
			
			if ( HasFireRing && Combatant != null && Alive && Hits > 0.8 * HitsMax && m_NextFireRing < DateTime.Now && Utility.RandomDouble() < FireRingChance )
				FireRing();
				
			if ( CanSpawnHelpers && Combatant != null && Alive && CanSpawnWave() )
				SpawnHelpers();
		}
		
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if ( m_Altar != null )
				m_Altar.OnPeerlessDeath();
		}		
		
		public BasePeerless( AIType aiType, FightMode fightMode, int rangePerception, int rangeFight, double activeSpeed, double passiveSpeed ) : base ( aiType, fightMode, rangePerception, rangeFight, activeSpeed, passiveSpeed )
		{
			m_NextFireRing = DateTime.Now + TimeSpan.FromSeconds( 10 );			
			m_CurrentWave = MaxHelpersWaves;
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( (Item) m_Altar );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_Altar = reader.ReadItem() as PeerlessAltar;
		}
		
		#region Helpers		
		public virtual bool CanSpawnHelpers{ get{ return false; } }
		public virtual int MaxHelpersWaves{ get{ return 0; } }
		public virtual double SpawnHelpersChance{ get{ return 0.05; } }
		
		private int m_CurrentWave;
		
		public int CurrentWave
		{
			get { return m_CurrentWave; }
			set { m_CurrentWave = value; }
		}

		public bool AllHelpersDead
		{
			get
			{
				if ( m_Altar != null )
					return m_Altar.AllHelpersDead();

				return true;
			}
		}
		
		public virtual bool CanSpawnWave()
		{
			if ( MaxHelpersWaves > 0 && m_CurrentWave > 0 )
			{
				double hits = ( Hits / (double) HitsMax );
				double waves = ( m_CurrentWave / (double) ( MaxHelpersWaves + 1 ) );
				
				if ( hits < waves && Utility.RandomDouble() < SpawnHelpersChance )
				{
					m_CurrentWave -= 1;
					return true;
				}
			}
			
			return false;
		}
		
		public virtual void SpawnHelpers()
		{			
		}		
		
		public void SpawnHelper( BaseCreature helper, int range )
		{
			SpawnHelper( helper, GetSpawnPosition( range ) );					
		}
		
		public void SpawnHelper( BaseCreature helper, int x, int y, int z )
		{
			SpawnHelper( helper, new Point3D( x, y, z ) );					
		}
		
		public void SpawnHelper( BaseCreature helper, Point3D location )
		{
			if ( helper == null )
				return;

			helper.Home = location;
			helper.RangeHome = 4;
		
			if ( m_Altar != null )
				m_Altar.AddHelper( helper );
				
			helper.MoveToWorld( location, Map );			
		}
		#endregion
		
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
		
		public virtual void PackTalismans( int amount )
		{			
			int count = Utility.Random( amount );
			
			for ( int i = 0; i < count; i ++ )
				PackItem( Loot.RandomTalisman() );
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
		
		#region Fire Ring
		private static int[] m_North = new int[]
		{
			-1, -1, 
			1, -1,
			-1, 2,
			1, 2
		};
		
		private static int[] m_East = new int[]
		{
			-1, 0,
			2, 0
		};		
		
		public virtual bool HasFireRing{ get{ return false; } }
		public virtual double FireRingChance{ get{ return 1.0; } }
		
		private DateTime m_NextFireRing = DateTime.Now;
		
		public virtual void FireRing()
		{
			for ( int i = 0; i < m_North.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_North[ i ];
				p.Y += m_North[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E27, 50 );
			}
			
			for ( int i = 0; i < m_East.Length; i += 2 ) 
			{
				Point3D p = Location;
				
				p.X += m_East[ i ];
				p.Y += m_East[ i + 1 ];
				
				IPoint3D po = p as IPoint3D;
				
				SpellHelper.GetSurfaceTop( ref po );
				
				Effects.SendLocationEffect( po, Map, 0x3E31, 50 );
			}
			
			m_NextFireRing = DateTime.Now + TimeSpan.FromSeconds( 10 );
		}
		#endregion
	}
}