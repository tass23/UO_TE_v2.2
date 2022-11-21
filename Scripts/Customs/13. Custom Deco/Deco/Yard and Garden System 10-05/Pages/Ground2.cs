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
	public class Ground2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Ground2(YardWand wand, Mobile owner, int id, int p)
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

			this.AddLabel(107, 91, 1150, @"Tall Iron Gate NW");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.TallIronGateNW, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 1150, @"Tall Iron Gate NE");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.TallIronGateNE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 1150, @"Tall Iron Gate EN");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.TallIronGateEN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 1150, @"Tall Iron Gate WN");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.TallIronGateWN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 1150, @"Tall Iron Gate SE");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.TallIronGateSE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 1150, @"Tall Iron Gate SW");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.TallIronGateSW, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 1150, @"Short Iron Gate NW");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.ShortIronGateNW, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 1150, @"Short Iron Gate NE");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.ShortIronGateNE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 1150, @"Short Iron Gate EN");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.ShortIronGateEN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 1150, @"Short Iron Gate WN");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.ShortIronGateWN, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 1150, @"Short Iron Gate SE");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.ShortIronGateSE, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 1150, @"Short Iron Gate SW");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.ShortIronGateSW, GumpButtonType.Reply, 0);

			this.AddLabel(250, 91, 1150, @"Light Wood NW");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.LightWoodNW, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 1150, @"Light Wood NE");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.LightWoodNE, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 1150, @"Light Wood EN");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.LightWoodEN, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 1150, @"Light Wood WN");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.LightWoodWN, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 1150, @"Light Wood SE");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.LightWoodSE, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 1150, @"Light Wood SW");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.LightWoodSW, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 1150, @"Dark Wood NW");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.DarkWoodNW, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 1150, @"Dark Wood NE");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.DarkWoodNE, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 1150, @"Dark Wood EN");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.DarkWoodEN, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 1150, @"Dark Wood WN");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.DarkWoodWN, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 1150, @"Dark Wood SE");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.DarkWoodSE, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 1150, @"Dark Wood SW");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.DarkWoodSW, GumpButtonType.Reply, 0);

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

			TallIronGateNW, TallIronGateNE, TallIronGateEN, TallIronGateWN, TallIronGateSE, TallIronGateSW,
			ShortIronGateNW, ShortIronGateNE, ShortIronGateEN, ShortIronGateWN, ShortIronGateSE, ShortIronGateSW,
			LightWoodNW, LightWoodNE, LightWoodEN, LightWoodWN, LightWoodSE, LightWoodSW,
			DarkWoodNW, DarkWoodNE, DarkWoodEN, DarkWoodWN, DarkWoodSE, DarkWoodSW
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

				case (int)Buttons.Plants:			{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:			{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Ground:			{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Previous:			{from.SendGump(new Ground1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Next:				{from.SendGump(new Ground3(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.TallIronGateNW:	{from.SendGump(new Ground2(m_Wand,from,2084,1500));break;}
				case (int)Buttons.TallIronGateEN:	{from.SendGump(new Ground2(m_Wand,from,2085,1500));break;}
				case (int)Buttons.TallIronGateNE:	{from.SendGump(new Ground2(m_Wand,from,2086,1500));break;}
				case (int)Buttons.TallIronGateWN:	{from.SendGump(new Ground2(m_Wand,from,2087,1500));break;}
				case (int)Buttons.TallIronGateSW:	{from.SendGump(new Ground2(m_Wand,from,2088,1500));break;}
				case (int)Buttons.TallIronGateSE:	{from.SendGump(new Ground2(m_Wand,from,2090,1500));break;}
				case (int)Buttons.ShortIronGateNW:	{from.SendGump(new Ground2(m_Wand,from,2124,1500));break;}
				case (int)Buttons.ShortIronGateEN:	{from.SendGump(new Ground2(m_Wand,from,2125,1500));break;}
				case (int)Buttons.ShortIronGateNE:	{from.SendGump(new Ground2(m_Wand,from,2126,1500));break;}
				case (int)Buttons.ShortIronGateWN:	{from.SendGump(new Ground2(m_Wand,from,2127,1500));break;}
				case (int)Buttons.ShortIronGateSW:	{from.SendGump(new Ground2(m_Wand,from,2128,1500));break;}
				case (int)Buttons.ShortIronGateSE:	{from.SendGump(new Ground2(m_Wand,from,2130,1500));break;}
				case (int)Buttons.LightWoodNW:		{from.SendGump(new Ground2(m_Wand,from,2105,1500));break;}
				case (int)Buttons.LightWoodEN:		{from.SendGump(new Ground2(m_Wand,from,2106,1500));break;}
				case (int)Buttons.LightWoodNE:		{from.SendGump(new Ground2(m_Wand,from,2107,1500));break;}
				case (int)Buttons.LightWoodWN:		{from.SendGump(new Ground2(m_Wand,from,2108,1500));break;}
				case (int)Buttons.LightWoodSW:		{from.SendGump(new Ground2(m_Wand,from,2109,1500));break;}
				case (int)Buttons.LightWoodSE:		{from.SendGump(new Ground2(m_Wand,from,2111,1500));break;}
				case (int)Buttons.DarkWoodNW:		{from.SendGump(new Ground2(m_Wand,from,2150,1500));break;}
				case (int)Buttons.DarkWoodEN:		{from.SendGump(new Ground2(m_Wand,from,2151,1500));break;}
				case (int)Buttons.DarkWoodNE:		{from.SendGump(new Ground2(m_Wand,from,2152,1500));break;}
				case (int)Buttons.DarkWoodWN:		{from.SendGump(new Ground2(m_Wand,from,2153,1500));break;}
				case (int)Buttons.DarkWoodSW:		{from.SendGump(new Ground2(m_Wand,from,2154,1500));break;}
				case (int)Buttons.DarkWoodSE:		{from.SendGump(new Ground2(m_Wand,from,2156,1500));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 2 );
		}
	}
}
