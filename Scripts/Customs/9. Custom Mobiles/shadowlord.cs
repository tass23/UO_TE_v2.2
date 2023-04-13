using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a shadowlord corpse" )]
	public class shadowlord : BaseCreature
	{
		public static double ChestChance = .30;
		
		[Constructable]
		public shadowlord () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Nosfentor The Shadowlord of Cowardice";
			Body = 704;
			BaseSoundID = 0x47D;
			Team = 1;
			SetStr( 190, 210 );
			SetDex( 450, 550 );
			SetInt( 350, 450 );
			NameHue = 44;
			SetHits( 90000, 120000 );

			SetDamage( 10, 30 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 90, 95 );
			SetResistance( ResistanceType.Fire, 90, 95 );
			SetResistance( ResistanceType.Cold, 90, 95 );
			SetResistance( ResistanceType.Poison, 90, 95 );
			SetResistance( ResistanceType.Energy, 90, 95 );

			SetSkill( SkillName.EvalInt, 100.0, 120.0 );
			SetSkill( SkillName.Magery, 190.0, 200.0 );
			SetSkill( SkillName.Meditation, 90.0, 120.0 );
			SetSkill( SkillName.MagicResist, 100.0, 150.0 );
			SetSkill( SkillName.Tactics, 80.0, 120.0 );
			SetSkill( SkillName.Wrestling, 190.0, 200.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			//AddLoot( LootPack.MedScrolls, 2 );
		}
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			c.DropItem( new RewardScroll(15) );
			if ( Utility.RandomDouble() < 0.25 )
			c.DropItem( new RewardScroll(2) );
			if ( Utility.RandomDouble() < 0.15 )
			c.DropItem( new RewardScroll(5) );
			if ( Utility.RandomDouble() < 0.10 )
			c.DropItem( new RewardScroll(10) );
			if ( Utility.RandomDouble() < 0.05 )
			c.DropItem( new RewardScroll(15) );
		}	
		
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		
		private static bool m_InHere;
		
		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from != this && !m_InHere )
			{
				m_InHere = true;
				AOS.Damage( from, this, Utility.RandomMinMax( 0, 2 ), 100, 0, 0, 0, 0 );

				MovingEffect( from, 0x37C4, 10, 0, false, false, 0, 0 );
				PlaySound( 0x47D );

				if ( 0.03 > Utility.RandomDouble() )
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( CreateRottingCorpse ), from );

				m_InHere = false;
			}
		}
        public virtual void CreateRottingCorpse( object state )
		{
			Mobile from = (Mobile)state;
			Map map = from.Map;

			if ( map == null )
				return;

			int count = Utility.RandomMinMax( 3, 6 );

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

				RottingCorpse summon1 = new RottingCorpse();
				Wraith summon2 = new Wraith();
				
				summon1.Team = 1;
				summon1.Hue = 44;
				summon1.Name = "a Nosfentor's minion";
				summon2.Team = 1;
				summon1.MoveToWorld( new Point3D( x, y, z ), map );
				summon2.MoveToWorld( new Point3D( x, y, z ), map );
				PlaySound( 0x653 );
			}
		}
		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "You feel an aura of overwhelming hatred!" );

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

                switch (Utility.Random(4))
                {
                    case 0: defender.FixedParticles( 14089, 1, 15, 0x00,  44, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x225 ); break;
                    case 1: defender.FixedParticles( 14013, 1, 15, 0x00,  44, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x11D ); break;
                    case 2: defender.FixedParticles( 0x3818, 1, 15, 0x00,  44, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x51E ); break;
                    case 3: defender.FixedParticles( 14217, 1, 15, 0x00,  44, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x029 ); break;
                 

                }
			
			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
	
		
                
			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();
		}

		public shadowlord( Serial serial ) : base( serial )
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
	}
}