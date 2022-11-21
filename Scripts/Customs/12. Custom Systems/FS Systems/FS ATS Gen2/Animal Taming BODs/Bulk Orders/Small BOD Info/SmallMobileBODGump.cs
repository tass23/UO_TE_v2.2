using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
	public class SmallMobileBODGump : Gump
	{
		private SmallMobileBOD m_Deed;
		private Mobile m_From;

		public SmallMobileBODGump( Mobile from, SmallMobileBOD deed ) : base( 25, 25 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeBODGump ) );
			m_From.CloseGump( typeof( SmallMobileBODGump ) );

			AddPage( 0 );

			AddBackground( 50, 10, 455, 260, 5054 );
			AddImageTiled( 58, 20, 438, 241, 2624 );
			AddAlphaRegion( 58, 20, 438, 241 );

			AddImage( 45, 5, 10460 );
			AddImage( 480, 5, 10460 );
			AddImage( 45, 245, 10460 );
			AddImage( 480, 245, 10460 );

			AddHtmlLocalized( 225, 25, 120, 20, 1045133, 0x7FFF, false, false ); // A bulk order

			AddLabel( 75, 48, 0x480, @"Amount to tame:"); // Amount to make:
			AddLabel( 275, 48, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 275, 76, 200, 20, 1045153, 0x7FFF, false, false ); // Amount finished:
			AddHtmlLocalized( 75, 72, 120, 20, 1045136, 0x7FFF, false, false ); // Item requested:

			AddItem( 410, 72, deed.Graphic );

			string s = deed.AnimalName;

			int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

			if( capsbreak > -1 )
			{
				string secondhalf = s.Substring( capsbreak );
 				string firsthalf = s.Substring(0, capsbreak );

				string newname = firsthalf + " " + secondhalf;

				AddLabel( 75, 96, 0x480, newname.ToString() );
			}
			else
			{
				AddLabel( 75, 96, 0x480, deed.AnimalName.ToString() );
			}
			AddLabel( 275, 96, 0x480, deed.AmountCur.ToString() );

			AddButton( 125, 192, 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 192, 300, 20, 1045154, 0x7FFF, false, false ); // Combine this deed with the item requested.

			AddButton( 125, 216, 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 216, 120, 20, 1011441, 0x7FFF, false, false ); // EXIT
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( m_From.Backpack ) )
				return;

			if ( info.ButtonID == 2 ) // Combine
			{
				m_From.SendGump( new SmallMobileBODGump( m_From, m_Deed ) );
				m_Deed.BeginMobileCombine( m_From );
			}
		}
	}
}