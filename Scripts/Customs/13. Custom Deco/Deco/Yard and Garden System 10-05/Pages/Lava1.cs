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
	public class Lava1 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Lava1(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(181, 76, 2362, 2084, 	(int) Buttons.Water, GumpButtonType.Reply, 0);
			this.AddButton(226, 76, 2084, 2360, 	(int) Buttons.Lava, GumpButtonType.Reply, 0);
			this.AddButton(271, 76, 2361, 2084, 	(int) Buttons.Swamp, GumpButtonType.Reply, 0);

			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 2362, 10810, (int)Buttons.Ground, GumpButtonType.Reply, 0);
//			this.AddLabel(148, 74, 198, "Lava And Related");
			this.AddButton(295, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 38, @"Lava East 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.LavaEast1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 38, @"Lava East 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.LavaEast2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 38, @"Lava East 3");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.LavaEast3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 38, @"Lava East 4");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.LavaEast4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 38, @"Lava South 1");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.LavaSouth1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 38, @"Lava South 2");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.LavaSouth2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 38, @"Lava South 3");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.LavaSouth3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 38, @"Lava South 4");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.LavaSouth4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 38, @"Lava Edge 1");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.LavaEdging1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 38, @"Lava Edge 2");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.LavaEdging2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 38, @"Lava Edge 3");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.LavaEdging3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 38, @"Lava Edge 4");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.LavaEdging4, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 38, @"Lava Edge 5");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.LavaEdging5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 38, @"Lava Edge 6");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.LavaEdging6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 38, @"Lava Edge 7");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.LavaEdging7, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 38, @"Lava Edge 8");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.LavaEdging8, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 38, @"Lava Edge 9");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.LavaEdging9, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 38, @"Lava Edge 10");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.LavaEdging10, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 38, @"Lava Edge 11");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.LavaEdging11, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 38, @"Lava Edge 12");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.LavaEdging12, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 38, @"Lava Edge 13");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.LavaEdging13, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 38, @"Lava Edge 14");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.LavaEdging14, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 38, @"Lava Edge 15");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.LavaEdging15, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 38, @"Lava Edge 16");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.LavaEdging16, GumpButtonType.Reply, 0);

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

			LavaEast1, LavaEast2, LavaEast3, LavaEast4, LavaSouth1, LavaSouth2, LavaSouth3, LavaSouth4,
			LavaEdging1, LavaEdging2, LavaEdging3, LavaEdging4, LavaEdging5, LavaEdging6,
			LavaEdging7, LavaEdging8, LavaEdging9, LavaEdging10, LavaEdging11, LavaEdging12,
			LavaEdging13, LavaEdging14, LavaEdging15, LavaEdging16
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			switch( info.ButtonID )
			{
				//PLACE
				case (int)Buttons.Place:	{if(itemid != 0)Place( from, itemid, price);break;}
				//Place
				//Pages
				case (int)Buttons.Settings:		{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Normal:		{from.SendGump(new Ground1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:		{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:			{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:		{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:		{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:		{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:		{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Next:			{from.SendGump(new Lava2(m_Wand,from,itemid,price));break;}

				//Pages
				case (int)Buttons.LavaEast1:	{from.SendGump(new Lava1(m_Wand,from,4846,1000));break;}
				case (int)Buttons.LavaEast2:	{from.SendGump(new Lava1(m_Wand,from,4852,1000));break;}
				case (int)Buttons.LavaEast3:	{from.SendGump(new Lava1(m_Wand,from,4858,1000));break;}
				case (int)Buttons.LavaEast4:	{from.SendGump(new Lava1(m_Wand,from,4864,1000));break;}
				case (int)Buttons.LavaSouth1:	{from.SendGump(new Lava1(m_Wand,from,4870,1000));break;}
				case (int)Buttons.LavaSouth2:	{from.SendGump(new Lava1(m_Wand,from,4876,1000));break;}
				case (int)Buttons.LavaSouth3:	{from.SendGump(new Lava1(m_Wand,from,4882,1000));break;}
				case (int)Buttons.LavaSouth4:	{from.SendGump(new Lava1(m_Wand,from,4888,1000));break;}
				case (int)Buttons.LavaEdging1:	{from.SendGump(new Lava1(m_Wand,from,4894,1000));break;}
				case (int)Buttons.LavaEdging2:	{from.SendGump(new Lava1(m_Wand,from,4897,1000));break;}
				case (int)Buttons.LavaEdging3:	{from.SendGump(new Lava1(m_Wand,from,4900,1000));break;}
				case (int)Buttons.LavaEdging4:	{from.SendGump(new Lava1(m_Wand,from,4903,1000));break;}
				case (int)Buttons.LavaEdging5:	{from.SendGump(new Lava1(m_Wand,from,4906,1000));break;}
				case (int)Buttons.LavaEdging6:	{from.SendGump(new Lava1(m_Wand,from,4909,1000));break;}
				case (int)Buttons.LavaEdging7:	{from.SendGump(new Lava1(m_Wand,from,4912,1000));break;}
				case (int)Buttons.LavaEdging8:	{from.SendGump(new Lava1(m_Wand,from,4915,1000));break;}
				case (int)Buttons.LavaEdging9:	{from.SendGump(new Lava1(m_Wand,from,4918,1000));break;}
				case (int)Buttons.LavaEdging10:	{from.SendGump(new Lava1(m_Wand,from,4921,1000));break;}
				case (int)Buttons.LavaEdging11:	{from.SendGump(new Lava1(m_Wand,from,4924,1000));break;}
				case (int)Buttons.LavaEdging12:	{from.SendGump(new Lava1(m_Wand,from,4927,1000));break;}
				case (int)Buttons.LavaEdging13:	{from.SendGump(new Lava1(m_Wand,from,4930,1000));break;}
				case (int)Buttons.LavaEdging14:	{from.SendGump(new Lava1(m_Wand,from,4933,1000));break;}
				case (int)Buttons.LavaEdging15:	{from.SendGump(new Lava1(m_Wand,from,4936,1000));break;}
				case (int)Buttons.LavaEdging16:	{from.SendGump(new Lava1(m_Wand,from,4939,1000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 5 );
		}
	}
}
