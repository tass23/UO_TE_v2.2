
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
    public class ReportLag : Gump
    {
        Mobile caller;
        public static void Initialize()
        {
            Commands.CommandSystem.Register("ReportLag", AccessLevel.GameMaster, new CommandEventHandler(ReportLag_OnCommand));
        }

        [Usage("ReportLag")]
        [Description("Makes A Call To Your Custom Gump.")]
        public static void ReportLag_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new ReportLag());
        }

        #region ReportLag Gump Configuration

        public ReportLag() : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddBackground(161, 139, 473, 343, 9200);
			AddItem(156, 454, 6914);
			AddItem(595, 455, 6913);
			AddItem(169, 355, 6920);
			AddItem(599, 138, 6917);
			AddItem(154, 139, 6916);
			AddBackground(180, 154, 442, 289, 5054);
			AddAlphaRegion(206, 194, 390, 209);
            AddTextEntry(213, 203, 377, 192, 0, (int)Buttons.TextEntry1, @"");
			AddImage(111, 220, 10400);
			AddImage(602, 234, 10411);
			AddButton(612, 337, 22150, 22151, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			AddLabel(332, 157, 695, @"Report a Lag Spike");
			AddLabel(350, 422, 0, @"The Expanse");
			AddButton(521, 422, 12009, 12010, (int)Buttons.Button2, GumpButtonType.Reply, 2);
			AddLabel(223, 441, 930, @"Lag Spike Reports Help Us Monitor Our Servers Stability");
			AddLabel(226, 458, 930, @"Without These Reports There Is Very Little We Can Do");
        }

        #endregion Edited By: A.A.R

        public enum Buttons
        {
            Button1,
            Button2,
            TextEntry1,
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            Account acct = (Account)from.Account;

            switch (info.ButtonID)
            {
                case (int)Buttons.Button1:
                    {
                        from.CloseGump(typeof(ReportLag));
                        from.SendMessage("You Decide Not To File A Lag Report.");
                        break;
                    }

                case (int)Buttons.Button2:
                    {
                        string submit = (string)info.GetTextEntry((int)Buttons.TextEntry1).Text;

                        Console.WriteLine("");
                        Console.WriteLine("{0} From Account {1} Filed A Lag Report", from.Name, acct.Username);
                        Console.WriteLine("");

                        if (!Directory.Exists("Export/Reports/")) //create directory
                            Directory.CreateDirectory("Export/Reports/");

                        using (StreamWriter op = new StreamWriter("Export/Reports/Lag.txt", true))
                        {
                            op.WriteLine("");
                            op.WriteLine("Name Of Character: {0}, Account:{1}", from.Name, acct.Username);
                            op.WriteLine("Message: {0}", submit);
                            op.WriteLine("");
                        }

                        from.SendMessage("Your Lag Report Has Been Filed! Thank You.");
                        break;
                    }
            }
        }
    }
}