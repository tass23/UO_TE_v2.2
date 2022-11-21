using System;
using System.IO;
using Server;
using Server.Commands;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
	public class GoldReport
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GoldReport", AccessLevel.Administrator, new CommandEventHandler( GoldReport_OnCommand ) );
		}

		private static void GoldReport_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "goldreport.log" ) )
			{
				float GoP = 0;

				foreach( Mobile MiW in World.Mobiles.Values )
				{
					float PG = 0;
					if( MiW is PlayerMobile )
					{
						PlayerMobile pm = (PlayerMobile)MiW;

						PG += SearchForGold( pm.BankBox );
						PG += SearchForGold( pm.Backpack );
					//	op.WriteLine( "{0}\t\t{1}", pm.Name, PG );
						op.WriteLine( "{0,-20}{1,-12}", pm.Name, PG );
						GoP += PG;
					}
				}
			}
			args.Mobile.SendMessage( "Gold Report done <runuo root>/goldreport.log" );

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