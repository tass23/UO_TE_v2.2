using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
	[FlipableAttribute( 16706, 16708 )]
    public class PostBox : Item
    {
        [Constructable]
        public PostBox(): base(16706)
        {
            Weight = 1.0;
            Movable = false;
            Name = "a large mailbox";
            Hue = 1774;
        }

        public PostBox(Serial serial)
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

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            PlayerLetter mail = dropped as PlayerLetter;
            Parcel box = dropped as Parcel;
            if (mail != null)
            {
                if (mail.m_From != null && mail.m_To != null)
                {
                    from.SendMessage("You post a letter!");
                    if (mail.m_From != from)
                        mail.m_From.SendMessage("Your letter has been posted by " + from.Name + ".");
                    mail.m_To.AddToBackpack(dropped);
                    mail.m_To.SendMessage("You have received a letter from " + mail.m_From.Name + "!");
                    
                    return true;
                }
                from.SendMessage("That letter hasn't been addressed!");

                return false;
            }
            else if (box != null)
            {
                if (box.From != null && box.To != null)
                {
                    from.SendMessage("You post a package!");
                    if (box.From != from)
                        box.From.SendMessage("Your package has been posted by " + from.Name + ".");
                    box.To.AddToBackpack(dropped);
                    box.To.SendMessage("You have received a package from " + box.From.Name + "!");

                    return true;
                }
                from.SendMessage("That package hasn't been addressed!");
                return false;
            }
            else
            {
                from.SendMessage("That's not a rubbish bin!");
                return false;
            }
        }
    }
}
