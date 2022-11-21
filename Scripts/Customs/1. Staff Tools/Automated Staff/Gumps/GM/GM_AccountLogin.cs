
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
    public class AccountLogin : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("AccountLogin", AccessLevel.Player, new CommandEventHandler(AccountLogin_OnCommand));
        }

        private static void AccountLogin_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new AccountLogin(e.Mobile));
        }

        private Mobile m_From;

        #region AccountLogin Gump Configuration

        public AccountLogin(Mobile owner): base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);

            AddBackground(161, 153, 473, 289, 9200);
            AddItem(156, 415, 6914);
            AddItem(595, 416, 6913);
            AddItem(169, 355, 6920);
            AddItem(599, 153, 6917);
            AddItem(154, 154, 6916);
            AddBackground(196, 182, 412, 235, 5054);
            AddImage(111, 220, 10400);
            AddImage(602, 234, 10411);
            AddButton(612, 337, 22150, 22151, 0, GumpButtonType.Reply, 0);
            AddAlphaRegion(196, 181, 411, 237);
            AddBackground(210, 196, 385, 207, 5054);
            AddLabel(288, 217, 1160, @"SERVER AND ACCOUNT INFORMATION");
            AddImageTiled(334, 266, 212, 25, 9304);
            AddImageTiled(334, 307, 212, 25, 9304);
            AddLabel(252, 269, 195, @"Username:");
            AddLabel(252, 309, 195, @"Password:");
            AddButton(472, 366, 12010, 12009, 1, GumpButtonType.Reply, 1);
            AddImage(254, 366, 2092);
            AddTextEntry(334, 266, 212, 25, 0, 1, @"");
            AddTextEntry(334, 307, 212, 25, 0, 2, @"");         
        }

        #endregion Edited By: A.A.R

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1)
            {
                Mobile from = state.Mobile;
                Account acct = (Account)from.Account;

                string user = (string)info.GetTextEntry(1).Text;
                string pass = (string)info.GetTextEntry(2).Text;

                if (user == acct.Username && acct.CheckPassword(pass))
                {
                    from.SendMessage(64, "Login Confirmed.");
                    from.SendGump(new AccountInfo(from));
                }
                else
                {
                    from.SendMessage(38, "Either the username or password you entered was incorrect, Please recheck your spelling and remember that passwords and usernames are case sensitive. Please try again.");
                }
            }
        }
    }
}


