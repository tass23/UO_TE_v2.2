using System;
using Server;
using System.Collections.Generic;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class ProfessionGump : Gump 
   { 
      public static void Initialize() 
      { 
          CommandSystem.Register( "ProfessionGump", AccessLevel.GameMaster, new CommandEventHandler( ProfessionGump_OnCommand ) ); 
      } 

      private static void ProfessionGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ProfessionGump( e.Mobile ) ); 
      } 

      public ProfessionGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "New Haven Profession Guide" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=#DFDFC0>Welcome to New Haven! You seek professional help? You have come to the right place! To learn more about the profession training New Haven has to offer, seek out the following guildmasters:<BR><BR>Warrior: Alexander Dumas<BR>Locacation: The Warriors Guild Hall is the first building to the Northeast.<BR><BR>Mage: Pyronarro<BR>Location: The Mage School is a few buildings to the North.<BR><BR>Blacksmith: Tiny DuPont<BR>Location: The Blacksmith Shop is the building directly North of the New Haven Bank.<BR><BR>Necromancy: Malifnae<BR>Location: The Necromancy School is a little ways out of New Haven to the Northeast.<BR><BR>Paladin: Brahman<BR>Location: The Paladin training area is on the second floor of an adjacent building connected to the Warriors Guild Hall.<BR><BR>Samurai: Haochi<BR>Location: The Samurai profession can be learned in a building a little ways North of the Mage School.<BR><BR>Ninja: Yago<BR>Location: The Ninja profession can be learned in a building a little ways West of the Mage School.<BR><BR>Good journey to you. Hope you enjoy your stay in New Haven.</BASEFONT></BODY>", false, true);
			

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
               break; 
            } 

         }
      }
   }
}
