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
	public class Stairs3 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Stairs3(YardWand wand, Mobile owner, int id, int p)
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
			this.AddBackground(372, 143, 165, 50, 3600);				//GoldGround

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
			this.AddItem(330, 8, 3497);											//RTree
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
//			this.AddButton(287, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 1150, @"S5 Block");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Stairs1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 1150, @"S5 Stair");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Stairs2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 1150, @"S5 Corner");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Stairs3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 1150, @"S5 Invert");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Stairs4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 1150, @"Red Block");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Stairs5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 1150, @"Red Stair");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Stairs6, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 1150, @"Red Curved");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Stairs7, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 1150, @"Platform 1");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Platform1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 1150, @"Platform 2");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Platform2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 1150, @"Platform 3");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Platform3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 1150, @"Wood Planks 1");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.WoodPlanks1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 1150, @"Wood Planks 2");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.WoodPlanks2, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 1150, @"Floor 1");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Floor1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 1150, @"Floor 2");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Floor2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 1150, @"Floor 3");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Floor3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 1150, @"Floor 4");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Floor4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 1150, @"Floor 5");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Floor5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 1150, @"Floor 6");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Floor6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 1150, @"Dirt 1");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Dirt1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 1150, @"Dirt 2");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Dirt2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 1150, @"Dirt 3");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Dirt3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 1150, @"Dirt 4");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Dirt4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 1150, @"Dirt 5");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Dirt5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 1150, @"Dirt 6");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Dirt6, GumpButtonType.Reply, 0);

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

			Stairs1, Stairs2, Stairs3, Stairs4, Stairs5, Stairs6, Stairs7, Platform1, Platform2, Platform3,
			WoodPlanks1, WoodPlanks2, Floor1, Floor2, Floor3, Floor4, Floor5, Floor6,
			Dirt1, Dirt2, Dirt3, Dirt4, Dirt5, Dirt6
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
				case (int)Buttons.Settings:			{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Normal:			{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:			{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:				{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:			{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:			{from.SendGump(new Plants1(m_Wand,from,0,0));break;}
				case (int)Buttons.Trees:			{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:			{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:			{from.SendGump(new Stairs2(m_Wand,from,0,0));break;}
//				case (int)Buttons.Next:				{from.SendGump(new Stairs3(m_Wand,from,0,0));break;}
				//Pages
				case (int)Buttons.Stairs1:			{from.SendGump(new Stairs3(m_Wand,from,1955,5000));break;}
				case (int)Buttons.Stairs2:			{from.SendGump(new Stairs3(m_Wand,from,1956,5000));break;}
				case (int)Buttons.Stairs3:			{from.SendGump(new Stairs3(m_Wand,from,1960,5000));break;}
				case (int)Buttons.Stairs4:			{from.SendGump(new Stairs3(m_Wand,from,1964,5000));break;}
				case (int)Buttons.Stairs5:			{from.SendGump(new Stairs3(m_Wand,from,1978,5000));break;}
				case (int)Buttons.Stairs6:			{from.SendGump(new Stairs3(m_Wand,from,1979,5000));break;}
				case (int)Buttons.Stairs7:			{from.SendGump(new Stairs3(m_Wand,from,1991,5000));break;}
				case (int)Buttons.Platform1:		{from.SendGump(new Stairs3(m_Wand,from,1981,5000));break;}
				case (int)Buttons.Platform2:		{from.SendGump(new Stairs3(m_Wand,from,1983,5000));break;}
				case (int)Buttons.Platform3:		{from.SendGump(new Stairs3(m_Wand,from,1987,5000));break;}
				case (int)Buttons.WoodPlanks1:		{from.SendGump(new Stairs3(m_Wand,from,1993,5000));break;}
				case (int)Buttons.WoodPlanks2:		{from.SendGump(new Stairs3(m_Wand,from,1997,5000));break;}
				case (int)Buttons.Floor1:			{from.SendGump(new Stairs3(m_Wand,from,1173,5000));break;}
				case (int)Buttons.Floor2:			{from.SendGump(new Stairs3(m_Wand,from,1193,5000));break;}
				case (int)Buttons.Floor3:			{from.SendGump(new Stairs3(m_Wand,from,1250,5000));break;}
				case (int)Buttons.Floor4:			{from.SendGump(new Stairs3(m_Wand,from,1289,5000));break;}
				case (int)Buttons.Floor5:			{from.SendGump(new Stairs3(m_Wand,from,1294,5000));break;}
				case (int)Buttons.Floor6:			{from.SendGump(new Stairs3(m_Wand,from,1301,5000));break;}
				case (int)Buttons.Dirt1:			{from.SendGump(new Stairs3(m_Wand,from,1035,5000));break;}
				case (int)Buttons.Dirt2:			{from.SendGump(new Stairs3(m_Wand,from,1039,5000));break;}
				case (int)Buttons.Dirt3:			{from.SendGump(new Stairs3(m_Wand,from,1043,5000));break;}
				case (int)Buttons.Dirt4:			{from.SendGump(new Stairs3(m_Wand,from,1047,5000));break;}
				case (int)Buttons.Dirt5:			{from.SendGump(new Stairs3(m_Wand,from,1051,5000));break;}
				case (int)Buttons.Dirt6:			{from.SendGump(new Stairs3(m_Wand,from,12789,5000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 22 );
		}
	}
}
