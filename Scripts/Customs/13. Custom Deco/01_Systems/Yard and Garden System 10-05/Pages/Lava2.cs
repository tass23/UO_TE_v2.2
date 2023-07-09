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
	public class Lava2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Lava2(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(102, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
//			this.AddButton(295, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 38, @"Lavafall East 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.LavafallEast1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 38, @"Lavafall East 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.LavafallEast2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 38, @"Lavafall East 3");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.LavafallEast3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 38, @"Lavafall East 4");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.LavafallEast4, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 38, @"Lavafall East 5");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.LavafallEast5, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 38, @"Lavafall East 6");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.LavafallEast6, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 38, @"Lavafall East 7");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.LavafallEast7, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 38, @"Lavafall East 8");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.LavafallEast8, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 38, @"Lavafall East 9");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.LavafallEast9, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 38, @"Lavafall East 10");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.LavafallEast10, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 38, @"Lava Stagnant 1");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.LavaStagnant1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 38, @"Bubble 1");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Bubble1, GumpButtonType.Reply, 0);

			this.AddLabel(191, 310, 38, @"Bubble 2");
			this.AddButton(173, 316, 5032, 5032, (int)Buttons.Bubble2, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 38, @"Lavafall South 1");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.LavafallSouth1, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 38, @"Lavafall South 2");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.LavafallSouth2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 38, @"Lavafall South 3");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.LavafallSouth3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 38, @"Lavafall South 4");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.LavafallSouth4, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 38, @"Lavafall South 5");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.LavafallSouth5, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 38, @"Lavafall South 6");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.LavafallSouth6, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 38, @"Lavafall South 7");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.LavafallSouth7, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 38, @"Lavafall South 8");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.LavafallSouth8, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 38, @"Lavafall South 9");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.LavafallSouth9, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 38, @"Lavafall South 10");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.LavafallSouth10, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 38, @"Lava Stagnant 2");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.LavaStagnant2, GumpButtonType.Reply, 0);
			this.AddLabel(275, 311, 38, @"Bubble 2");
			this.AddButton(257, 316, 5032, 5032, (int)Buttons.Bubble3, GumpButtonType.Reply, 0);

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

			LavafallEast1, LavafallEast2, LavafallEast3, LavafallEast4, LavafallEast5,
			LavafallEast6, LavafallEast7, LavafallEast8, LavafallEast9, LavafallEast10,
			LavafallSouth1, LavafallSouth2, LavafallSouth3, LavafallSouth4, LavafallSouth5,
			LavafallSouth6, LavafallSouth7, LavafallSouth8, LavafallSouth9, LavafallSouth10,
			LavaStagnant1, LavaStagnant2, Bubble1, Bubble2, Bubble3
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
				case (int)Buttons.Normal:					{from.SendGump(new Ground1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:					{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:						{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:					{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:					{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:					{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:					{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:					{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}

				//Pages
				case (int)Buttons.LavafallEast1:			{from.SendGump(new Lava2(m_Wand,from,6681,100));break;}
				case (int)Buttons.LavafallEast2:			{from.SendGump(new Lava2(m_Wand,from,6686,1000));break;}
				case (int)Buttons.LavafallEast3:			{from.SendGump(new Lava2(m_Wand,from,6691,1000));break;}
				case (int)Buttons.LavafallEast4:			{from.SendGump(new Lava2(m_Wand,from,6696,1000));break;}
				case (int)Buttons.LavafallEast5:			{from.SendGump(new Lava2(m_Wand,from,6701,1000));break;}
				case (int)Buttons.LavafallEast6:			{from.SendGump(new Lava2(m_Wand,from,6706,1000));break;}
				case (int)Buttons.LavafallEast7:			{from.SendGump(new Lava2(m_Wand,from,6711,1000));break;}
				case (int)Buttons.LavafallEast8:			{from.SendGump(new Lava2(m_Wand,from,6715,1000));break;}
				case (int)Buttons.LavafallEast9:			{from.SendGump(new Lava2(m_Wand,from,6719,1000));break;}
				case (int)Buttons.LavafallEast10:			{from.SendGump(new Lava2(m_Wand,from,6723,1000));break;}
				case (int)Buttons.LavafallSouth1:			{from.SendGump(new Lava2(m_Wand,from,6727,1000));break;}
				case (int)Buttons.LavafallSouth2:			{from.SendGump(new Lava2(m_Wand,from,6732,1000));break;}
				case (int)Buttons.LavafallSouth3:			{from.SendGump(new Lava2(m_Wand,from,6737,1000));break;}
				case (int)Buttons.LavafallSouth4:			{from.SendGump(new Lava2(m_Wand,from,6742,1000));break;}
				case (int)Buttons.LavafallSouth5:			{from.SendGump(new Lava2(m_Wand,from,6747,1000));break;}
				case (int)Buttons.LavafallSouth6:			{from.SendGump(new Lava2(m_Wand,from,6752,1000));break;}
				case (int)Buttons.LavafallSouth7:			{from.SendGump(new Lava2(m_Wand,from,6757,1000));break;}
				case (int)Buttons.LavafallSouth8:			{from.SendGump(new Lava2(m_Wand,from,6761,1000));break;}
				case (int)Buttons.LavafallSouth9:			{from.SendGump(new Lava2(m_Wand,from,6765,1000));break;}
				case (int)Buttons.LavafallSouth10:			{from.SendGump(new Lava2(m_Wand,from,6769,1000));break;}
				case (int)Buttons.LavaStagnant1:			{from.SendGump(new Lava2(m_Wand,from,13410,1000));break;}
				case (int)Buttons.LavaStagnant2:			{from.SendGump(new Lava2(m_Wand,from,13416,1000));break;}
				case (int)Buttons.Bubble1:					{from.SendGump(new Lava2(m_Wand,from,13371,1000));break;}
				case (int)Buttons.Bubble2:					{from.SendGump(new Lava2(m_Wand,from,13401,1000));break;}
				case (int)Buttons.Bubble3:					{from.SendGump(new Lava2(m_Wand,from,13390,1000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 6 );
		}
	}
}
