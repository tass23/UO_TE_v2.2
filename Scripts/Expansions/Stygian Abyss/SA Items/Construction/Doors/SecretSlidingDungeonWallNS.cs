using System;

namespace Server.Items
{
    public class SecretDungeonWallNS : BaseSliding
    {
        [Constructable]
        public SecretDungeonWallNS()
            : base(0x0242, 0x0244)
        {
            Name = "secret door";
        }

        public SecretDungeonWallNS(Serial serial)
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