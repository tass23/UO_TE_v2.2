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
	public class FindCitiesCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "FindCities", AccessLevel.GameMaster, new CommandEventHandler( FindCities_OnCommand ) );
		}

		[Usage( "FindCities" )]
		[Description( "Locates all cities on all facets." )]
		private static void FindCities_OnCommand( CommandEventArgs e )
		{
			e.Mobile.SendGump( new FindCitiesGump( 0, null, null ) );
		}
	}
}
