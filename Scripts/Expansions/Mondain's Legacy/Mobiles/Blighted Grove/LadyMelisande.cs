using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a lady melisande corpse" )]
	public class LadyMelisande : BasePeerless
	{
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler( delegate( PlayerDeathEventArgs e )
			{
				HandleDeath( e.Mobile );
			} );
		}

		[Constructable]
		public LadyMelisande() : base( AIType.AI_NecroMage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a lady melisande";
			Body = 0x102;
			BaseSoundID = 451;

			SetStr( 400, 1000 );
			SetDex( 300, 400 );
			SetInt( 1500, 1700 );

			SetHits( 100000 );	

			SetDamage( 11, 18 );
			
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 40, 60 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 70, 75 );
			SetResistance( ResistanceType.Energy, 70, 80 );
			
			SetSkill( SkillName.Wrestling, 100, 105 );
			SetSkill( SkillName.Tactics, 100, 105 );
			SetSkill( SkillName.MagicResist, 120 );
			SetSkill( SkillName.Magery, 120 );
			SetSkill( SkillName.EvalInt, 120 );
			SetSkill( SkillName.Meditation, 120 );
			SetSkill( SkillName.Necromancy, 120 );
			SetSkill( SkillName.SpiritSpeak, 120 );

			PackResources( 8 );
			PackTalismans( 5 );

			Timer.DelayCall( TimeSpan.FromSeconds( 1 ), new TimerCallback( SpawnSatyrs ) );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 50;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 8 );
			AddLoot( LootPack.Parrot, 1 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			c.DropItem( new DiseasedBark() );
			c.DropItem( new EternallyCorruptTree() );
			c.DropItem( new RewardScroll() );
			
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll() );
			if ( Utility.RandomDouble() < 0.001 )
			c.DropItem( new DiscountCoupon() );
			
			if ( Utility.RandomDouble() < 0.2 )	
				c.DropItem( new RainbowMountToken2() );

			int drop = Utility.Random( 4, 8 );

			for ( int i = 0; i < drop; i++ )
				c.DropItem( new MelisandesFermentedWine() );

			if ( Utility.RandomDouble() < 0.6 )
				c.DropItem( new ParrotItem() );

			if ( Utility.RandomDouble() < 0.2225 )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: c.DropItem( new MelisandesHairDye() ); break;
					case 1: c.DropItem( new MelisandesCorrodedHatchet() ); break;
					case 2: c.DropItem( new AlbinoSquirrelImprisonedInCrystal() ); break;
				}
			}

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new MyrmidonLegs() );

			if ( Utility.RandomDouble() < 0.025 )
				c.DropItem( new CrimsonCincture() );
				
			if ( Utility.RandomDouble() < 0.025 )
				c.DropItem( new HumilityCloak() );
		}
		
		public override void OnThink()
		{
			base.OnThink();

			if ( CanTakeLife( Combatant ) )
				TakeLife( Combatant );

			if ( CanSmackTalk() )
				SmackTalk();

			if ( CanPutridNausea() )
				PutridNausea();
		}

		public override void SetLocation( Point3D newLocation, bool isTeleport )
		{
			if ( newLocation.Z > -10 )
				base.SetLocation( newLocation, isTeleport );
		}
		
		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( willKill )
			{
				SpawnHelper( new Reaper(), 6490, 948, 19 );
				SpawnHelper( new InsaneDryad(), 6497, 946, 17 );
				SpawnHelper( new StoneHarpy(), 6511, 946, 28 ); 	

				Say( 1075118 ); // Noooooo!  You shall never defeat me.  Even if I should fall, my tree will sustain me and I will rise again.
			}
				
			base.OnDamage( amount, from, willKill );				
		}
	
		public override bool GivesMinorArtifact{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public LadyMelisande( Serial serial ) : base( serial )
		{
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
		
		#region Smack Talk
		private DateTime m_NextSmackTalk;

		public bool CanSmackTalk()
		{
			if ( m_NextSmackTalk > DateTime.Now )
				return false;

			if ( Combatant == null )
				return false;

			return Hits > 0.5 * HitsMax;
		}

		public void SmackTalk()
		{
			Say( Utility.RandomMinMax( 1075102, 1075115 ) ); // Muahahahaha!  I'll feast on your flesh.

			m_NextSmackTalk = DateTime.Now + TimeSpan.FromSeconds( 2 + Utility.RandomDouble() * 3 );	
		}
		#endregion
		
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
			
			Say( 1075117 );  // Muahahaha!  Your life essence is MINE!
			Say( 1075120 ); // An unholy aura surrounds Lady Melisande as her wounds begin to close.

			m_NextTakeLife = DateTime.Now + TimeSpan.FromSeconds( 15 + Utility.RandomDouble() * 45 );
		}
		#endregion

		#region Helpers
		public override bool CanSpawnHelpers{ get { return true; } }
		public override int MaxHelpersWaves{ get { return 1; } }

		public override void SpawnHelpers()
		{
			int count = 4;

			if ( Altar != null )
			{
				count = Math.Min( Altar.Fighters.Count, 4 );

				for ( int i = 0; i < count; i++ )
				{
					Mobile fighter = Altar.Fighters[ i ];

					if ( CanBeHarmful( fighter ) )
					{
						EnslavedSatyr satyr = new EnslavedSatyr();
						satyr.Combatant = fighter;
						SpawnHelper( satyr, GetSpawnPosition( fighter.Location, fighter.Map, 2 ) );

						fighter.SendLocalizedMessage( 1075116 ); // A twisted satyr scrambles onto the branch beside you and attacks!
					}
				}
			}
			else
			{
				for ( int i = 0; i < count; i++ )
					SpawnHelper( new EnslavedSatyr(), 4 );
			}
		}	

		public void SpawnSatyrs()
		{
			SpawnHelper( new EnslavedSatyr(), 6485, 945, 19 );
			SpawnHelper( new EnslavedSatyr(), 6486, 948, 22 );
			SpawnHelper( new EnslavedSatyr(), 6487, 945, 17 ); 
			SpawnHelper( new EnslavedSatyr(), 6488, 947, 23 ); 
		}
		#endregion
	}
}