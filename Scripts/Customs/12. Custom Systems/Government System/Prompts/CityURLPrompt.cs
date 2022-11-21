using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;

namespace Server.Prompts
{
	public class CityURLPrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_From;

		public CityURLPrompt( CityManagementStone stone, Mobile from )
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
				m_Stone.CityWebURL = text;
				from.SendMessage( "Your cities url has been updated." );
			}

			m_From.CloseGump( typeof( CityManagementGump ) );
			m_From.SendGump( new CityManagementGump( m_Stone, m_From ) );
		}
	}
}