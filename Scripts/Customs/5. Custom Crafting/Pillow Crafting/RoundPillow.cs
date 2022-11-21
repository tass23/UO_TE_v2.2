using System;
using Server;

namespace Server.Items
{
    [Flipable(0x13A4, 0x13A5)]
    public class RoundPillow : Item, IDyable
    {
        [Constructable]
        public RoundPillow()
            : base(0x13A4)
        {
            Name = "a round pillow";
			Hue = 17;
        }

        public RoundPillow(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public bool Dye(Mobile from, DyeTub sender)
        {
            if (Deleted)
                return false;

            Hue = sender.DyedHue;

            return true;
        }
    }
}

