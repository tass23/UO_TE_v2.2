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
	public class Water2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Water2(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(102, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
//			this.AddButton(287, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 198, @"Small Wave N");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.SmallWaveN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 198, @"Small Wave W");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.SmallWaveW, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 198, @"Small Wave E");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.SmallWaveE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 198, @"Small Wave S");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.SmallWaveS, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 198, @"Large Wave N");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.LargeWaveN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 198, @"Large Wave W");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.LargeWaveW, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 198, @"Large Wave E");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.LargeWaveE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 198, @"Large Wave S");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.LargeWaveS, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 198, @"Edging 1");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Edging1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 198, @"Edging 2");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Edging2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 198, @"Edging 3");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Edging3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 198, @"Edging 4");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Edging4, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 198, @"Edging 5");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Edging5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 198, @"Edging 6");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Edging6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 198, @"Edging 7");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Edging7, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 198, @"Edging 8");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Edging8, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 198, @"Edging 9");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Edging9, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 198, @"Edging 10");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Edging10, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 198, @"Edging 11");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Edging11, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 198, @"Edging 12");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Edging12, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 198, @"Edging 13");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Edging13, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 198, @"Edging 14");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Edging14, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 198, @"Edging 15");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Edging15, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 198, @"Edging 16");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Edging16, GumpButtonType.Reply, 0);

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
//			Next,
			Previous,

			SmallWaveN, SmallWaveW, SmallWaveE, SmallWaveS, LargeWaveN, LargeWaveW, LargeWaveE, LargeWaveS,
			Edging1, Edging2, Edging3, Edging4, Edging5, Edging6, Edging7, Edging8,
			Edging9, Edging10, Edging11, Edging12, Edging13, Edging14, Edging15, Edging16
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
				case (int)Buttons.Water:		{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:			{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:		{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:		{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:		{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:		{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:		{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
//				case (int)Buttons.Next:			{from.SendGump(new Ground5(from,itemid,price));break;}

				//Pages
				case (int)Buttons.SmallWaveN:	{from.SendGump(new Water2(m_Wand,from,8099,2000));break;}
				case (int)Buttons.SmallWaveW:	{from.SendGump(new Water2(m_Wand,from,8104,2000));break;}
				case (int)Buttons.SmallWaveE:	{from.SendGump(new Water2(m_Wand,from,8109,2000));break;}
				case (int)Buttons.SmallWaveS:	{from.SendGump(new Water2(m_Wand,from,8114,2000));break;}
				case (int)Buttons.LargeWaveN:	{from.SendGump(new Water2(m_Wand,from,8119,2000));break;}
				case (int)Buttons.LargeWaveW:	{from.SendGump(new Water2(m_Wand,from,8124,2000));break;}
				case (int)Buttons.LargeWaveE:	{from.SendGump(new Water2(m_Wand,from,8129,2000));break;}
				case (int)Buttons.LargeWaveS:	{from.SendGump(new Water2(m_Wand,from,8134,2000));break;}
				case (int)Buttons.Edging1:		{from.SendGump(new Water2(m_Wand,from,6045,500));break;}
				case (int)Buttons.Edging2:		{from.SendGump(new Water2(m_Wand,from,6046,500));break;}
				case (int)Buttons.Edging3:		{from.SendGump(new Water2(m_Wand,from,6047,500));break;}
				case (int)Buttons.Edging4:		{from.SendGump(new Water2(m_Wand,from,6048,500));break;}
				case (int)Buttons.Edging5:		{from.SendGump(new Water2(m_Wand,from,6049,500));break;}
				case (int)Buttons.Edging6:		{from.SendGump(new Water2(m_Wand,from,6050,500));break;}
				case (int)Buttons.Edging7:		{from.SendGump(new Water2(m_Wand,from,6051,500));break;}
				case (int)Buttons.Edging8:		{from.SendGump(new Water2(m_Wand,from,6052,500));break;}
				case (int)Buttons.Edging9:		{from.SendGump(new Water2(m_Wand,from,6053,500));break;}
				case (int)Buttons.Edging10:		{from.SendGump(new Water2(m_Wand,from,6054,500));break;}
				case (int)Buttons.Edging11:		{from.SendGump(new Water2(m_Wand,from,6055,500));break;}
				case (int)Buttons.Edging12:		{from.SendGump(new Water2(m_Wand,from,6056,500));break;}
				case (int)Buttons.Edging13:		{from.SendGump(new Water2(m_Wand,from,6057,500));break;}
				case (int)Buttons.Edging14:		{from.SendGump(new Water2(m_Wand,from,6058,500));break;}
				case (int)Buttons.Edging15:		{from.SendGump(new Water2(m_Wand,from,6059,500));break;}
				case (int)Buttons.Edging16:		{from.SendGump(new Water2(m_Wand,from,6060,500));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 19 );
		}
	}
}
