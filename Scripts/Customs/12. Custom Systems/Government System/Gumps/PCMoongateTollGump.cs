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
	public class PCMoongateTollGump : Gump
	{
		private Item m_Gate;
		private CityManagementStone m_Incoming;
		private CityManagementStone m_Outgoing;

		public PCMoongateTollGump( Item gate, CityManagementStone incomingCity, CityManagementStone outgoingCity ) : base( 50, 50 )
		{
			m_Gate = gate;
			m_Incoming = incomingCity;
			m_Outgoing = outgoingCity;

			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);

			int incomingTax = 0;
			int outgoingTax = 0;

			if ( incomingCity != null && incomingCity.TravelTax >= 1 )
				incomingTax = incomingCity.TravelTax;

			if ( outgoingCity != null && outgoingCity.TravelTax >= 1 )
				outgoingTax = outgoingCity.TravelTax;

			int totalTax = incomingTax + outgoingTax;

			AddBackground(23, 27, 276, 269, 5120);
			AddImageTiled(28, 57, 269, 7, 5121);
			AddHtml( 32, 33, 259, 19, @"<BASEFONT COLOR= WHITE><CENTER>Travel Tax Report</CENTER></BASEFONT>", (bool)false, (bool)false);
			AddHtml( 32, 64, 258, 100, @"<CENTER><B>Taxes Info</B></CENTER><BR><BR>You are being taxed for this trip, This total below includes your incoming and outgoing fees, Incoming fees are taxes for the city you are entering, Outgoing fees are taxes for the city you are leaving, You have to pay these taxes each time you enter or leave a player city if that city has a travel tax in place.", (bool)true, (bool)true);
			AddLabel(35, 170, 1149, @"Incoming Tax: " + incomingTax.ToString() );
			AddLabel(35, 190, 1149, @"Outgoing Tax: " + outgoingTax.ToString() );
			AddLabel(35, 210, 1149, @"Total Amount: " + totalTax.ToString() );
			AddImageTiled(28, 235, 269, 7, 5121);
			AddLabel(86, 235, 1149, @"Do you wish to continue?");
			AddButton(89, 260, 247, 248, 1, GumpButtonType.Reply, 0);
			AddButton(165, 260, 241, 242, 2, GumpButtonType.Reply, 0);

		}

      		public override void OnResponse( NetState state, RelayInfo info ) 
      		{ 
			Mobile from = state.Mobile; 

			if ( from == null )
				return;

        		if ( info.ButtonID == 1 ) // OKAY
         		{
				if ( from.InRange( m_Gate.GetWorldLocation(), 3 ) )
				{
					int incomingTax = 0;
					int outgoingTax = 0;

					if ( m_Incoming != null && m_Incoming.TravelTax >= 1 )
						incomingTax = m_Incoming.TravelTax;

					if ( m_Outgoing != null && m_Outgoing.TravelTax >= 1 )
						outgoingTax = m_Outgoing.TravelTax;

					int totalTax = incomingTax + outgoingTax;

					if ( Banker.Withdraw( from, totalTax ) )
					{
						if ( m_Outgoing != null )
							m_Outgoing.CityTreasury += outgoingTax;

						if ( m_Incoming != null )
							m_Incoming.CityTreasury += incomingTax;

						BaseCreature.TeleportPets( from, m_Incoming.MoongateLocation, m_Incoming.Map );
						from.Combatant = null;
						from.Warmode = false;
						from.Hidden = true;
						from.MoveToWorld( m_Incoming.MoongateLocation, m_Incoming.Map );
						Effects.PlaySound( m_Incoming.MoongateLocation, m_Incoming.Map, 0x1FE );
					}
					else
					{
						from.SendMessage( "You do not have enough gold in your bank account to do this." );
					}
				}
				else
				{
					from.SendMessage( "You are too far away from the moongate." );
				}
			}
		}
	}
}