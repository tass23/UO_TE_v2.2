/*
Script Name: playerwealth.cs
Author: CEO
Version: 1.0
Purpose: Registers command to create an html report of all player's wealth on shard.

Description: Creates playerwealth.html in your shard's root folder. This file contains a
	breakdown of all player's wealth on your shard by account. The report 'walks' player's
	backpacks and bank boxes as well as items in houses they own recording all gold and checks 
	for the account.
 
    A nice color-coded report is then produced allowing you to see who the tycoons are on your shard!

Installation: Install your custom scripts folder and restart.

Usage: ]playerwealth
*/
using System;
using System.IO;
using Server;
using Server.Accounting;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Commands;

namespace Server.Commands
{
	public class PlayerWealth
	{
		public static void Initialize()
		{
			CommandSystem.Register("PlayerWealth", AccessLevel.Owner, new CommandEventHandler(PlayerWealth_OnCommand));
		}

		public struct AccountInfo
		{
			public Account acct;
			public WealthInfo wealth;
			public long GrandTotal;
		}

		public struct WealthInfo
		{
			public long TotalGold;
			public long TotalChecks;
		}

		[Usage("PlayerWealth")]
		[Description("Creates the file playerwealth.html, which is a report of all gold/checks for players on shard.")]
		public static void PlayerWealth_OnCommand(CommandEventArgs e)
		{
			string filename = "playerwealth.html";
			ArrayList AccountWealth = new ArrayList();
			AccountInfo A;
			A.GrandTotal = 0;
			WealthInfo backpack;
			WealthInfo bank;
			WealthInfo home;
			Mobile m = e.Mobile;
			long shardtotal = 0;
			long totalnotreportedamt = 0;
			uint totalhomes = 0;
			uint totalnotreported = 0;
			uint totalaccounts = 0;
			uint totalchars = 0;
			double percent = 0;

			foreach (Account a in Accounts.GetAccounts())
			{
				if (a != null && a.Username != null)
				{
					totalaccounts++;
					A.acct = a;
					A.wealth.TotalGold = 0;
					A.wealth.TotalChecks = 0;
					backpack.TotalGold = 0;
					backpack.TotalChecks = 0;
					bank.TotalGold = 0;
					bank.TotalChecks = 0;
					home.TotalGold = 0;
					home.TotalChecks = 0;
					for (int i = 0; i < a.Length; i++) // First record gold in player's bank/backpack
					{
						Mobile cm = a[i];

						if (cm == null)
							continue;
						totalchars++;
						if (cm.Backpack != null)
							backpack = SearchContainer(cm.Backpack);
						if (cm.BankBox != null)
							bank = SearchContainer(cm.BankBox);
						A.wealth.TotalGold += backpack.TotalGold;
						A.wealth.TotalChecks += backpack.TotalChecks;
						A.wealth.TotalGold += bank.TotalGold;
						A.wealth.TotalChecks += bank.TotalChecks;
					}
					List<BaseHouse> allHouses = new List<BaseHouse>(2);
					for (int i = 0; i < a.Length; ++i) // Now houses they own
					{
						Mobile mob = a[i];

						if (mob != null)
							allHouses.AddRange(BaseHouse.GetHouses(mob));
					}
					for (int i = 0; i < allHouses.Count; ++i)
					{
						BaseHouse house = allHouses[i];

						totalhomes++;
						foreach (IEntity entity in house.GetHouseEntities())
						{
							if (entity is Item && !((Item)entity).Deleted)
							{
								Item item = (Item)entity;
								if (item is Gold)
									home.TotalGold += item.Amount;
								else if (item is BankCheck)
									home.TotalChecks += ((BankCheck)item).Worth;
								else if (item is Container)
									home = SearchContainer((Container)item, home);
							}
							/*else  // Vendors gold belongs to???? Let's skip this...
							{
							} */
						}
					}
					A.wealth.TotalGold += home.TotalGold;
					A.wealth.TotalChecks += home.TotalChecks;
					A.GrandTotal = A.wealth.TotalGold + A.wealth.TotalChecks;
					shardtotal += A.GrandTotal;
					if (A.GrandTotal < 10000)
					{
						totalnotreportedamt += A.GrandTotal;
						totalnotreported++;
					}
					else
						AccountWealth.Add(A);
				}
			}
			AccountWealth.Sort(new SortArray());
			using (StreamWriter op = new StreamWriter(filename))
			{
				op.WriteLine("<html><body><strong>Player Wealth report generated on {0}</strong>", DateTime.Now);
				op.WriteLine("<br/><strong>Total Accounts: {0}</strong>", totalaccounts);
				op.WriteLine("<br/><strong>Total Characters: {0}</strong>", totalchars);
				op.WriteLine("<br/><strong>Total Houses: {0}</strong>", totalhomes);
				op.WriteLine("<br/><strong>Total Gold/Checks: {0: ##,###,###,###}</strong>", shardtotal);
				op.WriteLine("<br/><br/>");
				op.WriteLine("<table width=\"500\"  border=\"2\" bordercolor=\"#FFFFFF\" bgcolor=\"#DEB887\"<td colspan=\"5\"><div align=\"center\"><font color=\"#8B0000\" size=\"+2\"><strong>Player Wealth</strong></font></div></td>");
				op.WriteLine("<tr bgcolor=\"#667C3F\"><font color=\"#FFFFFF\" size=\"+1\"><td align=\"center\">Account</td><td width=\"100\" align=\"right\">Gold</td><td width=\"100\" align=\"right\">Checks</td><td width=\"100\" align=\"right\">Total</td><td width=\"100\" align=\"right\">% of shard</td></tr></font>");
				foreach (AccountInfo ai in AccountWealth)
				{
					percent = (double)ai.GrandTotal / (double)shardtotal * 100.00f;
					op.WriteLine("<tr bgcolor=\"#{5}\"><td align=\"center\">{0}</td><td align=\"Right\">{1: ##,###,###,##0}</td><td align=\"Right\">{2: ##,###,###,##0}</td><td align=\"Right\">{3: ##,###,###,##0}</td><td align=\"Right\">{4: ##0.00}%</td></tr></div>", ai.acct.Username, ai.wealth.TotalGold, ai.wealth.TotalChecks, ai.GrandTotal, percent, percent > 0.70 ? (percent > 3.5 ? "DC143C" : (percent > 1.0 ? "B22222" : "F08080")) : (percent < 0.25 ? (percent < 0.01 ? "7FFFD4" : "90EE90") : "F0E68C"));
				}
				percent = (double)totalnotreportedamt / (double)shardtotal * 100.00f;
				op.WriteLine("<tr bgcolor=\"#D3D3D3\"><td align=\"center\" colspan=\"3\">{0} accounts < 10000 not reported.</td><td align=\"Right\">{1: ##,###,###,##0}</td><td align=\"Right\">{2: ##0.00}%</td></tr>", totalnotreported, totalnotreportedamt, percent);
				op.WriteLine("</table></body></html>");
			}
			m.SendMessage("Total accounts processed: {0}", totalaccounts);
		}

		public static WealthInfo SearchContainer(Container pack, WealthInfo w)
		{
			WealthInfo w1;
			w1.TotalGold = 0;
			w1.TotalChecks = 0;
			w1 = SearchContainer(pack);
			w.TotalGold += w1.TotalGold;
			w.TotalChecks += w1.TotalChecks;
			return w;
		}

		public static WealthInfo SearchContainer(Container pack)
		{
			WealthInfo w;
			w.TotalGold = 0;
			w.TotalChecks = 0;
			if (pack == null)
				return w;
			List<Item> packlist = pack.Items;
			for (int i = 0; i < packlist.Count; ++i)
			{
				Item item = (Item)packlist[i];

				if (item != null && !item.Deleted)
				{
					if (item is Container)
						w = SearchContainer((Container)item, w);
					else if (item is Gold)
						w.TotalGold += item.Amount;
					else if (item is BankCheck)
						w.TotalChecks += ((BankCheck) item).Worth;
				}
			}
			return w;
		}

		private class SortArray : IComparer
		{
			public SortArray()
				: base()
			{
			}

			public int Compare(object x, object y)
			{
				if (x == null || y == null || x == y)
					return 0;
				return ((((AccountInfo)x).GrandTotal > ((AccountInfo)y).GrandTotal) ? -1 : 1);
			}
		}
	}
}