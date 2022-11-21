using System;
using System.Text;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Gumps
{
    public class FontTestGump : Gump 
    {
		PlayerMobile caller;

		public static void Initialize() 
		{
			CommandSystem.Register("FontTestGump", AccessLevel.Administrator, new CommandEventHandler(FontTestGump_OnCommand));
		} 

		[Usage("FontTestGump")]
        [Description("Makes a call to your custom gump.")]
        public static void FontTestGump_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.HasGump(typeof(FontTestGump)))
                from.CloseGump(typeof(FontTestGump));
            from.SendGump(new FontTestGump(from));
        }

		public FontTestGump( Mobile from ) : base( 50,50 ) 
		{
			caller = (PlayerMobile)from;

			AddPage(0);
			AddImageTiled(54, 32, 510, 400, 2604);
			AddImage(87, 41, 9005);
			AddImageTiled(58, 39, 29, 390, 10460);
			AddImageTiled(563, 37, 31, 389, 10460);
			AddLabel(140, 60, 34, @"The Expanse!");
			AddHtml( 230, 61, 100, 20, @"<basefont>Index (default)</basefont>", (bool)false, (bool)false);
			AddHtml( 120, 347, 410, 66, @"<BODY>There are several different fonts available in the UO client. They are shown above, along with their corresponding code needed.", (bool)true, (bool)false);
			AddImageTiled(40, 38, 17, 391, 9263);
			AddImageTiled(595, 38, 17, 391, 9263);
			AddImage(6, 25, 10421);
			AddImage(34, 12, 10420);
			AddImageTiled(94, 25, 492, 15, 10304);
			AddImageTiled(40, 427, 572, 16, 10304);
			AddImage(-10, 314, 10402);
			AddImage(56, 150, 10411);
			AddImage(105, 123, 2103);
			AddImage(136, 84, 96);
			AddHtml( 120, 117, 200, 20, @"<B><basefont>Index (Bold)</basefont></B>", (bool)false, (bool)false);	//B
			AddImage(105, 166, 2103);
			AddHtml( 120, 160, 200, 20, @"<BIG><basefont>Index (Big)</basefont></BIG>", (bool)false, (bool)false);	//BIG
			AddImage(105, 210, 2103);
			AddHtml( 120, 204, 200, 20, @"<B><BIG><basefont>Index (Bold, Big)</basefont></BIG></B>", (bool)false, (bool)false);	//B BIG
			AddImage(105, 254, 2103);
			AddHtml( 120, 248, 200, 20, @"<I><BIG><basefont>Index (Italic, Big)</basefont></BIG></I>", (bool)false, (bool)false);	//I BIG
			AddImage(105, 300, 2103);
			AddHtml( 120, 294, 220, 20, @"<B><I><BIG><basefont>Index (Bold, Italic, Big)</basefont></BIG></I></B>", (bool)false, (bool)false);	//B I BIG
			AddImage(354, 123, 2103);
			AddHtml( 370, 117, 200, 20, @"<I><basefont>Index (Italic)</basefont></I>", (bool)false, (bool)false);	//I
			AddImage(354, 166, 2103);
			AddHtml( 370, 160, 200, 20, @"<SMALL><basefont>Index (Small)</basefont></SMALL>", (bool)false, (bool)false);	//SMALL
			AddImage(354, 210, 2103);
			AddHtml( 370, 204, 200, 20, @"<B><SMALL><basefont>Index (Bold, Small)</basefont></SMALL></B>", (bool)false, (bool)false);	//B SMALL
			AddImage(354, 254, 2103);
			AddHtml( 370, 248, 200, 20, @"<I><SMALL><basefont>Index (Italic, Small)</basefont></SMALL></I>", (bool)false, (bool)false);	//I SMALL
			AddImage(354, 300, 2103);
			AddHtml( 370, 294, 200, 20, @"<B><I><SMALL><basefont>Index (Bold, Italic, Small)</basefont></SMALL></I></B>", (bool)false, (bool)false);	//B I SMALL
			AddLabel(124, 142, 0, @"<BIG>");
			AddLabel(374, 142, 0, @"<SMALL>");
			AddLabel(124, 186, 0, @"<B><BIG>");
			AddLabel(374, 186, 0, @"<B><SMALL>");
			AddLabel(124, 230, 0, @"<I><BIG>");
			AddLabel(374, 230, 0, @"<I><SMALL>");
			AddLabel(124, 276, 0, @"<B><I><BIG>");
			AddLabel(374, 276, 0, @"<B><I><SMALL>");
			AddLabel(124, 99, 0, @"<B>");
			AddLabel(374, 99, 0, @"<I>");
			AddImage(580, 9, 10441);
		}

		public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
				case 0:
				{
					break;
				}
            }
        }
	}
}