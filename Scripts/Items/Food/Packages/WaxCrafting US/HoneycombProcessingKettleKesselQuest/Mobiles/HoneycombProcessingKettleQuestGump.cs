using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class HoneycombProcessingKettleQuestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "HoneycombProcessingKettleQuestGump", AccessLevel.GameMaster, new CommandEventHandler( HoneycombProcessingKettleQuestGump_OnCommand ) ); 
      } 

      private static void HoneycombProcessingKettleQuestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new HoneycombProcessingKettleQuestGump( e.Mobile ) ); 
      } 

      public HoneycombProcessingKettleQuestGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Honeycomb Hunting" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=RED>The old man takes a good look at you.<BR><BR>'Hey, you there! You look like someone who's willing and able to help me!<BR>" +
"<BASEFONT COLOR=RED>I am a candle crafter. Well, I used to be. I'm too old to roam the woods and collect honeycombs out of beehives. Get some for me! You find honeycombs in beehives, of course, but the bears that live in the close area around the hives tend to steal them and have honeycombs, too!<BR>" +
"<BASEFONT COLOR=RED>You wont regret helping me! Get me five honeycombs and I'll give you one of these kettles. You can use them to sepparate honey and wax out of a honeycomb, so you can process it further!'" +
						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

//			AddLabel( 113, 135, 0x34, "" );
//			AddLabel( 113, 150, 0x34, "" );
//			AddLabel( 113, 165, 0x34, "" );
//			AddLabel( 113, 180, 0x34, "" );
//			AddLabel( 113, 195, 0x34, "" );
//			AddLabel( 113, 210, 0x34, "" );
//			AddLabel( 113, 235, 0x34, "" );
//			AddLabel( 113, 250, 0x34, "" );
//			AddLabel( 113, 265, 0x34, "" );
//			AddLabel( 113, 280, 0x34, "" );
//			AddLabel( 113, 295, 0x34, "" );
//			AddLabel( 113, 310, 0x34, "" );
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
               from.SendMessage( "Sucht mir die Honigwaben!" );
               break; 
            } 

         }
      }
   }
}