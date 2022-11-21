  //
 //  Written by Haazen Mar 2007
//
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
//using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class SecretMapGump : Gump
	{
	//	private TMapBook m_Book;

	//	public TMapBook Book{ get{ return m_Book; } }




		public SecretMapGump( Mobile from ) : base( 25, 25 )
		{


			Resizable = false;

			AddPage(0);
			AddBackground(0, 0, 200, 250, 5170);
		//	AddImage(59, 63, 5528); //59,63

		//	AddImage(x, y, 2360);
			AddImage(120, 175, 2529); // North

			AddImage(30, 150, 5609); // world
			AddImage(50, 170, 9009); //2530); // pin
			AddItem(110, 125, 3840);

			AddLabel(30 , 35, 0x57, "This map is Secret");
			AddLabel(30 , 60, 0x57, "Only a Genie can read");
			AddLabel(30, 85, 0x57, "the invisible ink herein.");


		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;


				return;


 
		}

	}
}