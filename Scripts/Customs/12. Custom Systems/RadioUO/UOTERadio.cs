using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
	public class Radio 
	{ 
		public static void Initialize() 
		{ 
			CommandSystem.Register( "Radio", AccessLevel.Player, new CommandEventHandler( Radio_OnCommand ) ); 
		} 

		private static void Radio_OnCommand( CommandEventArgs e ) 
		{ 
			e.Mobile.SendGump( new UOTERadioGump( e.Mobile ) ); 
		} 
	}
}

