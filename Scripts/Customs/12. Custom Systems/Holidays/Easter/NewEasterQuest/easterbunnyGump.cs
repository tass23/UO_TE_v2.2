using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class easterbunnyquestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "easterbunnyquestGump", AccessLevel.GameMaster, new CommandEventHandler( easterbunnyquestGump_OnCommand ) ); 
      } 

      private static void easterbunnyquestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new easterbunnyquestGump( e.Mobile ) ); 
      } 
       
        

       
        

      public easterbunnyquestGump( Mobile owner ) : base( 50,50 ) 
      { 
                  {
                                                   this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			
			AddImageTiled(14, 9, 382, 433, 2524);
			AddImageTiled(8, 7, 388, 7, 40);
			AddImageTiled(11, 433, 382, 9, 40);
			AddImage(13, 18, 3005, 1152);
			AddImage(391, 188, 3003, 1152);
			AddImage(13, 187, 3005, 1152);
			AddImage(392, 16, 3003, 1152);
			AddTextEntry(83, 25, 170, 20, 25, 0, @"        Happy Easter!");
			AddTextEntry(75, 55, 200, 20, 147, 0, @"The Easter Egg Quest!");
			AddItem(31, 46, 2448);
			AddItem(32, 28, 8485, 143);
			AddItem(34, 43, 3167, 60);
			AddItem(33, 48, 2485, 20);
			AddImageTiled(15, 421, 376, 12, 50);
			AddItem(280, 52, 2485, 2578);
			
 
			AddHtml( 32, 91, 341, 285,"<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=GREEN>The rabbit look's at you and starts to talk<BR>" +
"<BASEFONT COLOR=GREEN>Hello giant, Happy Easter!<BR><BR>" +
"<BASEFONT COLOR=GREEN>I'm sick! So I paid some bunnies to do my job and give easter eggs to the children<BR>" +
"<BASEFONT COLOR=GREEN>These bunnies that I hired are really stupid...<BR>" +
"<BASEFONT COLOR=GREEN>All they did was eat all the chocolate eggs and then ran off with my cash...<BR>" +
"<BASEFONT COLOR=GREEN>So all you have to do is, find a dyed easter egg and to give it to the easter child.<BR>" +
"<BASEFONT COLOR=GREEN>And to make sure that you have not cheated<BR>" +
"<BASEFONT COLOR=GREEN>you must bring me the chocolate dust of the egg!<BR>"+
"<BASEFONT COLOR=GREEN>This last child wanders around outside the town of Zento...<BR>" +
						     "</BODY>", false, true);
			

		

			AddButton( 171, 388, 247, 248, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
     }
      } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Cancel 
               from.SendMessage( "The easter bunny shows you the pile of eggs next to him" );
               break; 
            } 

         }
      }
   }
}