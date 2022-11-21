using System;
using Server;

namespace Server.Items
{
    public class FireResistPotion : BaseResistPotion
    {
        [Constructable]
        public FireResistPotion()
            : base(PotionEffect.FireResist, 10.0, 10, ResistanceType.Fire)
        {
            Name = "Fire Resistance Potion";
            Hue = 0x489;
        }

        [Constructable]
        public FireResistPotion(double duration, int effect)
            : base(PotionEffect.FireResist, duration, effect, ResistanceType.Fire)
        {
            Name = "Fire Resistance Potion";
            Hue = 0x489;
        }

        public FireResistPotion(Serial serial)
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