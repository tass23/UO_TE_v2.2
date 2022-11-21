using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class NoCitiesGump : Gump
	{
		public NoCitiesGump() : base( 50, 50 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(20, 27, 413, 112, 5120);
			AddImageTiled(24, 58, 408, 10, 5121);
			AddBackground(33, 66, 389, 62, 5120);
			AddHtml( 27, 32, 400, 23, @"<BASEFONT COLOR=WHITE><CENTER>City Travel System</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 42, 74, 371, 43, @"<BASEFONT COLOR=WHITE><CENTER>No registered cities to display</CENTER></BASEFONT>", (bool)false, (bool)false);
		}
	}
}