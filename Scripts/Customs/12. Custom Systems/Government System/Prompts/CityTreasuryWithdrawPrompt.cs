using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityTreasuryWithdrawPrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityTreasuryWithdrawPrompt( CityManagementStone stone, Mobile from )
		{
			m_Stone = stone;
			m_From = from;
		}

		public override void OnCancel( Mobile from )
		{
			m_From.CloseGump( typeof( CityManagementGump ) );
			m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
		}

		public override void OnResponse( Mobile from, string text )
		{
			text = text.Trim();

			if ( text.Length > 40 )
				text = text.Substring( 0, 40 );

			if ( text.Length > 0 )
			{
                  		try 
                  		{ 
                     			int amount = Convert.ToInt32( text );

					if ( m_Stone.CityTreasury <= amount )
					{
						from.SendMessage( "The treasury lacks the amount you requested." );
					}
					else if ( amount <= 0 )
					{
						from.SendMessage( "You must withdraw a minimal of 1 gold." );
					}
					else if ( amount <= 4999 )
					{
						from.SendMessage( "You have withdrawn {0} gold from the treasury.", amount );
						m_Stone.CityTreasury -= amount;
						from.AddToBackpack( new Gold( amount ) );

						if ( m_Stone.Citizens != null )
						{
							foreach ( Mobile m in m_Stone.Citizens )
							{
								m.SendMessage( 53, "The mayor has withdrawn {0} gold from the cities treasury.", amount );

							}
						}
					}
					else if ( amount <= 1000000 )
					{
						from.SendMessage( "You have withdrawn {0} gold from the treasury.", amount );
						m_Stone.CityTreasury -= amount;
						from.AddToBackpack( new BankCheck( amount ) );

						foreach ( Mobile m in m_Stone.Citizens )
						{
							m.SendMessage( 53, "The mayor has withdrawn {0} gold from the cities treasury.", amount );

						}
					}
					else
					{
						from.SendMessage( "You can only withdraw a maxium of 1000000 gold." );
					}
				}
                 	 	catch 
                 		{ 
					from.SendMessage( "You must enter a number amount." ); 
                  		} 
			}

			m_From.CloseGump( typeof( CityManagementGump ) );
			m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
		}
	}
}