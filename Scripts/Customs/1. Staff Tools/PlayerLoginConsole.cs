using System;
using System.IO;
using System.Text;
using Server;
using Server.Network;
using Server.Accounting;
using Server.Mobiles;

namespace Server.Misc
{
	public class PlayerLogin 
	{
		public static void Initialize()
		{
			EventSink.Login += new LoginEventHandler( Player_Login );	
		}
        private static void Player_Login( LoginEventArgs args )
        {
            Mobile m = args.Mobile;
            Account acct = m.Account as Account;
            NetState ns = m.NetState;                  
            Console.WriteLine( "Login: {0}: Account:{1}, Has logged in with character:{2} - {3}", ns, acct, m.Name, DateTime.Now);
        }
     }
}