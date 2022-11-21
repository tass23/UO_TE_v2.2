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
	public class CityDisbandCityGump : Gump
	{
		private CityManagementStone m_Stone;

		public CityDisbandCityGump( CityManagementStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(32, 27, 277, 65, 5120);
			AddLabel(41, 30, 1149, @"Are you sure you want to disband the city?");
			AddButton(186, 55, 247, 248, 2, GumpButtonType.Reply, 0);
			AddButton(85, 55, 242, 243, 1, GumpButtonType.Reply, 0);

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 )
         		{
				from.SendGump( new CityManagementGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 2 )
         		{
				if ( m_Stone.Citizens != null )
				{
					foreach ( Mobile m in m_Stone.Citizens )
					{
						PlayerMobile pm = (PlayerMobile)m;
						m.SendMessage( 53, "The mayor has disbaned the city." );

						pm.City = null;
						pm.CityTitle = null;
						pm.ShowCityTitle = false;
					}
				}

				if ( m_Stone.isLockedDown != null )
				{
					foreach( Item ld in m_Stone.isLockedDown )
					{
						ld.Movable = true;
					}
				}

				m_Stone.Delete();
			}
		}
	}
}