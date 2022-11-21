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
	public class ConfirmLeaveCityGump : Gump
	{
		private CityManagementStone m_Stone;

		public ConfirmLeaveCityGump( CityManagementStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(32, 27, 267, 65, 5120);
			AddLabel(42, 30, 1149, @"Are you sure you want to leave the city?");
			AddButton(175, 55, 247, 248, 2, GumpButtonType.Reply, 0);
			AddButton(85, 55, 242, 243, 1, GumpButtonType.Reply, 0);

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
				from.SendGump( new CityCitizenGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 2 )
         		{
				PlayerMobile pm = (PlayerMobile)from;
				pm.City = null;
				pm.CityTitle = null;
				pm.ShowCityTitle = false;

				m_Stone.Citizens.Remove( from );
				from.SendMessage( "You have left the city." );

				foreach ( Mobile m in m_Stone.Citizens )
				{
					m.SendMessage( "CITY MESSAGE: {0} has left the city.", from.Name );
				}
			}
		}
	}
}