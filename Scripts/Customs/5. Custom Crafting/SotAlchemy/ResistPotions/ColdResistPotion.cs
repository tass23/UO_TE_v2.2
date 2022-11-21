using System;
using Server;

namespace Server.Items
{
    public class ColdResistPotion : BaseResistPotion
    {
        [Constructable]
        public ColdResistPotion()
            : base(PotionEffect.ColdResist, 10.0, 10, ResistanceType.Cold)
        {
            Name = "Cold Resistance Potion";
            Hue = 0x481;
        }

        [Constructable]
        public ColdResistPotion(double duration, int effect)
            : base(PotionEffect.ColdResist, duration, effect, ResistanceType.Cold)
        {
            Name = "Cold Resistance Potion";
            Hue = 0x481;
        }

        public ColdResistPotion(Serial serial)
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