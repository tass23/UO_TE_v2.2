
using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{
    public class SQ3Gump : Gump
    {

        public static void Initialize()
        {
            CommandSystem.Register("SQ3Gump", AccessLevel.GameMaster, new CommandEventHandler(SQ3Gump_OnCommand));
        }

        private static void SQ3Gump_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new SQ3Gump(e.Mobile));
        }

        public SQ3Gump(Mobile owner)
            : base(50, 50)
        {

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddBackground(65, 65, 386, 294, 9200);
            AddTextEntry(159, 81, 200, 26, 1163, 0, @"La Insep Ohm");
            AddButton(82, 304, 2152, 248, 2, GumpButtonType.Reply, 2);
            AddButton(82, 249, 2151, 248, 1, GumpButtonType.Reply, 1);
            AddButton(82, 194, 2151, 248, 0, GumpButtonType.Reply, 0);
            AddTextEntry(95, 114, 337, 19, 1142, 0, @"From Passion springs which virtue?");
            //AddTextEntry(97, 137, 200, 20, Ultima.Hue, 0, @"consist?");
            AddTextEntry(137, 198, 257, 20, 1142, 0, @"Feeling");
            AddTextEntry(137, 252, 257, 20, 1142, 0, @"Persistence");
            AddTextEntry(0, 0, 200, 20, 1142, 0, @"");
            AddTextEntry(136, 307, 258, 20, 1142, 0, @"Control");
            

        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.SendGump(new SQ4Gump(from));
                        from.CloseGump(typeof(SQ3Gump));
                        break;
                    }
                case 1:
                    {
                        from.SendLocalizedMessage(1112680);
                        from.CloseGump(typeof(SQ3Gump));
                        break;
                    }
                case 2:
                    {
                        from.SendLocalizedMessage(1112680);
                        from.CloseGump(typeof(SQ3Gump));
                        break;
                    }
            }
        }
    }
}
