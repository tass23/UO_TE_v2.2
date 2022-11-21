/* This was originally the Web Terminal by Sythen.
 * It was updated/changed by Shai'Tan Malkier to be
 * more compatible and user friendly and without 
 * modifying any mul files.*/

using System;
using System.Text;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Items
{
    public class WebStoneE : Item
    {
        [Constructable]
        public WebStoneE()
            : base(0xED7)
        {
            Movable = false;
            Name = "Web Stone";
        }

        public override void OnDoubleClick(Mobile from)
        {

            from.SendGump(new WebStoneGump(from));
        }

        public WebStoneE(Serial serial)
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

namespace Server.Items
{
    public class WebStoneS : Item
    {
        [Constructable]
        public WebStoneS()
            : base(0xED8)
        {
            Movable = false;
            Name = "Web Stone";
        }

        public override void OnDoubleClick(Mobile from)
        {

            from.SendGump(new WebStoneGump(from));
        }

        public WebStoneS(Serial serial)
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

namespace Server.Gumps
{
    public class WebStoneGump : Gump
    {
        private Mobile m_From;

        public WebStoneGump(Mobile from)
            : base(150, 50)
        {
            m_From = from;
            if (!(m_From is PlayerMobile))
                return;

            Closable = false;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            AddPage(0);
            AddBackground(25, 72, 501, 301, 5054);
            AddImage(92, 230, 9741);
            AddImage(91, 323, 9751);
            AddImage(181, 323, 9751);
            AddImage(274, 323, 9751);
            AddImage(454, 142, 9741);
            AddImage(454, 230, 9741);
            AddImage(365, 323, 9751);
            AddImage(454, 116, 9741);
            AddImage(362, 116, 9751);
            AddImage(273, 116, 9751);
            AddImage(184, 116, 9751);
            AddImage(92, 118, 9741);
            AddImage(92, 116, 9751);
            AddImage(92, 167, 9741);
            AddImage(41, 81, 10400);
            AddImage(41, 261, 10402);
            AddImage(430, 81, 10410);
            AddImage(430, 261, 10412);

            AddLabel(167, 91, 47, @"The Expanse");// Shard Name

            AddButton(120, 137, 9727, 9730, (int)Buttons.sewb, GumpButtonType.Reply, 0);
            AddLabel(160, 141, 93, @"The Expanse");//...................................Button Label 1

            AddButton(120, 173, 9727, 9730, (int)Buttons.cclb, GumpButtonType.Reply, 0);
            AddLabel(160, 177, 93, @"Staff Contact  List");//................................Button Label 2

            AddButton(120, 210, 9727, 9730, (int)Buttons.spgb, GumpButtonType.Reply, 0);
            AddLabel(160, 214, 93, @"Playguides");//............................Button Label 3

            AddButton(120, 247, 9727, 9730, (int)Buttons.pffb, GumpButtonType.Reply, 0);
            AddLabel(160, 251, 93, @"Forums");//...................................Button Label 4

            /*AddButton(120, 283, 9727, 9730, (int)Buttons.rmcb, GumpButtonType.Reply, 0);
            AddLabel(160, 287, 93, @"REPORT  MEMBER  CONDUCT");//..................................Button Label 5
			*/
            AddButton(242, 335, 242, 241, (int)Buttons.canc, GumpButtonType.Reply, 0);
        }

        public enum Buttons
        {
            sewb,
            cclb,
            spgb,
            pffb,
            //rmcb,
            canc
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.sewb: // Your Servers Website
                    {
                        sender.LaunchBrowser("http://www.uoexpanse.com");//.............................Web Address 1
                        break;
                    }

                case (int)Buttons.cclb:
                    {
                        sender.LaunchBrowser("http://www.uoexpanse.com/credit.php");//.............................Web Address 2
                        break;
                    }

                case (int)Buttons.spgb:
                    {
                        sender.LaunchBrowser("http://www.uoguide.com");////...........................Web Address 3
                        break;
                    }

                case (int)Buttons.pffb:
                    {
                        sender.LaunchBrowser("http://www.uoexpanse.com/forum");//.............................Web Address 4
                        break;
                    }

                /*case (int)Buttons.rmcb:
                    {
                        sender.LaunchBrowser("http://www.google.com");//.............................Web Address 5
                        break;
                    }
				*/
                case (int)Buttons.canc:
                    {
                        m_From.CloseGump(typeof(WebStoneGump));
                        break;
                    }
            }
        }
    }
}