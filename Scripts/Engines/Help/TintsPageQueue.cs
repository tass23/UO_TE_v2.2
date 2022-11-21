//////////////////////////////////////////////////////////
// Tintamar's Page In Queue								//
// Insted of sending a message that most GMs miss		//
// it will send a gump showing that someone paged		//
// also shows the time that the last page was recieved	//
//////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using Server;
using Server.Mobiles;
using Server.Gumps; 
using Server.Network;
using Server.Misc;
using Server.Accounting;
using Server.Engines.Help;

namespace Server.Gumps
{ 
	public class TintamarsPageInQueue : Gump 
	{ 
		public TintamarsPageInQueue( Mobile owner ) : base( 180, 50 ) 
		{ 
			this.Closable=false;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.AddPage(0);
			this.AddBackground(0, 0, 241, 93, 2620);
			this.AddHtml( 7, 10, 227, 25, @"<CENTER>There Is A Page In Queue", true, false);
			this.AddHtml( 7, 58, 227, 25, @"<CENTER>" + DateTime.Now, true, false);
			this.AddButton(90, 35, 247, 248, 1, GumpButtonType.Reply, 0);
			this.AddImage(18, 39, 57);
			this.AddImage(193, 39, 59);
      	} 

		public override void OnResponse( NetState state, RelayInfo info )
		{ 
			Mobile from = state.Mobile; 

			switch ( info.ButtonID ) 
			{ 
				case 0: break;
				case 1: from.SendGump(new PageQueueGump()); break; 
			}
		}
	}
}