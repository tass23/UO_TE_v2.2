using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class ViewCityAtWarGump : Gump
	{
		private CityManagementStone m_Stone;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public ViewCityAtWarGump( CityManagementStone stone, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
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
				ArrayList cities = m_Stone.Waring;
				ArrayList a = new ArrayList();

				foreach ( Item i in cities )
				{
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

					AddLabel(35, offset, 1149, citystone.CityName.ToString() );
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
				if ( from == m_Stone.Mayor )
				{
					from.CloseGump( typeof( CityManagementGump ) );
					from.SendGump( new CityManagementGump( m_Stone, from ) );
				}
				else
				{
					from.CloseGump( typeof( CityCitizenGump ) );
					from.SendGump( new CityCitizenGump( m_Stone, from ) );
				}
			}

        		if ( info.ButtonID == 1 ) // Previous page
         		{
         			if ( m_ListPage > 0 )
					from.SendGump( new ViewCityAtWarGump( m_Stone, m_ListPage - 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID == 2 ) // Next page
         		{ 
         			if ( (m_ListPage + 1) * 7 < m_List.Count )
					from.SendGump( new ViewCityAtWarGump( m_Stone, m_ListPage + 1, m_List, m_CountList ) );
			}
		}
	}
}