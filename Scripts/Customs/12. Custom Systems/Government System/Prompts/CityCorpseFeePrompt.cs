using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityCorpseFeePrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityCorpseFeePrompt( CityManagementStone stone, Mobile from )
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
						from.SendMessage( "This fee can only range from 0 - 1000" );
					}
					else if ( amount <= 1000 )
					{
						from.SendMessage( "You have set the cities corpse retrieval to {0}.", amount );
						m_Stone.CorpseFee = amount;
					}
					else
					{
						from.SendMessage( "This fee can only range from 0 - 1000" );
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