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
	public class Plants2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Plants2(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(239, 70, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);
			this.AddLabel(310, 70, 167, "2");

			this.AddLabel(107, 91, 167, @"Grasses 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Grasses1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 167, @"Grasses 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Grasses2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 167, @"Grasses 3");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Grasses3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 167, @"Grasses 4");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Grasses4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 167, @"Grasses 5");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Grasses5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 167, @"Grasses 6");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Grasses6, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 167, @"Grasses 7");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Grasses7, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 167, @"Grasses 8");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Grasses8, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 167, @"Grasses 9");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Grasses9, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 167, @"Grasses 10");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Grasses10, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 167, @"Grasses 11");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Grasses11, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 167, @"Grasses 12");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Grasses12, GumpButtonType.Reply, 0);


			this.AddLabel(250, 91, 167, @"Grasses 13");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Grasses13, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 167, @"Grasses 14");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Grasses14, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 167, @"Grasses 15");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Grasses15, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 167, @"Grasses 16");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Grasses16, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 167, @"Grasses 17");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Grasses17, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 167, @"Grasses 18");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Grasses18, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 167, @"Grasses 19");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Grasses19, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 167, @"Grasses 20");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Grasses20, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 167, @"Cattails 1");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Cattails1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 167, @"Cattails 2");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Cattails2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 167, @"Poppies 1");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Poppies1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 167, @"Poppies 2");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Poppies2, GumpButtonType.Reply, 0);

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
			Next,

			Grasses1, Grasses2, Grasses3, Grasses4, Grasses5, Grasses6, Grasses7, Grasses8,
			Grasses9, Grasses10, Grasses11, Grasses12, Grasses13, Grasses14, Grasses15, Grasses16,
			Grasses17, Grasses18, Grasses19, Grasses20, Cattails1, Cattails2, Poppies1, Poppies2
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

				case (int)Buttons.Next:			{from.SendGump(new Plants3(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Previous:		{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.Grasses1:		{from.SendGump(new Plants2(m_Wand,from,3244,1500));break;}
				case (int)Buttons.Grasses2:		{from.SendGump(new Plants2(m_Wand,from,3245,1500));break;}
				case (int)Buttons.Grasses3:		{from.SendGump(new Plants2(m_Wand,from,3246,1500));break;}
				case (int)Buttons.Grasses4:		{from.SendGump(new Plants2(m_Wand,from,3247,1500));break;}
				case (int)Buttons.Grasses5:		{from.SendGump(new Plants2(m_Wand,from,3248,1500));break;}
				case (int)Buttons.Grasses6:		{from.SendGump(new Plants2(m_Wand,from,3249,1500));break;}
				case (int)Buttons.Grasses7:		{from.SendGump(new Plants2(m_Wand,from,321500,1500));break;}
				case (int)Buttons.Grasses8:		{from.SendGump(new Plants2(m_Wand,from,3251,1500));break;}
				case (int)Buttons.Grasses9:		{from.SendGump(new Plants2(m_Wand,from,3252,1500));break;}
				case (int)Buttons.Grasses10:	{from.SendGump(new Plants2(m_Wand,from,3253,1500));break;}
				case (int)Buttons.Grasses11:	{from.SendGump(new Plants2(m_Wand,from,3254,1500));break;}
				case (int)Buttons.Grasses12:	{from.SendGump(new Plants2(m_Wand,from,3257,1500));break;}
				case (int)Buttons.Grasses13:	{from.SendGump(new Plants2(m_Wand,from,3258,1500));break;}
				case (int)Buttons.Grasses14:	{from.SendGump(new Plants2(m_Wand,from,3259,1500));break;}
				case (int)Buttons.Grasses15:	{from.SendGump(new Plants2(m_Wand,from,3260,1500));break;}
				case (int)Buttons.Grasses16:	{from.SendGump(new Plants2(m_Wand,from,3261,1500));break;}
				case (int)Buttons.Grasses17:	{from.SendGump(new Plants2(m_Wand,from,3269,1500));break;}
				case (int)Buttons.Grasses18:	{from.SendGump(new Plants2(m_Wand,from,3270,1500));break;}
				case (int)Buttons.Grasses19:	{from.SendGump(new Plants2(m_Wand,from,3378,1500));break;}
				case (int)Buttons.Grasses20:	{from.SendGump(new Plants2(m_Wand,from,3379,1500));break;}
				case (int)Buttons.Cattails1:	{from.SendGump(new Plants2(m_Wand,from,3255,1000));break;}
				case (int)Buttons.Cattails2:	{from.SendGump(new Plants2(m_Wand,from,3256,1000));break;}
				case (int)Buttons.Poppies1:		{from.SendGump(new Plants2(m_Wand,from,3262,1000));break;}
				case (int)Buttons.Poppies2:		{from.SendGump(new Plants2(m_Wand,from,3263,1000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 8 );
		}
	}
}
