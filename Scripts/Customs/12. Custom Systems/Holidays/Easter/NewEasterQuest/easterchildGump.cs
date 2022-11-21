using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class easterchildquestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "easterchildquestGump", AccessLevel.GameMaster, new CommandEventHandler( easterchildquestGump_OnCommand ) ); 
      } 

      private static void easterchildquestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new easterchildquestGump( e.Mobile ) ); 
      } 

      public easterchildquestGump( Mobile owner ) : base( 50,50 ) 
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(13, 5, 382, 433, 2524);
			AddImageTiled(9, 6, 388, 7, 40);
			AddImageTiled(11, 433, 382, 9, 40);
			AddImage(13, 18, 3005, 1152);
			AddImage(389, 188, 3003, 1152);
			AddImage(13, 187, 3005, 1152);
			AddImage(389, 17, 3003, 1152);
			AddImageTiled(15, 421, 376, 12, 50);
			AddImage(46, 12, 2080);
                                                AddTextEntry(82, 25, 170, 20, 33, 0, @"          Easter Quest!");
                                                AddTextEntry(69, 52, 200, 20, 58, 0, @"Bring Me Back An Easter Egg!");
			

			

			
			

			AddHtml(  31, 93, 346, 281, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=GREEN>Please! can you get me easter eggs!<BR><BR>" + 
"<BASEFONT COLOR=GREEN>I didn't receive any today and its Easter Day!<BR><BR>" +
"<BASEFONT COLOR=GREEN>Please bring me an Easter Eggs and in return<BR><BR>" +
"<BASEFONT COLOR=GREEN>I will give you some Chocolate Dust which the Easter Bunny needs<BR><BR>" +
"<BASEFONT COLOR=GREEN>Word is that one of those stealing rabbits lives<BR><BR>" +
"<BASEFONT COLOR=GREEN>In an old run down miners shack east of here near the water!<BR><BR>" +
						     "</BODY>", false, true);
			

                                               AddButton(163, 385, 247, 248, 0, GumpButtonType.Reply, 0);
			AddItem(23, 66, 10248, 24);
			AddItem(327, 66, 10248, 33);
			
			

			

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
               from.SendMessage( "The child almost starts to cry" );
               break; 
            } 

         }
      }
   }
}