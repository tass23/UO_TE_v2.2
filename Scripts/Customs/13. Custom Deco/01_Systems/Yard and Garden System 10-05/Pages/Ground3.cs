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
	public class Ground3 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Ground3(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(136, 74, 2084, 1210, 	(int) Buttons.Normal, GumpButtonType.Reply, 0);
			this.AddButton(181, 76, 2362, 2084, 	(int) Buttons.Water, GumpButtonType.Reply, 0);
			this.AddButton(226, 76, 2360, 2084, 	(int) Buttons.Lava, GumpButtonType.Reply, 0);
			this.AddButton(271, 76, 2361, 2084, 	(int) Buttons.Swamp, GumpButtonType.Reply, 0);

			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 2362, 10810, (int)Buttons.Ground, GumpButtonType.Reply, 0);
//			this.AddLabel(138, 74, 198, "Ground Tiles & Fences");
			this.AddButton(102, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
			this.AddButton(287, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 1150, @"Rock 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Rock1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 1150, @"Rock 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Rock2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 1150, @"Rock 3");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Rock3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 1150, @"Rock 4");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Rock4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 1150, @"Rock 5");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Rock5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 1150, @"Rock 6");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Rock6, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 1150, @"Rock 7");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Rock7, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 1150, @"Rock 8");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Rock8, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 1150, @"Rock 9");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Rock9, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 1150, @"Rock 10");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Rock10, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 1150, @"Rock 11");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Rock11, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 1150, @"Rock 12");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Rock12, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 1150, @"Rock 13");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Rock13, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 1150, @"Rock 14");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Rock14, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 1150, @"Rock 15");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Rock15, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 1150, @"Rock 16");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Rock16, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 1150, @"Rock 17");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Rock17, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 1150, @"Rock 18");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Rock18, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 1150, @"Rock 19");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Rock19, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 1150, @"Rock 20");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Rock20, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 1150, @"Rock 21");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Rock21, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 1150, @"Rock 22");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Rock22, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 1150, @"Rock 23");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Rock23, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 1150, @"Rotating Rock");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.RotatingRock, GumpButtonType.Reply, 0);

			if(id != 0)
			this.AddItem(410, 235, id);										//Choice Pic
//End Page 1
		}

		public enum Buttons
		{
			Exit,
			Settings,
			Place,
			Normal,
			Water,
			Lava,
			Swamp,
			Plants,
			Trees,
			Ground,
			Next,
			Previous,

			Rock1, Rock2, Rock3, Rock4, Rock5, Rock6, Rock7, Rock8, Rock9, Rock10, Rock11,
			Rock12, Rock13, Rock14, Rock15, Rock16, Rock17, Rock18, Rock19, Rock20, Rock21,
			Rock22, Rock23, RotatingRock
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				//PLACE
				case (int)Buttons.Place:			{if(itemid != 0)Place( from, itemid, price);break;}
				//Place
				//Pages
				case (int)Buttons.Settings:		{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Normal:			{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:			{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:				{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:			{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:			{from.SendGump(new Plants1(m_Wand,from,0,0));break;}
				case (int)Buttons.Trees:			{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:			{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:			{from.SendGump(new Ground2(m_Wand,from,0,0));break;}
				case (int)Buttons.Next:				{from.SendGump(new Stairs1(m_Wand,from,0,0));break;}
				//Pages
				case (int)Buttons.Rock1:			{from.SendGump(new Ground3(m_Wand,from,4963,500));break;}
				case (int)Buttons.Rock2:			{from.SendGump(new Ground3(m_Wand,from,4964,500));break;}
				case (int)Buttons.Rock3:			{from.SendGump(new Ground3(m_Wand,from,4965,500));break;}
				case (int)Buttons.Rock4:			{from.SendGump(new Ground3(m_Wand,from,4966,500));break;}
				case (int)Buttons.Rock5:			{from.SendGump(new Ground3(m_Wand,from,4967,500));break;}
				case (int)Buttons.Rock6:			{from.SendGump(new Ground3(m_Wand,from,4968,500));break;}
				case (int)Buttons.Rock7:			{from.SendGump(new Ground3(m_Wand,from,4969,500));break;}
				case (int)Buttons.Rock8:			{from.SendGump(new Ground3(m_Wand,from,4970,500));break;}
				case (int)Buttons.Rock9:			{from.SendGump(new Ground3(m_Wand,from,4971,500));break;}
				case (int)Buttons.Rock10:			{from.SendGump(new Ground3(m_Wand,from,4972,500));break;}
				case (int)Buttons.Rock11:			{from.SendGump(new Ground3(m_Wand,from,4973,500));break;}
				case (int)Buttons.Rock12:			{from.SendGump(new Ground3(m_Wand,from,6001,500));break;}
				case (int)Buttons.Rock13:			{from.SendGump(new Ground3(m_Wand,from,6002,500));break;}
				case (int)Buttons.Rock14:			{from.SendGump(new Ground3(m_Wand,from,6003,500));break;}
				case (int)Buttons.Rock15:			{from.SendGump(new Ground3(m_Wand,from,6004,500));break;}
				case (int)Buttons.Rock16:			{from.SendGump(new Ground3(m_Wand,from,6005,500));break;}
				case (int)Buttons.Rock17:			{from.SendGump(new Ground3(m_Wand,from,6006,500));break;}
				case (int)Buttons.Rock18:			{from.SendGump(new Ground3(m_Wand,from,6007,500));break;}
				case (int)Buttons.Rock19:			{from.SendGump(new Ground3(m_Wand,from,6008,500));break;}
				case (int)Buttons.Rock20:			{from.SendGump(new Ground3(m_Wand,from,6009,500));break;}
				case (int)Buttons.Rock21:			{from.SendGump(new Ground3(m_Wand,from,6010,500));break;}
				case (int)Buttons.Rock22:			{from.SendGump(new Ground3(m_Wand,from,6011,500));break;}
				case (int)Buttons.Rock23:			{from.SendGump(new Ground3(m_Wand,from,6012,500));break;}
				case (int)Buttons.RotatingRock:		{from.SendGump(new Ground3(m_Wand,from,4534,1000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 3 );
		}
	}
}
