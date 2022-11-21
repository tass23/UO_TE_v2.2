using System;
using System.Collections;
using Server;

namespace Server.Items
{
    public class BaseResistPotion : BasePotion
    {
        private double m_Duration;
        [CommandProperty(AccessLevel.GameMaster)]
        public double Duration
        {
            get { return m_Duration; }
            set { m_Duration = value; }
        }

        private int m_Effect;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Effect
        {
            get { return m_Effect; }
            set { m_Effect = value; }
        }

        private ResistanceType m_ResistType;
        public ResistanceType ResistType
        {
            get { return m_ResistType; }
            set { m_ResistType = value; }
        }
        
        public BaseResistPotion(PotionEffect potionEffect, double duration, int effect, ResistanceType resistType)
            : base(0xF06, potionEffect)
        {
            m_Duration = duration;
            m_Effect = effect;
            m_ResistType = resistType;
        }

        public BaseResistPotion(Serial serial)
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
            switch (m_ResistType)
            {
                case (ResistanceType.Physical): table = PhysResistTable; break;
            }

            ExpireTimer timer = (ExpireTimer)table[from];

            if (timer != null)
                timer.DoExpire();

            ResistanceMod mod = new ResistanceMod(m_ResistType, m_Effect);
            
            from.AddResistanceMod(mod);

            timer = new ExpireTimer(from, mod, table, TimeSpan.FromSeconds(m_Duration));
            timer.Start();
            table[from] = timer;

            from.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
            from.PlaySound(0x1E3);
            
            BasePotion.PlayDrinkEffect(from);
            this.Consume();
        }

        private static Hashtable PhysResistTable = new Hashtable();

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private ResistanceMod m_Mod;
            private Hashtable m_Table;

            public ExpireTimer(Mobile m, ResistanceMod mod, Hashtable table, TimeSpan delay)
                : base(delay)
            {
                m_Mobile = m;
                m_Mod = mod;
                m_Table = table;
                Priority = TimerPriority.OneSecond;
            }

            public void DoExpire()
            {
                m_Mobile.RemoveResistanceMod(m_Mod);
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