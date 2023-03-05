using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{
   public class BaltoGump : Gump
   {
		public static void Initialize()
		{
			CommandSystem.Register( "BaltoGump", AccessLevel.GameMaster, new CommandEventHandler( BaltoGump_OnCommand ) );
		}

		private static void BaltoGump_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new BaltoGump( e.Mobile ) );
		}

		public BaltoGump( Mobile owner ) : base( 50,50 )
		{
//----------------------------------------------------------------------------------------------------
			AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );
			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "A talk With Gallin" );
			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
			"<BASEFONT COLOR=YELLOW>A worried young man stands before you fidgeting with his staff.<BR><BR>I am sorry to bother you traveler.<BR>" +
			"<BASEFONT COLOR=YELLOW>But it seems that I am in a bit of trouble.  I was on my way to my master to deliver a special item…<BR>" +
			"<BASEFONT COLOR=YELLOW>and I was robbed, I do not know what to do.. I am an apprentice to Master Gallin, the Arch Spell Crafter.<BR><BR>If you can please go and tell my master what has happened, I'm sure he will reward you. Tell him Balto sent you. <BR>" +
			"<BASEFONT COLOR=YELLOW>Please be quick so we may find what was stolen quickly! <br><br>" +
			"<BASEFONT COLOR=YELLOW>You can find him at Moonglow's Finest Alchemy shop! Safe travels, adventurer.<br><br>"+
			"</BODY>", false, true);
//<BASEFONT COLOR=#7B6D20>
			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );
			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 );
//--------------------------------------------------------------------------------------------------------------
		} 
		
		public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons
		{
			Mobile from = state.Mobile;
			switch ( info.ButtonID )
			{
				case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0
				{
					//Cancel
					from.SendMessage( "Please, go find Master Gallin!" );
					break;
				}
			}
		}
	}
}