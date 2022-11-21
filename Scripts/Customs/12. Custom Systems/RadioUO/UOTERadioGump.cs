using System; 
using Server;
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class UOTERadioGump : Gump 
	{ 
		public static void Initialize() 
		{ 
			CommandSystem.Register( "UOTERadioGump", AccessLevel.GameMaster, new CommandEventHandler( UOTERadioGump_OnCommand ) ); 
		} 

		private static void UOTERadioGump_OnCommand( CommandEventArgs e ) 
		{ 
			e.Mobile.SendGump( new UOTERadioGump( e.Mobile ) ); 
		} 

		public UOTERadioGump( Mobile owner ) : base( 50,50 ) 
		{
		    this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
//----------------------------------------------------------------------------------------------------

			AddPage( 0 );
			AddImageTiled( 233, 406, 142, 19, 2627 );

//--------------------------------------Window size bar--------------------------------------------
			
			AddImage(213, 197, 2623);
			AddImage(368, 196, 2625);
			AddImage(213, 406, 2626);
			AddImage(368, 406, 2628);
			AddImage(212, 148, 10452, 1084);
			AddImageTiled(230, 197, 149, 211, 2624);
			AddLabel(251, 201, 1165, @"UO-The Expanse");
			AddLabel(247, 225, 1074, @"The BEST Rock");
			AddLabel(245, 244, 1074, @"on the 'Net!");
			AddLabel(332, 302, 1153, @"Tune In");
			AddLabel(222, 302, 1153, @"Cancel");
			AddButton(348, 320, 5540, 5541, 0, GumpButtonType.Reply, 0);
			AddButton(229, 320, 5537, 5538, 1, GumpButtonType.Reply, 0);
			AddLabel(225, 342, 1153, @"Clicking Tune In will open");
			AddLabel(225, 360, 1153, @"a playlist in your default");
			AddLabel(230, 378, 1153, @"MP3 player if you open");
			AddLabel(232, 396, 1153, @"it first before clicking.");
			AddButton(285, 302, 4029, 4030, 2, GumpButtonType.Reply, 0);
			AddLabel(259, 280, 1090, @"UO-The Expanse Website");

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
					from.LaunchBrowser( "http://www.uoexpanse.com" );
					break; 
				} 

				case 1: //Case uses the ActionIDs defined above. Case 0 defines the actions for the button with the action id 0 
				{ 
				   break; 
				}
				
				case 2: //Case uses the ActionIDs defined above. Case 0 defines the actions for the button with the action id 0 
				{
					from.LaunchBrowser( "http://www.uoexpanse.com" );
					break; 
				}
			}
		}
	}
}