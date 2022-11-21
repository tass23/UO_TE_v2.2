using System;

namespace Server.Items
{
    public class MedusaDarkScales : Item
    {
        public override int LabelNumber { get { return 1112626; } } // Medusa Scales

        [Constructable]
        public MedusaDarkScales()
            : this(1)
        {
        }

        [Constructable]
        public MedusaDarkScales(int amount)
            : base(9908)
        {
            Hue = 1266;
            Stackable = true;
            Amount = amount;
        }

        public MedusaDarkScales(Serial serial)
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