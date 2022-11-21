using System;
using Server;
using Server.Network;
using Server.Items;
using System.Collections;

namespace Server.Items
{
    public class SGLocationAddon2South : BaseAddon
    {
        [Constructable]
        public SGLocationAddon2South()
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
            AddComponent(new SGBlock2(), -1, 0, 0);
            AddComponent(new SGBlock2(), +1, 0, 0);

            AddComponent(new SGBlock3(), 0, 0, 5);

            AddComponent(new SGBlock10(), 0, -1, 0);
            AddComponent(new SGBlock11(), 0, +1, 0);

            AddComponent(new SGBlock6(), -1, -1, 0);
            AddComponent(new SGBlock7(), -1, +1, 0);
            AddComponent(new SGBlock8(), +1, -1, 0);
            AddComponent(new SGBlock9(), +1, +1, 0);

            // Pillar Sections
            AddComponent(new SGBlock2(), -1, 0, 5);
            AddComponent(new SGBlock2(), -1, 0, 10);
            AddComponent(new SGBlock2(), -1, 0, 15);
            AddComponent(new SGBlock2(), -1, 0, 20);

            AddComponent(new SGBlock2(), +1, 0, 5);
            AddComponent(new SGBlock2(), +1, 0, 10);
            AddComponent(new SGBlock2(), +1, 0, 15);
            AddComponent(new SGBlock2(), +1, 0, 20);

            // Top Parts
            AddComponent(new SGBlock2(), 0, 0, 25);
            AddComponent(new SGBlock5(), -1, 0, 25);
            AddComponent(new SGBlock4(), +1, 0, 25);

            AddComponent(new AddonComponent(14324), 0, 0, 26);

            // Animated Sections
            AddComponent(new AddonComponent(6571), 0, 0, 37);

            AddComponent(new SGLocationLantern2South(), -1, +1, 20);
            AddComponent(new SGLocationLantern2South(), +1, +1, 20);
        }

        public SGLocationAddon2South(Serial serial) : base(serial)
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

    public class SGLocationLantern2South : AddonComponent
    {
        [Constructable]
        public SGLocationLantern2South() : base(2572)
        {
            Name = "A Torch";
            Light = LightType.Circle225;
        }

        public SGLocationLantern2South(Serial serial) : base(serial)
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