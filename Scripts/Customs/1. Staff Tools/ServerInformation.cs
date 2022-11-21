using System;
using System.Collections;
using System.Reflection;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Regions;
using Server.Gumps;

namespace Server.Commands
{
	public class ServerInformation : Gump
	{
		public static void Initialize() 
		{
            CommandSystem.Register("ServerInformation", AccessLevel.GameMaster, new CommandEventHandler(ShardInfo_OnCommand)); 
		} 

		private static void ShardInfo_OnCommand( CommandEventArgs e ) 
		{ 
			e.Mobile.SendGump( new ServerInformation( e.Mobile ) ); 
		} 

		public ServerInformation( Mobile owner ) : base( 50,50 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(82, 69, 391, 332, 9200);
			this.AddAlphaRegion(82, 5, 490, 430);
			this.AddImage(33, 7, 10440);
			this.AddImage(441, 7, 10441);
			this.AddImage(229, 84, 2445);
			this.AddImage(198, 114, 2440);
			this.AddLabel(245, 85, 89, @"The Expanse");
			this.AddLabel(222, 117, 62, @"Information Center");

			AddHtml( 107, 155, 350, 230, "<BODY>" +

//----------------------------------------------------------------------------/
//----------------------------Introduction------------------------------------/
//Use <BR> for new paragraphs | Use <BR><BR> for spaces in between paragraphs

"<BASEFONT COLOR=AQUA><BR><BR>Welcome To The Expanse Information Center!" +
"<BASEFONT COLOR=LIME><BR><BR>This Is Where You Will Learn About Any New Server Changes That Have Been Made In The Last Week. In Addition You Can Use This Gump As A Quick Reference Guide For Any Server Commands Which Are Available To You.<BR><BR>Should You Have Any Questions Please Visit Any One Of Our Visitors Information Centers. Thank You. You can also visit us at... http://www.uoexpanse.com" +

//----------------------------------------------------------------------------/
//---START-----------Type All Server Changes Below----------------------------/

"<BASEFONT COLOR=WHITE><BR><BR>LATEST SERVER UPDATES." +

//Change 01
"<BASEFONT COLOR=RED><BR><BR>8/21/2013-8/27/2013"+
"<BASEFONT COLOR=LIME><BR><BR>Patch v1.7 Released!" +

"<BASEFONT COLOR=AQUA><BR><BR>This patch was mostly to make new artwork available for player's houses, but also a few other nice things." +

"<BASEFONT COLOR=AQUA><BR><BR>10 New Pet Parrot colors to allow for more two-tone styles, 18 New House Decorations, 24 Exclusive New Walls," +
"<BASEFONT COLOR=AQUA><BR><BR>10 Exclusive New Doors were added during this patch." +

//Change 02
"<BASEFONT COLOR=LIME><BR><BR>Plant Sprinklers!" +
"<BASEFONT COLOR=AQUA><BR><BR>With the addition of Plant Sprinklers, caring for plants has never been easy and less time consuming!" +
"<BASEFONT COLOR=AQUA><BR><BR>The Quality Gardener in the center of Moonglow's Hedge Maze sells the various sprinklers and a special" +
"<BASEFONT COLOR=AQUA><BR><BR>sprinkler filler used to fill the water sprinkler. Check the forums for more info." +

//Change 03
"<BASEFONT COLOR=LIME><BR><BR>Sailboat Transportation" +
"<BASEFONT COLOR=AQUA><BR><BR>Head to just about any dock in Trammel and you will see a Sail Master." +
"<BASEFONT COLOR=AQUA><BR><BR>You can purchase a Sailboat Membership Card and sail for free." +
"<BASEFONT COLOR=AQUA><BR><BR>Or you can pay gold to 'sail' to various port cities around Trammel." +

//---END-------Server Changes Can Be Infinite (Just Copy And Paste)-----------/
//----------------------------------------------------------------------------/

//----------------------------------------------------------------------------/

//----------------------------------------------------------------------------/
//---START-----------Type All Server Changes Below----------------------------/

"<BASEFONT COLOR=WHITE><BR><BR>COMMUNITY COMMANDS" +

//Command 01
"<BASEFONT COLOR=YELLOW><BR><BR>[pc" +
"<BASEFONT COLOR=LIME><BR><BR>General Chat" +
"<BASEFONT COLOR=AQUA><BR><BR>Free range, open chat channel." +

//Command 02
"<BASEFONT COLOR=YELLOW><BR><BR>[h" +
"<BASEFONT COLOR=LIME><BR><BR>Help Chat" +
"<BASEFONT COLOR=AQUA><BR><BR>If you need some help and/or guidance, use Help Chat." +

//Command 03
"<BASEFONT COLOR=YELLOW><BR><BR>[mystats" +
"<BASEFONT COLOR=LIME><BR><BR>Character Stat Window" +
"<BASEFONT COLOR=AQUA><BR><BR>This will show you some overall stats for your character." +

//Command 04
"<BASEFONT COLOR=YELLOW><BR><BR>[tourguide" +
"<BASEFONT COLOR=LIME><BR><BR>The Expanse Tour" +
"<BASEFONT COLOR=AQUA><BR><BR>This allows you to retake the introductory shard tour whenver you want." +

//Command 05
"<BASEFONT COLOR=YELLOW><BR><BR>[roll" +
"<BASEFONT COLOR=LIME><BR><BR>Rolling For _______" +
"<BASEFONT COLOR=AQUA><BR><BR>if you're in a party and you need to roll for loot, this outputs your roll into party chat." +

//Command 06
"<BASEFONT COLOR=YELLOW><BR><BR>[questlog" +
"<BASEFONT COLOR=LIME><BR><BR>Player Quest Log" +
"<BASEFONT COLOR=AQUA><BR><BR>This will show you all the quests you are on and the quests that are available to do again." +

//Command 07
"<BASEFONT COLOR=YELLOW><BR><BR>[emote" +
"<BASEFONT COLOR=LIME><BR><BR>Sound Emotes" +
"<BASEFONT COLOR=AQUA><BR><BR>For a full list of these, check the forums." +

//Command 08
"<BASEFONT COLOR=YELLOW><BR><BR>[donate" +
"<BASEFONT COLOR=LIME><BR><BR>Donate Items" +
"<BASEFONT COLOR=AQUA><BR><BR>If you have some items that you no longer want, don't throw them away, donate them." +

//---END-------Server Changes Can Be Infinite (Just Copy And Paste)-----------/
//----------------------------------------------------------------------------/

"</BODY>", false, true);
		}
		
		public enum Buttons
		{
			TextEntry1,
		}

	}
}