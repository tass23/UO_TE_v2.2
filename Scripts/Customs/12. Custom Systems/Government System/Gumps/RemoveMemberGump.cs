using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class RemoveMemberGump : Gump
	{
		private CityManagementStone m_Stone;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public RemoveMemberGump( CityManagementStone stone, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
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
				ArrayList total = new ArrayList( m_Stone.Citizens );
				ArrayList a = new ArrayList();

				foreach ( Mobile person in total )
				{
					a.Add( person );
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
            			Mobile mob = m_List[index] as Mobile;
				
				if ( mob is PlayerMobile )
				{
					PlayerMobile m = (PlayerMobile)mob;

					int offset = 30 + ( i * 25 );

               				if ( m.AccessLevel != AccessLevel.Player )
               				{
                  				--i;
               				}
               				else
               				{
						AddButton(35, offset, 4002, 4003, 100 + index, GumpButtonType.Reply, 0);
						AddLabel(75, offset, 1149, m.Name.ToString() );
					}
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
					from.SendGump( new RemoveMemberGump( m_Stone, m_ListPage - 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID == 2 ) // Next page
         		{ 
         			if ( (m_ListPage + 1) * 7 < m_List.Count )
					from.SendGump( new RemoveMemberGump( m_Stone, m_ListPage + 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID >= 100 ) // Remove Member
         		{
            			Mobile mob = m_List[info.ButtonID - 100] as Mobile;
				PlayerMobile pm = (PlayerMobile)mob;

				pm.City = null;
				pm.CityTitle = null;
				pm.ShowCityTitle = false;

				from.SendMessage( "You have removed them from the city." );
				m_Stone.Citizens.RemoveAt( info.ButtonID - 100 );
			}
		}
	}
}