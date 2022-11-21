using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Regions;
using Server.Mobiles;
using Server.Network;
using System.Collections;

namespace Server.Gumps
{
	public class PCMoongateGump : Gump
	{
		private Item m_Gate;
      		private ArrayList m_List;
      		private int m_ListPage;
     		private ArrayList m_CountList;

		public PCMoongateGump( Item gate, int listPage, ArrayList list, ArrayList count ) : base( 0, 0 )
		{
			m_Gate = gate;
         		m_List = list;
         		m_ListPage = listPage;   
         		m_CountList = count;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(20, 27, 413, 336, 5120);
			AddImageTiled(24, 58, 408, 10, 5121);
			AddBackground(33, 66, 389, 256, 5120);
			AddHtml( 27, 32, 400, 23, @"<BASEFONT COLOR=WHITE><CENTER>City Travel System</CENTER></BASEFONT>", (bool)false, (bool)false);

         		if ( m_List == null )
			{
				ArrayList a = new ArrayList();

				foreach ( Item i in World.Items.Values )
				{
					if ( i is CityManagementStone )
					{
						CityManagementStone s = (CityManagementStone)i;
						
						if ( s.HasMoongate == true && s.IsRegistered == true )
							a.Add( s );
					}
				}

				m_List = a;
			}

         		if ( listPage > 0 )
			{
				AddButton(45, 330, 4014, 4015, 1, GumpButtonType.Reply, 0);
				AddLabel(85, 330, 1149, @"Last Page");
			}

         		if ( (listPage + 1) * 7 < m_List.Count )
			{
				AddButton(377, 330, 4005, 4005, 2, GumpButtonType.Reply, 0);
				AddLabel(302, 330, 1149, @"Next Page");
			}

         		int k = 0;

         		for ( int i = 0, j = 0, index=((listPage*7)+k) ; i < 7 && index >= 0 && index < m_List.Count && j >= 0; ++i, ++j, ++index )
         		{
            			Item item = m_List[index] as Item;
				
				if ( item is CityManagementStone )
				{
					CityManagementStone citystone = (CityManagementStone)item;

					int offset = 75 + ( i * 25 );

					AddButton(45, offset, 4005, 4006, 100 + index, GumpButtonType.Reply, 0);
					AddLabel(85, offset, 1149, citystone.CityName.ToString() );
				}
			}
		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Previous page
         		{
         			if ( m_ListPage > 0 )
					from.SendGump( new PCMoongateGump( m_Gate, m_ListPage - 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID == 2 ) // Next page
         		{ 
         			if ( (m_ListPage + 1) * 7 < m_List.Count )
					from.SendGump( new PCMoongateGump( m_Gate, m_ListPage + 1, m_List, m_CountList ) );
			}

        		if ( info.ButtonID >= 100 ) // Travel
         		{
				if ( from.InRange( m_Gate.GetWorldLocation(), 3 ) )
				{
					CityManagementStone incomingCity = m_List[ info.ButtonID - 100 ] as CityManagementStone;
					CityManagementStone outgoingCity = null;
					Region currentRegion = Region.Find( from.Location, from.Map );

					if ( currentRegion != null )
					{
						if ( currentRegion is PlayerCityRegion )
						{
							PlayerCityRegion pcr = (PlayerCityRegion)currentRegion;

							outgoingCity = pcr.Stone;
						}	
					}

					
					
					if ( incomingCity.MoongateLocation == m_Gate.Location )
					{
						from.SendMessage( "You are already there." );
					}
					else if ( incomingCity.TravelTax >= 1 && !IsCitizen( from, incomingCity ) )
					{
						from.SendGump( new PCMoongateTollGump( m_Gate, incomingCity, outgoingCity ) );
					}
					else if ( outgoingCity != null && outgoingCity.TravelTax >= 1 && !IsCitizen( from, outgoingCity ) )
					{
						from.SendGump( new PCMoongateTollGump( m_Gate, incomingCity, outgoingCity ) );
					}
					else
					{
						BaseCreature.TeleportPets( from, incomingCity.MoongateLocation, incomingCity.Map );
						from.Combatant = null;
						from.Warmode = false;
						from.Hidden = true;
						from.MoveToWorld( incomingCity.MoongateLocation, incomingCity.Map );
						Effects.PlaySound( incomingCity.MoongateLocation, incomingCity.Map, 0x1FE );
					}
				}
				else
				{
					from.SendMessage( "You are to far away from the moongate." );
				}
			}
		}
      		
      		public bool IsCitizen( Mobile from, CityManagementStone stone )
      		{
      			 bool citizen = false;
      			
      			if ( from == stone.Mayor || stone.Citizens.Contains( from ) )
      				citizen = true;
      			
      			return citizen;
      			
      			
      		}
	}
}
