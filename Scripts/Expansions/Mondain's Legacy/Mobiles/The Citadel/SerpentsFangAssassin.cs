using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
    [CorpseName("a black order mage corpse")]
    public class SerpentsFangAssassin : BaseCreature
    {
        public override bool AlwaysMurderer { get { return true; } }
        public override bool ShowFameTitle { get { return false; } }

        [Constructable]
        public SerpentsFangAssassin()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Black Order Assassin";
            Title = "of the Serpent's Fang Sect";
            Female = Utility.RandomBool();
            Race = Race.Human;
            Hue = Race.RandomSkinHue();
            HairItemID = Race.RandomHair(Female);
            HairHue = Race.RandomHairHue();
            Race.RandomFacialHair(this);

            AddItem(new ThighBoots(0x51D));
            AddItem(new FancyShirt(0x51D));
            AddItem(new StuddedMempo());
            AddItem(new JinBaori(0x2A));

            Item item;

            item = new StuddedGloves();
            item.Hue = 0x2A;
            AddItem(item);

            item = new LeatherNinjaPants();
            item.Hue = 0x51D;
            AddItem(item);

            item = new LightPlateJingasa();
            item.Hue = 0x51D;
            AddItem(item);

            item = new Sai();
            item.Hue = 0x51D;
            AddItem(item);

            SetStr(325, 375);
            SetDex(175, 275);
            SetInt(85, 105);

            SetHits(350, 375);

            SetDamage(14, 22);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 60);
            SetResistance(ResistanceType.Fire, 45, 65);
            SetResistance(ResistanceType.Cold, 25, 45);
            SetResistance(ResistanceType.Poison, 40, 60);
            SetResistance(ResistanceType.Energy, 40, 65);

            SetSkill(SkillName.MagicResist, 80.0, 100.0);
            SetSkill(SkillName.Tactics, 115.0, 130.0);
            SetSkill(SkillName.Wrestling, 95.0, 120.0);
            SetSkill(SkillName.Anatomy, 105.0, 120.0);
            SetSkill(SkillName.Fencing, 78.0, 100.0);
            SetSkill(SkillName.Swords, 90.1, 105.0);
            SetSkill(SkillName.Ninjitsu, 90.0, 120.0);
            SetSkill(SkillName.Hiding, 100.0, 120.0);
            SetSkill(SkillName.Stealth, 100.0, 120.0);

            Fame = 5000;
            Karma = -5000;

            VirtualArmor = 58;
        }

        public SerpentsFangAssassin(Serial serial)
            : base(serial)
        {
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.AosFilthyRich, 4);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.RandomDouble() < 0.3)
                c.DropItem(new SerpentFangSectBadge());
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