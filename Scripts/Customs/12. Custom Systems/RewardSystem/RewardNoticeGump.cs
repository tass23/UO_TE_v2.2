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
	public class RewardNoticeGump : Gump
	{
		private Mobile m_From;

		public RewardNoticeGump( Mobile from ) : base( 0, 0 )
		{
			m_From = from;

			from.CloseGump( typeof( RewardNoticeGump ) );

			AddPage( 0 );
         
            AddBackground(144, 206, 500, 135, 5054);
            AddAlphaRegion(154, 216, 476, 114);

			/* You have reward items available.
			 * Click 'ok' below to get the selection menu or 'cancel' to be prompted upon your next login.
			 */

            AddLabel(181, 229, 18, @"SERVER REWARD SYSTEM");
			AddHtmlLocalized( 374, 229, 240, 92, 1006046, true, true );

            AddImage(94, 191, 10400);
            AddImage(612, 191, 10410);

			AddButton( 168, 291, 4005, 4007, 1, GumpButtonType.Reply, 0 );
            AddLabel(201, 291, 2036, @"CONTINUE");
			//AddHtmlLocalized( 196, 289, 73, 17, 1006044, false, false ); // Ok

			AddButton( 283, 291, 4017, 4019, 0, GumpButtonType.Reply, 0 );
            AddLabel(315, 291, 2036, @"CANCEL");
			//AddHtmlLocalized( 319, 289, 48, 20, 1006045, false, false ); // Cancel
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 1 )
				m_From.SendGump( new RewardChoiceGump( m_From ) );
		}
	}
}