using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Commands
{
	public class Status
	{		
 		public static void Initialize() 
   		 { 
   		   CommandSystem.Register( "Totals", AccessLevel.Player, new CommandEventHandler( Status_OnCommand ) ); 
  		  } 

		[Usage( "Totals" )]
		[Description( "Allows players to view current session online time, when they joined fame karma. Players just say [totals in-game." )]

 		public static void Status_OnCommand( CommandEventArgs e ) 
    		{ 
		Mobile m = e.Mobile; 
	
			if( m is PlayerMobile )
			{
				PlayerMobile from = (PlayerMobile)m;
                from.SendGump( new SFTotalsGump(from) ); 
			}
		}

	}

}