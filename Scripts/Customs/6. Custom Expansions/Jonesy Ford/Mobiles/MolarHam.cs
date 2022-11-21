using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "Molar Ham's corpse" )]
	public class MolarHam : BasePeerless
	{
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler( delegate( PlayerDeathEventArgs e )
			{
				HandleDeath( e.Mobile );
			} );
		}
	
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.CrushingBlow;
			}
		}
		
		[Constructable]
		public MolarHam() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Molar Ham";
			Body = 183;
			BaseSoundID = 0x165;

			SetStr( 500 );
			SetDex( 175 );
			SetInt( 1000 );

			SetHits( 45000 );
			SetMana( 5000 );

			SetDamage( 33, 37 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 50 );
			SetResistance( ResistanceType.Fire, 50 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 50 );
			SetResistance( ResistanceType.Energy, 50 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.Necromancy, 120 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 110.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 28000;
			Karma = -28000;

			Timer.DelayCall( TimeSpan.FromSeconds( 5 ), new TimerCallback( SpawnChangelings ) );
			
			VirtualArmor = 64;
			
			StaffOfTheMagi weapon = new StaffOfTheMagi();
			weapon.Movable = false;
			AddItem( weapon );

			DragonHelm helm = new DragonHelm();
			helm.Movable = false;
			AddItem( helm );

			BoneGloves gloves = new BoneGloves();
			gloves.Movable = false;
			gloves.Hue = 2412;
			AddItem( gloves );
			
			BoneArms arms = new BoneArms();
			arms.Movable = false;
			arms.Hue = 2412;
			AddItem( arms );

			Cloak cloak = new Cloak();
			cloak.Movable = false;
			cloak.Hue = 2412;
			AddItem( cloak );
			
			StuddedLegs legs = new StuddedLegs();
			legs.Movable = false;
			legs.Hue = 2412;
			AddItem( legs );

			ThighBoots boots = new ThighBoots();
			boots.Movable = false;
			boots.Hue = 2412;
			AddItem( boots );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 2 );
			AddLoot( LootPack.Parrot, 1 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			c.DropItem(new RewardScroll());
			c.DropItem(new SankaraStones());
			
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll() );
		}
		
		public override void OnThink()
		{
			base.OnThink();

			if ( CanTakeLife( Combatant ) )
				TakeLife( Combatant );

			if ( CanPutridNausea() )
				PutridNausea();
		}
		
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override int TreasureMapLevel{ get{ return 6; } }

		private static bool m_InHere;

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from != this && !m_InHere )
			{
				m_InHere = true;
				AOS.Damage( from, this, Utility.RandomMinMax( 8, 20 ), 100, 0, 0, 0, 0 );

				MovingEffect( from, 0xECA, 10, 0, false, false, 0, 0 );
				PlaySound( 0x491 );

				if ( 0.05 > Utility.RandomDouble() )
					Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( CreateBones_Callback ), from );

				m_InHere = false;
			}
			
			if ( willKill )
			{
				SpawnHelper( new Changeling(), 6368, 1630, 1 );
				SpawnHelper( new Changeling(), 6371, 1633, 1 );
				SpawnHelper( new Changeling(), 6374, 1630, 1 ); 	

				Say( "You shall never defeat me!" );
			}
				
			base.OnDamage( amount, from, willKill );
		}

		public virtual void CreateBones_Callback( object state )
		{
			Mobile from = (Mobile)state;
			Map map = from.Map;

			if ( map == null )
				return;

			int count = Utility.RandomMinMax( 1, 3 );

			for ( int i = 0; i < count; ++i )
			{
				int x = from.X + Utility.RandomMinMax( -1, 1 );
				int y = from.Y + Utility.RandomMinMax( -1, 1 );
				int z = from.Z;

				if ( !map.CanFit( x, y, z, 16, false, true ) )
				{
					z = map.GetAverageZ( x, y );

					if ( z == from.Z || !map.CanFit( x, y, z, 16, false, true ) )
						continue;
				}

				UnholyBone bone = new UnholyBone();

				bone.Hue = 0;
				bone.Name = "Sacrificial Bones";
				bone.ItemID = Utility.Random( 0xECA, 9 );

				bone.MoveToWorld( new Point3D( x, y, z ), map );
			}
		}

		public MolarHam( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		#region Putrid Nausea
		private static Dictionary<Mobile,Timer> m_Table = new Dictionary<Mobile,Timer>();
		private DateTime m_NextPutridNausea;

		public bool CanPutridNausea()
		{
			if ( m_NextPutridNausea > DateTime.Now )
				return false;

			return Combatant != null;
		}

		public void PutridNausea()
		{
			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in GetMobilesInRange( 4 ) )
			{
				if ( CanBeHarmful( m ) && m.Player )
					list.Add( m );
			}

			for ( int i = 0; i < list.Count; i++ )
			{
				Mobile m = list[ i ];

				if ( m_Table.ContainsKey( m ) )
				{
					Timer timer = m_Table[ m ];

					if ( timer != null )
						timer.Stop();

					m_Table[ m ] = Timer.DelayCall<Mobile>( TimeSpan.FromSeconds( 30 ), new TimerStateCallback<Mobile>( EndPutridNausea ), m );
				}
				else
					m_Table.Add( m, Timer.DelayCall<Mobile>( TimeSpan.FromSeconds( 30 ), new TimerStateCallback<Mobile>( EndPutridNausea ), m ) );

				m.Animate( 32, 5, 1, true, false, 0 ); // bow animation
				m.SendLocalizedMessage( 1072068 ); // Your enemy's putrid presence envelops you, overwhelming you with nausea.
			}

			m_NextPutridNausea = DateTime.Now + TimeSpan.FromSeconds( 40 + Utility.RandomDouble() * 30 );
		}

		public void EndPutridNausea( Mobile m )
		{
			m_Table.Remove( m );
		}

		public static void HandleDeath( Mobile m )
		{
			if ( m_Table.ContainsKey( m ) )
			{
				Timer timer = m_Table[ m ];

				if ( timer != null )
					timer.Stop();

				m_Table.Remove( m );
			}
		}

		public static bool UnderPutridNausea( Mobile m )
		{			
			return m_Table.ContainsKey( m );
		}
		#endregion
		
		#region Take Life
		private DateTime m_NextTakeLife;

		public bool CanTakeLife( Mobile from )
		{
			if ( m_NextTakeLife > DateTime.Now )
				return false;

			if ( !CanBeHarmful( from ) )
				return false;

			if ( Hits > 0.1 * HitsMax || Hits < 0.025 * HitsMax )
				return false;

			return true;
		}
				
		public void TakeLife( Mobile from )
		{
			Hits += from.Hits / ( from.Player ? 2 : 6 );
			
			FixedParticles( 0x376A, 9, 32, 5005, EffectLayer.Waist );
			PlaySound( 0x1F2 );
			
			Say( "Kali shawked mi to deh" );
			Say( "An unholy aura surrounds Molar Ham as his wounds begin to close." );

			m_NextTakeLife = DateTime.Now + TimeSpan.FromSeconds( 15 + Utility.RandomDouble() * 45 );
		}
		#endregion
		
		#region Helpers
		public override bool CanSpawnHelpers{ get { return true; } }
		public override int MaxHelpersWaves{ get { return 1; } }

		public override void SpawnHelpers()
		{
			int count = 4;
			
			for ( int i = 0; i < count; i++ )
				SpawnHelper( new Changeling(), 4 );
		}	

		public void SpawnChangelings()
		{
			SpawnHelper( new Changeling(), 6370, 1630, 1 );
			SpawnHelper( new Changeling(), 6368, 1630, 1 );
			SpawnHelper( new Changeling(), 6371, 1633, 1 ); 
			SpawnHelper( new Changeling(), 6374, 1630, 1 ); 
		}
		#endregion
	}
}		