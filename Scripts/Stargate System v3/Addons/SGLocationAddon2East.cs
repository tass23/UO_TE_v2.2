using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
    public class SGLocationAddon2East : BaseAddon
    {
        [Constructable]
        public SGLocationAddon2East ()
        {
            // Floor Tiles
            AddComponent(new SGFloorTile2(), +2, 0, 0);
            AddComponent(new SGFloorTile2(), +1, 0, 0);
            AddComponent(new SGFloorTile2(), 0, 0, 0);
            AddComponent(new SGFloorTile2(), -1, 0, 0);
            AddComponent(new SGFloorTile2(), -2, 0, 0);

            AddComponent(new SGFloorTile2(), +2, -1, 0);
            AddComponent(new SGFloorTile2(), +1, -1, 0);
            AddComponent(new SGFloorTile2(), 0, -1, 0);
            AddComponent(new SGFloorTile2(), -1, -1, 0);
            AddComponent(new SGFloorTile2(), -2, -1, 0);

            AddComponent(new SGFloorTile2(), +2, -2, 0);
            AddComponent(new SGFloorTile2(), +1, -2, 0);
            AddComponent(new SGFloorTile2(), 0, -2, 0);
            AddComponent(new SGFloorTile2(), -1, -2, 0);
            AddComponent(new SGFloorTile2(), -2, -2, 0);

            AddComponent(new SGFloorTile2(), +2, +1, 0);
            AddComponent(new SGFloorTile2(), +1, +1, 0);
            AddComponent(new SGFloorTile2(), 0, +1, 0);
            AddComponent(new SGFloorTile2(), -1, +1, 0);
            AddComponent(new SGFloorTile2(), -2, +1, 0);

            AddComponent(new SGFloorTile2(), +2, +2, 0);
            AddComponent(new SGFloorTile2(), +1, +2, 0);
            AddComponent(new SGFloorTile2(), 0, +2, 0);
            AddComponent(new SGFloorTile2(), -1, +2, 0);
            AddComponent(new SGFloorTile2(), -2, +2, 0);

            // 1st Layer
            AddComponent(new SGBlock2(), 0, 0, 0);
            AddComponent(new SGBlock2(), 0, -1, 0);
            AddComponent(new SGBlock2(), 0, +1, 0);

            AddComponent(new SGBlock3(), 0, 0, 5);

            AddComponent(new SGBlock4(), +1, 0, 0);
            AddComponent(new SGBlock5(), -1, 0, 0);

            AddComponent(new SGBlock6(), -1, -1, 0);
            AddComponent(new SGBlock7(), -1, +1, 0);
            AddComponent(new SGBlock8(), +1, -1, 0);
            AddComponent(new SGBlock9(), +1, +1, 0);

            // Pillar Sections
            AddComponent(new SGBlock2(), 0, -1, 5);
            AddComponent(new SGBlock2(), 0, -1, 10);
            AddComponent(new SGBlock2(), 0, -1, 15);
            AddComponent(new SGBlock2(), 0, -1, 20);

            AddComponent(new SGBlock2(), 0, +1, 5);
            AddComponent(new SGBlock2(), 0, +1, 10);
            AddComponent(new SGBlock2(), 0, +1, 15);
            AddComponent(new SGBlock2(), 0, +1, 20);

            // Top Parts
            AddComponent(new SGBlock2(), 0, 0, 25);
            AddComponent(new SGBlock10(), 0, -1, 25);
            AddComponent(new SGBlock11(), 0, +1, 25);

            AddComponent(new AddonComponent(14324), 0, 0, 26);

            // Animated Sections
            AddComponent(new AddonComponent(6571), 0, 0, 37); // Flame on top

            AddComponent(new SGLocationLantern2East(), +1, +1, 20);
            AddComponent(new SGLocationLantern2East(), +1, -1, 20);
        }

        public SGLocationAddon2East(Serial serial) : base(serial)
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

    public class SGLocationLantern2East : AddonComponent
    {
        [Constructable]
        public SGLocationLantern2East() : base(2567)
        {
            Name = "A Torch";
            Light = LightType.Circle225;
        }

        public SGLocationLantern2East(Serial serial) : base(serial)
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

    public class SGFloorTile2 : AddonComponent
    {
        [Constructable]
        public SGFloorTile2() : base(1316)
        {
            Hue = 1314;
        }

        public SGFloorTile2(Serial serial) : base(serial)
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

    public class SGBlock2 : AddonComponent
    {
        [Constructable]
        public SGBlock2() : base(1822)
        {
            Hue = 1314;
        }

        public SGBlock2(Serial serial) : base(serial)
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

    public class SGBlock3 : AddonComponent
    {
        [Constructable]
        public SGBlock3() : base(1179)
        {
            Hue = 1314;
        }

        public SGBlock3(Serial serial) : base(serial)
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

    public class SGBlock4 : AddonComponent
    {
        [Constructable]
        public SGBlock4() : base(1846)
        {
            Hue = 1314;
        }

        public SGBlock4(Serial serial) : base(serial)
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

    public class SGBlock5 : AddonComponent
    {
        [Constructable]
        public SGBlock5() : base(1865)
        {
            Hue = 1314;
        }

        public SGBlock5(Serial serial) : base(serial)
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

    public class SGBlock6 : AddonComponent
    {
        [Constructable]
        public SGBlock6() : base(1866)
        {
            Hue = 1314;
        }

        public SGBlock6(Serial serial) : base(serial)
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

    public class SGBlock7 : AddonComponent
    {
        [Constructable]
        public SGBlock7() : base(1869)
        {
            Hue = 1314;
        }

        public SGBlock7(Serial serial) : base(serial)
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

    public class SGBlock8 : AddonComponent
    {
        [Constructable]
        public SGBlock8() : base(1868)
        {
            Hue = 1314;
        }

        public SGBlock8(Serial serial) : base(serial)
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

    public class SGBlock9 : AddonComponent
    {
        [Constructable]
        public SGBlock9() : base(1867)
        {
            Hue = 1314;
        }

        public SGBlock9(Serial serial) : base(serial)
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

    public class SGBlock10 : AddonComponent
    {
        [Constructable]
        public SGBlock10() : base(1847)
        {
            Hue = 1314;
        }

        public SGBlock10(Serial serial) : base(serial)
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

    public class SGBlock11 : AddonComponent
    {
        [Constructable]
        public SGBlock11() : base(1823)
        {
            Hue = 1314;
        }

        public SGBlock11(Serial serial) : base(serial)
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