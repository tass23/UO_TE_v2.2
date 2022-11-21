using System;
using Server;
using Server.Network;
using Server.Gumps;

namespace Server.Gumps
{
	public class BountyDetailsGump : Gump
	{
		private Mobile m_Wanted;
		private int m_Reward;
		private DateTime m_Expires;

		public BountyDetailsGump( Mobile wanted, int reward, DateTime expires ) : base( 75, 75 )
		{
			m_Wanted = wanted;
			m_Reward = reward;
			m_Expires = expires;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(28, 29, 417, 325, 9380);
			AddLabel(55, 33, 1149, @"A Bounty Contract");

			string body = String.Format( "{0} is wanted for the horrible crime of murder, {0} has committed {1} murder(s) and a bounty has been placed on {0}'s head for the sum of {2} gold coins. If you can hunt down and kill {0}, bring the head of this swine to your local bounty officer to collect your reward.", m_Wanted.Name, m_Wanted.Kills, m_Reward );

			AddHtml( 53, 69, 367, 184, body, (bool)true, (bool)true);

			AddLabel(55, 260, 1160, @"Wanted: " + m_Wanted.Name.ToString() );
			AddLabel(55, 280, 1160, @"Reward: " + m_Reward.ToString() );
			AddLabel(55, 300, 1160, @"Expires: " + m_Expires.ToLongDateString() );
		}

		public override void OnResponse( NetState state, RelayInfo info ) 
      	{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        	if ( info.ButtonID == 0 ) // Close
         	{
				from.SendGump( new BountyHunterGump( from, 0, null, null ) );
			}
		}
	}
}