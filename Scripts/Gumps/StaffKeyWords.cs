//Completely Automated Staff Team - By Tresdni - Please leave this header :)  I worked hard on this system!
using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class StaffKeyWords : Gump
    {                         
        public StaffKeyWords(Mobile from) : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(18, 2, 241, 518, 9300);
			AddLabel(54, 24, 0x66C, @"Staff Member Key Words");  //Add your own keywords to go with cases in the StaffBot.cs!  It's unlimited as to what these guys can do!  Get creative! (This is only partially what mine do atm.)
			AddHtml( 58, 61, 151, 437, @"<br /><br />stuck<br />pull<br />donation<br />suggestion<br />hiring<br />harassment<br />spellweaving<br />gauntlet<br />tot<br />treasuresoftokuno<br />vetrewards<br />factionkick<br />realperson<br />owner<br />nark<br />report<br />", (bool)true, (bool)true);
			

            
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