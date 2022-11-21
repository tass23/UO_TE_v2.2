using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Engines.BulkOrders
{
	public class LargeMobileBODAcceptGump : Gump
	{
		private LargeMobileBOD m_Deed;
		private Mobile m_From;

		public LargeMobileBODAcceptGump( Mobile from, LargeMobileBOD deed ) : base( 50, 50 )
		{
			m_From = from;
			m_Deed = deed;

			m_From.CloseGump( typeof( LargeMobileBODAcceptGump ) );
			m_From.CloseGump( typeof( SmallMobileBODAcceptGump ) );

			LargeMobileBulkEntry[] entries = deed.Entries;

			AddPage( 0 );

			AddBackground( 25, 10, 430, 240 + (entries.Length * 24), 5054 );

			AddImageTiled( 33, 20, 413, 221 + (entries.Length * 24), 2624 );
			AddAlphaRegion( 33, 20, 413, 221 + (entries.Length * 24) );

			AddImage( 20, 5, 10460 );
			AddImage( 430, 5, 10460 );
			AddImage( 20, 225 + (entries.Length * 24), 10460 );
			AddImage( 430, 225 + (entries.Length * 24), 10460 );

			AddHtmlLocalized( 180, 25, 120, 20, 1045134, 0x7FFF, false, false ); // A large bulk order

			AddHtmlLocalized( 40, 48, 350, 20, 1045135, 0x7FFF, false, false ); // Ah!  Thanks for the goods!  Would you help me out?

			AddLabel( 40, 72, 1152, @"Amount to tame:"); // Amount to make:
			AddLabel( 250, 72, 1152, deed.AmountMax.ToString() );

			AddHtmlLocalized( 40, 96, 120, 20, 1045137, 0x7FFF, false, false ); // Items requested:

			int y = 120;

			for ( int i = 0; i < entries.Length; ++i, y += 24 )
			{
				string s = entries[i].Details.AnimalName;

				int capsbreak = s.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(),1);

				if( capsbreak > -1 )
				{
					string secondhalf = s.Substring( capsbreak );
 					string firsthalf = s.Substring(0, capsbreak );

					string newname = firsthalf + " " + secondhalf;

					AddLabel( 40, y, 1152, newname.ToString() );
				}
				else
				{
					AddLabel( 40, y, 1152, entries[i].Details.AnimalName.ToString() );
				}
			}

			AddHtmlLocalized( 40, 192 + (entries.Length * 24), 350, 20, 1045139, 0x7FFF, false, false ); // Do you want to accept this order?

			AddButton( 100, 216 + (entries.Length * 24), 4005, 4007, 1, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 135, 216 + (entries.Length * 24), 120, 20, 1006044, 0x7FFF, false, false ); // Ok

			AddButton( 275, 216 + (entries.Length * 24), 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtmlLocalized( 310, 216 + (entries.Length * 24), 120, 20, 1011012, 0x7FFF, false, false ); // CANCEL
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( info.ButtonID == 1 ) // Ok
			{
				if ( m_From.PlaceInBackpack( m_Deed ) )
				{
					m_From.SendLocalizedMessage( 1045152 ); // The bulk order deed has been placed in your backpack.
				}
				else
				{
					m_From.SendLocalizedMessage( 1045150 ); // There is not enough room in your backpack for the deed.
					m_Deed.Delete();
				}
			}
			else
			{
				m_Deed.Delete();
			}
		}
	}
}