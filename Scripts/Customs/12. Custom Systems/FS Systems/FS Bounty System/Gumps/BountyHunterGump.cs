using System;
using Server;
using Server.Gumps;
using Server.Guilds;
using Server.Mobiles;
using Server.Network;
using System.Collections;
using Server.FSBountyHunterSystem;

namespace Server.Gumps
{
	public class BountyHunterGump : Gump
	{
      	private ArrayList m_List;
      	private int m_ListPage;
     	private ArrayList m_CountList;

		public BountyHunterGump( Mobile from, int listPage, ArrayList list, ArrayList count ) : base( 50, 50 )
		{
         	m_List = list;
         	m_ListPage = listPage;   
         	m_CountList = count;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddImage(4, 4, 2170);
			AddLabel(181, 41, 1149, @"Bounty Hunter Board");

         	if ( m_List == null )
			{
				ArrayList total = FSBountySystem.GetAllBounties();
				ArrayList newlist = new ArrayList();

				foreach ( FSBountySystem.Bounty b in total )
				{
					newlist.Add( b );
				}

				m_List = newlist;
			}

         	if ( listPage > 0 )
			{
				AddButton(116, 349, 5603, 5607, 1, GumpButtonType.Reply, 0);
			}

         	if ( (listPage + 1) * 8 < m_List.Count )
			{
				AddButton(370, 349, 5601, 5605, 2, GumpButtonType.Reply, 0);
			}

         	int k = 0;

         	for ( int i = 0, j = 0, index=( (listPage * 8 ) +k ) ; i < 8 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index )
         	{
            	FSBountySystem.Bounty b = m_List[index] as FSBountySystem.Bounty;

				int buttonOffset = 170 + ( i * 20 );
				int labelOffset = 168 + ( i * 20 );

				if ( m_List.Count > 0 )
				{
					AddButton(115, buttonOffset, 4033, 4033, 100 + index, GumpButtonType.Reply, 0);
					AddLabel(135, labelOffset, 1149, @"Wanted: " + b.Wanted.Name.ToString() );
				}
				else
				{
					AddLabel(135, 168, 1149, @"No bounties at this time." );
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
			}

        	if ( info.ButtonID == 1 ) // Previous page
         	{
         		if ( m_ListPage > 0 )
				from.SendGump( new BountyHunterGump( from, m_ListPage - 1, m_List, m_CountList ) );
			}

        	if ( info.ButtonID == 2 ) // Next page
         	{ 
         		if ( (m_ListPage + 1) * 8 < m_List.Count )
				from.SendGump( new BountyHunterGump( from, m_ListPage + 1, m_List, m_CountList ) );
			}

        	if ( info.ButtonID >= 100 ) // Open Details Gump
         	{
            	FSBountySystem.Bounty b = m_List[info.ButtonID - 100] as FSBountySystem.Bounty;
				from.SendGump( new BountyDetailsGump( b.Wanted, b.Reward, b.Expires ) );
			}
		}
	}
}