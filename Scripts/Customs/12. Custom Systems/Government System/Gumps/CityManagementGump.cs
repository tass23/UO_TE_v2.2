using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.Regions;

namespace Server.Gumps
{
	public class CityManagementGump : Gump
	{
		private CityManagementStone m_Stone;

		public CityManagementGump( CityManagementStone stone, Mobile from ) : base( 50, 50 )
		{
			m_Stone = stone;

			int xLong = 0, yLat = 0;
			int xMins = 0, yMins = 0;
			bool xEast = false, ySouth = false;
			int X = stone.Center.X;
			int Y = stone.Center.Y;
			int Z = 0;
			Point3D loc = new Point3D( X, Y, Z );
			string fmt;

			if ( Sextant.Format( loc, stone.Map, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
				fmt = String.Format( "{0}°{1}'{2},{3}°{4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
			else
				fmt = "???";

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			AddImage(396, 160, 10462);
			AddImage(396, 60, 10462);
			AddBackground(15, 20, 407, 380, 5120);
			AddBackground(428, 20, 166, 208, 5120);
			AddButton(440, 50, 5601, 5601, 1, GumpButtonType.Page, 1);
			AddButton(440, 75, 5601, 5601, 2, GumpButtonType.Page, 2);
			AddButton(440, 100, 5601, 5601, 3, GumpButtonType.Page, 3);
			AddButton(440, 125, 5601, 5601, 4, GumpButtonType.Page, 4);
			AddButton(440, 150, 5601, 5601, 5, GumpButtonType.Page, 5);
			AddButton(440, 175, 5601, 5601, 6, GumpButtonType.Page, 6);
			AddButton(440, 200, 5601, 5601, 7, GumpButtonType.Page, 7);
			AddLabel(475, 23, 1149, @"Navigation");
			AddLabel(465, 47, 1149, @"Information");
			AddImageTiled(20, 54, 399, 9, 5121);
			AddLabel(465, 72, 1149, @"General Settings");
			AddLabel(465, 97, 1149, @"Membership");
			AddLabel(465, 123, 1149, @"Treasury Info");
			AddLabel(465, 147, 1149, @"Levy Taxes");
			AddLabel(465, 172, 1149, @"War Dept.");
			AddLabel(465, 197, 1149, @"Misc.");

			AddPage(1);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>Information</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddLabel(25, 60, 1149, @"City Name: " + stone.CityName.ToString() );
			AddLabel(25, 80, 1149, @"City Level: " + stone.Level.ToString() );

			int l1offset = PlayerGovernmentSystem.L1CLOffset * 2;
			int l2offset = PlayerGovernmentSystem.L2CLOffset * 2;
			int l3offset = PlayerGovernmentSystem.L3CLOffset * 2;
			int l4offset = PlayerGovernmentSystem.L4CLOffset * 2;
			int l5offset = PlayerGovernmentSystem.L5CLOffset * 2;
			int l6offset = PlayerGovernmentSystem.L6CLOffset * 2;

			if ( stone.Level == 1 )
				AddLabel(25, 100, 1149, @"City Limits: " + l1offset.ToString() + "x" + l1offset.ToString() );
			else if ( stone.Level == 2 )
				AddLabel(25, 100, 1149, @"City Limits: " + l2offset.ToString() + "x" + l2offset.ToString() );
			else if ( stone.Level == 3 )
				AddLabel(25, 100, 1149, @"City Limits: " + l3offset.ToString() + "x" + l3offset.ToString() );
			else if ( stone.Level == 4 )
				AddLabel(25, 100, 1149, @"City Limits: " + l4offset.ToString() + "x" + l4offset.ToString() );
			else if ( stone.Level == 5 )
				AddLabel(25, 100, 1149, @"City Limits: " + l5offset.ToString() + "x" + l5offset.ToString() );
			else if ( stone.Level == 6 )
				AddLabel(25, 100, 1149, @"City Limits: " + l6offset.ToString() + "x" + l6offset.ToString() );
			else
				AddLabel(25, 100, 1149, @"City Limits: ???");

			AddLabel(25, 120, 1149, @"City Population: " + stone.Citizens.Count.ToString() );
			AddLabel(25, 140, 1149, @"City Location: " + fmt );
			AddLabel(25, 160, 1149, @"Treasury Balance: " + stone.CityTreasury.ToString() );

			if ( stone.Level == 1 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title1.ToString() );
			else if ( stone.Level == 2 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title2.ToString() );
			else if ( stone.Level == 3 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title3.ToString() );
			else if ( stone.Level == 4 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title4.ToString() );
			else if ( stone.Level == 5 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title5.ToString() );
			else if ( stone.Level == 6 )
				AddLabel(25, 180, 1149, @"City Rank: " + PlayerGovernmentSystem.Title6.ToString() );
			else
				AddLabel(25, 180, 1149, @"City Rank: ???");

			if ( stone.IsGuarded == true )
				AddLabel(25, 200, 1149, @"City Guards: Enabled");
			else
				AddLabel(25, 200, 1149, @"City Guards: Disabled");

			if ( stone.AllowHousing == true )
				AddLabel(25, 220, 1149, @"House Placement: Enabled");
			else
				AddLabel(25, 220, 1149, @"House Placement: Disabled");

			AddLabel(25, 240, 1149, @"Current Income Tax: " + stone.IncomeTax.ToString() );
			AddLabel(25, 260, 1149, @"Current Property Tax: " + stone.HousingTax.ToString() );
			AddLabel(25, 280, 1149, @"Current Travel Tax: " + stone.TravelTax.ToString() );
			AddLabel(25, 300, 1149, @"Max Decorations: " + stone.MaxDecore.ToString() );
			AddLabel(25, 320, 1149, @"Current Decorations: " + stone.CurrentDecore.ToString() );
			AddLabel(25, 340, 1149, @"Town Vendors: " + stone.Vendors.Count.ToString() );
			AddLabel(25, 360, 1149, @"Town AddOns: " + stone.AddOns.Count.ToString() );

			AddPage(2);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>General Settings</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 60, 4005, 4006, 1, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 2, GumpButtonType.Reply, 0);
			AddButton(30, 120, 4005, 4006, 3, GumpButtonType.Reply, 0);
			AddButton(30, 150, 4005, 4006, 4, GumpButtonType.Reply, 0);
			AddImageTiled(27, 200, 385, 109, 5154);
			AddLabel(31, 177, 1149, @"City Rules, You can use HTML");
			AddTextEntry(27, 200, 385, 109, 0, 1, @"");
			AddButton(210, 60, 4005, 4006, 5, GumpButtonType.Reply, 0);
			AddButton(210, 90, 4005, 4006, 6, GumpButtonType.Reply, 0);
			AddButton(210, 120, 4005, 4006, 7, GumpButtonType.Reply, 0);
			AddButton(210, 150, 4005, 4006, 8, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Change City Name");

			if ( stone.AllowHousing == true )
				AddLabel(70, 90, 1149, @"Housing (ON)");
			else
				AddLabel(70, 90, 1149, @"Housing (Off)");

			if ( stone.IsGuarded == true )
				AddLabel(70, 120, 1149, @"Guards (ON)");
			else
				AddLabel(70, 120, 1149, @"Guards (Off)");

			AddLabel(70, 150, 1149, @"Register City");
			AddLabel(250, 60, 1149, @"Change City URL");
			AddLabel(250, 90, 1149, @"View City Ban List");
			AddLabel(250, 120, 1149, @"Ban Someone From City");
			AddLabel(250, 150, 1149, @"Lift A Ban");
			AddButton(30, 315, 4005, 4006, 9, GumpButtonType.Reply, 0);
			AddLabel(70, 315, 1149, @"Set City Rules");

			AddPage(3);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>Membership</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 60, 4005, 4006, 11, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 12, GumpButtonType.Reply, 0);
			AddButton(30, 120, 4005, 4006, 13, GumpButtonType.Reply, 0);
			AddButton(30, 150, 4005, 4006, 14, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Add Citizen");
			AddLabel(70, 90, 1149, @"Remove Citizen");
			AddLabel(70, 120, 1149, @"View Sponsored");
			AddLabel(70, 150, 1149, @"View Citizen List");
			AddLabel(250, 60, 1149, @"Citizens: " + stone.Citizens.Count.ToString() );
			AddLabel(250, 90, 1149, @"Sponsored: " + stone.Sponsored.Count.ToString() );
			AddButton(30, 180, 4005, 4006, 10, GumpButtonType.Reply, 0);
			AddLabel(70, 180, 1149, @"Change A Citizens Title");
			AddLabel( 70, 210, 1149, @"Select an Assistant Mayor");
			AddButton(30, 210, 4005, 4006, 34, GumpButtonType.Reply, 0);
			AddLabel(250, 120, 1149, @"Current Assistant Mayor");
			
			if ( m_Stone.AssistMayor != null )
				AddLabel(250, 150, 1149, m_Stone.AssistMayor.Name );
			else
				AddLabel(250, 150, 1149, @"None" );

			AddPage(4);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>Treasury</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 60, 4005, 4006, 15, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 16, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Deposit To Treasury");
			AddLabel(70, 90, 1149, @"Withdraw From Treasury");
			AddLabel(250, 60, 1149, @"Balance: " + stone.CityTreasury.ToString() );

			AddPage(5);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>Taxes</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 60, 4005, 4006, 17, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 18, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Set Income Tax");
			AddLabel(70, 90, 1149, @"Set Property Tax");
			AddLabel(250, 60, 1149, @"Income Tax: " + stone.IncomeTax.ToString() );
			AddButton(30, 120, 4005, 4006, 19, GumpButtonType.Reply, 0);
			AddLabel(70, 120, 1149, @"Set Travel Tax");
			AddLabel(250, 90, 1149, @"Property Tax: " + stone.HousingTax.ToString() );
			AddLabel(250, 120, 1149, @"Travel Tax: " + stone.TravelTax.ToString() );
		
			if ( m_Stone.HasHealer == true )
			{
				AddButton(30, 150, 4005, 4006, 32, GumpButtonType.Reply, 0);
				AddButton(30, 180, 4005, 4006, 33, GumpButtonType.Reply, 0);
				AddLabel(70, 150, 1149, @"Set Resurrection Fee");
				AddLabel(250, 150, 1149, @"Ressurrection Fee: " + stone.ResFee.ToString() );
				AddLabel(70, 180, 1149, @"Set Corpse Retrieval Fee");
				AddLabel(250, 180, 1149, @"Retrieval Fee: " + stone.CorpseFee.ToString() );
			}

			AddPage(6);

			AddButton(30, 60, 4005, 4006, 20, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 21, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Declare War!");
			AddLabel(70, 90, 1149, @"View Wars Declared");
			AddLabel(250, 60, 1149, @"Waring: " + stone.Waring.Count.ToString() );
			AddButton(30, 120, 4005, 4006, 22, GumpButtonType.Reply, 0);
			AddLabel(70, 120, 1149, @"View War Invitations");
			AddLabel(250, 90, 1149, @"Wars Declared: " + stone.WarsDeclared.Count.ToString() );
			AddLabel(250, 120, 1149, @"War Invites: " + stone.WarsInvited.Count.ToString() );
			AddButton(30, 150, 4005, 4006, 23, GumpButtonType.Reply, 0);
			AddLabel(70, 150, 1149, @"Declare Peace");
			AddButton(30, 180, 4005, 4006, 24, GumpButtonType.Reply, 0);
			AddLabel(70, 180, 1149, @"View Cities We Are At War With");
			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>War Department</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 210, 4005, 4006, 25, GumpButtonType.Reply, 0);
			AddButton(30, 240, 4005, 4006, 26, GumpButtonType.Reply, 0);
			AddButton(30, 270, 4005, 4006, 27, GumpButtonType.Reply, 0);
			AddButton(30, 300, 4005, 4006, 28, GumpButtonType.Reply, 0);
			AddButton(30, 330, 4005, 4006, 29, GumpButtonType.Reply, 0);
			AddLabel(70, 210, 1149, @"Declare Allegiance");
			AddLabel(70, 240, 1149, @"View Allegiances Proposed");
			AddLabel(70, 270, 1149, @"View Allegiances Invited");
			AddLabel(70, 300, 1149, @"Cancel An Allegiance");
			AddLabel(70, 330, 1149, @"View Cities We Are Allied With");
			AddLabel(250, 210, 1149, @"Allegiances: " + stone.Allegiances.Count.ToString() );
			AddLabel(250, 240, 1149, @"Allegiances Declared: " + stone.AllegiancesDeclared.Count.ToString() );
			AddLabel(250, 270, 1149, @"Allegiances Invites: " + stone.AllegiancesInvited.Count.ToString() );

			AddPage(7);

			AddHtml( 22, 27, 393, 21, @"<BASEFONT COLOR=WHITE><CENTER>Misc.</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddButton(30, 60, 4005, 4006, 30, GumpButtonType.Reply, 0);
			AddButton(30, 90, 4005, 4006, 31, GumpButtonType.Reply, 0);
			AddLabel(70, 60, 1149, @"Disband City");
			AddLabel(70, 90, 1149, @"View Maintenance Report");
		}

	
		public override void OnResponse( NetState state, RelayInfo info )
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // Change city name
         		{
				from.SendMessage( "Please enter the new name for the city, You have up to 40 characters." );
				from.Prompt = new CityNamePrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 2 ) // Toggle Housing
         		{
				if ( m_Stone.AllowHousing == true )
				{
					from.SendMessage( "You have disabled housing within your city." );
					m_Stone.AllowHousing = false;
				}
				else
				{
					from.SendMessage( "You have enabled housing within your city." );
					m_Stone.AllowHousing = true;
				}

				from.CloseGump( typeof( CityManagementGump ) );
				from.SendGump( new CityManagementGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 3 ) // Toggle Guards
         		{
				if ( m_Stone.Level >= 3 )
				{
					if ( m_Stone.IsGuarded == true )
					{
						from.SendMessage( "You have disabled guards within your city." );
						m_Stone.IsGuarded = false;
					}
					else
					{
						from.SendMessage( "You have enabled guards within your city." );
						m_Stone.IsGuarded = true;
					}
				}
				else
				{
					from.SendMessage( "Your city must be at least level 3 or higher before you can enable guards." );
				}

				from.CloseGump( typeof( CityManagementGump ) );
				from.SendGump( new CityManagementGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 4 ) // Reg City
         		{
				if ( m_Stone.IsRegistered == true )
				{
					from.SendMessage( "Your city is already registered." );
				}
				else if ( m_Stone.Level >= 3 )
				{
					from.SendMessage( "You have registered your city, It will now show up on all public city moongates." );
					m_Stone.IsRegistered = true;
				}
				else
				{
					from.SendMessage( "Your city must be at least level 3 or higher before you can register your city." );
				}

				from.CloseGump( typeof( CityManagementGump ) );
				from.SendGump( new CityManagementGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 5 ) // Change URL
         		{
				from.SendMessage( "Please enter your city's URL. Be sure to put http:// in front of it." );
				from.Prompt = new CityURLPrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 6 ) // View Ban List
         		{
				from.SendGump( new ViewCityBansGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 7 ) // Ban
         		{
				from.SendMessage( "Who would you like to ban from the city?" );
				from.Target = new CityBanTarget( m_Stone );
			}

        		if ( info.ButtonID == 8 ) // Lift Ban
         		{
				from.SendGump( new RemoveCityBanGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 9 ) // Set Rules
         		{
            			string rules = (string)info.GetTextEntry( 1 ).Text;
				
				if ( rules != null )
				{
					m_Stone.CityRules = rules;
					from.SendMessage( "City rules have been saved." );
				}
				else
				{
					from.SendMessage( "You must type in some rules to submit." );
				}
			}

        		if ( info.ButtonID == 10 ) // Change Citizen Title
         		{
				from.SendGump( new ChangeCitizenTitleGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 11 ) // Add Member
         		{
				from.SendMessage( "Who would you like to add?" );
				from.Target = new JoinCityTarget( m_Stone );
			}

        		if ( info.ButtonID == 12 ) // Remove Member
         		{
				from.SendGump( new RemoveMemberGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 13 ) // View/Add Sponsored
         		{
				from.SendGump( new ViewSponsoredGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 14 ) // View Members
         		{
				from.SendGump( new ViewCityMembersGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 15 ) // Depoist To Treasury
         		{
				from.SendMessage( "Please enter the amount you wish to deposit from your bank." );
				from.Prompt = new CityTreasuryDepoistPrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 16 ) // Withdraw From Treasury
         		{
				from.SendMessage( "Please enter the amount you wish to withdraw from the treasury." );
				from.Prompt = new CityTreasuryWithdrawPrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 17 ) // Income Tax
         		{
					from.SendMessage( "Please enter the percentage you wish the tax to be." );
					from.Prompt = new CityTaxesIncomePrompt( m_Stone, from );
				
				}

        		if ( info.ButtonID == 18 ) // Property Tax
         		{
				if ( m_Stone.Level >= 3 )
				{
					from.SendMessage( "Please enter the amount you wish the tax to be." );
					from.Prompt = new CityTaxesPropertyPrompt( m_Stone, from );
				}
				else
				{
					from.SendMessage( "The city must be at least level 3 or higher to levy taxes." );
				}
			}

        		if ( info.ButtonID == 19 ) // Travel
         		{
				if ( m_Stone.Level >= 3 )
				{
					from.SendMessage( "Please enter the amount you wish the tax to be." );
					from.Prompt = new CityTaxesTravelPrompt( m_Stone, from );
				}
				else
				{
					from.SendMessage( "The city must be at least level 3 or higher to levy taxes." );
				}
			}

        		if ( info.ButtonID == 20 ) // Declare War
         		{
				from.SendGump( new DeclareCityWarGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 21 ) // View Declared
         		{
				from.SendGump( new ViewDeclaredWarsGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 22 ) // View Invited
         		{
				from.SendGump( new ViewInvitedWarsGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 23 ) // Declare Peace
         		{
				from.SendGump( new DeclarePeaceGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 24 ) // View Cities At War With
         		{
				from.SendGump( new ViewCityAtWarGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 25 ) // Declare Allegiances
         		{
				from.SendGump( new DeclareCityAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 26 ) // View Declared Allegiances
         		{
				from.SendGump( new ViewDeclaredAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 27 ) // View Invited Allegiances
         		{
				from.SendGump( new ViewInvitedAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 28 ) // End Allegiances
         		{
				from.SendGump( new CancelAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 29 ) // View Cities Allegiances With
         		{
				from.SendGump( new ViewCityAllegiancesGump( m_Stone, 0, null, null ) );
			}

        		if ( info.ButtonID == 30 ) // Disband City
         		{
				from.SendGump( new CityDisbandCityGump( m_Stone, from ) );
			}

        		if ( info.ButtonID == 31 ) // Maintenance Report
         		{
				from.SendGump( new MaintenanceReportGump( m_Stone ) );
			}

        		if ( info.ButtonID == 32 ) // Set Res Fee
         		{
				from.SendMessage( "Please enter the amount you wish to set the resurrection fee to." );
				from.Prompt = new CityResFeePrompt( m_Stone, from );
			}

        		if ( info.ButtonID == 33 ) // Set Corpse Fee
         		{
				from.SendMessage( "Please enter the amount you wish to set the corpse retrieval fee to." );
				from.Prompt = new CityCorpseFeePrompt( m_Stone, from );
				}
        		
        		if ( info.ButtonID == 34 ) // Select Assistant Mayor
        		{
        			from.SendMessage( "Select the citizen you wish to name Assistant Mayor" );
        			from.SendGump( new AssistMayorGump( m_Stone, 0, null, null ) );
        		}
		}
	}
}
