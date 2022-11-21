using System;
using Server;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Mobiles;

namespace Server.Mobiles
{
    [CorpseName("Garamon's Corpse")]
    public class Garamon : Mobile
    {
        public virtual bool IsInvulnerable { get { return true; } }

        [Constructable]
        public Garamon()
        {
            Name = "Garamon";
            HairItemID = 0x2044;
            HairHue = 1153;
            FacialHairItemID = 0x204B;
            FacialHairHue = 1153;
            Body = 0x190;
            CantWalk = true;

            AddItem(new Server.Items.Sandals(927));
            AddItem(new Robe(927));

            Blessed = true;


        }

        public Garamon(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.Location, 8))
                return true;

            return base.HandlesOnSpeech(from);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            if (!e.Handled && e.Mobile.InRange(this.Location, 2))
            {
                PlayerMobile pm = e.Mobile as PlayerMobile;

                if (pm.AbyssEntry)
                {
                    pm.SendMessage("You have completed a Sacred quest already!");
                }
                else
                {
                    string keyword = e.Speech;

                    switch (keyword)
                    {
                        case "Hello":
                            {
                                Say(String.Format("Greetings Adventurer! If you are seeking to enter the Abyss, I may be of assitance to you."));
                                break;
                            }
                        case "hello":
                            {
                                Say(String.Format("Greetings Adventurer! If you are seeking to enter the Abyss, I may be of assitance to you."));
                                break;
                            }
                        case "Key":
                            {
                                Say(String.Format("It's three parts that you must find, and reunite as one!"));
                                break;
                            }
                        case "key":
                            {
                                Say(String.Format("It's three parts that you must find, and reunite as one!"));
                                break;
                            }
                        case "Abyss":
                            {
                                Say(String.Format("It's entrance is protected by stone guardians who will only grant passage to the carrier of a Tripartite Key!"));
                                break;
                            }
                        case "abyss":
                            {
                                Say(String.Format("It's entrance is protected by stone guardians who will only grant passage to the carrier of a Tripartite Key!"));
                                break;
                            }
                    }
                }
                base.OnSpeech(e);
            }
        }
    }

    public class Greeter1 : BaseGuildmaster
    {
        public override NpcGuild NpcGuild { get { return NpcGuild.TailorsGuild; } }

        public override bool ClickTitle { get { return false; } }

        private static bool m_Talked; // flag to prevent spam 

        string[] kfcsay = new string[] // things to say while greating 
		      { 
		         "Greetings Adventurer! If you are seeking to enter the Abyss, I may be of assitance to you.",   
      };

        [Constructable]
        public Greeter1(): base("Greeter1")
        {
        }

        public override void InitBody()
        {
            InitStats(100, 100, 100);
            Name = "Garamon";
            Body = 0x190;

        }
        public override void InitOutfit()
        {
            AddItem(new Robe(1));
            AddItem(new Sandals(1));

            HairItemID = 0x203B;
            HairHue = 0;

        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m_Talked == false)
            {
                if (m.InRange(this, 2))
                {
                    m_Talked = true;
                    SayRandom(kfcsay, this);
                    this.Move(GetDirectionTo(m.Location));

                    SpamTimer t = new SpamTimer();
                    t.Start();
                }
            }
        }

        private class SpamTimer : Timer
        {
            public SpamTimer(): base(TimeSpan.FromSeconds(90))
            {
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                m_Talked = false;
            }
        }

        private static void SayRandom(string[] say, Mobile m)
        {
            m.Say(say[Utility.Random(say.Length)]);
        }

        public override bool HandlesOnSpeech(Mobile from)
        {
            if (from.InRange(this.Location, 2))
                return true;

            return base.HandlesOnSpeech(from);
        }

        public override void OnSpeech(SpeechEventArgs e)
        {
            Mobile from = e.Mobile;

            if (!e.Handled && from is PlayerMobile && from.InRange(this.Location, 2) && e.Speech.Contains("abyss"))
            {
                PlayerMobile pm = (PlayerMobile)from;

                if (e.Speech.Contains("abyss"))
                    SayTo(from, "It's entrance is protected by stone guardians, who will only grant passage to the carrier of a Tripartite Key!");

                else if (e.Speech.Contains("key"))
                    SayTo(from, "It's three parts you must find and re-unite as one!");


                e.Handled = true;
            }
            base.OnSpeech(e);
        }

        public Greeter1(Serial serial): base(serial)
        {
        }
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            list.Add(new Greeter1Entry(from, this));
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public class Greeter1Entry : ContextMenuEntry
        {
            private Mobile m_Mobile;
            private Mobile m_Giver;

            public Greeter1Entry(Mobile from, Mobile giver): base(6146, 3)
            {
                m_Mobile = from;
                m_Giver = giver;
            }
        }
    }
}
