using System;
using Server;
using Server.Commands;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	public class PlayerGold
	{
		public static void Initialize()
		{
			CommandSystem.Register( "PlayerGold", AccessLevel.GameMaster, new CommandEventHandler( PlayerGold_OnCommand ) );
		}

		private static void PlayerGold_OnCommand( CommandEventArgs args )
		{
			float goldcount = GoldOnPlayers( args.Mobile );
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

			args.Mobile.SendMessage( "There is " + String.Format( "{0:f" + decimalworld + "}", goldcount) + formatworld + " Gold owned by players." );
		}

		public static float GoldOnPlayers( Mobile m )
		{
			float GoP = 0;

			foreach( Mobile MiW in World.Mobiles.Values )
			{
				if( MiW is PlayerMobile )
				{
					PlayerMobile pm = (PlayerMobile)MiW;

					GoP += SearchForGold( pm.BankBox );
					GoP += SearchForGold( pm.Backpack );
					m.SendMessage( "Checking " + pm.Name );
				}
			}
			return GoP;
		}

		public static float SearchForGold( Container c )
		{
			float goldcount = 0;

			foreach( Item i in c.Items )
				if( i is Container )
					goldcount += SearchForGold( (Container)i );
				else if( i is Gold )
					goldcount += ((Gold)i).Amount;
				else if( i is BankCheck )
					goldcount += ((BankCheck)i).Worth;
			return goldcount;
		}
	}
}