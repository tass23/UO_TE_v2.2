using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityTaxesTravelPrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityTaxesTravelPrompt( CityManagementStone stone, Mobile from )
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
						from.SendMessage( "Taxes can only range from 0 - 10000" );
					}
					else if ( amount <= 10000 )
					{
						from.SendMessage( "You have set the city's travel tax to {0}.", amount );
						m_Stone.TravelTax = amount;

						foreach ( Mobile m in m_Stone.Citizens )
						{
							m.SendMessage( 53, "The city's travel tax has changed to {0}.", amount );

						}
					}
					else
					{
						from.SendMessage( "Taxes can only range from 0 - 10000" );
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
