using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
    public class SGLocationAddon1East : BaseAddon
    {
        [Constructable]
        public SGLocationAddon1East ()
        {
            // Floor Tiles
            AddComponent(new AddonComponent(1316), +2, 0, 0);
            AddComponent(new AddonComponent(1316), +1, 0, 0);
            AddComponent(new AddonComponent(1316), 0, 0, 0);
            AddComponent(new AddonComponent(1316), -1, 0, 0);
            AddComponent(new AddonComponent(1316), -2, 0, 0);

            AddComponent(new AddonComponent(1316), +2, -1, 0);
            AddComponent(new AddonComponent(1316), +1, -1, 0);
            AddComponent(new AddonComponent(1316), 0, -1, 0);
            AddComponent(new AddonComponent(1316), -1, -1, 0);
            AddComponent(new AddonComponent(1316), -2, -1, 0);

            AddComponent(new AddonComponent(1316), +2, -2, 0);
            AddComponent(new AddonComponent(1316), +1, -2, 0);
            AddComponent(new AddonComponent(1316), 0, -2, 0);
            AddComponent(new AddonComponent(1316), -1, -2, 0);
            AddComponent(new AddonComponent(1316), -2, -2, 0);

            AddComponent(new AddonComponent(1316), +2, +1, 0);
            AddComponent(new AddonComponent(1316), +1, +1, 0);
            AddComponent(new AddonComponent(1316), 0, +1, 0);
            AddComponent(new AddonComponent(1316), -1, +1, 0);
            AddComponent(new AddonComponent(1316), -2, +1, 0);

            AddComponent(new AddonComponent(1316), +2, +2, 0);
            AddComponent(new AddonComponent(1316), +1, +2, 0);
            AddComponent(new AddonComponent(1316), 0, +2, 0);
            AddComponent(new AddonComponent(1316), -1, +2, 0);
            AddComponent(new AddonComponent(1316), -2, +2, 0);

            // 1st Layer
            AddComponent(new AddonComponent(1822), 0, 0, 0);
            AddComponent(new AddonComponent(1822), 0, -1, 0);
            AddComponent(new AddonComponent(1822), 0, +1, 0);

            AddComponent(new AddonComponent(1179), 0, 0, 5);

            AddComponent(new AddonComponent(1846), +1, 0, 0);
            AddComponent(new AddonComponent(1865), -1, 0, 0);

            AddComponent(new AddonComponent(1866), -1, -1, 0);
            AddComponent(new AddonComponent(1869), -1, +1, 0);
            AddComponent(new AddonComponent(1868), +1, -1, 0);
            AddComponent(new AddonComponent(1867), +1, +1, 0);

            // Pillar Sections
            AddComponent(new AddonComponent(1822), 0, -1, 5);
            AddComponent(new AddonComponent(1822), 0, -1, 10);
            AddComponent(new AddonComponent(1822), 0, -1, 15);
            AddComponent(new AddonComponent(1822), 0, -1, 20);

            AddComponent(new AddonComponent(1822), 0, +1, 5);
            AddComponent(new AddonComponent(1822), 0, +1, 10);
            AddComponent(new AddonComponent(1822), 0, +1, 15);
            AddComponent(new AddonComponent(1822), 0, +1, 20);

            // Top Parts
            AddComponent(new AddonComponent(1822), 0, 0, 25);
            AddComponent(new AddonComponent(1847), 0, -1, 25);
            AddComponent(new AddonComponent(1823), 0, +1, 25);

            AddComponent(new AddonComponent(14324), 0, 0, 26);

            // Animated Sections
            AddComponent(new AddonComponent(6571), 0, 0, 37); // Flame on top

            AddComponent(new SGLocationLantern1East(), +1, +1, 20);
            AddComponent(new SGLocationLantern1East(), +1, -1, 20);
        }

        public SGLocationAddon1East(Serial serial) : base(serial)
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

    public class SGLocationLantern1East : AddonComponent
    {
        [Constructable]
        public SGLocationLantern1East() : base(2567)
        {
            Name = "A Torch";
            Light = LightType.Circle225;
        }

        public SGLocationLantern1East(Serial serial) : base(serial)
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