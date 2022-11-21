using System;
using Server;
using Server.Commands;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	public class WorldGold
	{
		public static void Initialize()
		{
			CommandSystem.Register( "WorldGold", AccessLevel.Seer, new CommandEventHandler( WorldGold_OnCommand ) );
		}

		private static void WorldGold_OnCommand( CommandEventArgs args )
		{
			float goldcount = GoldInWorld();
			string formatworld = "";
			int decimalworld = 0;

			if ( goldcount > 999999 )
			{
				goldcount = goldcount / 1000000;
				formatworld = " Million";
				decimalworld = 3;
			}
			else if( goldcount >= 1000 )
			{
				goldcount = goldcount / 1000;
				formatworld = " Thousand";
				decimalworld = 1;
			}

			args.Mobile.SendMessage( "There is " + String.Format( "{0:f" + decimalworld + "}", goldcount) + formatworld + " Gold in the world" );
		}

		public static float GoldInWorld()
		{
			float goldcount = 0;

			foreach( Item i in World.Items.Values )
				if( i is Gold )
					goldcount += ((Gold)i).Amount;
				else if( i is BankCheck )
					goldcount += ((BankCheck)i).Worth;
			return goldcount;
		}
	}
}