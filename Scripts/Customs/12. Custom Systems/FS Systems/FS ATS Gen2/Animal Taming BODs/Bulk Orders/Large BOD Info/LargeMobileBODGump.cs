using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
	public class LargeMobileBODGump : Gump
	{
		private LargeMobileBOD m_Deed;
		private Mobile m_From;

		public LargeMobileBODGump( Mobile from, LargeMobileBOD deed ) : base( 25, 25 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeMobileBODGump ) );
			m_From.CloseGump( typeof( SmallMobileBODGump ) );

			LargeMobileBulkEntry[] entries = deed.Entries;

			AddPage( 0 );

			AddBackground( 50, 10, 455, 236 + (entries.Length * 24), 5054 );

			AddImageTiled( 58, 20, 438, 217 + (entries.Length * 24), 2624 );
			AddAlphaRegion( 58, 20, 438, 217 + (entries.Length * 24) );

			AddImage( 45, 5, 10460 );
			AddImage( 480, 5, 10460 );
			AddImage( 45, 221 + (entries.Length * 24), 10460 );
			AddImage( 480, 221 + (entries.Length * 24), 10460 );

			AddHtmlLocalized( 225, 25, 120, 20, 1045134, 0x7FFF, false, false ); // A large bulk order

			AddLabel( 75, 48, 0x480, @"Amount to tame:"); // Amount to make:
			AddLabel( 275, 48, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 75, 72, 120, 20, 1045137, 0x7FFF, false, false ); // Items requested:
			AddHtmlLocalized( 275, 76, 200, 20, 1045153, 0x7FFF, false, false ); // Amount finished:

			int y = 96;

			for ( int i = 0; i < entries.Length; ++i )
			{
				LargeMobileBulkEntry entry = entries[i];
				SmallMobileBulkEntry details = entry.Details;

				string s = details.AnimalName;

				int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

				if( capsbreak > -1 )
				{
					string secondhalf = s.Substring( capsbreak );
 					string firsthalf = s.Substring(0, capsbreak );

					string newname = firsthalf + " " + secondhalf;

					AddLabel( 75, y, 0x480, newname.ToString() );
					AddLabel( 275, y, 0x480, entry.Amount.ToString() );
				}
				else
				{
					AddLabel( 75, y, 0x480, details.AnimalName.ToString() );
					AddLabel( 275, y, 0x480, entry.Amount.ToString() );
				}

				y += 24;
			}

			AddButton( 125, 168 + (entries.Length * 24), 4005, 4007, 2, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 168 + (entries.Length * 24), 300, 20, 1045155, 0x7FFF, false, false ); // Combine this deed with another deed.

			AddButton( 125, 192 + (entries.Length * 24), 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 160, 192 + (entries.Length * 24), 120, 20, 1011441, 0x7FFF, false, false ); // EXIT
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Deed.Deleted || !m_Deed.IsChildOf( m_From.Backpack ) )
				return;

			if ( info.ButtonID == 2 ) // Combine
			{
				m_From.SendGump( new LargeMobileBODGump( m_From, m_Deed ) );
				m_Deed.BeginMobileCombine( m_From );
			}
		}
	}
}