using System;

namespace Server.Items
{
    public class CDBanner : Item
    {

        [Constructable]
        public CDBanner(): base(0x4037)
        {
            Weight = 10;
			Hue = 1151;
			Name = "Defender Banner";
        }

        public CDBanner(Serial serial)
            : base(serial)
        {
        }

        public override bool ForceShowProperties { get { return ObjectPropertyList.Enabled; } }

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
    }
}