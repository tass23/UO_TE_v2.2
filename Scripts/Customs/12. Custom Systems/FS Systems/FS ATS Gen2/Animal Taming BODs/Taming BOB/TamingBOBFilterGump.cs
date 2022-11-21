using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.BulkOrders
{
	public class TamingBOBFilterGump : Gump
	{
		private PlayerMobile m_From;
		private TamingBulkOrderBook m_Book;

		private const int LabelColor = 0x7FFF;

		private static int[,] m_TypeFilters = new int[,]
			{
				{ 1062229, 0 }, // All
				{ 1062224, 1 }, // Small
				{ 1062225, 2 }  // Large
			};

		private static int[,] m_AmountFilters = new int[,]
			{
				{ 1062229, 0 }, // All
				{ 1049706, 1 }, // 10
				{ 1016007, 2 }, // 15
				{ 1062239, 3 }  // 20
			};

		private static int[][,] m_Filters = new int[][,]
			{
				m_TypeFilters,
				m_AmountFilters
			};

		private static int[] m_XOffsets_Type = new int[]{ 0, 75, 170 };
		private static int[] m_XOffsets_Amount = new int[]{ 0, 75, 180, 275 };

		private static int[] m_XWidths_Small = new int[]{ 50, 50, 70, 50 };
		private static int[] m_XWidths_Large = new int[]{ 80, 50, 50, 50, 50, 50 };

		private void AddFilterList( int x, int y, int[] xOffsets, int yOffset, int[,] filters, int[] xWidths, int filterValue, int filterIndex )
		{
			for ( int i = 0; i < filters.GetLength( 0 ); ++i )
			{
				int number = filters[i, 0];

				if ( number == 0 )
					continue;

				bool isSelected = ( filters[i, 1] == filterValue );

				if ( !isSelected && (i % xOffsets.Length) == 0 )
					isSelected = ( filterValue == 0 );

				AddHtmlLocalized( x + 35 + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), xWidths[i % xOffsets.Length], 32, number, isSelected ? 16927 : LabelColor, false, false );
				AddButton( x + xOffsets[i % xOffsets.Length], y + ((i / xOffsets.Length) * yOffset), 4005, 4007, 4 + filterIndex + (i * 4), GumpButtonType.Reply, 0 );
			}
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			TamingBOBFilter f = ( m_From.UseOwnFilter ? m_From.TamingBOBFilter : m_Book.Filter );

			int index = info.ButtonID;

			switch ( index )
			{
				case 0: // Apply
				{
					m_From.SendGump( new TamingBOBGump( m_From, m_Book ) );

					break;
				}
				case 1: // Set Book Filter
				{
					m_From.UseOwnFilter = false;
					m_From.SendGump( new TamingBOBFilterGump( m_From, m_Book ) );

					break;
				}
				case 2: // Set Your Filter
				{
					m_From.UseOwnFilter = true;
					m_From.SendGump( new TamingBOBFilterGump( m_From, m_Book ) );

					break;
				}
				case 3: // Clear Filter
				{
					f.Clear();
					m_From.SendGump( new TamingBOBFilterGump( m_From, m_Book ) );

					break;
				}
				default:
				{
					index -= 4;

					int type = index % 2;

					//index -= type;

					index /= 4;

					if ( type >= 0 && type < m_Filters.Length )
					{
						int[,] filters = m_Filters[type];

						if ( index >= 0 && index < filters.GetLength( 0 ) )
						{
							if ( filters[index, 0] == 0 )
								break;

							switch ( type )
							{
								case 0: f.Type = filters[index, 1]; break;
								case 1: f.Quantity = filters[index, 1]; break;
							}

							m_From.SendGump( new TamingBOBFilterGump( m_From, m_Book ) );
						}
					}

					break;
				}
			}
		}

		public TamingBOBFilterGump( PlayerMobile from, TamingBulkOrderBook book ) : base( 12, 24 )
		{
			from.CloseGump( typeof( TamingBOBGump ) );
			from.CloseGump( typeof( TamingBOBFilterGump ) );

			m_From = from;
			m_Book = book;

			TamingBOBFilter f = ( from.UseOwnFilter ? from.TamingBOBFilter : book.Filter );

			AddPage( 0 );

			AddBackground( 10, 10, 600, 439, 5054 );

			AddImageTiled( 18, 20, 583, 420, 2624 );
			AddAlphaRegion( 18, 20, 583, 420 );

			AddImage( 5, 5, 10460 );
			AddImage( 585, 5, 10460 );
			AddImage( 5, 424, 10460 );
			AddImage( 585, 424, 10460 );

			AddHtmlLocalized( 270, 32, 200, 32, 1062223, LabelColor, false, false ); // Filter Preference

			AddHtmlLocalized( 26, 258, 120, 32, 1062228, LabelColor, false, false ); // Bulk Order Type
			AddFilterList( 25, 288, m_XOffsets_Type, 40, m_TypeFilters, m_XWidths_Small, f.Type, 0 );

			AddHtmlLocalized( 26, 320, 120, 32, 1062217, LabelColor, false, false ); // Amount
			AddFilterList( 25, 352, m_XOffsets_Amount, 40, m_AmountFilters, m_XWidths_Small, f.Quantity, 1 );

			AddHtmlLocalized( 75, 416, 120, 32, 1062477, ( from.UseOwnFilter ? LabelColor : 16927 ), false, false ); // Set Book Filter
			AddButton( 40, 416, 4005, 4007, 1, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 235, 416, 120, 32, 1062478, ( from.UseOwnFilter ? 16927 : LabelColor ), false, false ); // Set Your Filter
			AddButton( 200, 416, 4005, 4007, 2, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 405, 416, 120, 32, 1062231, LabelColor, false, false ); // Clear Filter
			AddButton( 370, 416, 4005, 4007, 3, GumpButtonType.Reply, 0 );

			AddHtmlLocalized( 540, 416, 50, 32, 1011046, LabelColor, false, false ); // APPLY
			AddButton( 505, 416, 4017, 4018, 0, GumpButtonType.Reply, 0 );
		}
	}
}