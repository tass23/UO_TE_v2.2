using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.ACC.CSS.Systems.LightForce
{
    public class LightVortex : BaseCreature
    {
        private Timer m_Timer;
		public override bool IsDispellable { get { return false; } }
		public override bool IsBondable { get { return false; } }
		
        [Constructable]
        public LightVortex()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Force Vortex";
            Body = 573;
			Hue = 1385;

            m_Timer = new InternalTimer(this);
            m_Timer.Start();
            AddItem(new LightSource());

			SetStr( 100, 135 );
			SetDex( 100, 115 );
			SetInt( 100, 150 );

			SetDamage( 17, 25 );

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Energy, 50);

			SetResistance( ResistanceType.Physical, 100, 105 );
			SetResistance( ResistanceType.Fire, 100, 105 );
			SetResistance( ResistanceType.Cold, 100, 105 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 100 );

            SetSkill(SkillName.MagicResist, 99.9);
            SetSkill(SkillName.Tactics, 90.0);
            SetSkill(SkillName.Wrestling, 100.0);

            Fame = 0;
            Karma = 0;

            VirtualArmor = 60;
            ControlSlots = 5;
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override int GetAngerSound()
        {
            return 0x15;
        }

        public override int GetAttackSound()
        {
            return 0x28;
        }
        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            attacker.BoltEffect(0);
            AOS.Damage(this, attacker, 20, 0, 0, 0, 0, 100);
        }
        public override void OnGaveMeleeAttack(Mobile attacker)
        {
            base.OnGaveMeleeAttack(attacker);

            attacker.BoltEffect(0);
            AOS.Damage(this, attacker, 20, 0, 0, 0, 0, 100);
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            base.AlterDamageScalarFrom(caster, ref scalar);
            caster.BoltEffect(0);
            AOS.Damage(this, caster, 20, 0, 0, 0, 0, 100);

        }
        public LightVortex(Serial serial)
            : base(serial)
        {
            m_Timer = new InternalTimer(this);
            m_Timer.Start();
        }

        public override void OnDelete()
        {
            m_Timer.Stop();

            base.OnDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        private class InternalTimer : Timer
        {
            private LightVortex m_Owner;
            private int m_Count = 0;

            public InternalTimer(LightVortex owner)
                : base(TimeSpan.FromSeconds(0.1), TimeSpan.FromSeconds(0.1))
            {
                m_Owner = owner;
            }

            protected override void OnTick()
            {
                if ((m_Count++ & 0x3) == 0)
                {
                    m_Owner.Direction = (Direction)(Utility.Random(8) | 0x80);
                }

                m_Owner.Move(m_Owner.Direction);
            }
        }
    }
}