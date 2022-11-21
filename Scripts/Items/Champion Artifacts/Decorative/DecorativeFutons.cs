using System;
using Server;

namespace Server.Items
{
    [Flipable]
    public class DecorativePinkFuton : Item
    {
        [Constructable]
        public DecorativePinkFuton()
            : base(0x295C)
        {
            Weight = 5.0;
        }

        public DecorativePinkFuton(Serial serial)
            : base(serial)
        {
        }

        public void Flip()
        {
            switch (ItemID)
            {
                case 0x295C: ItemID = 0x295D; break;

                case 0x295D: ItemID = 0x295C; break;
            }
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

    [Flipable]
    public class DecorativeGoldFuton : Item
    {
        [Constructable]
        public DecorativeGoldFuton()
            : base(0x295E)
        {
            Weight = 5.0;
        }

        public DecorativeGoldFuton(Serial serial)
            : base(serial)
        {
        }

        public void Flip()
        {
            switch (ItemID)
            {
                case 0x295E: ItemID = 0x295F; break;

                case 0x295F: ItemID = 0x295E; break;
            }
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