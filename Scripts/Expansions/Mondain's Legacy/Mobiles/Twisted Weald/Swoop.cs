using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a swoop corpse")]
    public class Swoop : BaseCreature
    {
        [Constructable]
        public Swoop()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a Swoop";
            Body = 0x5;
            BaseSoundID = 0x2EE;
            Hue = 0x1A8;

            SetStr(115, 135);
            SetDex(421, 470);
            SetInt(76, 88);

            SetHits(692, 775);
            SetStam(421, 470);
            SetMana(12, 14);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 76, 90);
            SetResistance(ResistanceType.Fire, 61, 69);
            SetResistance(ResistanceType.Cold, 71, 85);
            SetResistance(ResistanceType.Poison, 54, 60);
            SetResistance(ResistanceType.Energy, 55, 60);

            SetSkill(SkillName.Anatomy, 0, 9.1);
            SetSkill(SkillName.MagicResist, 94.5, 103.9);
            SetSkill(SkillName.Tactics, 118.4, 140);
            SetSkill(SkillName.Wrestling, 121.9, 140.4);

            PackReg(4);

            PackArcaneScroll(0, 1);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 54;
        }

        public static double SpeedBuff = 1.20;

        public Swoop(Serial serial)
            : base(serial)
        {
        }

        public override bool GivesMinorArtifact { get { return true; } }
        public override int Feathers { get { return 72; } }
        public override int Meat { get { return 1; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 3);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.025)
            {
                switch (Utility.Random(18))
                {
                    case 0: c.DropItem(new AssassinChest()); break;
                    case 1: c.DropItem(new AssassinArms()); break;
                    case 2: c.DropItem(new DeathChest()); break;
                    case 3: c.DropItem(new MyrmidonArms()); break;
                    case 4: c.DropItem(new MyrmidonLegs()); break;
                    case 5: c.DropItem(new MyrmidonGorget()); break;
                    case 6: c.DropItem(new LeafweaveGloves()); break;
                    case 7: c.DropItem(new LeafweaveLegs()); break;
                    case 8: c.DropItem(new LeafweavePauldrons()); break;
                    case 9: c.DropItem(new PaladinGloves()); break;
                    case 10: c.DropItem(new PaladinGorget()); break;
                    case 11: c.DropItem(new PaladinArms()); break;
                    case 12: c.DropItem(new HunterArms()); break;
                    case 13: c.DropItem(new HunterGloves()); break;
                    case 14: c.DropItem(new HunterLegs()); break;
                    case 15: c.DropItem(new HunterChest()); break;
                    case 16: c.DropItem(new GreymistArms()); break;
                    case 17: c.DropItem(new GreymistGloves()); break;
                }
            }

            if (Utility.RandomDouble() < 0.1)
                c.DropItem(new ParrotItem());
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.1 > Utility.RandomDouble())
            {
                ExpireTimer timer = (ExpireTimer)m_Table[defender];

                if (timer != null)
                {
                    timer.DoExpire();
                    defender.SendLocalizedMessage(1070837); // The creature lands another blow in your weakened state.
                }
                else
                    defender.SendLocalizedMessage(1070836); // The blow from the creature's claws has made you more susceptible to physical attacks.

                int effect = -(defender.PhysicalResistance * 15 / 100);

                ResistanceMod mod = new ResistanceMod(ResistanceType.Physical, effect);

                int duration = Utility.RandomMinMax(5, 9);

                defender.FixedEffect(0x37B9, 10, duration);
                defender.AddResistanceMod(mod);

                timer = new ExpireTimer(defender, mod, TimeSpan.FromSeconds(5.0));
                timer.Start();
                m_Table[defender] = timer;
            }
        }

        private static Hashtable m_Table = new Hashtable();

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private ResistanceMod m_Mod;

            public ExpireTimer(Mobile m, ResistanceMod mod, TimeSpan delay)
                : base(delay)
            {
                m_Mobile = m;
                m_Mod = mod;
                Priority = TimerPriority.TwoFiftyMS;
            }

            public void DoExpire()
            {
                m_Mobile.RemoveResistanceMod(m_Mod);
                Stop();
                m_Table.Remove(m_Mobile);
            }

            protected override void OnTick()
            {
                m_Mobile.SendLocalizedMessage(1070838); // Your resistance to physical attacks has returned.
                DoExpire();
            }
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
    }
}
