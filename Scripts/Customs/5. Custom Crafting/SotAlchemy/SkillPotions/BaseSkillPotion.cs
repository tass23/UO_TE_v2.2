using System;
using System.Collections;
using Server;

namespace Server.Items
{
    public class BaseSkillPotion : BasePotion
    {
        private double m_Duration;
        [CommandProperty(AccessLevel.GameMaster)]
        public double Duration
        {
            get { return m_Duration; }
            set { m_Duration = value; }
        }

        private double m_Effect;
        [CommandProperty(AccessLevel.GameMaster)]
        public double Effect
        {
            get { return m_Effect; }
            set { m_Effect = value; }
        }

        private SkillName m_Skill;
        public SkillName Skill
        {
            get { return m_Skill; }
            set { m_Skill = value; }
        }

        public BaseSkillPotion(PotionEffect potionEffect, double duration, double effect, SkillName skill)
            : base(0xF06, potionEffect)
        {
            m_Duration = duration;
            m_Effect = effect;
            m_Skill = skill;
        }

        public BaseSkillPotion(Serial serial)
            : base(serial)
        {
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

        public override void Drink(Mobile from)
        {
            Hashtable table = new Hashtable();
            switch (m_Skill)
            {
                case (SkillName.MagicResist): table = MagicResistSkillTable; break;
            }

            ExpireTimer timer = (ExpireTimer)table[from];

            if (timer != null)
                timer.DoExpire();

            SkillMod sm = new DefaultSkillMod(m_Skill, true, m_Effect);
            from.AddSkillMod(sm);

            timer = new ExpireTimer(from, sm, table, TimeSpan.FromSeconds(m_Duration));
            timer.Start();
            table[from] = timer;

            from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            from.PlaySound(0x1E3);

            BasePotion.PlayDrinkEffect(from);
            this.Consume();
        }

        private static Hashtable MagicResistSkillTable = new Hashtable();

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private SkillMod m_SkillMod;
            private Hashtable m_Table;

            public ExpireTimer(Mobile m, SkillMod sm, Hashtable table, TimeSpan delay)
                : base(delay)
            {
                m_Mobile = m;
                m_SkillMod = sm;
                m_Table = table;
                Priority = TimerPriority.OneSecond;
            }

            public void DoExpire()
            {
                m_Mobile.RemoveSkillMod(m_SkillMod);
                Stop();
                m_Table.Remove(m_Mobile);
            }

            protected override void OnTick()
            {
                DoExpire();
            }
        }
    }
}