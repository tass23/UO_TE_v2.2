
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
    public class AccountInfo : Gump
    {
        private Mobile m_From;

        private int m_PassLength = 6;

        #region AccountInfo Gump Configuration

        public AccountInfo(Mobile from): base(0, 0)
        {
            m_From = from;

            Account acct = (Account)from.Account;
            PlayerMobile pm = (PlayerMobile)from;
            NetState ns = from.NetState;
            ClientVersion v = ns.Version;

            TimeSpan totalTime = (DateTime.Now - acct.Created);

            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            AddPage(0);
            AddPage(1);

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
            AddLabel(296, 217, 1160, @"GENERAL ACCOUNT INFORMATION");
            AddAlphaRegion(227, 254, 350, 101);
            AddLabel(234, 254, 195, @"Client Version:");
            AddLabel(234, 274, 195, @"IP Address:");
            AddLabel(234, 294, 195, @"Creation Date:");
            AddLabel(234, 315, 195, @"Played Time:");
            AddLabel(234, 335, 195, @"Account Age:");
            AddLabel(332, 254, 64, v == null ? "(null)" : v.ToString());
            AddLabel(310, 274, 64, ns.ToString());
            AddLabel(332, 294, 64, acct.Created.ToString());
            AddButton(472, 366, 12010, 12009, 1, GumpButtonType.Page, 2);
            AddLabel(254, 363, 132, @"Click Button To Change Password:");
            AddLabel(385, 230, 1160, acct.Username.ToString());

            string gt = pm.GameTime.Days + " Days, " + pm.GameTime.Hours + " Hrs, " + pm.GameTime.Minutes + " Minutes, " + pm.GameTime.Seconds + " Seconds ";
            AddLabel(318, 315, 64, gt.ToString());

            string tt = totalTime.Days + " Days, " + totalTime.Hours + " Hrs, " + totalTime.Minutes + " Minutes, " + totalTime.Seconds + " Seconds ";
            AddLabel(321, 334, 64, tt.ToString());

            AddPage(2);

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
            AddImageTiled(357, 252, 212, 25, 9304);
            AddImageTiled(357, 287, 212, 25, 9304);
            AddImageTiled(356, 322, 212, 25, 9304);
            AddLabel(256, 217, 1160, @"ACCOUNT LOGIN PASSWORD CONTROL PANEL");
            AddLabel(235, 255, 195, @"Current Password:");
            AddLabel(235, 289, 195, @"New Password:");
            AddLabel(235, 324, 195, @"Confirm Password:");
            AddImage(335, 292, 2092);
            AddTextEntry(357, 252, 212, 25, 0, 1, @"");
            AddTextEntry(357, 287, 212, 25, 0, 2, @"");
            AddTextEntry(356, 322, 212, 25, 0, 3, @"");
            AddButton(472, 366, 12010, 12009, 1, GumpButtonType.Reply, 1);
        }

        #endregion Edited By: A.A.R

        public override void OnResponse(NetState state, RelayInfo info)
        {
            if (info.ButtonID == 1)
            {
                Mobile from = state.Mobile;
                Account acct = (Account)from.Account;

                string cpass = (string)info.GetTextEntry(1).Text;
                string newpass = (string)info.GetTextEntry(2).Text;
                string newpass2 = (string)info.GetTextEntry(3).Text;

                if (acct.CheckPassword(cpass))
                {
                    if (newpass == null || newpass2 == null)
                    {
                        from.SendMessage(38, "You must type in a new password and confirm it.");
                    }
                    else if (newpass.Length <= m_PassLength)
                    {
                        from.SendMessage(38, "Your new password must be at least characters {0} long.", m_PassLength);
                    }
                    else if (newpass == newpass2)
                    {
                        from.SendMessage("Your password has been changed to {0}.", newpass);
                        acct.SetPassword(newpass);
                        //CommandLogging.WriteLine( from, "{0} {1} has changed thier password for account {2} using the [accountlogin command", from.AccessLevel, CommandLogging.Format( from ), acct.Username );
                    }
                    else
                    {
                        from.SendMessage(38, "Your new password did not match your confirm password. Please check your spelling and try again.");
                        from.SendMessage(38, "Just a reminder. Passwords are case sensitive.");
                    }
                }
                else
                {
                    from.SendMessage(38, "The current password you typed in did not match your current password on record. Please check your spelling and try again.");
                    from.SendMessage(38, "Just a reminder. Passwords are case sensitive.");
                }
            }
        }
    }
}