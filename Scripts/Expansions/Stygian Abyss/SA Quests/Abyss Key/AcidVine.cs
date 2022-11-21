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
	public class AcidVine : Item
	{
		[Constructable]
		public AcidVine() : base( 3313 )
		{
            Name = "vines";
			Weight = 1.0;
            Movable = false;
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (from.InRange(this.GetWorldLocation(), 1))
            {
                from.SendMessage("The vines tighten their grip, stopping you from opening the secret door!");
            }
            else if (from.InRange(this.GetWorldLocation(), 4))
            {
                from.SendMessage("You notice something odd about the vines covering the wall.");
            }
            else if (!from.InRange(this.GetWorldLocation(), 4))
            {
                from.SendMessage("I can't reach that!");
            }

            base.OnDoubleClick(from);
        }

		public AcidVine(Serial serial) : base(serial)
		{
		}

		public override bool ForceShowProperties{ get{ return ObjectPropertyList.Enabled; } }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}