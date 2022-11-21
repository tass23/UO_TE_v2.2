using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;

#region About This Reward System
/*
 >>>>>>>>>>>>>>>>>>>>EDITED BY: A.A.R (AKA SythenSE)<<<<<<<<<<<<<<<<<<<<<
 This Reward System Will Replace The Default EA/OSI Veteran Reward System.
 It Is Genericly Named So That Admins Can Use It On Their Servers As-Is.
 I Did Not Code This System - Its The Same Default Veteran Reward System.
 All I Did Was Rename It To RewardSystem And Give It New Gumps I Designed.
 
 Please Note: All Reward Items Will Need To Be Edited So That References
 Made To 'VeteranReward' Are Changed To 'RewardSystem'. I Did This So You
 Can Use This System For More Than Just Veteran Rewards Making It Generic.
*/
#endregion About This Reward System

namespace Server.Engines.RewardSystem
{
	public class RewardConfirmGump : Gump
	{
		private Mobile m_From;
		private RewardEntry m_Entry;

		public RewardConfirmGump( Mobile from, RewardEntry entry ) : base( 0, 0 )
		{
			m_From = from;
			m_Entry = entry;

			from.CloseGump( typeof( RewardConfirmGump ) );

			AddPage( 0 );
            AddBackground(144, 206, 500, 135, 5054);
            AddAlphaRegion(154, 216, 476, 114);

			if ( entry.NameString != null )
                AddHtml(211, 259, 150, 35, "<BASEFONT COLOR=#B3B8BC>" + entry.NameString, false, false);
			else
				AddHtmlLocalized( 211, 259, 150, 35, entry.Name, false, false );

            AddHtml(374, 229, 240, 92, @"Are You Sure You Want To Select This Reward Item? If So, Please Click The 'Get Item' Button To Confirm. If Not, Please Click The 'Back' Button To Start Over. If You Do Not Recieve Your Selection After Confirming The Item You Want, Then Please Email Our Support Staff From Our Server's Website With Your Account Name, Character, And A Description Of The Issue That You Are Having.", (bool)true, (bool)true);

			AddButton( 168, 291, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddLabel(201, 291, 2036, @"GET ITEM");

			AddButton( 283, 291, 4017, 4019, 0, GumpButtonType.Reply, 0 );
            AddLabel(315, 291, 2036, @"BACK");

            AddLabel(181, 229, 18, @"SERVER REWARD SYSTEM");
            AddImage(94, 191, 10400);
            AddImage(612, 191, 10410);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 1 )
			{
				if ( !RewardSystem.HasAccess( m_From, m_Entry ) )
					return;

				Item item = m_Entry.Construct();

				if ( item != null )
				{
					if ( item is Server.Items.RedSoulstone )
						((Server.Items.RedSoulstone) item).Account = m_From.Account.Username;	
					
					if ( RewardSystem.ConsumeRewardPoint( m_From ) )
						m_From.AddToBackpack( item );
					else
						item.Delete();
				}
			}

			int cur, max;

			RewardSystem.ComputeRewardInfo( m_From, out cur, out max );

			if ( cur < max )
				m_From.SendGump( new RewardNoticeGump( m_From ) );
		}
	}
}