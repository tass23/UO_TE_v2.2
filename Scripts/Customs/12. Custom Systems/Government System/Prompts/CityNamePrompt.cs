using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityNamePrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityNamePrompt( CityManagementStone stone, Mobile from )
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
				if ( PlayerGovernmentSystem.CheckCityName( text ) )
				{
					m_From.SendMessage( "That name conflicts with another cities name." );
				}
				else
				{
					m_Stone.CityName = text;

					if ( m_Stone.Citizens != null )
					{
						foreach ( Mobile m in m_Stone.Citizens )
						{
							m.SendMessage( 53, "Your cities name has been changed to {0}.", text );
							
							PlayerMobile pm = (PlayerMobile)m;
							
							if ( pm.ShowCityTitle == true ) // Updates Title
								pm.ShowCityTitle = true;
						}
					}
				}
			}

			m_From.CloseGump( typeof( CityManagementGump ) );
			m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
		}
	}
}