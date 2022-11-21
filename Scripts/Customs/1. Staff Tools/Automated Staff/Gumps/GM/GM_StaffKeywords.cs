
#region Automated Server Staff Acknowledgements
/*
    This Automated NPC System Idea Originated
    With A Script Coded By: Tresdni from the
    RunUO (www.runuo.com) Forums; It Is Named:
    
       **Completely Automated Staff Team** 
 http://www.runuo.com/community/threads/completely-automated-staff-team-oh-yes-i-did.460720/
 
    This Released Version Of The Script Named
    Above Is My Own Variation On What I Think
    Might Complete The System Tresdni Started.
    
    The Code In This Script File Is Annoted. I
    Have Regioned Out Most Areas And Outlined
    Others So That You Know What Code Can Be
    Copy And Pasted To Other Scripts To Add The
    Same Functionality For Another System. 
 
    The Author Of Each Line Of Code Varies, I
    Got The Shell Of This Script From Tresdni,
    However A Lot Has Come From Many Other 
    Sources Over The Years; I Have A Library
    Of Annotated Methods I've Been Working On,
    That Help Me Build The Scripts I Upload.
 
    A Special Thank You Goes Out To The Following
    People For Helping Me Complete This System
    Addition To The Completely Automated Staff Team,
    Written By: Tresdni:
 
    THANK YOU GUYS!! THE HELP WAS MUCH APPRECIATED
                   -Sythen (A.A.R)-
    ______________________________________________
    ** JamzeMcC | Morexton | Soteric | James420 **
    ----------------------------------------------
 */
#endregion Edited By: A.A.R

using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Targeting;
using Server.Items;
using Server.ContextMenus;
using Server.Multis;
using Server.Spells;
using Server.Commands;
using Server.Gumps;
using Server.Mobiles;
using Server.Accounting;
using Server.Misc;
using Server.Network;

namespace Server.Gumps
{
    public class GM_StaffKeywords : Gump
    {
        Mobile caller;
        public static void Initialize()
        {
            Commands.CommandSystem.Register("GM", AccessLevel.GameMaster, new CommandEventHandler(GM_OnCommand));
        }

        [Usage("GM")]
        [Description("Makes A Call To Your Custom Gump.")]
        public static void GM_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new GM_StaffKeywords());
        }

        #region StaffKeywords Gump Configuration
       
        public GM_StaffKeywords() : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddBackground(279, 111, 231, 377, 9200);
			AddItem(475, 110, 6917);
			AddItem(471, 461, 6913);
			AddImage(229, 215, 10400);
			AddButton(488, 337, 22150, 22151, 0, GumpButtonType.Reply, 0);
			AddItem(274, 460, 6914);
			AddItem(272, 111, 6916);
			AddItem(288, 357, 6920);
			AddBackground(299, 124, 199, 352, 5054);
			AddAlphaRegion(329, 164, 140, 272);
			AddHtml( 335, 171, 129, 259, @"> serverinfo
> tosagreement
> serverrules
> meetourstaff
> showcredits
  ------------
> reportplayer
> reportlag
> reportguild
> reportdefect
> reportadmin
  ------------
> teleportme
> relocateme
> accounthelp", (bool)true, (bool)true);
			AddLabel(339, 127, 695, @"Staff Keywords");
			AddLabel(347, 454, 0, @"The Expanse");
			AddImage(478, 232, 10411);       
        }

        #endregion Edited By: A.A.R

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 0:
                    {
                        from.CloseGump(typeof(GM_StaffKeywords));
                        break;
                    }

            }
        }
    }
}