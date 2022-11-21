using System;
using Server;

namespace Server.Items
{
    //[Flipable(0x13A4, 0x13A5)]
    public class LargePillow : Item, IDyable
    {
        [Constructable]
        public LargePillow()
            : base(0x163A)
        {
            Name = "a large pillow";
			Hue = 618;
        }

        public LargePillow(Serial serial)
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

