using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Mobiles;

namespace Server.Prompts
{
	public class CityTaxesIncomePrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityTaxesIncomePrompt( CityManagementStone stone, Mobile from )
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

					if ( amount <= -1 )
					{
						from.SendMessage( "Income Tax can only range from 0 - 100" );
					}
					else if ( amount <= 100 )
					{
						from.SendMessage( "You have set the cities income tax to {0}.", amount );
						m_Stone.IncomeTax = amount;

						if ( m_Stone.Vendors.Count > 0 )
							foreach ( Mobile mob in m_Stone.Vendors )
							{
								CityPlayerVendor vend = (CityPlayerVendor)mob;
								vend.TaxRate = amount;
							}
						
						
						foreach ( Mobile m in m_Stone.Citizens )
						{
							m.SendMessage( 53, "The cities income tax has changed to {0}.", amount );

						}
					}
					else
					{
						from.SendMessage( "Taxes can only range from 0 - 100" );
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
