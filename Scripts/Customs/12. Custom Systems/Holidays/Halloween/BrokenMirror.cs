using System;

namespace Server.Items
{
    public class BrokenMirror : Item
    {
        [Constructable]
        public BrokenMirror()
            : base(0x4044)
        {
            Weight = 10;
			Name = "Broken Mirror";
        }

        public BrokenMirror(Serial serial)
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
        }
    }
}