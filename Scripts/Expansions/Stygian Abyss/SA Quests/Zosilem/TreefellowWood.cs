using System;
using Server;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
    public class TreefellowWood : Item
    {
      
        [Constructable]
        public TreefellowWood()
            : base(0x1BDD)
        {
            Name = "Treefellow Wood";  

            Hue = 2425;
            Movable = true;
        }

             
        public TreefellowWood(Serial serial)
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


