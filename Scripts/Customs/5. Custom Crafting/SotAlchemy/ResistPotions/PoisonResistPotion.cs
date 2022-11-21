using System;
using Server;

namespace Server.Items
{
    public class PoisonResistPotion : BaseResistPotion
    {
        [Constructable]
        public PoisonResistPotion()
            : base(PotionEffect.PoisonResist, 10.0, 10, ResistanceType.Poison)
        {
            Name = "Poison Resistance Potion";
            Hue = 0x483;
        }

        [Constructable]
        public PoisonResistPotion(double duration, int effect)
            : base(PotionEffect.PoisonResist, duration, effect, ResistanceType.Poison)
        {
            Name = "Poison Resistance Potion";
            Hue = 0x483;
        }

        public PoisonResistPotion(Serial serial)
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
    }
}