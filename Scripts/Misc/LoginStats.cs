using System;
using Server.Network;
using Server.Gumps;
using Server.Engines.XmlSpawner2;
using Server.Mobiles;
using Server;

namespace Server.Misc
{
	public class LoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static void EventSink_Login( LoginEventArgs args )
		{
			int userCount = NetState.Instances.Count;
			int itemCount = World.Items.Count;
			int mobileCount = World.Mobiles.Count;

			Mobile m = args.Mobile;

			m.SendMessage( "Welcome, {0}! There {1} currently {2} user{3} online, with {4} item{5} and {6} mobile{7} in the world.",
				args.Mobile.Name,
				userCount == 1 ? "is" : "are",
				userCount, userCount == 1 ? "" : "s",
				itemCount, itemCount == 1 ? "" : "s",
				mobileCount, mobileCount == 1 ? "" : "s" );

				PlayerMobile pm = (PlayerMobile)m;

			#region Player Government
			if ( pm.OwesBackTaxes == true )
			{
				if ( pm.City != null )
				{
					if ( Banker.Withdraw( m, pm.BackTaxesAmount ) )
					{
						m.SendMessage( "You have paid your back taxes in full from the money in your bank account." );
						pm.City.CityTreasury += pm.BackTaxesAmount;
						pm.OwesBackTaxes = false;
						pm.BackTaxesAmount = 0;
					}
					else
					{
						int balance = Banker.GetBalance( m );
					
						if ( Banker.Withdraw( m, balance ) )
						{
							pm.City.CityTreasury += balance;
							pm.BackTaxesAmount -= 0;
							m.SendMessage( "You have made a payment on your back taxes of {0} you now owe {1} in back taxes.", balance, pm.BackTaxesAmount );
						}
					}
				}
				else
				{
					pm.OwesBackTaxes = false;
					pm.BackTaxesAmount = 0;
				}
			}
			#endregion
			
			XmlData a = (XmlData)XmlAttach.FindAttachment(m, typeof(XmlData), "FinishedTour");

            if (a != null && a.Data != "true")
            {
                m.SendGump(new TourGuideGump(m, 0));
                m.Frozen = true;
                m.Blessed = true;
				m.LightLevel = 0;
            }
            else if (a == null)
            {
                XmlAttach.AttachTo(m, new XmlData("FinishedTour", "false"));

                m.SendGump(new TourGuideGump(m, 0));
                m.Frozen = true;
                m.Blessed = true;
				m.LightLevel = 0;
            }

			#region Tintamar's Page In Queue
			if ( m.AccessLevel >= AccessLevel.Counselor )
			{
				Server.Engines.Help.PageQueue.Pages_OnCalled( m );
			}
			#endregion
		}
	}
}