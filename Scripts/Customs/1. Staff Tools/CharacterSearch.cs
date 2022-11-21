using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Gumps
{
	public class CharSearchGump : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register( "CS", AccessLevel.Administrator, new CommandEventHandler( CharSearch_OnCommand ) );
			CommandSystem.Register( "CharSearch", AccessLevel.Administrator, new CommandEventHandler( CharSearch_OnCommand ) );
		}

		[Usage( "CharSearch" )]
		[Aliases( "CS" )]
		[Description( "Searches all chars and returns a list of Chars and Accounts." )]
		private static void CharSearch_OnCommand( CommandEventArgs e )
		{
			if ( e.Mobile.HasGump( typeof( CharSearchGumpResult ) ) )
			{
				e.Mobile.CloseGump( typeof( CharSearchGumpResult ) );
			}
			if ( e.Length == 0 )
			{
				e.Mobile.SendGump( new CharSearchGump( e.Mobile, null ) );
			}
			else
			{
				ArrayList results = new ArrayList();
				string matchEntry = e.GetString( 0 );
				string search = matchEntry.ToLower();
				ArrayList mobiles = new ArrayList( World.Mobiles.Values );

				foreach( Mobile m in mobiles )
				{
					if ( m != null && m.Name != null && m.Name.ToLower().IndexOf( search ) >= 0 )
					{
						if ( m is PlayerMobile )
						{
							results.Add( m );
						}
					}
				}
				if ( results.Count == 0 )
				{
					e.Mobile.SendMessage( "There are no characters to display." );
					e.Mobile.SendGump( new CharSearchGump( e.Mobile, null ) );
				}
				else
				{
					e.Mobile.SendGump( new CharSearchGumpResult( e.Mobile, results, 0 ) );
				}
			}
		}

		public CharSearchGump( Mobile from, string name ) : base( 100, 100 )
		{
			
				AddPage(0);
				AddBackground(0, 0, 243, 89, 9200);
				AddLabel(72, 3, 1153, "Enter Name Below");
				AddImageTiled(22, 31, 200, 25, 9354);
				AddTextEntry(22, 31, 200, 25, 0, 0, name);
				AddLabel(85, 60, 1153, "Search for Character");
				AddButton(40, 59, 4005, 4007, 1, GumpButtonType.Reply, 0);

		}
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			ArrayList results = new ArrayList();

			switch ( info.ButtonID )
			{
				case 0: // Close
				{
					return;
				}
				case 1:
				{
					TextRelay matchEntry = info.GetTextEntry( 0 );
					string search = matchEntry.Text.ToLower();
					ArrayList mobiles = new ArrayList( World.Mobiles.Values );

					foreach( Mobile m in mobiles )
					{
						if ( m != null && m.Name != null && m.Name.ToLower().IndexOf( search ) >= 0 )
						{
							if ( m is PlayerMobile )
							{
								results.Add( m );
							}
						}
					}
					if ( results.Count == 0 )
					{
						from.SendMessage( "There are no characters to display." );
						from.SendGump( new CharSearchGump( from, null ) );
					}
					else
					{
						from.SendGump( new CharSearchGumpResult( from, results, 0 ) );
					}
					break;
				}
			}
		}
	}
	
	public class CharSearchGumpResult : Gump
	{
		public static bool OldStyle = PropsConfig.OldStyle;

		public const int GumpOffsetX = 100;
		public const int GumpOffsetY = 100;

		public const int TextHue = 0;
		public const int TextOffsetX = 2;

		public const int OffsetGumpID = 0x0A40;
		public const int HeaderGumpID = 0x0E14;
		public const int  EntryGumpID = 0x0BBC;
		public const int   BackGumpID = 0x13BE;
		public const int    SetGumpID = 0x0E14;

		public const int SetWidth = 20;
		public const int SetOffsetX = 2, SetOffsetY = 2;
		public const int SetButtonID1 = 0x15E1;
		public const int SetButtonID2 = 0x15E5;

		public const int PrevWidth = 20;
		public const int PrevOffsetX = 2, PrevOffsetY = 2;
		public const int PrevButtonID1 = 0x15E3;
		public const int PrevButtonID2 = 0x15E7;

		public const int NextWidth = 20;
		public const int NextOffsetX = 2, NextOffsetY = 2;
		public const int NextButtonID1 = 0x15E1;
		public const int NextButtonID2 = 0x15E5;

		public const int OffsetSize = 1;

		public const int EntryHeight = 20;
		public const int BorderSize = 10;

		private static bool PrevLabel = false, NextLabel = false;

		private const int PrevLabelOffsetX = PrevWidth + 1;
		private const int PrevLabelOffsetY = 0;

		private const int NextLabelOffsetX = -29;
		private const int NextLabelOffsetY = 0;

		private const int EntryWidth = 180;
		private const int EntryCount = 15;

		private const int TotalWidth = OffsetSize + EntryWidth + OffsetSize + SetWidth + OffsetSize;
		private const int TotalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (EntryCount + 1));

		private const int BackWidth = BorderSize + TotalWidth + BorderSize;
		private const int BackHeight = BorderSize + TotalHeight + BorderSize;

		private Mobile m_Owner;
		private ArrayList m_Mobiles;
		private int m_Page;

		private class InternalComparer : IComparer
		{
			public static readonly IComparer Instance = new InternalComparer();

			public InternalComparer()
			{
			}

			public int Compare( object x, object y )
			{
				if ( x == null && y == null )
				return 0;
				else if ( x == null )
				return -1;
				else if ( y == null )
				return 1;

				Mobile a = x as Mobile;
				Mobile b = y as Mobile;

				if ( a == null || b == null )
				throw new ArgumentException();

				if ( a.AccessLevel > b.AccessLevel )
				return -1;
				else if ( a.AccessLevel < b.AccessLevel )
				return 1;
				else
				return Insensitive.Compare( a.Name, b.Name );
			}
		}

		public CharSearchGumpResult( Mobile owner, ArrayList list, int page ) : base( GumpOffsetX, GumpOffsetY )
		{
			owner.CloseGump( typeof( CharSearchGumpResult ) );

			m_Owner = owner;
			list.Sort( InternalComparer.Instance );
			m_Mobiles = list;

			Initialize( page );
		}

		public void Initialize( int page )
		{
			m_Page = page;

			int count = m_Mobiles.Count - (page * EntryCount);

			if ( count < 0 )
			count = 0;
			else if ( count > EntryCount )
			count = EntryCount;

			int totalHeight = OffsetSize + ((EntryHeight + OffsetSize) * (count + 1));

			AddPage( 0 );

			AddBackground( 0, 0, BackWidth, BorderSize + totalHeight + BorderSize + 20, BackGumpID );
			AddImageTiled( BorderSize, BorderSize, TotalWidth - (OldStyle ? SetWidth + OffsetSize : 0), totalHeight, OffsetGumpID );

			int x = BorderSize + OffsetSize;
			int y = BorderSize + OffsetSize;

			int emptyWidth = TotalWidth - PrevWidth - NextWidth - (OffsetSize * 4) - (OldStyle ? SetWidth + OffsetSize : 0);

			if ( !OldStyle )
			AddImageTiled( x - (OldStyle ? OffsetSize : 0), y, emptyWidth + (OldStyle ? OffsetSize * 2 : 0), EntryHeight, EntryGumpID );

			AddLabel( x + TextOffsetX, y, TextHue, String.Format( "Page {0} of {1} ({2})", page+1, (m_Mobiles.Count + EntryCount - 1) / EntryCount, m_Mobiles.Count ) );

			x += emptyWidth + OffsetSize;

			if ( OldStyle )
			AddImageTiled( x, y, TotalWidth - (OffsetSize * 3) - SetWidth, EntryHeight, HeaderGumpID );
			else
			AddImageTiled( x, y, PrevWidth, EntryHeight, HeaderGumpID );

			if ( page > 0 )
			{
				AddButton( x + PrevOffsetX, y + PrevOffsetY, PrevButtonID1, PrevButtonID2, 1, GumpButtonType.Reply, 0 );

				if ( PrevLabel )
				AddLabel( x + PrevLabelOffsetX, y + PrevLabelOffsetY, TextHue, "Previous" );
			}

			x += PrevWidth + OffsetSize;

			if ( !OldStyle )
			AddImageTiled( x, y, NextWidth, EntryHeight, HeaderGumpID );

			if ( (page + 1) * EntryCount < m_Mobiles.Count )
			{
				AddButton( x + NextOffsetX, y + NextOffsetY, NextButtonID1, NextButtonID2, 2, GumpButtonType.Reply, 1 );

				if ( NextLabel )
				AddLabel( x + NextLabelOffsetX, y + NextLabelOffsetY, TextHue, "Next" );
			}

//Goto and Admin Label Start
			AddLabel( 165, BorderSize + totalHeight + BorderSize + 0, 1153, String.Format( "Goto" ) );
			AddLabel( 200, BorderSize + totalHeight + BorderSize + 0, 1153, String.Format( "A" ) );
//Goto and Admin Label Start
			//m_Owner is the Mobile using [CharSearch command
			PlayerMobile pm = (PlayerMobile)m_Owner;

			for ( int i = 0, index = page * EntryCount; i < EntryCount && index < m_Mobiles.Count; ++i, ++index )
			{
				x = BorderSize + OffsetSize;
				y += EntryHeight + OffsetSize;

				Mobile m = (Mobile)m_Mobiles[index];
				AddImageTiled( x, y, EntryWidth, EntryHeight, EntryGumpID );//White background behind Name

				AddLabelCropped( x + TextOffsetX, y, EntryWidth - TextOffsetX, EntryHeight, GetHueFor( m ), m.Deleted ? "(deleted)" : m.Name + " - " + m.Account );
//Start Goto Button
				AddButton( x + 160, y + SetOffsetY, 1209, 1210, 100000 + i + 10, GumpButtonType.Reply, 0 );
//Stop Goto Button
				x += EntryWidth + OffsetSize;

				if ( SetGumpID != 0 )
				AddImageTiled( x, y, SetWidth, EntryHeight, SetGumpID );

				AddButton( x + SetOffsetX, y + SetOffsetY, SetButtonID1, SetButtonID2, i + 10, GumpButtonType.Reply, 0 );
			}
		}

		private static int GetHueFor( Mobile m )
		{
			switch ( m.AccessLevel )
			{
				case AccessLevel.Administrator: return 0x516;
				case AccessLevel.Seer: return 0x144;
				case AccessLevel.GameMaster: return 0x21;
				case AccessLevel.Counselor: return 0x2;
				case AccessLevel.Player: default:
				{
					if ( m.Kills >= 5 )
					return 0x21;
					else if ( m.Criminal )
					return 0x3B1;

					return 0x58;
				}
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			switch ( info.ButtonID )
			{
				case 0: //Close
				{
					return;
				}
				case 1: //Previous
				{
					if ( m_Page > 0 )
					from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page - 1 ) );

					break;
				}
				case 2: //Next
				{
					if ( (m_Page + 1) * EntryCount < m_Mobiles.Count )
					from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page + 1 ) );

					break;
				}

				default:
				{
					int index;
//Admin Start
					if ( info.ButtonID < 100000 )
					{
						index = (m_Page * EntryCount) + (info.ButtonID - 10);
	
						if ( index >= 0 && index < m_Mobiles.Count )
						{
							Mobile m = (Mobile)m_Mobiles[index];
							Account a = m.Account as Account;
							
							if ( m != null )
							{
								from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page ) );
								from.SendGump( new AdminGump( from, AdminGumpPage.ClientInfo, 0, null, null, m ) );
							}
							else if ( a != null )
							{
								from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page + 1 ) );
								from.SendGump( new AdminGump( from, AdminGumpPage.AccountDetails_Information, 0, null, null, a ) );
							}
						}
					}
//Admin	Stop
//Goto Start
					else if ( info.ButtonID >= 100000 )
					{
						index = (m_Page * EntryCount) + (info.ButtonID - 10) - 100000;
						Mobile m1 = (Mobile)m_Mobiles[index];
						Map map = m1.Map;
						Point3D loc = m1.Location;
	
						if ( map == null || map == Map.Internal )
						{
							map = m1.LogoutMap;
							loc = m1.LogoutLocation;
							from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page ) );
						}
	
						if ( map != null && map != Map.Internal )
						{
							from.MoveToWorld( loc, map );
							from.SendMessage( "You have been teleported to their location." );
							from.SendGump( new CharSearchGumpResult( from, m_Mobiles, m_Page ) );
						}
					}

					break;
//Goto Stop
				}
			}
		}
	}
}