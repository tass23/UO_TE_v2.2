/* Created by Hammerhand & Milva */

using System;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Engines.Harvest;

namespace Server.Items
{

    public class GoldPan : BaseHarvestTool, IUsesRemaining
    {
        public override HarvestSystem HarvestSystem { get { return GoldPanning.System; } }

        [Constructable]
        public GoldPan(): this(75)
        {
            Name = "a Gold Pan";
        }
        [Constructable]
        public GoldPan(int uses): base(0x9D7)
        {
            Name = "a Gold Pan";
            Weight = 2.0;
            Hue = 0;
            Movable = true;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
        }

        public GoldPan(Serial serial): base(serial)
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