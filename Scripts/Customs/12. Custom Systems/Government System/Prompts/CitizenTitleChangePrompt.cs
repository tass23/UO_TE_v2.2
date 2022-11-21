using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Prompts;

namespace Server.Prompts
{
	public class CitizenTitleChangePrompt : Prompt
	{
		private CityManagementStone m_Stone;
		private Mobile m_Citizen;
		private Mobile m_Mayor;

		public CitizenTitleChangePrompt( CityManagementStone stone, Mobile citizen, Mobile mayor )
		{
			m_Stone = stone;
			m_Citizen = citizen;
			m_Mayor = mayor;
		}

		public override void OnCancel( Mobile from )
		{
			m_Mayor.CloseGump( typeof( CityManagementGump ) );
			m_Mayor.SendGump( new CityManagementGump( m_Stone, m_Mayor ) );
		}

		public override void OnResponse( Mobile from, string text )
		{
			text = text.Trim();

			if ( text.Length > 25 )
				text = text.Substring( 0, 25 );

			if ( text.Length > 0 )
			{
				PlayerMobile pm = (PlayerMobile)m_Citizen;
				pm.CityTitle = text;
				pm.ShowCityTitle = true;
				m_Citizen.SendMessage( "Your title has been changed to {0}.", text );
				m_Mayor.SendMessage( "You have changed thier title." );
			}

			m_Mayor.CloseGump( typeof( CityManagementGump ) );
			m_Mayor.SendGump( new CityManagementGump( m_Stone, m_Mayor ) );
		}
	}
}