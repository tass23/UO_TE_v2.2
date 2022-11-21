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
	public class Swamp2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Swamp2(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(226, 76, 2360, 2084, 	(int) Buttons.Lava, GumpButtonType.Reply, 0);
			this.AddButton(271, 76, 2084, 2361, 	(int) Buttons.Swamp, GumpButtonType.Reply, 0);

			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 0);
			this.AddButton(30, 155, 10850, 2360, (int)Buttons.Trees, GumpButtonType.Reply, 0);
			this.AddButton(30, 195, 2362, 10810, (int)Buttons.Ground, GumpButtonType.Reply, 0);
//			this.AddLabel(148, 74, 198, "Lava And Related");
			this.AddButton(102, 74, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
//			this.AddButton(295, 74, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 67, @"Edging 11");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Edging11, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 67, @"Edging 12");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Edging12, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 67, @"Edging 13");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Edging13, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 67, @"Edging 14");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Edging14, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 67, @"Edging 15");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Edging15, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 67, @"Edging 16");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Edging16, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 67, @"Edging 17");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Edging17, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 67, @"Edging 18");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Edging18, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 67, @"Edging 19");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Edging19, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 67, @"Edging 20");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Edging20, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 67, @"Edging 21");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Edging21, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 67, @"Edging 22");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Edging22, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 67, @"Edging 23");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Edging23, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 67, @"Edging 24");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Edging24, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 67, @"Edging 25");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Edging25, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 67, @"Edging 26");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Edging26, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 67, @"Edging 27");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Edging27, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 67, @"Edging 28");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Edging28, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 67, @"Edging 29");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Edging29, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 67, @"Edging 30");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Edging30, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 67, @"Edging 31");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Edging31, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 67, @"Edging 32");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Edging32, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 67, @"Edging 33");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Edging33, GumpButtonType.Reply, 0);
//			this.AddLabel(250, 311, 67, @"Edging 34");
//			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Edging34, GumpButtonType.Reply, 0);

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

			Edging11, Edging12, Edging13, Edging14, Edging15, Edging16, Edging17, Edging18,
			Edging19, Edging20, Edging21, Edging22, Edging23, Edging24, Edging25, Edging26,
			Edging27, Edging28, Edging29, Edging30, Edging31, Edging32, Edging33
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
				case (int)Buttons.Lava:			{from.SendGump(new Lava1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Swamp:		{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Plants:	{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:	{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Ground:	{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:{from.SendGump(new Swamp1(m_Wand,from,itemid,price));break;}

				//Pages
				case (int)Buttons.Edging11:		{from.SendGump(new Swamp2(m_Wand,from,12898,2000));break;}
				case (int)Buttons.Edging12:		{from.SendGump(new Swamp2(m_Wand,from,12899,2000));break;}
				case (int)Buttons.Edging13:		{from.SendGump(new Swamp2(m_Wand,from,12900,2000));break;}
				case (int)Buttons.Edging14:		{from.SendGump(new Swamp2(m_Wand,from,12901,2000));break;}
				case (int)Buttons.Edging15:		{from.SendGump(new Swamp2(m_Wand,from,12902,2000));break;}
				case (int)Buttons.Edging16:		{from.SendGump(new Swamp2(m_Wand,from,12903,2000));break;}
				case (int)Buttons.Edging17:		{from.SendGump(new Swamp2(m_Wand,from,12904,2000));break;}
				case (int)Buttons.Edging18:		{from.SendGump(new Swamp2(m_Wand,from,12912,500));break;}
				case (int)Buttons.Edging19:		{from.SendGump(new Swamp2(m_Wand,from,12913,500));break;}
				case (int)Buttons.Edging20:		{from.SendGump(new Swamp2(m_Wand,from,12914,500));break;}
				case (int)Buttons.Edging21:		{from.SendGump(new Swamp2(m_Wand,from,12915,500));break;}
				case (int)Buttons.Edging22:		{from.SendGump(new Swamp2(m_Wand,from,12916,500));break;}
				case (int)Buttons.Edging23:		{from.SendGump(new Swamp2(m_Wand,from,12917,500));break;}
				case (int)Buttons.Edging24:		{from.SendGump(new Swamp2(m_Wand,from,12918,500));break;}
				case (int)Buttons.Edging25:		{from.SendGump(new Swamp2(m_Wand,from,12919,500));break;}
				case (int)Buttons.Edging26:		{from.SendGump(new Swamp2(m_Wand,from,12920,500));break;}
				case (int)Buttons.Edging27:		{from.SendGump(new Swamp2(m_Wand,from,12921,500));break;}
				case (int)Buttons.Edging28:		{from.SendGump(new Swamp2(m_Wand,from,12922,500));break;}
				case (int)Buttons.Edging29:		{from.SendGump(new Swamp2(m_Wand,from,12923,500));break;}
				case (int)Buttons.Edging30:		{from.SendGump(new Swamp2(m_Wand,from,12924,500));break;}
				case (int)Buttons.Edging31:		{from.SendGump(new Swamp2(m_Wand,from,12925,500));break;}
				case (int)Buttons.Edging32:		{from.SendGump(new Swamp2(m_Wand,from,12926,500));break;}
				case (int)Buttons.Edging33:		{from.SendGump(new Swamp2(m_Wand,from,12927,500));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 13 );
		}
	}
}
