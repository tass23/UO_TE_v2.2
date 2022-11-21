using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class MaintenanceReportGump : Gump
	{
		private CityManagementStone m_Stone;

		public MaintenanceReportGump( CityManagementStone stone ) : base( 50, 50 )
		{
			m_Stone = stone;

			int cost = 0;

			int hallCost = 5000 * m_Stone.Level;

			cost += hallCost;

			int citizensCost = m_Stone.Citizens.Count * 2;
			cost += citizensCost;

			int decoreCost = m_Stone.CurrentDecore * 3;
			cost += decoreCost;
			
			int addoncost = m_Stone.AddOns.Count * 100;
			cost += addoncost;

			if ( m_Stone.IsGuarded == true )
				cost += 1000;

			if ( m_Stone.IsRegistered == true )
				cost += 1000;

			if ( m_Stone.HasBank == true )	
				cost += 1000;

			if ( m_Stone.HasTavern == true )
				cost += 1000;

			if ( m_Stone.HasHealer == true )
				cost += 1000;

			if ( m_Stone.HasMoongate == true )
				cost += 1000;

			if ( m_Stone.HasStable == true )
				cost += 1000;
			
			if ( m_Stone.HasMarket == true )
				cost += 1000;
			

			int gardenCost = m_Stone.Gardens.Count * 1000;
			cost += gardenCost;
			
			int parkCost = m_Stone.Parks.Count * 1000;
			cost += parkCost;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddBackground(30, 29, 289, 360, 5120);
			AddImageTiled(35, 58, 280, 10, 5121);
			AddHtml( 37, 34, 276, 19, @"<BASEFONT COLOR=WHITE><CENTER>Maintenance Report</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(45, 65, 1149, @"Citizen Cost: " + citizensCost.ToString() );
			AddLabel(45, 85, 1149, @"Decore Cost: " + decoreCost.ToString() );
			AddLabel(45, 105, 1149, @"City Hall Cost: " + hallCost.ToString() );

			if ( m_Stone.HasBank == true )	
				AddLabel(45, 125, 1149, @"Bank Cost: 1000");
			else
				AddLabel(45, 125, 1149, @"Bank Cost: 0");

			if ( m_Stone.HasHealer == true )
				AddLabel(45, 145, 1149, @"Healer Cost: 1000");
			else
				AddLabel(45, 145, 1149, @"Healer Cost: 0");

			if ( m_Stone.HasTavern == true )
				AddLabel(45, 165, 1149, @"Tavern Cost: 1000");
			else
				AddLabel(45, 165, 1149, @"Tavern Cost: 0");

			if ( m_Stone.HasMoongate == true )
				AddLabel(45, 185, 1149, @"Moongate Cost: 1000");
			else
				AddLabel(45, 185, 1149, @"Moongate Cost: 0");

			if ( m_Stone.HasStable == true )
				AddLabel(45, 205, 1149, @"Stable Cost: 1000");
			else
				AddLabel(45, 205, 1149, @"Stable Cost: 0");

			if ( m_Stone.IsGuarded == true )
				AddLabel(45, 225, 1149, @"Guards Cost: 1000" );
			else
				AddLabel(45, 225, 1149, @"Guards Cost: 0" );

			if ( m_Stone.IsRegistered == true )
				AddLabel(45, 245, 1149, @"Registration Cost: 1000");
			else
				AddLabel(45, 245, 1149, @"Registration Cost: 0");

			AddLabel(45, 265, 1149, @"Gardens Cost: " + gardenCost.ToString() );

			AddLabel(45, 285, 1149, @"Parks Cost: " + parkCost.ToString() );
			AddLabel( 45, 305, 1149, @"AddOn Cost: " + addoncost.ToString() );
			
			if ( m_Stone.HasMarket )
				AddLabel( 45, 325, 1149, @"Market Cost: 1000");
			else
				AddLabel( 45, 325, 1149, @"Market Cost: 0");

			AddImageTiled(35, 343, 280, 10, 5121);

			AddLabel(45, 350, 1149, @"Total Maintenance: " + cost.ToString() );
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
		}
	}
}
