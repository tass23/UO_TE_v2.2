using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class DeclareCityWarGump : Gump
	{
		private CityManagementStone m_Stone;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public DeclareCityWarGump( CityManagementStone stone, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
		{
			m_Stone = stone;
         		m_List = list;
         		m_ListPage = listPage;   
         		m_CountList = count;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(18, 15, 336, 243, 5120);
			AddImageTiled(25, 20, 323, 196, 2702);
			AddImageTiled(30, 24, 314, 187, 2604);
			AddAlphaRegion(25, 20, 323, 195);

         		if ( m_List == null )
			{
				ArrayList a = new ArrayList();

				foreach ( Item i in World.Items.Values )
				{
					if ( i is CityManagementStone )
						a.Add( i );
				}

				m_List = a;
			}

         		if ( listPage > 0 )
			{
				AddButton(30, 223, 4014, 4015, 1, GumpButtonType.Reply, 0);
				AddLabel(66, 223, 1149, @"Last Page");
			}

         		if ( (listPage + 1) * 7 < m_List.Count )
			{
				AddButton(145, 223, 4005, 4006, 2, GumpButtonType.Reply, 0);
				AddLabel(182, 224, 1149, @"Next Page");
			}

         		int k = 0;

         		for ( int i = 0, j = 0, index=((listPage*7)+k) ; i < 7 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index )
         		{
            			Item item = m_List[index] as Item;
				
				if ( item is CityManagementStone )
				{
					CityManagementStone citystone = (CityManagementStone)item;

					int offset = 30 + ( i * 25 );

					AddButton(35, offset, 4029, 4030, 100 + index, GumpButtonType.Reply, 0);
					AddLabel(75, offset, 1149, citystone.CityName.ToString() );
				}
			}
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 0 ) // Close
         		{
				from.SendGump( new CityManagementGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 1 ) // Previous page
         		{
         			if ( m_ListPage > 0 )
					from.SendGump( new DeclareCityWarGump( m_Stone, m_ListPage - 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID == 2 ) // Next page
         		{ 
         			if ( (m_ListPage + 1) * 7 < m_List.Count )
					from.SendGump( new DeclareCityWarGump( m_Stone, m_ListPage + 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID >= 100 ) // Declare War
         		{
				CityManagementStone stone = m_List[ info.ButtonID - 100 ] as CityManagementStone;

				if ( stone == m_Stone )
				{
					from.SendMessage( "You cannot declare war on your own city." );
				}
				else if ( stone.AllegiancesInvited.Contains( m_Stone ) || m_Stone.AllegiancesInvited.Contains( stone ) || m_Stone.AllegiancesDeclared.Contains( stone ) || stone.AllegiancesDeclared.Contains( m_Stone ) || m_Stone.Allegiances.Contains( stone ) || stone.Allegiances.Contains( m_Stone ) )
				{
					from.SendMessage( "You are allied with this city." );
				}
				else if ( stone.WarsInvited.Contains( m_Stone ) || m_Stone.WarsInvited.Contains( stone ) || m_Stone.WarsDeclared.Contains( stone ) || stone.WarsDeclared.Contains( m_Stone ) || m_Stone.Waring.Contains( stone ) || stone.Waring.Contains( m_Stone ) )
				{
					from.SendMessage( "You are already at war with this city." );
				}
				else
				{
					if ( !m_Stone.WarsDeclared.Contains( stone ) )
					{
						m_Stone.WarsDeclared.Add( stone );

						foreach ( Mobile m in m_Stone.Citizens )
						{
							m.SendMessage( "CITY MESSAGE: We have declared war against {0}.", stone.CityName );
						}
					}
			
					if ( !stone.WarsInvited.Contains( m_Stone ) )
					{
						stone.WarsInvited.Add( m_Stone );

						foreach ( Mobile m in stone.Citizens )
						{
							m.SendMessage( "CITY MESSAGE: {0} has declared war against us.", m_Stone.CityName );
						}
					}
				}	
			}
		}
	}
}