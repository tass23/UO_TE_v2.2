using System;

namespace Server.Items
{
    public class SecretStoneWallNS : BaseSliding
    {
        [Constructable]
        public SecretStoneWallNS()
            : base(0x3C9, 0x3CA)
        {
            Name = "secret door";
            Hue = 744;
        }

        public SecretStoneWallNS(Serial serial)
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