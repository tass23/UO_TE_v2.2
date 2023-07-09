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
	public class Trees2 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Trees2(YardWand wand, Mobile owner, int id, int p)
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
			this.AddButton(30, 115, 10830, 2361, (int)Buttons.Plants, GumpButtonType.Reply, 1);
			this.AddButton(30, 155, 2360, 10850, (int)Buttons.Trees, GumpButtonType.Reply, 3);
			this.AddButton(30, 195, 10810, 2362, (int)Buttons.Ground, GumpButtonType.Reply, 4);
			this.AddLabel(170, 74, 172, "Small Trees");
			this.AddButton(145, 70, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
			this.AddButton(250, 70, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 537, @"Tree 1T");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Tree1T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 172, @"Leaves 1N ");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Leaves1N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 43, @"Leaves 1F");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Leaves1F, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 537, @"Tree 2T");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Tree2T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 172, @"Leaves 2N");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Leaves2N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 43, @"Leaves 2F");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Leaves2F, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 537, @"Tree 3T");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Tree3T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 172, @"Leaves 3N");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Leaves3N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 43, @"Leaves 3F");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Leaves3F, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 537, @"Tree 4T");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Tree4T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 172, @"Leaves 4N");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Leaves4N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 43, @"Leaves 4F");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Leaves4F, GumpButtonType.Reply, 0);


			this.AddLabel(250, 91, 537, @"Tree 5T");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Tree5T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 172, @"Leaves 5N");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Leaves5N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 43, @"Leaves 5F");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Leaves5F, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 537, @"Tree 6T");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Tree6T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 172, @"Leaves 6N");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Leaves6N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 43, @"Leaves 6F");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Leaves6F, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 537, @"Tree 7T");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Tree7T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 172, @"Leaves 7N");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Leaves7N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 43, @"Leaves 7F");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Leaves7F, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 537, @"Tree 8T");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Tree8T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 172, @"Leaves 8N");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Leaves8N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 43, @"Leaves 8F");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Leaves8F, GumpButtonType.Reply, 0);

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

			Tree1T, Leaves1N, Leaves1F, Tree2T, Leaves2N, Leaves2F,
			Tree3T, Leaves3N, Leaves3F, Tree4T, Leaves4N, Leaves4F,
			Tree5T, Leaves5N, Leaves5F, Tree6T, Leaves6N, Leaves6F,
			Tree7T, Leaves7N, Leaves7F, Tree8T, Leaves8N, Leaves8F,
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
				case (int)Buttons.Settings:	{from.SendGump(new YGSettingsGump(m_Wand,from));break;}
				case (int)Buttons.Plants:	{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:	{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Ground:	{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}

				case (int)Buttons.Next:		{from.SendGump(new Trees3(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Previous:	{from.SendGump(new Trees1(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.Tree1T:	{from.SendGump(new Trees2(m_Wand,from,3277,500));break;}
				case (int)Buttons.Leaves1N:	{from.SendGump(new Trees2(m_Wand,from,3278,5000));break;}
				case (int)Buttons.Leaves1F:	{from.SendGump(new Trees2(m_Wand,from,3279,5000));break;}
				case (int)Buttons.Tree2T:	{from.SendGump(new Trees2(m_Wand,from,3280,500));break;}
				case (int)Buttons.Leaves2N:	{from.SendGump(new Trees2(m_Wand,from,3281,5000));break;}
				case (int)Buttons.Leaves2F:	{from.SendGump(new Trees2(m_Wand,from,3282,5000));break;}
				case (int)Buttons.Tree3T:	{from.SendGump(new Trees2(m_Wand,from,3283,500));break;}
				case (int)Buttons.Leaves3N:	{from.SendGump(new Trees2(m_Wand,from,3284,5000));break;}
				case (int)Buttons.Leaves3F:	{from.SendGump(new Trees2(m_Wand,from,3285,5000));break;}
				case (int)Buttons.Tree4T:	{from.SendGump(new Trees2(m_Wand,from,3290,500));break;}
				case (int)Buttons.Leaves4N:	{from.SendGump(new Trees2(m_Wand,from,3291,5000));break;}
				case (int)Buttons.Leaves4F:	{from.SendGump(new Trees2(m_Wand,from,3292,5000));break;}
				case (int)Buttons.Tree5T:	{from.SendGump(new Trees2(m_Wand,from,3293,5000));break;}
				case (int)Buttons.Leaves5N:	{from.SendGump(new Trees2(m_Wand,from,3294,5000));break;}
				case (int)Buttons.Leaves5F:	{from.SendGump(new Trees2(m_Wand,from,3295,5000));break;}
				case (int)Buttons.Tree6T:	{from.SendGump(new Trees2(m_Wand,from,3296,5000));break;}
				case (int)Buttons.Leaves6N:	{from.SendGump(new Trees2(m_Wand,from,3297,5000));break;}
				case (int)Buttons.Leaves6F:	{from.SendGump(new Trees2(m_Wand,from,3298,5000));break;}
				case (int)Buttons.Tree7T:	{from.SendGump(new Trees2(m_Wand,from,3299,5000));break;}
				case (int)Buttons.Leaves7N:	{from.SendGump(new Trees2(m_Wand,from,3300,5000));break;}
				case (int)Buttons.Leaves7F:	{from.SendGump(new Trees2(m_Wand,from,3301,5000));break;}
				case (int)Buttons.Tree8T:	{from.SendGump(new Trees2(m_Wand,from,3302,5000));break;}
				case (int)Buttons.Leaves8N:	{from.SendGump(new Trees2(m_Wand,from,3303,5000));break;}
				case (int)Buttons.Leaves8F:	{from.SendGump(new Trees2(m_Wand,from,3304,5000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 15 );
		}
	}
}
