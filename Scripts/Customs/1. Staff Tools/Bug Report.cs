using System;
using Server;
using System.IO;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;
using System.Collections;
using Server.Commands;
using System.Text;

namespace Server.Gumps
{
	public class BugReport : Gump
	{
		public BugReport()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(256, 207, 298, 287, 5054);
			this.AddLabel(353, 228, 0, @"Report Bugs:");//Sugestion
			this.AddButton(377, 464, 247, 248, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			this.AddAlphaRegion(280, 251, 251, 202);
			this.AddImage(450, 466, 5411);
			this.AddImage(347, 466, 5411);
			this.AddTextEntry(280, 252, 253, 202, 0, (int)Buttons.TextEntry1, @"");

		}
		
		public enum Buttons
		{
			Button1,
			TextEntry1,
		}
		
		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			Account acct = (Account)from.Account;
			
			switch ( info.ButtonID )
			{
				case (int)Buttons.Button1:
				string tudo = (string)info.GetTextEntry( (int)Buttons.TextEntry1 ).Text;
					
				Console.WriteLine("");
                Console.WriteLine("{0} From Account {1} Sent a bug report", from.Name, acct.Username);//from.Name of account send a bug report
				Console.WriteLine("");
						
				if ( !Directory.Exists( "Bug Reports" ) ) //create directory
					Directory.CreateDirectory( "Bug Reports" );
						
				using ( StreamWriter op = new StreamWriter("Bug Reports/bugreports.txt", true) )
				{	
						op.WriteLine("");
						op.WriteLine("Name Of Character: {0}, Account:{1}",from.Name,acct.Username);
						op.WriteLine("Message: {0}", tudo);
						op.WriteLine("");	
				}
						
				from.SendMessage("Thanks for reporting bugs!");//thanks to send your bug report
						
				break;		
			}
		}		
	}
}