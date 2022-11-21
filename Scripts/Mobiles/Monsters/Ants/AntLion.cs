using System;
using System.Collections;
using Server.Items;
using Server.Spells;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ant lion corpse" )]
	public class AntLion : BaseCreature
	{
		[Constructable]
		public AntLion() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ant lion";
			Body = 787;
			BaseSoundID = 1006;
			SpeechHue = 0x3B2;

			SetStr( 296, 320 );
			SetDex( 81, 105 );
			SetInt( 36, 60 );

			SetHits( 151, 162 );

			SetDamage( 7, 21 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Poison, 30 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 25, 35 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 30, 35 );

			SetSkill( SkillName.MagicResist, 70.0 );
			SetSkill( SkillName.Tactics, 90.0 );
			SetSkill( SkillName.Wrestling, 90.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 45;

			PackItem( new FertileDirt( Utility.RandomMinMax( 1, 5 ) ) );

			if ( Core.ML && Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomPeculiarSeed(2) );

			Item orepile = null; /* no trust, no love :( */

			switch ( Utility.Random( 4 ) )
			{
				case 0: orepile = new DullCopperOre(); break;
				case 1: orepile = new ShadowIronOre(); break;
				case 2: orepile = new CopperOre(); break;
				default: orepile = new BronzeOre(); break;
			}
			orepile.Amount = Utility.RandomMinMax(1, 10);
			orepile.ItemID = 0x19B9;
			PackItem(orepile);

			for ( int i = 0; i < 3; i++ )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0: PackItem( new BoneShards() ); break;
					case 1: PackItem( new SpineBone() ); break;
					case 2: PackItem( new RibCage() ); break; ;
					case 3: PackItem( new PelvisBone() ); break;
					case 4: PackItem( new Skull() ); break;
				}
			}

			if ( 0.07 >= Utility.RandomDouble() )
			{
				switch ( Utility.Random( 3 ) )
				{
					case 0: PackItem( new UnknownBardSkeleton() ); break;
					case 1: PackItem( new UnknownMageSkeleton() ); break;
					case 2: PackItem( new UnknownRogueSkeleton() ); break;
				}
			}
		}

		public override int GetAngerSound()
		{
			return 0x5A;
		}

		public override int GetIdleSound()
		{
			return 0x5A;
		}

		public override int GetAttackSound()
		{
			return 0x164;
		}

		public override int GetHurtSound()
		{
			return 0x187;
		}

		public override int GetDeathSound()
		{
			return 0x1BA;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 1 );
		}

		public override void OnThink()
		{
			base.OnThink();

			if ( 0.05 >= Utility.RandomDouble() )
				BeginAcidBreath();
			else
				BeginTunneling();
		}

		public override void OnDamage(int amount, Mobile from, bool willKill)
		{
			if ( m_TunnelTimer != null && m_TunnelTimer.Running )
			{
				Frozen = false;
				m_TunnelTimer.Stop();

				SayTo( from, "* You interrupt the ant lion's digging! *" );
			}

			if ( 0.25 >= Utility.RandomDouble() )
				BeginAcidBreath();

			base.OnDamage( amount, from, willKill );
		}

		#region Acid Breath
		private DateTime m_NextAcidBreath;

		public void BeginAcidBreath()
		{
			Mobile m = Combatant;

			if ( m == null || m.Deleted || !m.Alive || !Alive || m_NextAcidBreath > DateTime.Now || !CanBeHarmful( m ) )
				return;

			PlaySound( 0x118 );
			MovingEffect( m, 0x36D4, 1, 0, false, false, 0x3F, 0 );

			TimeSpan delay = TimeSpan.FromSeconds( GetDistanceToSqrt( m ) / 5.0 );
			Timer.DelayCall<Mobile>( delay, new TimerStateCallback<Mobile>( EndAcidBreath ), m );

			m_NextAcidBreath = DateTime.Now + TimeSpan.FromSeconds( 5 );
		}

		public void EndAcidBreath( Mobile m )
		{
			if ( m == null || m.Deleted || !m.Alive || !Alive )
				return;

			if ( 0.2 >= Utility.RandomDouble() )
				m.ApplyPoison( this, Poison.Greater );

			AOS.Damage( m, Utility.RandomMinMax( 20, 40 ), 0, 0, 0, 100, 0 );
		}
		#endregion

		#region Tunneling
		private DateTime m_NextTunneling;
		private Timer m_TunnelTimer;
		private Point3D m_NewLocation;
		private Map m_NewMap;

		public void BeginTunneling()
		{
			Mobile m = Combatant;

			if ( m == null || m.Deleted || !m.Alive || !Alive || m_NextTunneling > DateTime.Now || !CanBeHarmful( m ) || !m.InRange( this, 2 ) )
				return;

			Frozen = true;

			Hole hole = new Hole( 0xF34 );
			hole.MoveToWorld( Location, Map );

			m_NewLocation = Location;
			m_NewMap = Map;

			PlaySound( 0x21E );
			Say( "* The ant lion begins tunneling into the ground *" );

			m_TunnelTimer = Timer.DelayCall<Mobile>( TimeSpan.FromSeconds( 3 ), new TimerStateCallback<Mobile>( EndTunneling ), m );
			m_NextTunneling = DateTime.Now + TimeSpan.FromSeconds( Utility.RandomMinMax( 12, 20 ) );
		}

		public void EndTunneling( Mobile target )
		{
			Hole hole = new Hole( 0x1363 );
			hole.MoveToWorld( Location, Map );

			Internalize();

			m_TunnelTimer = Timer.DelayCall<Mobile>( TimeSpan.FromSeconds( 3 ), new TimerStateCallback<Mobile>( Reappear ), target );
		}

		public void Reappear( Mobile target )
		{
			Hits += 50;
			Frozen = false;

			if ( !Alive || target == null || target.Deleted || !target.Alive || !target.InRange( m_NewLocation, 10 ) || target.Map != m_NewMap )
			{
				MoveToWorld( m_NewLocation, m_NewMap );
			}
			else
			{
				Point3D location = target.Location;

				if ( SpellHelper.FindValidSpawnLocation( m_NewMap, ref location, true ) )
				{
					Hole hole = new Hole( 0xF34 );
					hole.MoveToWorld( location, m_NewMap );
					hole = new Hole( 0x1363 );
					hole.MoveToWorld( location, m_NewMap );

					MoveToWorld( location, m_NewMap );
				}
				else
					MoveToWorld( m_NewLocation, m_NewMap );
			}

			Combatant = target;
		}

		private class Hole : Static
		{
			public Hole( int itemID ) : base( itemID )
			{
				Hue = 0x1;
				Name = "a hole";

				Timer.DelayCall( TimeSpan.FromSeconds( 10 ), new TimerCallback( Delete ) );
			}

			public Hole( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.WriteEncodedInt( 0 );
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadEncodedInt();

				Delete();
			}
		}
		#endregion

		public AntLion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			if ( m_TunnelTimer != null && m_TunnelTimer.Running )
			{
				writer.Write( true );
				writer.Write( m_NewLocation );
				writer.Write( m_NewMap );
			}
			else
				writer.Write( false );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					if ( reader.ReadBool() )
					{
						m_NewLocation = reader.ReadPoint3D();
						m_NewMap = reader.ReadMap();

						Reappear( null );
					}
				
					break;
				}
				case 0:
				{
					SpeechHue = 0x3B2;

					break;
				}
			}
		}
	}
}