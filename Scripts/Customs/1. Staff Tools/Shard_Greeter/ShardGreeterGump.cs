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
    public class ShardGreeterGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("ShardGreeterGump", AccessLevel.GameMaster, new CommandEventHandler(ShardGreeterGump_OnCommand)); 
      } 

      private static void ShardGreeterGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ShardGreeterGump( e.Mobile ) ); 
      } 

      public ShardGreeterGump( Mobile owner ) : base( 50,50 ) 
      { 


			AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );

			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "The Expanse!" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +

"<BASEFONT Color=White><i>Latest Updates</i><br><br>" + 
"<BASEFONT COLOR=White>Patch Released 7-30-2016!<br><br>" +
"<BASEFONT Color=White>Check the forums for information on all updates.<br><br>" +
"<BASEFONT COLOR=White>www.UOExpanse.com/forums/<br>" +
"</BODY>", false, true);
			
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

            /*AddButton(120, 137, 9727, 9730, (int)Buttons.sewb, GumpButtonType.Reply, 0);
            AddLabel(160, 141, 93, @"The Expanse Forums");//...................................Button Label 1
			*/
			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
      } 

        /*public enum Buttons
        {
            sewb
        }
		*/
	  
      public override void OnResponse( NetState state, RelayInfo info ) 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         {
            /*case (int)Buttons.sewb: // Your Servers Website
            {
                sender.LaunchBrowser("http://www.uoexpanse.com/forum/forumdisplay.php?fid=1");//.............................Web Address 1
                break;
            }*/
            case 0:  
            { 
               
               from.SendMessage( "Have fun and be safe!" );
               break; 
            } 

         }
      }
   }
}
