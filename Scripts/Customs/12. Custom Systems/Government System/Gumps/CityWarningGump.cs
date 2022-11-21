using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class CityWarningGump : Gump
	{
		public CityWarningGump() : base( 50, 50 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(14, 15, 455, 338, 5120);
			AddImageTiled(19, 45, 447, 10, 5121);
			AddHtml( 23, 20, 438, 19, @"<BASEFONT COLOR=WHITE><CENTER>Message From The Britannia Housing Commission</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddAlphaRegion(22, 52, 439, 290);
			AddHtml( 22, 52, 439, 290, String.Format( "<BASEFONT COLOR=WHITE><CENTER><U>Congratulations on founding your city</U><BR><BR> Your new city application is now under review by the housing commission, You have 24 hours to get at least {0} citizens in your city, If you fail to get the required citizens before the 24 hour deadline your city application will be rejected and your city removed. You will not be able to reclaim your city hall deed back.<BR><BR> You should go access your city management stone and change your cities name, and other features. You will need to enable housing within the city before others can place houses in your city. You should also add some funds to your city treasury, Each time your city updates (Once in 24 hours and every {1} days after that) your city will be charged some money in maintenance.<BR><BR> You should also run for office on your cites voting stone if you plan to say mayor over this city. Your city has an election every {2} days.<BR><BR>Some city commands to know<BR>I wish to lock this down // Locks down decore in city<BR>I wish to release this // Releases decore in city<BR>I ban thee // Bans a person from city<BR><BR>Please note that banning someone will not keep them from entering the city, but they will be attack able by any member of the city while the banned person is in the city.<BR><BR>If you have any other questions you should review the help menu via saying [GovHelp in game.<BR><BR>Again congratulations on your new city.<BR><BR>-Ronin- Britannia Housing Commission</CENTER></BASEFONT>", PlayerGovernmentSystem.Level1.ToString(), PlayerGovernmentSystem.CityUpdate.Days.ToString(), PlayerGovernmentSystem.VoteUpdate.Days.ToString() ), (bool)false, (bool)true);

		}
		

	}
}
