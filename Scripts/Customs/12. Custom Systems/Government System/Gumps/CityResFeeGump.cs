using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class CityResFeeGump : Gump
	{
		private CityResurrectionStone m_Stone;

		public CityResFeeGump( CityResurrectionStone stone ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(17, 12, 300, 139, 5120);
			AddItem(7, 15, 2);
			AddItem(28, 15, 3);
			AddHtml( 76, 18, 234, 66, @"<BASEFONT COLOR=WHITE><CENTER>This city has a resurrection fee.<BR>Would you like to resurrect here?</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(29, 92, 1149, @"Cost: " + stone.Sign.Stone.ResFee.ToString() );
			AddButton(82, 115, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(190, 115, 241, 242, 2, GumpButtonType.Reply, 0);

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Accept
         		{
				if ( Banker.Withdraw( from, m_Stone.Sign.Stone.ResFee ) )
				{
					m_Stone.Sign.Stone.CityTreasury += m_Stone.Sign.Stone.ResFee;
					Ankhs.Resurrect( from, m_Stone );
				}
				else
				{
					from.SendMessage( "You do not have enough money in your back account." );
				}
			}
		}
	}
}