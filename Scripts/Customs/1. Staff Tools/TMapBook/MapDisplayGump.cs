  //
 //  Written by Haazen June 2005
//
using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;
using Server.Prompts;
//using Server.Multis;
using Server.Targeting;

namespace Server.Gumps
{
	public class MapDisplayGump : Gump
	{
		private TMapBook m_Book;

		public TMapBook Book{ get{ return m_Book; } }


		public string GetName( string name )
		{
			if ( name == null || (name = name.Trim()).Length <= 0 )
				return "(indescript)";

			return name;
		}

		public MapDisplayGump( Mobile from, int xx, int yy ) : base( 25, 25 )
		{

			double dx = xx/13.06;
			double dy = yy/10.9;
			int x = (int)dx + 50; //60
			int y = (int)dy + 65; //65
			//int x = 430;
			//int y = 63; //435

			//m_Book = book;
			Resizable = true;
			Resizable=false;
			AddPage(0);
			AddBackground(52, 25, 393, 430, 5120);
			AddImage(59, 63, 5528); //59,63

		//	AddImage(x, y, 9009); //2360
			AddItem(x, y, 575);
			AddImage(60, 65, 2360);
			AddImage(430, 65, 2360);
			AddImage(430, 435, 2360);
			AddImage(60, 435, 2360);
			//from.SendMessage( 

		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			//if ( m_Book.Deleted || !from.InRange( m_Book.GetWorldLocation(), 1 ) || !Multis.DesignContext.Check( from ) )
				return;

		//	int buttonID = info.ButtonID;
 
		}

	}
}