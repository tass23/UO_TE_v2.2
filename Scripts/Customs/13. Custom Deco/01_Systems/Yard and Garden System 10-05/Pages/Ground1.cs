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
	public class Ground1 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Ground1(YardWand wand, Mobile owner, int id, int p)
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
			this.AddBackground(34, 0, 350, 50, 3600);					//TitleGround
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
			this.AddButton(136, 74, 2084, 1210, (int) Buttons.Normal, GumpButtonType.Reply, 0);
			this.AddButton(181, 76, 2362, 2084, (int) Buttons.Water, GumpButtonType.Reply, 0);
			this.AddButton(226, 76, 2360, 2084, (int) Buttons.Lava, GumpButtonType.Reply, 0);
			this.AddButton(271, 76, 2361, 2084, (int) Buttons.Swamp, GumpButtonType.Reply, 0);

			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 2362, 10810, (int)Buttons.Ground, GumpButtonType.Reply, 0);

//			this.AddLabel(138, 74, 198, "Ground Tiles & Fences");
//			this.AddButton(109, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
			this.AddButton(295, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 1150, @"Short Bush 1");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.ShortBush1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 1150, @"Short Bush 2");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.ShortBush2, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 1150, @"Tall Bush");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.TallBush, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 1150, @"Tall Iron N");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.TallIronN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 1150, @"Tall Iron E");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.TallIronE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 1150, @"Tall Iron C");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.TallIronC, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 1150, @"Short Iron N");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.ShortIronN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 1150, @"Short Iron E");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.ShortIronE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 1150, @"Short Iron C");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.ShortIronC, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 1150, @"Wood  1N");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Wood1N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 1150, @"Wood  1E");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Wood1E, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 1150, @"Wood  1C");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Wood1C, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 1150, @"Wood  1T");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Wood1T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 1150, @"Wood 2N");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Wood2N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 1150, @"Wood 2E");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Wood2E, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 1150, @"Wood 2C");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Wood2C, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 1150, @"Wood 2T");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Wood2T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 1150, @"Wood 2NLink");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Wood2NLink, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 1150, @"Wood 2ELink");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Wood2ELink, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 1150, @"Wood 3N");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Wood3N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 1150, @"Wood 3E");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Wood3E, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 1150, @"Wood 3C");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Wood3C, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 1150, @"Wood 3T");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Wood3T, GumpButtonType.Reply, 0);
//			this.AddLabel(250, 311, 1150, @"New Label");
//			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Unused, GumpButtonType.Reply, 0);

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

			ShortBush1, ShortBush2, TallBush, TallIronN, TallIronE, TallIronC,ShortIronN,
			ShortIronE, ShortIronC, Wood1N, Wood1E, Wood1C, Wood1T,
			Wood2N, Wood2E, Wood2C, Wood2T, Wood2NLink, Wood2ELink,
			Wood3N, Wood3E, Wood3C, Wood3T
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
				case (int)Buttons.Normal:		{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Water:		{from.SendGump(new Water1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Lava:			{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:		{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:		{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:		{from.SendGump(new Trees1(m_Wand,from,0,0));break;}
				case (int)Buttons.Ground:		{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Next:			{from.SendGump(new Ground2(m_Wand,from,itemid,price));break;}

				//Pages
				case (int)Buttons.ShortBush1:	{from.SendGump(new Ground1(m_Wand,from,3215,1500));break;}
				case (int)Buttons.ShortBush2:	{from.SendGump(new Ground1(m_Wand,from,3217,1500));break;}
				case (int)Buttons.TallBush:		{from.SendGump(new Ground1(m_Wand,from,3512,1500));break;}
				case (int)Buttons.TallIronN:	{from.SendGump(new Ground1(m_Wand,from,2081,1500));break;}
				case (int)Buttons.TallIronE:	{from.SendGump(new Ground1(m_Wand,from,2083,1500));break;}
				case (int)Buttons.TallIronC:	{from.SendGump(new Ground1(m_Wand,from,2082,1500));break;}
				case (int)Buttons.ShortIronN:	{from.SendGump(new Ground1(m_Wand,from,2121,1500));break;}
				case (int)Buttons.ShortIronE:	{from.SendGump(new Ground1(m_Wand,from,2123,1500));break;}
				case (int)Buttons.ShortIronC:	{from.SendGump(new Ground1(m_Wand,from,2122,1500));break;}
				case (int)Buttons.Wood1N:		{from.SendGump(new Ground1(m_Wand,from,2103,1500));break;}
				case (int)Buttons.Wood1E:		{from.SendGump(new Ground1(m_Wand,from,2102,1500));break;}
				case (int)Buttons.Wood1C:		{from.SendGump(new Ground1(m_Wand,from,2101,1500));break;}
				case (int)Buttons.Wood1T:		{from.SendGump(new Ground1(m_Wand,from,2104,1500));break;}
				case (int)Buttons.Wood2N:		{from.SendGump(new Ground1(m_Wand,from,2142,1500));break;}
				case (int)Buttons.Wood2E:		{from.SendGump(new Ground1(m_Wand,from,2141,1500));break;}
				case (int)Buttons.Wood2C:		{from.SendGump(new Ground1(m_Wand,from,2140,1500));break;}
				case (int)Buttons.Wood2T:		{from.SendGump(new Ground1(m_Wand,from,2143,1500));break;}
				case (int)Buttons.Wood2NLink:	{from.SendGump(new Ground1(m_Wand,from,2145,1500));break;}
				case (int)Buttons.Wood2ELink:	{from.SendGump(new Ground1(m_Wand,from,2144,1500));break;}
				case (int)Buttons.Wood3N:		{from.SendGump(new Ground1(m_Wand,from,2148,1500));break;}
				case (int)Buttons.Wood3E:		{from.SendGump(new Ground1(m_Wand,from,2147,1500));break;}
				case (int)Buttons.Wood3C:		{from.SendGump(new Ground1(m_Wand,from,2146,1500));break;}
				case (int)Buttons.Wood3T:		{from.SendGump(new Ground1(m_Wand,from,2149,1500));break;}

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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 1 );
		}
	}
}
