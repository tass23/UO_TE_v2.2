using System;
using Server;
using Server.Items; 
using Server.Gumps; 
using Server.Mobiles;
using Server.Targeting;
using System.Collections;
using Server.Commands.Generic;

namespace Server.Commands
{
	public class GovHelpCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GovHelp", AccessLevel.Player, new CommandEventHandler( GovHelp_OnCommand ) );
		}

		[Usage( "GovHelp" )]
		[Description( "Displaies Help Menu For FS Government System" )]
		private static void GovHelp_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new GovHelpGump() );
		}
	}
}
