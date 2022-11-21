using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Misc;
using System.Text;
using Server.Targeting;
using Server.Multis;
using Server.ContextMenus;

namespace Server.Gumps
{
	public class Plants5 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Plants5(YardWand wand, Mobile owner, int id, int p)
			: base( wand.xstart, wand.ystart )
		{
			m_Wand = wand;

			itemid = id;
			computeGold( owner );
			price = p;
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

//Page 0
			this.AddPage(0);

			this.AddBackground(59, 55, 300, 300, 3600);					//MainGround
			this.AddBackground(34, 0, 350, 50, 3600);						//TitleGround
			this.AddBackground(385, 209, 150, 200, 3600);				//PicGround

			this.AddBackground(59, 358, 300, 50, 3600);					//PriceGround
			this.AddBackground(372, 93, 165, 50, 3600);					//PlaceGround
			this.AddBackground(372, 143, 165, 50, 3600);					//GoldGround

			this.AddButton(472, 102, 2642, 2643, (int)Buttons.Place, GumpButtonType.Reply, 0);
			this.AddLabel(398, 109, 197, "PLACE");
			this.AddItem(455, 98, 6022);										//LPGrass
			this.AddItem(489, 98, 6024);										//RPGrass

			this.AddLabel(136, 16, 68, @"Yard & Garden System");
			this.AddLabel(116, 375, 37, @"Price : ");
			this.AddLabel(166, 375, 37, price + " Gold");
			this.AddLabel(387, 160, 48, "Gold : " + TGold);

			this.AddItem(337, 110, 6019);										//TGrass
			this.AddItem(337, 155, 6019);										//BGrass
			this.AddItem(510, 183, 6024);										//FGrass
			this.AddItem(328, 190, 3317);										//TLog1
			this.AddItem(348, 195, 3318);										//TLog2
			this.AddItem(371, 221, 3319);										//TLog3
			this.AddItem(339, 354, 3316);										//BLog1
			this.AddItem(362, 338, 3315);										//BLog2

			this.AddItem(0, 8, 3497);											//LTree
			this.AddItem(330, 8, 3497);										//RTree
			this.AddItem(334, 266, 3312);										//RBVine
			this.AddItem(334, 192, 3312);										//RMVine
			this.AddItem(334, 118, 3312);										//RTVine
			this.AddItem(39, 266, 3308);										//LBVine
			this.AddItem(39, 192, 3308);										//LMVine
			this.AddItem(39, 118, 3308);										//LTVine

			this.AddItem(35, 325, 3310);										//LPVine
			this.AddItem(307, 325, 3314);										//RPVine
			this.AddButton( 490, 365, 22124, 22125, (int)Buttons.Settings, GumpButtonType.Reply, 0 );
//End Page 0
//Page 1
			this.AddPage(1);
			this.AddButton(30, 115, 2361, 10830, (int)Buttons.Plants, GumpButtonType.Reply, 1);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 3);
			this.AddButton(30, 195, 10810, 2362, (int)Buttons.Ground, GumpButtonType.Reply, 4);
			this.AddLabel(187, 74, 167, "Plants");
			this.AddButton(157, 70, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
//			this.AddButton(239, 70, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);
			this.AddLabel(310, 70, 167, "5");

			this.AddLabel(107, 91, 167, @"Vines 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Vines1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 167, @"Vines 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Vines2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 167, @"Vines 3");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Vines3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 167, @"Vines 4");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Vines4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 167, @"Vines 5");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Vines5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 167, @"Vines 6");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Vines6, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 167, @"Vines 7");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Vines7, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 167, @"Vines 8");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Vines8, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 167, @"Morning Glories");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.MorningGlories, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 167, @"Grapevines 1");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Grapevines1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 167, @"Grapevines 2");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Grapevines2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 167, @"Grapevines 3");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Grapevines3, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 167, @"Grapevines 4");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Grapevines4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 167, @"Grapevines 5");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Grapevines5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 167, @"Grapevines 6");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Grapevines6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 167, @"Grapevines 7");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Grapevines7, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 167, @"Grapevines 8");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Grapevines8, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 167, @"Grapevines 9");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Grapevines9, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 167, @"Grapevines 10");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Grapevines10, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 167, @"Log Piece 1");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Log1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 167, @"Log Piece 2");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Log2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 167, @"Log Piece 3");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Log3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 167, @"Log Piece 4");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Log4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 167, @"Log Piece 5");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Log5, GumpButtonType.Reply, 0);

			if(id != 0)
			this.AddItem(410, 235, id);										//Choice Pic
//End Page 1
		}

		public enum Buttons
		{
			Exit,
			Settings,
			Place,
			Plants,
			Trees,
			Ground,

			Previous,

			Vines1, Vines2, Vines3, Vines4, Vines5, Vines6, Vines7, Vines8,
			MorningGlories, Grapevines1, Grapevines2, Grapevines3, Grapevines4,
			Grapevines5, Grapevines6,  Grapevines7, Grapevines8, Grapevines9,
			Grapevines10, Log1, Log2, Log3, Log4, Log5
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				//PLACE
				case (int)Buttons.Place:		{if(itemid != 0)Place( from, itemid, price);break;}
				//Place
				//Pages
				case (int)Buttons.Settings:		{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Plants:		{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Trees:		{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Ground:		{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Previous:		{from.SendGump(new Plants4(m_Wand,from,itemid,price));break;}
				//case (int)Buttons.Next:			{from.SendGump(new Plants5(from,itemid,price));break;}
				//Pages
				case (int)Buttons.Vines1:		{from.SendGump(new Plants5(m_Wand,from,3307,1000));break;}
				case (int)Buttons.Vines2:		{from.SendGump(new Plants5(m_Wand,from,3308,1000));break;}
				case (int)Buttons.Vines3:		{from.SendGump(new Plants5(m_Wand,from,3309,1000));break;}
				case (int)Buttons.Vines4:		{from.SendGump(new Plants5(m_Wand,from,3310,1000));break;}
				case (int)Buttons.Vines5:		{from.SendGump(new Plants5(m_Wand,from,3311,1000));break;}
				case (int)Buttons.Vines6:		{from.SendGump(new Plants5(m_Wand,from,3312,1000));break;}
				case (int)Buttons.Vines7:		{from.SendGump(new Plants5(m_Wand,from,3313,1000));break;}
				case (int)Buttons.Vines8:		{from.SendGump(new Plants5(m_Wand,from,3314,1000));break;}
				case (int)Buttons.MorningGlories:{from.SendGump(new Plants5(m_Wand,from,3380,500));break;}
				case (int)Buttons.Grapevines1:	{from.SendGump(new Plants5(m_Wand,from,3355,1000));break;}
				case (int)Buttons.Grapevines2:	{from.SendGump(new Plants5(m_Wand,from,3356,1000));break;}
				case (int)Buttons.Grapevines3:	{from.SendGump(new Plants5(m_Wand,from,3357,1000));break;}
				case (int)Buttons.Grapevines4:	{from.SendGump(new Plants5(m_Wand,from,3358,1000));break;}
				case (int)Buttons.Grapevines5:	{from.SendGump(new Plants5(m_Wand,from,3359,1000));break;}
				case (int)Buttons.Grapevines6:	{from.SendGump(new Plants5(m_Wand,from,3360,1000));break;}
				case (int)Buttons.Grapevines7:	{from.SendGump(new Plants5(m_Wand,from,3361,1000));break;}
				case (int)Buttons.Grapevines8:	{from.SendGump(new Plants5(m_Wand,from,3362,1000));break;}
				case (int)Buttons.Grapevines9:	{from.SendGump(new Plants5(m_Wand,from,3363,1000));break;}
				case (int)Buttons.Grapevines10:	{from.SendGump(new Plants5(m_Wand,from,3364,1000));break;}
				case (int)Buttons.Log1:			{from.SendGump(new Plants5(m_Wand,from,3315,500));break;}
				case (int)Buttons.Log2:			{from.SendGump(new Plants5(m_Wand,from,3316,500));break;}
				case (int)Buttons.Log3:			{from.SendGump(new Plants5(m_Wand,from,3317,500));break;}
				case (int)Buttons.Log4:			{from.SendGump(new Plants5(m_Wand,from,3318,500));break;}
				case (int)Buttons.Log5:			{from.SendGump(new Plants5(m_Wand,from,3319,500));break;}
			}
		}

		public void computeGold( Mobile from )
		{
			int Gold = 0;
			foreach( Item item in from.Backpack.Items )
			{
				if( item is Gold )
				{
					Gold += item.Amount;
				}
				else if( item is Container )
				{
					List<Item> list = item.Items;
					for( int i=0; i<list.Count; i++ )
					{
						Item inbag = (Item)list[i];
						if( inbag is Gold )
						{
							Gold += inbag.Amount;
						}
					}
				}
			}
			TGold = makeReal(Gold);
		}

		public string makeReal( int amount )
		{
			if( amount < 1000 )
			{
				return amount.ToString();
			}
			else if( amount >= 1000 && amount < 1000000 )
			{
				string samount = amount.ToString();

				StringBuilder sb = new StringBuilder(samount);
				sb.Insert(samount.Length - 3, "," );
				samount = sb.ToString();
				return samount;
			}
			else if( amount >= 1000000 && amount < 1000000000 )
			{
				string samount = amount.ToString();

				StringBuilder sb = new StringBuilder(samount);
				sb.Insert(samount.Length - 6, "," );
				samount = sb.ToString();
				StringBuilder sb2 = new StringBuilder(samount);
				sb2.Insert(samount.Length - 3, "," );
				samount = sb2.ToString();

				return samount;
			}
			return "Null";
		}

		public void Place( Mobile from, int itemid, int p )
		{
			from.SendMessage( "Please chose where to place the item" );
			from.Target = new YardTarget( m_Wand, from, itemid, p, 11 );
		}
	}
}
