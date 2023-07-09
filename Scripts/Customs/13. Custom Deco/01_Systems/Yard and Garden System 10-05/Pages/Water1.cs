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
	public class Water1 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Water1(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(136, 74, 1210, 2084, 	(int) Buttons.Normal, GumpButtonType.Reply, 0);
			this.AddButton(181, 76, 2084, 2362, 	(int) Buttons.Water, GumpButtonType.Reply, 0);
			this.AddButton(226, 76, 2360, 2084, 	(int) Buttons.Lava, GumpButtonType.Reply, 0);
			this.AddButton(271, 76, 2361, 2084, 	(int) Buttons.Swamp, GumpButtonType.Reply, 0);

			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 2362, 10810, (int)Buttons.Ground, GumpButtonType.Reply, 0);
//			this.AddLabel(148, 74, 198, "Water And Related");
//			this.AddButton(109, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
			this.AddButton(295, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 198, @"Water East");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.WaterEast, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 198, @"Water South");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.WaterSouth, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 198, @"Water Stagnant");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.WaterStagnant, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 198, @"Whirlpool");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Whirlpool, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 198, @"Waterfall E1");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.WaterfallE1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 198, @"Waterfall E2");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.WaterfallE2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 198, @"Waterfall E3");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.WaterfallE3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 198, @"Waterfall E4");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.WaterfallE4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 198, @"Waterfall E5");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.WaterfallE5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 198, @"Waterfall S1");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.WaterfallS1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 198, @"Waterfall S2");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.WaterfallS2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 198, @"Waterfall S3");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.WaterfallS3, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 198, @"Waterfall S4");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.WaterfallS4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 198, @"Waterfall S5");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.WaterfallS5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 198, @"Large Rock E1");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.LargeRockE1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 198, @"Large Rock E2");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.LargeRockE2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 198, @"Large Rock S");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.LargeRockS, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 198, @"Small Rock E1");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.SmallRockE1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 198, @"Small Rock E2");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.SmallRockE2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 198, @"Small Rock S1");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.SmallRockS1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 198, @"Small Rock S2");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.SmallRockS2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 198, @"Post");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Post, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 198, @"Fountain 1");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Fountain1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 198, @"Fountain 2");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Fountain2, GumpButtonType.Reply, 0);

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
//			Previous,

			WaterEast, WaterSouth, WaterStagnant, Whirlpool, WaterfallE1, WaterfallE2, WaterfallE3,
			WaterfallE4, WaterfallE5, WaterfallS1, WaterfallS2, WaterfallS3, WaterfallS4, WaterfallS5,
			LargeRockE1, LargeRockE2, LargeRockS, SmallRockE1, SmallRockE2, SmallRockS1, SmallRockS2,
			Post, Fountain1, Fountain2
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

				case (int)Buttons.Normal:	{from.SendGump(new Ground1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:	{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:		{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:	{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Plants:	{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:	{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Ground:	{from.SendGump(new YardGump(from,m_Wand));break;}

				case (int)Buttons.Next:		{from.SendGump(new Water2(m_Wand,from,itemid,price));break;}

				//Pages
				case (int)Buttons.WaterEast:		{from.SendGump(new Water1(m_Wand,from,13422,2000));break;}
				case (int)Buttons.WaterSouth:		{from.SendGump(new Water1(m_Wand,from,13460,2000));break;}
				case (int)Buttons.WaterStagnant:	{from.SendGump(new Water1(m_Wand,from,6039,2000));break;}
				case (int)Buttons.Whirlpool:		{from.SendGump(new Water1(m_Wand,from,13493,2000));break;}
				case (int)Buttons.WaterfallE1:		{from.SendGump(new Water1(m_Wand,from,13555,2000));break;}
				case (int)Buttons.WaterfallE2:		{from.SendGump(new Water1(m_Wand,from,13549,2000));break;}
				case (int)Buttons.WaterfallE3:		{from.SendGump(new Water1(m_Wand,from,13561,2000));break;}
				case (int)Buttons.WaterfallE4:		{from.SendGump(new Water1(m_Wand,from,13567,2000));break;}
				case (int)Buttons.WaterfallE5:		{from.SendGump(new Water1(m_Wand,from,13573,2000));break;}
				case (int)Buttons.WaterfallS1:		{from.SendGump(new Water1(m_Wand,from,13585,2000));break;}
				case (int)Buttons.WaterfallS2:		{from.SendGump(new Water1(m_Wand,from,13579,2000));break;}
				case (int)Buttons.WaterfallS3:		{from.SendGump(new Water1(m_Wand,from,13591,2000));break;}
				case (int)Buttons.WaterfallS4:		{from.SendGump(new Water1(m_Wand,from,13597,2000));break;}
				case (int)Buttons.WaterfallS5:		{from.SendGump(new Water1(m_Wand,from,13603,2000));break;}
				case (int)Buttons.SmallRockE1:		{from.SendGump(new Water1(m_Wand,from,13446,2000));break;}
				case (int)Buttons.SmallRockE2:		{from.SendGump(new Water1(m_Wand,from,13451,2000));break;}
				case (int)Buttons.LargeRockE1:		{from.SendGump(new Water1(m_Wand,from,13345,2000));break;}
				case (int)Buttons.LargeRockE2:		{from.SendGump(new Water1(m_Wand,from,13356,2000));break;}
				case (int)Buttons.SmallRockS1:		{from.SendGump(new Water1(m_Wand,from,13484,2000));break;}
				case (int)Buttons.SmallRockS2:		{from.SendGump(new Water1(m_Wand,from,13488,2000));break;}
				case (int)Buttons.LargeRockS:		{from.SendGump(new Water1(m_Wand,from,13350,2000));break;}
				case (int)Buttons.Post:				{from.SendGump(new Water1(m_Wand,from,942,2000));break;}
				case (int)Buttons.Fountain1:		{from.SendGump(new Water1(m_Wand,from,5952,50000));break;}
				case (int)Buttons.Fountain2:		{from.SendGump(new Water1(m_Wand,from,6610,50000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 18 );
		}
	}
}
