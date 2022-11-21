using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using System.IO;
using Server.Commands;
using Server.Items;
using Server.Network;
using Server.Prompts;
using Server.Multis;
using Server.Targeting;
using Server.Accounting;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Gumps
{
	
	public class SearchVendorGump : Gump
	{
		
		
		public static void Initialize()
		{
			CommandSystem.Register( "SV", AccessLevel.Player, new CommandEventHandler( SearchVendor_OnCommand ) );
		}

		[Usage( "SV <string>" )]
		[Description( "Finds vendor items." )]
		public static void SearchVendor_OnCommand( CommandEventArgs e )
		{
			if ( e.Length != 1 )
			{
				e.Mobile.SendMessage( "Format: SV <min 3 character string>" );
				return;
			}
			Mobile from = e.Mobile;
			ArrayList list = new ArrayList();
			string searchValue = "";
			string str = "";

			for ( int i = 0; i < e.Length; i++ )
			{
				str = e.GetString( i ).ToLower();
			}
			searchValue = str;
			if ( searchValue.Length < 3 )
			{
				e.Mobile.SendMessage( "Format: SV <min 3 character string>" );
				return;
			}

			string cfg = Path.Combine( Core.BaseDirectory, "PVI.log" );

			if ( !File.Exists( cfg ) )
			{
				from.SendMessage( "Vendor Inventory file not available." );
				return;
			}
			else
			{
				from.SendMessage( "Searching Vendors, please wait." );

				using ( StreamReader ip = new StreamReader( cfg ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						if ( line.ToLower().IndexOf( searchValue ) >= 0 )
						{
							list.Add( line );
						}
					}
				}
			}
			e.Mobile.SendGump( new SearchVendorGump( e.Mobile, list, 1 ) );
		}

		private ArrayList m_List;
		private int m_DefaultIndex;
		private int m_Page;
		private Mobile m_From;

		public SearchVendorGump( Mobile from, ArrayList list, int page ) : base( 50, 40 )
		{
			from.CloseGump( typeof( SearchVendorGump ) );
			int itemsfound = 0;
			m_Page = page;
			m_From = from;
			int pageCount = 0;
			m_List = list;

			AddPage( 0 );

			AddBackground( 0, 0, 740, 315, 9270 );
			AddBackground( 10, 10, 720, 280, 3000 );

			if ( m_List == null )
			{
				return;
			}
			else
			{
				itemsfound = list.Count;

				if ( list.Count % 12 == 0 )
				{
					pageCount = (list.Count / 12);
				}
				else
				{
					pageCount = (list.Count / 12) + 1;
				}
			}

			AddLabelCropped( 32, 16, 120, 20, 1152, "Vendor" );
			AddLabelCropped( 150,16, 120, 20, 1152, "House");
			AddLabelCropped( 268,16, 120, 20, 1152, "Item");
			AddLabelCropped( 440, 16, 120, 20, 1152, "Price" );
			AddLabelCropped( 490, 16, 120, 20, 1152, "Description" );
			AddLabel( 80, 287, 93, String.Format( "The Expanse Vendor Search              {0} items found", itemsfound ));
			
			if ( page > 1 )
				AddButton( 570, 18, 0x15E3, 0x15E7, 1, GumpButtonType.Reply, 0 );
			else
				AddImage( 570, 18, 0x25EA );

			if ( pageCount > page )
				AddButton( 587, 18, 0x15E1, 0x15E5, 2, GumpButtonType.Reply, 0 );
			else
				AddImage( 587, 18, 0x25E6 );

			if ( m_List.Count == 0 )
				AddLabel( 135, 80, 1152, "Nothing found to match your request." );

			if ( page == pageCount )
			{
				for ( int i = (page * 12) -12; i < itemsfound; ++i )
					AddDetails( i, from );
			}
			else
			{
				for ( int i = (page * 12) -12; i < page * 12; ++ i )
					AddDetails( i, from );
			}
		}

		private void AddDetails( int index, Mobile from )
		{
			string owner;
			if ( index < m_List.Count )
			{
				try
				{
				
				int row;
				string line = m_List[index].ToString();
				int linelength = line.Length;
				int desclength = linelength - 67;
				if ( desclength > 35 )
					desclength = 35;

				row = index % 12;
				string ssvendor = line.Substring( 0, 16 );
				string ssowner = line.Substring (17, 16 );
				string ssitem = line.Substring( 34, 25 );
				string ssprice = line.Substring( 60, 7 );
				string ssdesc = line.Substring( 67, desclength );


				AddLabel(32, 40 +(row * 20), 395, String.Format( "{0}", ssvendor ));
				AddLabel(150, 40 +(row * 20), 395, String.Format( "{0}", ssowner ));
				AddLabel(268, 40 +(row * 20), 395, String.Format( "{0}", ssitem ));
				AddLabel(423, 40 +(row * 20), 395, String.Format( "{0}", ssprice ));
				AddLabel(483, 40 +(row * 20), 395, String.Format( "{0}", ssdesc ));

				}
				catch {}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			int buttonID = info.ButtonID;
			if ( buttonID == 2 )
			{
				m_Page ++;
				from.CloseGump( typeof( SearchVendorGump ) );
				from.SendGump( new SearchVendorGump( from, m_List, m_Page ) );
			}
			if ( buttonID == 1 )
			{
				m_Page --;
				from.CloseGump( typeof( SearchVendorGump ) );
				from.SendGump( new SearchVendorGump( from, m_List, m_Page ) );
			}
		}
	}
}
