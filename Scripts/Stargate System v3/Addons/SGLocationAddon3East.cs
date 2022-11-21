using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
    public class SGLocationAddon3East : BaseAddon
    {
        [Constructable]
        public SGLocationAddon3East ()
        {
            // Floor Tiles
            AddComponent(new SGFloorTile3(), +2, 0, 0);
            AddComponent(new SGFloorTile3(), +1, 0, 0);
            AddComponent(new SGFloorTile3(), 0, 0, 0);
            AddComponent(new SGFloorTile3(), -1, 0, 0);
            AddComponent(new SGFloorTile3(), -2, 0, 0);

            AddComponent(new SGFloorTile3(), +2, -1, 0);
            AddComponent(new SGFloorTile3(), +1, -1, 0);
            AddComponent(new SGFloorTile3(), 0, -1, 0);
            AddComponent(new SGFloorTile3(), -1, -1, 0);
            AddComponent(new SGFloorTile3(), -2, -1, 0);

            AddComponent(new SGFloorTile3(), +2, -2, 0);
            AddComponent(new SGFloorTile3(), +1, -2, 0);
            AddComponent(new SGFloorTile3(), 0, -2, 0);
            AddComponent(new SGFloorTile3(), -1, -2, 0);
            AddComponent(new SGFloorTile3(), -2, -2, 0);

            AddComponent(new SGFloorTile3(), +2, +1, 0);
            AddComponent(new SGFloorTile3(), +1, +1, 0);
            AddComponent(new SGFloorTile3(), 0, +1, 0);
            AddComponent(new SGFloorTile3(), -1, +1, 0);
            AddComponent(new SGFloorTile3(), -2, +1, 0);

            AddComponent(new SGFloorTile3(), +2, +2, 0);
            AddComponent(new SGFloorTile3(), +1, +2, 0);
            AddComponent(new SGFloorTile3(), 0, +2, 0);
            AddComponent(new SGFloorTile3(), -1, +2, 0);
            AddComponent(new SGFloorTile3(), -2, +2, 0);

            // 1st Layer
            AddComponent(new SGBlockA3(), 0, 0, 0);
            AddComponent(new SGBlockA3(), 0, -1, 0);
            AddComponent(new SGBlockA3(), 0, +1, 0);

            AddComponent(new SGBlockA4(), +1, 0, 0);
            AddComponent(new SGBlockA5(), -1, 0, 0);

            AddComponent(new SGBlockA6(), -1, -1, 0);
            AddComponent(new SGBlockA8(), -1, +1, 0);
            AddComponent(new SGBlockA7(), +1, -1, 0);
            AddComponent(new SGBlockA7(), +1, +1, 0);

            // Pillar Sections
            AddComponent(new SGBlockA3(), 0, -1, 5);
            AddComponent(new SGBlockA3(), 0, -1, 10);
            AddComponent(new SGBlockA3(), 0, -1, 15);
            AddComponent(new SGBlockA3(), 0, -1, 20);

            AddComponent(new SGBlockA3(), 0, +1, 5);
            AddComponent(new SGBlockA3(), 0, +1, 10);
            AddComponent(new SGBlockA3(), 0, +1, 15);
            AddComponent(new SGBlockA3(), 0, +1, 20);

            // Top Parts
            AddComponent(new SGBlockA3(), 0, 0, 25);
            AddComponent(new SGBlockA10(), 0, 0, 30);
            AddComponent(new SGBlockA11(), 0, 0, 47);
            AddComponent(new SGBlockA9(), 0, -1, 25);
            AddComponent(new SGBlockA9(), 0, +1, 25);

            AddComponent(new SGLocationLantern3East(), +1, +1, 21);
            AddComponent(new SGLocationLantern3East(), +1, -1, 21);
        }

        public SGLocationAddon3East(Serial serial) : base(serial)
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

    public class SGLocationLantern3East : AddonComponent
    {
        [Constructable]
        public SGLocationLantern3East() : base(2567)
        {
            Name = "A Torch";
            Light = LightType.Circle225;
        }

        public SGLocationLantern3East(Serial serial) : base(serial)
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

    public class SGFloorTile3 : AddonComponent
    {
        [Constructable]
        public SGFloorTile3() : base(1314)
        {
            Hue = 1314;
        }

        public SGFloorTile3(Serial serial) : base(serial)
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

    public class SGBlockA3 : AddonComponent
    {
        [Constructable]
        public SGBlockA3() : base(1872)
        {
            Hue = 1314;
        }

        public SGBlockA3(Serial serial) : base(serial)
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

    public class SGBlockA4 : AddonComponent
    {
        [Constructable]
        public SGBlockA4() : base(1874)
        {
            Hue = 1314;
        }

        public SGBlockA4(Serial serial) : base(serial)
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

    public class SGBlockA5 : AddonComponent
    {
        [Constructable]
        public SGBlockA5() : base(1876)
        {
            Hue = 1314;
        }

        public SGBlockA5(Serial serial) : base(serial)
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

    public class SGBlockA6 : AddonComponent
    {
        [Constructable]
        public SGBlockA6() : base(1877)
        {
            Hue = 1314;
        }

        public SGBlockA6(Serial serial) : base(serial)
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

    public class SGBlockA7 : AddonComponent
    {
        [Constructable]
        public SGBlockA7() : base(114)
        {
            Hue = 1314;
        }

        public SGBlockA7(Serial serial) : base(serial)
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

    public class SGBlockA8 : AddonComponent
    {
        [Constructable]
        public SGBlockA8() : base(118)
        {
            Hue = 1314;
        }

        public SGBlockA8(Serial serial) : base(serial)
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

    public class SGBlockA9 : AddonComponent
    {
        [Constructable]
        public SGBlockA9() : base(8705)
        {
            Hue = 1314;
        }

        public SGBlockA9(Serial serial) : base(serial)
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

    public class SGBlockA10 : AddonComponent
    {
        [Constructable]
        public SGBlockA10() : base(119)
        {
            Hue = 1314;
        }

        public SGBlockA10(Serial serial) : base(serial)
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

    public class SGBlockA11 : AddonComponent
    {
        [Constructable]
        public SGBlockA11() : base(8782)
        {
            Hue = 1314;
        }

        public SGBlockA11(Serial serial) : base(serial)
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