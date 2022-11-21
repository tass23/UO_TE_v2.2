using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
	public class AcceptJoinGump : Gump
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public AcceptJoinGump( CityManagementStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;
			m_From = from;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(59, 80, 353, 363, 5120);
			AddImageTiled(63, 104, 348, 9, 5121);
			AddLabel(66, 82, 1149, @"You are being offered to join this city.");
			AddLabel(70, 110, 1149, @"City Name: " + stone.CityName.ToString() );
			AddLabel(70, 130, 1149, @"Mayor: " + stone.Mayor.Name.ToString() );
			AddLabel(70, 150, 1149, @"Current Income Tax: " + stone.IncomeTax.ToString() );
			AddLabel(70, 170, 1149, @"Current Property Tax: " + stone.HousingTax.ToString() );
			AddLabel(70, 190, 1149, @"Current Travel Tax: " + stone.TravelTax.ToString() );
			AddHtml( 70, 241, 333, 131, stone.CityRules, (bool)true, (bool)true);
			AddLabel(70, 220, 1149, @"City Rules");
			AddImageTiled(62, 401, 348, 9, 5121);
			AddLabel(70, 375, 1149, @"City URL: " + stone.CityWebURL.ToString() );
			AddButton(70, 409, 4008, 4009, 1, GumpButtonType.Reply, 0);
			AddLabel(104, 409, 1149, @"I accept and wish to join.");
			AddButton(270, 409, 4017, 4018, 2, GumpButtonType.Reply, 0);
			AddLabel(305, 409, 1149, @"I Decline");
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
				m_Stone.DoJoin( m_From, from );
			}

        		if ( info.ButtonID == 2 )
         		{
				m_From.SendMessage( "{0} has declined your offer.", from.Name );
				from.SendMessage( "You decline {0}'s offer.", m_From.Name );
			}
		}
	}
}