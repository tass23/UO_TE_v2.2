using System;
using Server;

namespace Server.Items
{
    [Flipable(0x11EA, 0x11EB)]
    public class ShagPillow : Item, IDyable
    {
        [Constructable]
        public ShagPillow()
            : base(0x11EA)
        {
            Name = "a shag pillow";
			Hue = 72;
        }

        public ShagPillow(Serial serial)
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

