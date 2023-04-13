using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using Server.Spells;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "a shadowlord corpse" )]
	public class shadowlord2 : BaseCreature
	{
		public static double ChestChance = .30;
		
		[Constructable]
		public shadowlord2 () : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Faulinei The Shadowlord of Falsehood";
			Body = 704;
			BaseSoundID = 0x47D;
			Team = 1;
			SetStr( 190, 210 );
			SetDex( 450, 550 );
			SetInt( 350, 450 );
			NameHue = 362;
			SetHits( 90000, 120000 );

			SetDamage( 10, 30 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 90, 95 );
			SetResistance( ResistanceType.Fire, 90, 95 );
			SetResistance( ResistanceType.Cold, 90, 95 );
			SetResistance( ResistanceType.Poison, 90, 95 );
			SetResistance( ResistanceType.Energy, 90, 95 );

			SetSkill( SkillName.Focus, 100.0, 120.0 );
			SetSkill( SkillName.Magery, 190.0, 200.0 );
			SetSkill( SkillName.Anatomy, 90.0, 120.0 );
			SetSkill( SkillName.Necromancy, 100.0, 150.0 );
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
	
		public override void OnThink()
		{
			base.OnThink();

			if ( Combatant != null && m_NextWeb < DateTime.Now )
				DoWebAttack();
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
	
	#region Web Attack

		private DateTime m_NextWeb;

        public void DoWebAttack()
        {
            List<Mobile> targets = new List<Mobile>();

            foreach (Mobile m in GetMobilesInRange(RangePerception))
                if (CanBeHarmful(m) && m.Player && !InRange(m, 1) && !m.Paralyzed)
                    targets.Add(m);

            if (targets.Count > 0)
            {
                Mobile target = targets[Utility.Random(targets.Count)];
                TimeSpan delay = TimeSpan.FromSeconds(GetDistanceToSqrt(target) / 15.0);
                Effects.SendMovingEffect(target, this, 0x46E6, 20, 1, false, false);
                Timer.DelayCall<Mobile>(delay, new TimerStateCallback<Mobile>(Entangle), target);
            }

            m_NextWeb = DateTime.Now + TimeSpan.FromSeconds(Utility.RandomMinMax(5, 15));
        }

        public void Entangle( Mobile m )
		{
			Point3D p = Location;

			if ( SpellHelper.FindValidSpawnLocation( Map, ref p, true ) )
			{
				TimeSpan delay = TimeSpan.FromSeconds( Utility.RandomMinMax( 3, 6 ) );
				m.MoveToWorld( p, Map );
				m.PlaySound( 0x230 );
				m.LocalOverheadMessage( MessageType.Regular,  362, true, "*The Shadowlord tried to capture your soul!*" ); 
				p.Z += 2;

				Combatant = m;
			}
		}
	#endregion
	
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

				m.SendMessage( "You feel a terrible fatigue!" );

				int toDrain = Utility.RandomMinMax( 40, 80 );

				Stam += toDrain;
				m.Stam -= toDrain;
				
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

                switch (Utility.Random(4))
                {
                    case 0: defender.FixedParticles( 14089, 1, 15, 0x00,  1765, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x225 ); break;
                    case 1: defender.FixedParticles( 14013, 1, 15, 0x00,  1765, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x11D ); break;
                    case 2: defender.FixedParticles( 0x3818, 1, 15, 0x00,  1765, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x51E ); break;
                    case 3: defender.FixedParticles( 14217, 1, 15, 0x00,  1765, 1, EffectLayer.LeftFoot ); defender.PlaySound( 0x029 ); break;
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

		public shadowlord2( Serial serial ) : base( serial )
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