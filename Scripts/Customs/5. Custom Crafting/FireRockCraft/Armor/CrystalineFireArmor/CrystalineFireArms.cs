/* Created by Hammerhand */

using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2657, 0x2658)]
    public class CrystalineFireArms : BaseArmor
    {
        public override int Hue { get { return 1357; } }
        public override int BaseFireResistance { get { return 6; } }

        public override int InitMinHits { get { return 100; } }
        public override int InitMaxHits { get { return 150; } }

        public override int AosStrReq { get { return 80; } }
        public override int OldStrReq { get { return 40; } }

        public override int OldDexBonus { get { return -2; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Dragon; } }

        [Constructable]
        public CrystalineFireArms()
            : base(0x2657)
        {
            Name = "CrystalineFire Arms";
            Hue = 1357;
            Weight = 5.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.LowerRegCost = Utility.RandomMinMax(4, 15);
            Attributes.Luck = Utility.RandomMinMax(25, 100);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 8);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 12;

            EnergyBonus = Utility.RandomMinMax(5, 9);
            FireBonus = Utility.RandomMinMax(10, 25);
            PoisonBonus = Utility.RandomMinMax(3, 8);
            PhysicalBonus = Utility.RandomMinMax(2, 7);
            ColdBonus = Utility.RandomMinMax(2, 6);

            LootType = LootType.Regular;
        }


        public CrystalineFireArms(Serial serial)
            : base(serial)
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

            if (Weight == 1.0)
                Weight = 5.0;
        }
    }
}