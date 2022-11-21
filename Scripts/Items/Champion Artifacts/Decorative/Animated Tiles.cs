using System;

namespace Server.Items
{
    public class LavaTile : Item
    {
        [Constructable]
        public LavaTile()
            : base(0x343B)
        {
            ItemID = Utility.RandomList(0x343B, 0x3447, 0x344E, 0x3462, 0x3468);
        }

        public LavaTile(Serial serial)
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

    public class SwampTile : Item
    {
        [Constructable]
        public SwampTile()
            : base(0x3236)
        {
            ItemID = Utility.RandomList(0x3236, 0x3241, 0x320D, 0x3226, 0x3213, 0x3220);
        }

        public SwampTile(Serial serial)
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

    public class Pier : Item
    {
        [Constructable]
        public Pier()
            : base(0x3AE)
        {
        }

        public Pier(Serial serial)
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

    public class SmallRockWater : Item
    {
        [Constructable]
        public SmallRockWater()
            : base(0x3486)
        {
        }

        public SmallRockWater(Serial serial)
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

    public class SmallRocksWater : Item
    {
        [Constructable]
        public SmallRocksWater()
            : base(0x348B)
        {
        }

        public SmallRocksWater(Serial serial)
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