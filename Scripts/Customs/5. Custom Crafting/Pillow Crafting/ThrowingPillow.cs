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
using Server.Targeting;

namespace Server.Items
{
    [Flipable(0x13AB, 0x13AC)]
    public class ThrowingPillow : Item, IDyable
    {
        [Constructable]
        public ThrowingPillow()
            : base(0x13AB)
        {
            Name = "a throwing pillow";
        }

        //public override void OnDoubleClick(Mobile from)
        //{
        //    if (from.Items.Contains(this))
        //    {
        //        InternalTarget t = new InternalTarget(this);
        //        from.Target = t;
        //    }
        //    else
        //    {
        //        from.SendMessage("You must be holding the pillow to use it.");
        //    }
        //}

        public override void OnDoubleClick(Mobile from)
        {
            //if (from.Items.Contains(this))
            //{
                InternalTarget t = new InternalTarget(this);
                from.Target = t;
            //}
            //else
            //{
            //    from.SendMessage("You must be holding that weapon to use it.");
            //}
        }

        private class InternalTarget : Target
        {
            private ThrowingPillow m_Pillow;

            public InternalTarget(ThrowingPillow Pillow)
                : base(10, false, TargetFlags.Harmful)
            {
                m_Pillow = Pillow;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (m_Pillow.Deleted)
                {
                    return;
                }
                else if (targeted is Mobile)
                {
                    Mobile m = (Mobile)targeted;

                    Effects.SendLocationEffect(m.Location, m.Map, 0x3728, 20, 10); //smoke or dust
                    Effects.PlaySound(m.Location, m.Map, 0x11C);
                    new Feather().MoveToWorld(m.Location, m.Map);
                    new Feather().MoveToWorld(m.Location, m.Map);
                    new Feather().MoveToWorld(m.Location, m.Map);
                    new Feather().MoveToWorld(m.Location, m.Map);
                    new Feather().MoveToWorld(m.Location, m.Map);
                  
                }

               // Destroy();  //this might not be quite right.
		m_Pillow.Delete();
            }
        }
        public ThrowingPillow(Serial serial)
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
