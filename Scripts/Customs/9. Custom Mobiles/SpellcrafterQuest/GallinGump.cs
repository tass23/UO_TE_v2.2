using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class GallinGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "GallinGump", AccessLevel.GameMaster, new CommandEventHandler( GallinGump_OnCommand ) ); 
      } 

      private static void GallinGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new GallinGump( e.Mobile ) ); 
      } 

      public GallinGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Gallin watches you while approaching him, gripping his staff tighter.  After explaining what happened, Gallin looks at you carefully." +
"<BASEFONT COLOR=YELLOW>I suppose it was the very rare spellcrafting gem that was stolen from me by Delhi, my ex-apprentice.  He has gone too far this time.<BR>" +
"<BASEFONT COLOR=YELLOW>He swore, one day he would be more powerful than me; with that stone, it will be true.<BR><BR>Lately, he has been selling servants to thieves and brigands.<BR>" +
"<BASEFONT COLOR=YELLOW>Last I heard, he was North of Britain, selling to a gorup of Brigands.<BR><BR>Please help me put an end to his evil and get my gem back!<br><br>" +
"<BASEFONT COLOR=YELLOW>If you return the gem,  I shall give you a spell crafting book for your troubles.<BR><BR>Seek him out.<BR>Bring my gem back, Please!" +

						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

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
               from.SendMessage( "If you do not get the gem from him, who knows what kind of evil he will cause with it." );
               break; 
            } 

         }
      }
   }
}
