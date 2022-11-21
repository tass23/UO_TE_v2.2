using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Multis;

namespace Knives.Chat3
{
	
	public class InternalChatMessage
	{
		public static void SendInternalChatMsg( Mobile to, string subject, string message )
		{
			
			Data.GetData( to ).AddMessage(new Message( to, subject, message, MsgType.Normal));
                General.PmNotify( to );

                              
            if (Data.LogPms)
                Logging.LogPm(String.Format(DateTime.Now + " <Mail> System to {0}: {1}",  (to == null ? "All" : to.RawName), message));
 
            foreach( Data data in Data.Datas.Values)
            	if (data.Mobile.AccessLevel > AccessLevel.GameMaster )
                    data.Mobile.SendMessage(data.GlobalMC, String.Format("(Global) <Mail> System to {0}: {1}",  (to == null ? "All" : to.RawName), message ));
		
		}
		
		public static void SendFromInternalChatMsg( Mobile whofrom, Mobile to, string subject, string message )
		{
			Data.GetData( to ).AddMessage(new Message( whofrom, subject, message, MsgType.Normal));
                General.PmNotify( to );

                              
            if (Data.LogPms)
                Logging.LogPm(String.Format(DateTime.Now + " <Mail> System to {0}: {1}",  (to == null ? "All" : to.RawName), message));
 
            foreach( Data data in Data.Datas.Values)
            	if (data.Mobile.AccessLevel > AccessLevel.GameMaster )
                    data.Mobile.SendMessage(data.GlobalMC, String.Format("(Global) <Mail> System to {0}: {1}",  (to == null ? "All" : to.RawName), message ));
		}
		
	}
	
}


