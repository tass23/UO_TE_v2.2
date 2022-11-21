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
	public class Plants1 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Plants1(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(30, 115, 2361, 10830, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 10810, 2362, (int)Buttons.Ground, GumpButtonType.Reply, 0);
			this.AddLabel(187, 70, 167, "Plants");
			this.AddButton(239, 70, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);
			this.AddLabel(310, 70, 167, "1");

			this.AddLabel(107, 91, 167, @"Campion Flowers 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.CampionFlowers1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 167, @"Foxglove Flowers 1");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.FoxgloveFlowers1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 167, @"Orfluer Flower");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.OrfluerFlower, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 167, @"Red Poppies");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.RedPoppies, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 167, @"Campion Flowers 2");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.CampionFlowers2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 167, @"Snowdrops 1");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Snowdrops1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 167, @"Campion Flowers 3");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.CampionFlowers3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 167, @"Foxglove Flowers 2");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.FoxgloveFlowers2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 167, @"White Flowers 1");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.WhiteFlowers1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 167, @"White Flowers 2");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.WhiteFlowers2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 167, @"White Poppies");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.WhitePoppies, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 167, @"Snowdrops 2");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Snowdrops2, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 167, @"Blade Plant");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.BladePlant, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 167, @"Bulrushes");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Bullrushes, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 167, @"Coconut Palm");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.CoconutPalm, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 167, @"Date Palm");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.DatePalm, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 167, @"Elephant Ear");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.ElephantEar, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 167, @"Fan Plant");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.FanPlant, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 167, @"Small Palm 1");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.SmallPalm1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 167, @"Small Palm 2");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.SmallPalm2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 167, @"Small Palm 3");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.SmallPalm3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 167, @"Small Palm 4");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.SmallPalm4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 167, @"Small Palm 5");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.SmallPalm5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 167, @"O'hii Tree");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.OhiiTree, GumpButtonType.Reply, 0);

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
			Next,

			CampionFlowers1, FoxgloveFlowers1, OrfluerFlower, RedPoppies, CampionFlowers2, Snowdrops1,
			CampionFlowers3, FoxgloveFlowers2, WhiteFlowers1, WhiteFlowers2, WhitePoppies, Snowdrops2,
			BladePlant, Bullrushes, CoconutPalm, DatePalm, ElephantEar, FanPlant, SmallPalm1, SmallPalm2,
			SmallPalm3, SmallPalm4, SmallPalm5, OhiiTree
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				//PLACE
				case (int)Buttons.Place:				{if(itemid != 0)Place( from, itemid, price);break;}
				//Place
				//Pages
				case (int)Buttons.Settings:		{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Plants:				{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Trees:				{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Ground:				{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Next:					{from.SendGump(new Plants2(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.CampionFlowers1:		{from.SendGump(new Plants1(m_Wand,from,3203,1500));break;}
				case (int)Buttons.FoxgloveFlowers1:		{from.SendGump(new Plants1(m_Wand,from,3204,1500));break;}
				case (int)Buttons.OrfluerFlower:		{from.SendGump(new Plants1(m_Wand,from,3205,1500));break;}
				case (int)Buttons.RedPoppies:			{from.SendGump(new Plants1(m_Wand,from,3206,1500));break;}
				case (int)Buttons.CampionFlowers2:		{from.SendGump(new Plants1(m_Wand,from,3207,1500));break;}
				case (int)Buttons.Snowdrops1:			{from.SendGump(new Plants1(m_Wand,from,3208,1500));break;}
				case (int)Buttons.CampionFlowers3:		{from.SendGump(new Plants1(m_Wand,from,3209,1500));break;}
				case (int)Buttons.FoxgloveFlowers2:		{from.SendGump(new Plants1(m_Wand,from,3210,1500));break;}
				case (int)Buttons.WhiteFlowers1:		{from.SendGump(new Plants1(m_Wand,from,3211,1500));break;}
				case (int)Buttons.WhiteFlowers2:		{from.SendGump(new Plants1(m_Wand,from,3212,1500));break;}
				case (int)Buttons.WhitePoppies:			{from.SendGump(new Plants1(m_Wand,from,3213,1500));break;}
				case (int)Buttons.Snowdrops2:			{from.SendGump(new Plants1(m_Wand,from,3214,1500));break;}
				case (int)Buttons.BladePlant:			{from.SendGump(new Plants1(m_Wand,from,3219,1500));break;}
				case (int)Buttons.Bullrushes:			{from.SendGump(new Plants1(m_Wand,from,3220,1500));break;}
				case (int)Buttons.CoconutPalm:			{from.SendGump(new Plants1(m_Wand,from,3221,1500));break;}
				case (int)Buttons.DatePalm:				{from.SendGump(new Plants1(m_Wand,from,3222,1500));break;}
				case (int)Buttons.ElephantEar:			{from.SendGump(new Plants1(m_Wand,from,3223,1500));break;}
				case (int)Buttons.FanPlant:				{from.SendGump(new Plants1(m_Wand,from,3224,1500));break;}
				case (int)Buttons.SmallPalm1:			{from.SendGump(new Plants1(m_Wand,from,3225,1500));break;}
				case (int)Buttons.SmallPalm2:			{from.SendGump(new Plants1(m_Wand,from,3226,1500));break;}
				case (int)Buttons.SmallPalm3:			{from.SendGump(new Plants1(m_Wand,from,3227,1500));break;}
				case (int)Buttons.SmallPalm4:			{from.SendGump(new Plants1(m_Wand,from,3228,1500));break;}
				case (int)Buttons.SmallPalm5:			{from.SendGump(new Plants1(m_Wand,from,3229,1500));break;}
				case (int)Buttons.OhiiTree:				{from.SendGump(new Plants1(m_Wand,from,3230,1500));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 7 );
		}
	}
}
