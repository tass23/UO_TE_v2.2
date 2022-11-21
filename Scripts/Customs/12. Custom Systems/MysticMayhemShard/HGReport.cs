using System;
using System.IO;
using System.Collections;
using Server;
using Server.Commands;
using Server.Network;
using Server.Guilds;
using Server.Multis;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Misc
{
	public class HGReport
	{
		public static void Initialize()
		{
			CommandSystem.Register( "HGReport", AccessLevel.Administrator, new CommandEventHandler( HouseReport_OnCommand ) );
		}

		private static void HouseReport_OnCommand( CommandEventArgs args )
		{
			using ( StreamWriter op = new StreamWriter( "HGReport.log" ) )
			{
				op.WriteLine( "Account           Character           Gold     Map      Where                 Guild" );

				foreach( Mobile MiW in World.Mobiles.Values )
				{
				//	string hyn = " ";
					float PG = 0;
					int xLong = 0, yLat = 0, xMins = 0, yMins = 0;
					bool xEast = false, ySouth = false;
					string location = " ";
					string mmap = " ";
					if( MiW is PlayerMobile )
					{
						PlayerMobile pm = (PlayerMobile)MiW;
						PG += SearchForGold( pm.BankBox );
						PG += SearchForGold( pm.Backpack );
						Account acct = pm.Account as Account;
						ArrayList list = GetHouses( pm );

					/*	if ( list.Count == 0 )
						{ 
							hyn = "---";
						}
						else
						{
							hyn = "Yes";
						}  */

				
						if ( list.Count == 1 )
						{

							BaseHouse sel = (BaseHouse)list[0];

							Map map = sel.Map;
							mmap = map.Name;
							bool valid = Sextant.Format( sel.Location, map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth );

							if ( valid )
						  	location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
						}  
try{
TimeSpan ts = pm.GameTime;

string gt = String.Format( "{0:D3}.{1:D2}:{2:D2}:{3:D2}", ts.Days, ts.Hours % 24, ts.Minutes % 60, ts.Seconds % 60 );
                  Guild g = pm.Guild as Guild;

			if ( g != null )
			{
//				op.WriteLine( "{0}\t{1,-18}{2,-20}{3,-25}\t{4,-9}\t{5,-9}{6}", hyn, acct, pm.Name, pm.Guild.Name, PG, mmap, location );
				op.WriteLine( "{0,-18}{1,-20}{2,-9}{3,-9}{4,-22}{5,-15} {6,-6}{7}", acct, pm.Name, PG, mmap, location, gt, pm.Guild.Abbreviation, pm.Guild.Name );
			}
			else
			{
				op.WriteLine( "{0,-18}{1,-20}{2,-9}{3,-9}{4,-22}{5,-15}", acct, pm.Name, PG, mmap, location, gt );
			}
}
catch{}
					}
				}
			}
			args.Mobile.SendMessage( "House Report done <runuo root>/HGReport.log" );

		}
		public static float SearchForGold( Container c )
		{
			float goldcount = 0;
		   try
		   {
			//float goldcount = 0;

			foreach( Item i in c.Items )
				if( i is Container )
					goldcount += SearchForGold( (Container)i );
				else if( i is Gold )
					goldcount += ((Gold)i).Amount;
				else if( i is BankCheck )
					goldcount += ((BankCheck)i).Worth;

		   }
		   catch {  }
			return goldcount;
		}
		public static ArrayList GetHouses( Mobile owner )
		{
			ArrayList list = new ArrayList();

			Account acct = owner.Account as Account;
		   try
		   {
			//ArrayList list = new ArrayList();

			//Account acct = owner.Account as Account;

			if ( acct == null )
			{
				list.AddRange( BaseHouse.GetHouses( owner ) );
			}
			else
			{
				for ( int i = 0; i < acct.Length; ++i )
				{
					Mobile mob = acct[i];

					if ( mob != null )
						list.AddRange( BaseHouse.GetHouses( mob ) );
				}
			}
			
		   }
		   catch {  }
			return list;
		}
	}
}