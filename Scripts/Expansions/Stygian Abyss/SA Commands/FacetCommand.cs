using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Commands
{
	public class FacetCommand
	{
		public static void Initialize()
		{ 
			CommandSystem.Register( "Facet", AccessLevel.Counselor, new CommandEventHandler( Facet_OnCommand ) );
		}
 
		public static void Facet_OnCommand( CommandEventArgs e )
		{
			Mobile m = e.Mobile;
			m.SendGump( new FacetGump( m ) );
		}
	}
}