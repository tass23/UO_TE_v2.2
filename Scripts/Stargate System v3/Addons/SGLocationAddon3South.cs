using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
    public class SGLocationAddon3South : BaseAddon
    {
        [Constructable]
        public SGLocationAddon3South ()
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
            AddComponent(new SGBlockA3(), -1, 0, 0);
            AddComponent(new SGBlockA3(), +1, 0, 0);

            AddComponent(new SGBlockA3Step2(), 0, -1, 0);
            AddComponent(new SGBlockA3Step(), 0, +1, 0);

            AddComponent(new SGBlockA3Corner1(), -1, -1, 0);
            AddComponent(new SGBlockA3Corner3(), -1, +1, 0);
            AddComponent(new SGBlockA3Corner2(), +1, -1, 0);
            AddComponent(new SGBlockA3Corner3(), +1, +1, 0);

            // Pillar Sections
            AddComponent(new SGBlockA3(), -1, 0, 5);
            AddComponent(new SGBlockA3(), -1, 0, 10);
            AddComponent(new SGBlockA3(), -1, 0, 15);
            AddComponent(new SGBlockA3(), -1, 0, 20);

            AddComponent(new SGBlockA3(), +1, 0, 5);
            AddComponent(new SGBlockA3(), +1, 0, 10);
            AddComponent(new SGBlockA3(), +1, 0, 15);
            AddComponent(new SGBlockA3(), +1, 0, 20);

            // Top Parts
            AddComponent(new SGBlockA3(), 0, 0, 25);
            AddComponent(new SGBlockA10(), 0, 0, 30);
            AddComponent(new SGBlockA3Top(), 0, 0, 47);
            AddComponent(new SGBlockA9(), -1, 0, 25);
            AddComponent(new SGBlockA9(), +1, 0, 25);

            AddComponent(new SGLocationLantern3South(), -1, +1, 21);
            AddComponent(new SGLocationLantern3South(), +1, +1, 21);
        }

        public SGLocationAddon3South(Serial serial) : base(serial)
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

    public class SGLocationLantern3South : AddonComponent
    {
        [Constructable]
        public SGLocationLantern3South() : base(2572)
        {
            Name = "A Torch";
            Light = LightType.Circle225;
        }

        public SGLocationLantern3South(Serial serial) : base(serial)
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

    public class SGBlockA3Step : AddonComponent
    {
        [Constructable]
        public SGBlockA3Step() : base(1873)
        {
            Hue = 1314;
        }

        public SGBlockA3Step(Serial serial) : base(serial)
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

    public class SGBlockA3Step2 : AddonComponent
    {
        [Constructable]
        public SGBlockA3Step2() : base(1875)
        {
            Hue = 1314;
        }

        public SGBlockA3Step2(Serial serial) : base(serial)
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

    public class SGBlockA3Corner1 : AddonComponent
    {
        [Constructable]
        public SGBlockA3Corner1()
            : base(1877)
        {
            Hue = 1314;
        }

        public SGBlockA3Corner1(Serial serial)
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

    public class SGBlockA3Corner2 : AddonComponent
    {
        [Constructable]
        public SGBlockA3Corner2() : base(117)
        {
            Hue = 1314;
        }

        public SGBlockA3Corner2(Serial serial) : base(serial)
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

    public class SGBlockA3Corner3 : AddonComponent
    {
        [Constructable]
        public SGBlockA3Corner3() : base(116)
        {
            Hue = 1314;
        }

        public SGBlockA3Corner3(Serial serial) : base(serial)
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

    public class SGBlockA3Top : AddonComponent
    {
        [Constructable]
        public SGBlockA3Top() : base(8783)
        {
            Hue = 1314;
        }

        public SGBlockA3Top(Serial serial) : base(serial)
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