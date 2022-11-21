//This had no header on it when I found it, but credit goes to whomever wrote the suggestion box script.  I yanked it to go with our bots.  Thank you!
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
    public class Suggestion : Gump
    {
        public Suggestion()
            : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);
            this.AddBackground(256, 207, 298, 287, 9200);
            this.AddLabel(309, 224, 0, @"Suggestion Box:");//Sugestion
            this.AddButton(377, 464, 247, 248, (int)Buttons.Button1, GumpButtonType.Reply, 0);
            this.AddButton(477, 464, 242, 248, (int)Buttons.Cancel, GumpButtonType.Reply, 0);
            this.AddAlphaRegion(280, 251, 251, 202);
            this.AddImage(450, 466, 5411);
            this.AddImage(347, 466, 5411);
            this.AddTextEntry(280, 252, 253, 202, 0, (int)Buttons.TextEntry1, @"");

        }

        public enum Buttons
        {
            Cancel,
            Button1,
            TextEntry1,
          
        }


        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;
			Account acct = (Account)from.Account;
		    
            switch ( info.ButtonID )
			{
				
                		case (int)Buttons.Cancel:
				{
					from.SendMessage("You decide not to send a suggestion.");
					break;
                }
                
                case (int)Buttons.Button1:
						string tudo = (string)info.GetTextEntry( (int)Buttons.TextEntry1 ).Text;
					
						Console.WriteLine("");
                        Console.WriteLine("{0} From Account {1} Sent a suggestion", from.Name, acct.Username);//from.Name of account send a suggestion
						Console.WriteLine("");
						
						if ( !Directory.Exists( "Suggestions" ) ) //create directory
							Directory.CreateDirectory( "Suggestion" );
						
						using ( StreamWriter op = new StreamWriter("Suggestion/suggestions.txt", true) )  //Suggestions get saved to this file.
						{	
							op.WriteLine("");
							op.WriteLine("Name Of Character: {0}, Account:{1}",from.Name,acct.Username);
							op.WriteLine("Message: {0}", tudo);
							op.WriteLine("");	
						}
						
						
						from.SendMessage("Your suggestions mean a lot to us, thank you for the input!");//thanks to send your suggestion
						
						break;
					
					
			}
			
			
		}		
		
		
	}
    }
