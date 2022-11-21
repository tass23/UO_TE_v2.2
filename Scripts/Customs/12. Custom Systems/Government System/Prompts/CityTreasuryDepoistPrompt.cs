using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityTreasuryDepoistPrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityTreasuryDepoistPrompt( CityManagementStone stone, Mobile from )
		{
			m_Stone = stone;
			m_From = from;
		}

		public override void OnCancel( Mobile from )
		{
			if ( from == m_Stone.Mayor )
			{
				m_From.CloseGump( typeof( CityManagementGump ) );
				m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
			}
			else
			{
				m_From.CloseGump( typeof( CityCitizenGump ) );
				m_From.SendGump( new CityCitizenGump( m_Stone, m_From ) );
			}
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

					if ( amount <= 0 )
					{
						from.SendMessage( "You must enter an amount higher than that." );
					}
					else if ( Banker.Withdraw( from, amount ) )
					{
						from.SendMessage( "You deposit {0} into the cities treasury.", amount );
						m_Stone.CityTreasury += amount;
					}
					else
					{
						from.SendMessage( "You lack the gold in your back account." );
					}
				}
                 	 	catch 
                 		{ 
					from.SendMessage( "You must enter a number amount." ); 
                  		} 
			}

			if ( from == m_Stone.Mayor )
			{
				m_From.CloseGump( typeof( CityManagementGump ) );
				m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
			}
			else
			{
				m_From.CloseGump( typeof( CityCitizenGump ) );
				m_From.SendGump( new CityCitizenGump( m_Stone, m_From ) );
			}
		}
	}
}
