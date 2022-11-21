/*                                                             .---.
                                                              /  .  \
                                                             |\_/|   |
                                                             |   |  /|
  .----------------------------------------------------------------' |
 /  .-.                                                              |
|  /   \         Contribute To The Orbsydia SA Project               |
| |\_.  |                                                            |
|\|  | /|                        By Lotar84                          |
| `---' |                                                            |
|       |       (Orbanised by Orb SA Core Development Team)          | 
|       |                                                           /
|       |----------------------------------------------------------'
\       |
 \     /
  `---'
*/
using System;
using Server.Mobiles;
using Server.Items;
using Server.Targeting;
using Server;

namespace Server.Items
{
    public class TastyTreat : Item
    {
        public override int LabelNumber { get { return 1113003; } }

        private bool m_Used;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Used
        {
            get { return m_Used; }
            set { m_Used = value; }
        }

        [Constructable]
        public TastyTreat(): base(0xF7E)
        {
            Hue = 1745;

            m_Used = false;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1113213);
            list.Add(1113214);
            list.Add(1070722, "Duration: 20 Min");
            list.Add(1042971, "Cooldown: 2 Min");
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (!m_Used)
            {
                from.SendMessage("which animal you want to Target ?");

                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(1113051);
            }
        }

        private class InternalTarget : Target
        {
            private TastyTreat m_Tasty;

            private int Change1;
            private int Change2;
            private int Change3;

            private int Change4;
            private int Change5;
            private int Change6;


            public InternalTarget(TastyTreat tasty): base(10, false, TargetFlags.None)
            {
                m_Tasty = tasty;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (m_Tasty.Deleted)
                    return;

                if (targeted is BaseCreature)
                {
                    BaseCreature creature = (BaseCreature)targeted;

                    Change1 = (int)((creature.RawStr) * 1.05);
                    Change2 = (int)((creature.RawDex) * 1.05);
                    Change3 = (int)((creature.RawInt) * 1.05);

                    Change4 = (int)(creature.RawStr);
                    Change5 = (int)(creature.RawDex);
                    Change6 = (int)(creature.RawInt);

                    if ((creature.Controlled || creature.Summoned) && (from == creature.ControlMaster) && !(creature.Sleep))
                    {
                        creature.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                        creature.PlaySound(0x1EB);

                        creature.RawStr = Change1;
                        creature.RawDex = Change2;
                        creature.RawInt = Change3;

                        from.SendMessage("You have increased the Stats of your pet by 5% for 20 Minutes!!");
                        m_Tasty.m_Used = true;
                        creature.Sleep = true;


                        Timer.DelayCall(TimeSpan.FromMinutes(20.0), delegate()
                        {

                            creature.RawStr = Change4;
                            creature.RawDex = Change5;
                            creature.RawInt = Change6;
                            creature.PlaySound(0x1DF);

                            m_Tasty.m_Used = true;
                            creature.Sleep = false;
                            from.SendMessage("The Effect Of The Tasty Treat Has Run Its Course!");

                            Timer.DelayCall(TimeSpan.FromMinutes(2.0), delegate()
                            {
                                m_Tasty.m_Used = false;
                            });

                        });

                    }
                    else if ((creature.Controlled || creature.Summoned) && (from == creature.ControlMaster) && (creature.Sleep))
                    {
                        from.SendLocalizedMessage(502676);
                    }
                    else
                    {
                        from.SendLocalizedMessage(1113049);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(500329);
                }
            }

        }

        public TastyTreat(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((bool)m_Used);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Used = reader.ReadBool();
        }
    }

    public class DeliciouslyTastyTreat : Item
    {
        public override int LabelNumber { get { return 1113004; } }

        private bool m_Used;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Used
        {
            get { return m_Used; }
            set { m_Used = value; }
        }

        [Constructable]
        public DeliciouslyTastyTreat(): base(0xF7E)
        {
            Hue = 1745;

            m_Used = false;
        }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1113213);
            list.Add(1113215);
            list.Add(1070722, "Duration: 10 Min");
            list.Add(1042971, "Cooldown: 60 Min");
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (!m_Used)
            {
                from.SendMessage("Which animal you want to Targhet ?");

                from.Target = new InternalTarget(this);
            }
            else
            {
                from.SendLocalizedMessage(1113051);
            }
        }

        private class InternalTarget : Target
        {
            private DeliciouslyTastyTreat m_Tasty;

            private int Change1;
            private int Change2;
            private int Change3;

            private int Change4;
            private int Change5;
            private int Change6;

            public InternalTarget(DeliciouslyTastyTreat tasty): base(10, false, TargetFlags.None)
            {
                m_Tasty = tasty;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                PlayerMobile pm = from as PlayerMobile;

                if (m_Tasty.Deleted)
                    return;

                if (targeted is BaseCreature)
                {
                    BaseCreature creature = (BaseCreature)targeted;

                    Change1 = (int)((creature.RawStr) * 1.10);
                    Change2 = (int)((creature.RawDex) * 1.10);
                    Change3 = (int)((creature.RawInt) * 1.10);

                    Change4 = (int)(creature.RawStr);
                    Change5 = (int)(creature.RawDex);
                    Change6 = (int)(creature.RawInt);

                    if ((creature.Controlled || creature.Summoned) && (from == creature.ControlMaster) && !(creature.Sleep))
                    {
                        creature.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
                        creature.PlaySound(0x1EA);

                        creature.RawStr = Change1;
                        creature.RawDex = Change2;
                        creature.RawInt = Change3;

                        from.SendMessage("You have increased the Stats of your pet by 10% for 10 Minutes !!");
                        m_Tasty.m_Used = true;
                        creature.Sleep = true;


                        Timer.DelayCall(TimeSpan.FromMinutes(10.0), delegate()
                        {

                            creature.RawStr = Change4;
                            creature.RawDex = Change5;
                            creature.RawInt = Change6;
                            creature.PlaySound(0x1EB);

                            m_Tasty.m_Used = true;
                            creature.Sleep = false;

                            from.SendMessage("The effect of Deliciously Tasty Treat is Finish !");

                            Timer.DelayCall(TimeSpan.FromMinutes(60.0), delegate()
                            {
                                m_Tasty.m_Used = false;
                            });

                        });

                    }
                    else if ((creature.Controlled || creature.Summoned) && (from == creature.ControlMaster) && (creature.Sleep))
                    {
                        from.SendLocalizedMessage(502676);
                    }
                    else
                    {
                        from.SendLocalizedMessage(1113049);
                    }
                }
                else
                {
                    from.SendLocalizedMessage(500329);
                }
            }

        }

        public DeliciouslyTastyTreat(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write((bool)m_Used);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Used = reader.ReadBool();
        }
    }
}