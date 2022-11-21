// 15AUG2007 written by RavonTUS
//
//   /\\           |\\  ||
//  /__\\  |\\ ||  | \\ ||  /\\  \ //
// /    \\ | \\||  |  \\||  \//  / \\ 
// Play at An Nox, the cure for the UO addiction
// http://annox.no-ip.com  // RavonTUS@Yahoo.com

/*
 * Created by SharpDevelop.
 * User: alexanderfb
 * Date: 1/25/2005
 * Time: 10:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Server;

namespace Server.Items
{
    [Flipable(0x13AB, 0x13AC)]
    public class TaggedPillow : Item, IScissorable, IDyable
    {
        [Constructable]
        public TaggedPillow()
            : base(0x13AB)
        {
            Name = "a tagged pillow";
        }

        public bool Scissor(Mobile from, Scissors scissors)
        {
            if (Deleted || !from.CanSee(this)) return false;

            //base.ScissorHelper(from, new Cloth(), 50);

            from.SendMessage("You removed the tag from the pillow, that is illegal.  You are now a criminal.");
            from.Criminal = true;
            
            Delete();
            return true;
        }

        //public void OnChop(Mobile from)
        //{
        //    if (!from.InRange(GetWorldLocation(), 2))
        //    {
        //        from.SendLocalizedMessage(500446); // That is too far away. 
        //    }
        //    else
        //    {
        //        from.SendMessage("You removed the tag from the pillow, that is illegal.  You are now a criminal.");
        //        from.Criminal = true;
        //    }
        //}

        public TaggedPillow(Serial serial)
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

