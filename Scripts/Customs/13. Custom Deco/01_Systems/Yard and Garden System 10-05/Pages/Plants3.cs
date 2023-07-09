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
	public class Plants3 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Plants3(YardWand wand, Mobile owner, int id, int p)
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
			this.AddLabel(310, 70, 167, "3");

			this.AddLabel(107, 91, 167, @"Orfluer Flowers 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.OrfluerFlowers1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 167, @"Orfluer Flowers 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.OrfluerFlowers2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 167, @"Pampas Grass 1");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.PampasGrass1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 167, @"Century Plant 1");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.CenturyPlant1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 167, @"Century Plant 2");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.CenturyPlant2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 167, @"Yucca");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Yucca, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 167, @"Pampas Grass 2");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.PampasGrass2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 167, @"Ponytail Palm");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.PonytailPalm, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 167, @"Rushes");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Rushes, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 167, @"Small Banana Tree");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.SmallBananaTree, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 167, @"Snake Plant");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.SnakePlant, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 167, @"Banana Tree");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.BananaTree, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 167, @"Fern 1");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Fern1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 167, @"Fern 2");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Fern2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 167, @"Fern 3");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Fern3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 167, @"Fern 4");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Fern4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 167, @"Fern 5");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Fern5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 167, @"Fern 6");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Fern6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 167, @"Spider Tree");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.SpiderTree, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 167, @"Sapling 1");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Sapling1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 167, @"Sapling 2");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Sapling2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 167, @"Muck");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Muck, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 167, @"Weed");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Weed, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 167, @"Juniper Bush");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.JuniperBush, GumpButtonType.Reply, 0);

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

			OrfluerFlowers1, OrfluerFlowers2, PampasGrass1, CenturyPlant1, CenturyPlant2, Yucca,
			PampasGrass2, PonytailPalm, Rushes, SmallBananaTree, SnakePlant, BananaTree,
			Fern1, Fern2, Fern3, Fern4, Fern5, Fern6, SpiderTree, Sapling1, Sapling2,
			Muck, Weed, JuniperBush

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

				case (int)Buttons.Previous:		{from.SendGump(new Plants2(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Next:			{from.SendGump(new Plants4(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.OrfluerFlowers1:	{from.SendGump(new Plants3(m_Wand,from,3264,1500));break;}
				case (int)Buttons.OrfluerFlowers2:	{from.SendGump(new Plants3(m_Wand,from,3265,1500));break;}
				case (int)Buttons.PampasGrass1:		{from.SendGump(new Plants3(m_Wand,from,3237,1500));break;}
				case (int)Buttons.CenturyPlant1:	{from.SendGump(new Plants3(m_Wand,from,3376,2000));break;}
				case (int)Buttons.CenturyPlant2:	{from.SendGump(new Plants3(m_Wand,from,3377,2000));break;}
				case (int)Buttons.Yucca:			{from.SendGump(new Plants3(m_Wand,from,3383,2000));break;}
				case (int)Buttons.PampasGrass2:		{from.SendGump(new Plants3(m_Wand,from,3268,1500));break;}
				case (int)Buttons.PonytailPalm:		{from.SendGump(new Plants3(m_Wand,from,3238,1500));break;}
				case (int)Buttons.Rushes:			{from.SendGump(new Plants3(m_Wand,from,3239,1500));break;}
				case (int)Buttons.SmallBananaTree:	{from.SendGump(new Plants3(m_Wand,from,3240,2000));break;}
				case (int)Buttons.SnakePlant:		{from.SendGump(new Plants3(m_Wand,from,3241,1500));break;}
				case (int)Buttons.BananaTree:		{from.SendGump(new Plants3(m_Wand,from,3242,2000));break;}

				case (int)Buttons.Fern1:			{from.SendGump(new Plants3(m_Wand,from,3231,1500));break;}
				case (int)Buttons.Fern2:			{from.SendGump(new Plants3(m_Wand,from,3232,1500));break;}
				case (int)Buttons.Fern3:			{from.SendGump(new Plants3(m_Wand,from,3233,1500));break;}
				case (int)Buttons.Fern4:			{from.SendGump(new Plants3(m_Wand,from,3234,1500));break;}
				case (int)Buttons.Fern5:			{from.SendGump(new Plants3(m_Wand,from,3235,1500));break;}
				case (int)Buttons.Fern6:			{from.SendGump(new Plants3(m_Wand,from,3236,1500));break;}
				case (int)Buttons.SpiderTree:		{from.SendGump(new Plants3(m_Wand,from,3273,2000));break;}
				case (int)Buttons.Sapling1:			{from.SendGump(new Plants3(m_Wand,from,3305,2000));break;}
				case (int)Buttons.Sapling2:			{from.SendGump(new Plants3(m_Wand,from,3306,2000));break;}
				case (int)Buttons.Muck:				{from.SendGump(new Plants3(m_Wand,from,3267,500));break;}
				case (int)Buttons.Weed:				{from.SendGump(new Plants3(m_Wand,from,3271,500));break;}
				case (int)Buttons.JuniperBush:		{from.SendGump(new Plants3(m_Wand,from,3272,2000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 9 );
		}
	}
}
