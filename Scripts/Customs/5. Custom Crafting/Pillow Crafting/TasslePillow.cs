using System;
using Server;

namespace Server.Items
{
    //[Flipable(0x13A4, 0x13A5)]
    public class TasslePillow : Item, IDyable
    {
        [Constructable]
        public TasslePillow()
            : base(0x13AC)
        {
            Name = "a tassle pillow";
			Hue = 82;
        }

        public TasslePillow(Serial serial)
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

