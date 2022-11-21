
using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.ContextMenus;
using Server.Engines.Craft;
using Server.Engines.Plants;

    namespace Server.Items
    {
        public class ColorFixative : Item
        {
            public override int LabelNumber { get { return 1112135; } } // color fixative

            [Constructable]
            public ColorFixative(): base(0x182D)
            {
                Weight = 1.0;
                Hue = 473;  // ...make this the proper shade of green
            }

            public ColorFixative(Serial serial)
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
    }
