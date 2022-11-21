using System;
using Server;
using System.Collections;
using Server.Gumps;
using Server.Network;

namespace Server.Gumps
{
	public class FacetGump : Gump
	{
		private Mobile m_Owner;
		public Mobile Owner{ get{ return m_Owner; } set{ m_Owner = value; } }

		public FacetGump(Mobile owner) : base( 10, 10 )
		{
			owner.CloseGump( typeof( FacetGump ) );

			int gumpX = 0;
			int gumpY = 0;
			// bool initialState = false;

			m_Owner = owner;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;

			AddPage( 0 );

			gumpX = 0; gumpY = 0;
			AddBackground( gumpX, gumpY, 150, 150, 0xA3C );

			gumpX = 30; gumpY = 10;
			AddLabel( gumpX, gumpY, 965, "Felucca" );

			gumpX = 30; gumpY = 30;
			AddLabel( gumpX, gumpY, 965, "Trammel" );

			gumpX = 30; gumpY = 50;
			AddLabel( gumpX, gumpY, 965, "Ilshenar" );

			gumpX = 30; gumpY = 70;
			AddLabel( gumpX, gumpY, 965, "Malas" );

			gumpX = 30; gumpY = 90;
			AddLabel( gumpX, gumpY, 965, "Tokuno" );

			gumpX = 30; gumpY = 110;
			AddLabel( gumpX, gumpY, 965, "TerMur" );

			gumpX = 10; gumpY = 10;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 1, GumpButtonType.Reply, 0 );

			gumpX = 10; gumpY = 30;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 2, GumpButtonType.Reply, 0 );

			gumpX = 10; gumpY = 50;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 3, GumpButtonType.Reply, 0 );

			gumpX = 10; gumpY = 70;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 4, GumpButtonType.Reply, 0 );

			gumpX = 10; gumpY = 90;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 5, GumpButtonType.Reply, 0 );

			gumpX = 10; gumpY = 110;
			AddButton( gumpX, gumpY, 0xA9A, 0xA9A, 6, GumpButtonType.Reply, 0 );

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch( info.ButtonID )
			{
				case 1:
					CityInfo city = new CityInfo( "Britain", "Town Center", 1475, 1645, 20 );

					from.MoveToWorld( city.Location, Map.Felucca );
					break;
				case 2:
					CityInfo city1 = new CityInfo( "Britain", "Town Center", 1475, 1645, 20 );

					from.MoveToWorld( city1.Location, Map.Trammel );
					break;
				case 3:
					CityInfo city2 = new CityInfo( "Lakeshire", "Town Center", 1203, 1124, -25 );

					from.MoveToWorld( city2.Location, Map.Ilshenar );
					break;
				case 4:
					CityInfo city3 = new CityInfo( "Luna", "Town Center", 989, 519, -50 );

					from.MoveToWorld( city3.Location, Map.Malas );
					break;

				case 5: 
					CityInfo city4 = new CityInfo( "Zento", "Town Center", 735, 1257, 30 );

					from.MoveToWorld( city4.Location, Map.Tokuno );
					break;

				case 6:
					CityInfo city5 = new CityInfo( "Test", "Test", 852, 3526, -43 );

					from.MoveToWorld( city5.Location, Map.TerMur );
					break;

			}
		}
	}
}