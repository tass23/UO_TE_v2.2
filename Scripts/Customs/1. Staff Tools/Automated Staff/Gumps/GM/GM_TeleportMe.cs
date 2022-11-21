
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
    public class GM_TeleportMe : Gump
    {
        Mobile caller;
        public static void Initialize()
        {
            Commands.CommandSystem.Register("GM_TeleportMe", AccessLevel.GameMaster, new CommandEventHandler(GM_TeleportMe_OnCommand));
        }

        [Usage("GM_TeleportMe")]
        [Description("Makes A Call To Your Custom Gump.")]
        public static void GM_TeleportMe_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new GM_TeleportMe());
        }

        #region ReportGuild Gump Configuration

        public GM_TeleportMe() : base( 0, 0 )
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
			AddButton(488, 337, 22150, 22151, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			AddItem(274, 460, 6914);
			AddItem(272, 111, 6916);
			AddItem(288, 357, 6920);
			AddBackground(299, 124, 199, 352, 5054);
			AddLabel(339, 127, 695, @"Select a Location");
			AddLabel(347, 454, 0, @"The Expanse");
			AddImage(478, 232, 10411);
			AddAlphaRegion(329, 162, 20, 274);
			AddButton(332, 166, 1209, 1210, (int)Buttons.Button3, GumpButtonType.Reply, 3);
			AddButton(332, 193, 1209, 1210, (int)Buttons.Button4, GumpButtonType.Reply, 4);
			AddButton(332, 220, 1209, 1210, (int)Buttons.Button5, GumpButtonType.Reply, 5);
			AddButton(332, 248, 1209, 1210, (int)Buttons.Button6, GumpButtonType.Reply, 6);
			AddButton(332, 276, 1209, 1210, (int)Buttons.Button7, GumpButtonType.Reply, 7);
			AddButton(332, 305, 1209, 1210, (int)Buttons.Button8, GumpButtonType.Reply, 8);
			AddButton(332, 333, 1209, 1210, (int)Buttons.Button9, GumpButtonType.Reply, 9);
			AddButton(332, 362, 1209, 1210, (int)Buttons.Button10, GumpButtonType.Reply, 10);
			AddButton(332, 391, 1209, 1210, (int)Buttons.Button11, GumpButtonType.Reply, 11);
			AddButton(332, 419, 1209, 1210, (int)Buttons.Button12, GumpButtonType.Reply, 12);

            //Edit Location Labels Here
			AddLabel(358, 163, 0, @"Server Location a");
			AddLabel(358, 190, 0, @"Server Location b");
			AddLabel(358, 217, 0, @"Server Location c");
			AddLabel(358, 246, 0, @"Server Location d");
			AddLabel(358, 273, 0, @"Server Location e");
			AddLabel(358, 302, 0, @"Server Location f");
			AddLabel(358, 329, 0, @"Server Location g");
			AddLabel(358, 358, 0, @"Server Location h");
			AddLabel(358, 387, 0, @"Server Location i");
			AddLabel(358, 415, 0, @"Server Location j");      
        }

        #endregion Edited By: A.A.R

        public enum Buttons
		{
			Button1,
			Button3,
			Button4,
			Button5,
			Button6,
			Button7,
			Button8,
			Button9,
			Button10,
			Button11,
			Button12,
		}

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
                case (int)Buttons.Button1:
				{
                    from.CloseGump(typeof(GM_TeleportMe));
                    from.SendMessage("You Decide Not To Travel Anywhere");
                    break;
				}

                //Gate To Moonglow Moongate
				case (int)Buttons.Button3:
				{
                    MoonglowGate mglg = new MoonglowGate();
                    mglg.Location = from.Location;
                    mglg.Map = from.Map;
                    break;
				}

                //Gate To Britain Moongate
				case (int)Buttons.Button4:
				{
                    BritainGate brig = new BritainGate();
                    brig.Location = from.Location;
                    brig.Map = from.Map;
                    break;
				}

                //Gate To Jhelom Moongate
				case (int)Buttons.Button5:
				{
                    JhelomGate jheg = new JhelomGate();
                    jheg.Location = from.Location;
                    jheg.Map = from.Map;
                    break;
				}

                //Gate To Yew Moongate
				case (int)Buttons.Button6:
				{
                    YewGate yewg = new YewGate();
                    yewg.Location = from.Location;
                    yewg.Map = from.Map;
                    break;
				}

                //Gate To Minoc Moongate
				case (int)Buttons.Button7:
				{
                    MinocGate ming = new MinocGate();
                    ming.Location = from.Location;
                    ming.Map = from.Map;
                    break;
				}

                //Gate To Trinsic Moongate
				case (int)Buttons.Button8:
				{
                    TrinsicGate trig = new TrinsicGate();
                    trig.Location = from.Location;
                    trig.Map = from.Map;
                    break;
				}

                //Gate To SkaraBrae Moongate    
				case (int)Buttons.Button9:
				{
                    SkaraBraeGate skag = new SkaraBraeGate();
                    skag.Location = from.Location;
                    skag.Map = from.Map;
                    break;
				}

                //Gate To Magincia Moongate 
				case (int)Buttons.Button10:
				{
                    MaginciaGate magg = new MaginciaGate();
                    magg.Location = from.Location;
                    magg.Map = from.Map;
                    break;
				}

                //Gate To NewHaven Moongate 
				case (int)Buttons.Button11:
				{
                    NewHavenGate nhvg = new NewHavenGate();
                    nhvg.Location = from.Location;
                    nhvg.Map = from.Map;
                    break;
				}

                //Gate To BucsDen Moongate
				case (int)Buttons.Button12:
				{
                    BucsDenGate bucg = new BucsDenGate();
                    bucg.Location = from.Location;
                    bucg.Map = from.Map;
                    break;
				}

            }
        }
    }
}