using System;
using Server;

namespace Server.Items
{
    public class PhysResistPotion : BaseResistPotion
    {
        [Constructable]
        public PhysResistPotion()
            : base(PotionEffect.PhysResist, 10.0, 10, ResistanceType.Physical)
        {
            Name = "Physical Resistance Potion";
            Hue = 0x37D;
        }

        [Constructable]
        public PhysResistPotion(double duration, int effect)
            : base(PotionEffect.PhysResist, duration, effect, ResistanceType.Physical)
        {
            Name = "Physical Resistance Potion";
            Hue = 0x37D;
        }

        public PhysResistPotion(Serial serial)
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