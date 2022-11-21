using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class CityCitizenGump : Gump
	{
		private CityManagementStone m_Stone;

		public CityCitizenGump( CityManagementStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(25, 26, 409, 363, 5120);
			AddImageTiled(29, 57, 404, 9, 5121);
			AddHtml( 33, 32, 394, 21, @"<BASEFONT COLOR=WHITE><CENTER>City Option Menu</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(35, 70, 4005, 4006, 1, GumpButtonType.Reply, 0);
			AddButton(35, 100, 4005, 4006, 2, GumpButtonType.Reply, 0);
			AddButton(35, 130, 4005, 4006, 3, GumpButtonType.Reply, 0);
			AddButton(35, 160, 4005, 4006, 4, GumpButtonType.Reply, 0);
			AddButton(35, 190, 4005, 4006, 5, GumpButtonType.Reply, 0);
			AddLabel(70, 70, 1149, @"Resign From City");
			AddLabel(70, 100, 1149, @"Depoist To Treasury");
			AddLabel(70, 130, 1149, @"View Waring Cities");
			AddLabel(70, 160, 1149, @"View Allied Cities");
			AddLabel(70, 190, 1149, @"View Citizen List");
			AddLabel(35, 220, 1149, @"Total Citizens: " + stone.Citizens.Count.ToString() );
			AddLabel(35, 240, 1149, @"Treasury Balance: " + stone.CityTreasury.ToString() );
			AddLabel(35, 260, 1149, @"City Level: " + stone.Level.ToString() );
			AddLabel(35, 280, 1149, @"Total Waring Cities: " + stone.Waring.Count.ToString() );
			AddLabel(35, 300, 1149, @"Total Allied Cities: " + stone.Allegiances.Count.ToString() );
			AddLabel(35, 320, 1149, @"Current Income Tax: " + stone.IncomeTax.ToString() );
			AddLabel(35, 340, 1149, @"Current Property Tax: " + stone.HousingTax.ToString() );
			AddLabel(35, 360, 1149, @"Current Travel Tax: " + stone.TravelTax.ToString() );
			AddButton(250, 70, 4005, 4006, 6, GumpButtonType.Reply, 0);
			AddLabel(285, 70, 1149, @"Sponsor A Citizen");

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Resign From City
         		{
				from.SendGump( new ConfirmLeaveCityGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 2 ) // Depoist To Treasury
         		{
				from.SendMessage( "Please enter the amount you wish to depoist from your bank." );
				from.Prompt = new CityTreasuryDepoistPrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 3 ) // View Waring
         		{
				from.SendGump( new ViewCityAtWarGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 4 ) // View Allies
         		{
				from.SendGump( new ViewCityAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 5 ) // View Citizens
         		{
				from.SendGump( new ViewCityMembersGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 6 ) // Sponsor Citzen
         		{
				from.SendMessage( "Who would you like to sponsor into the city?" );
				from.Target = new CitySponsorTarget( m_Stone );
			}
		}
	}
}