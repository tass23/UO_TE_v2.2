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
	public class Trees4 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Trees4(YardWand wand, Mobile owner, int id, int p)
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
			this.AddLabel(170, 74, 172, "Large Trees");
			this.AddButton(145, 70, 9909, 9910, (int)Buttons.Previous, GumpButtonType.Reply, 0);
//			this.AddButton(250, 70, 9903, 9904, (int)Buttons.Next, GumpButtonType.Reply, 0);

			this.AddLabel(107, 91, 537, @"Jungle 1T");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Jungle1T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 172, @"Leaves 1N");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Leaves1N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 172, @"Leaves 1O");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Leaves1O, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 537, @"Jungle 3T");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Jungle3T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 172, @"Leaves 3N");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Leaves3N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 172, @"Leaves 3O");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Leaves3O, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 537, @"Yew Tree T");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.YewTreeT, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 167, @"Vines 1");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Vines1, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 167, @"Vines 3");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Vines3, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 537, @"Tree 15T");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Tree15T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 172, @"Leaves 15N");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Leaves15N, GumpButtonType.Reply, 0);
/*			this.AddLabel(107, 311, 167, @"");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons., GumpButtonType.Reply, 0);
*/

			this.AddLabel(250, 91, 537, @"Jungle 2T");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Jungle2T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 172, @"Leaves 2N");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Leaves2N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 172, @"Leaves 2O");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Leaves2O, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 537, @"Jungle 4T");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Jungle4T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 172, @"Leaves 4N");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Leaves4N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 172, @"Leaves 4O");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Leaves4O, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 172, @"Yew Tree L");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.YewTreeL, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 167, @"Vines 2");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Vines2, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 167, @"Vines 4");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Vines3, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 537, @"Tree 16T");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Tree16T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 172, @"Leaves 16N");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Leaves16N, GumpButtonType.Reply, 0);
/*			this.AddLabel(250, 311, 167, @"");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons., GumpButtonType.Reply, 0);
*/
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

			Jungle1T, Leaves1N, Leaves1O, Jungle2T, Leaves2N, Leaves2O,
			Jungle3T, Leaves3N, Leaves3O, Jungle4T, Leaves4N, Leaves4O,
			YewTreeT, YewTreeL, Vines1, Vines2, Vines3, Vines4,
			Tree15T, Leaves15N, Tree16T, Leaves16N
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
				case (int)Buttons.Plants:		{from.SendGump(new Plants1(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Trees:		{from.SendGump(new YardGump(from,m_Wand));break;}
				case (int)Buttons.Ground:		{from.SendGump(new GroundBase(m_Wand,from,itemid,price));break;}

//				case (int)Buttons.Next:			{from.SendGump(new Trees5(from,0,0));break;}
				case (int)Buttons.Previous:		{from.SendGump(new Trees3(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.Jungle1T:		{from.SendGump(new Trees4(m_Wand,from,3395,6000));break;}
				case (int)Buttons.Leaves1N:		{from.SendGump(new Trees4(m_Wand,from,3401,5000));break;}
				case (int)Buttons.Leaves1O:		{from.SendGump(new Trees4(m_Wand,from,3408,5000));break;}
				case (int)Buttons.Jungle2T:		{from.SendGump(new Trees4(m_Wand,from,3417,6000));break;}
				case (int)Buttons.Leaves2N:		{from.SendGump(new Trees4(m_Wand,from,3423,5000));break;}
				case (int)Buttons.Leaves2O:		{from.SendGump(new Trees4(m_Wand,from,3430,5000));break;}
				case (int)Buttons.Jungle3T:		{from.SendGump(new Trees4(m_Wand,from,3440,6000));break;}
				case (int)Buttons.Leaves3N:		{from.SendGump(new Trees4(m_Wand,from,3446,5000));break;}
				case (int)Buttons.Leaves3O:		{from.SendGump(new Trees4(m_Wand,from,3453,5000));break;}
				case (int)Buttons.Jungle4T:		{from.SendGump(new Trees4(m_Wand,from,3461,6000));break;}
				case (int)Buttons.Leaves4N:		{from.SendGump(new Trees4(m_Wand,from,3465,5000));break;}
				case (int)Buttons.Leaves4O:		{from.SendGump(new Trees4(m_Wand,from,3470,5000));break;}
				case (int)Buttons.YewTreeT:		{from.SendGump(new Trees4(m_Wand,from,4793,1000));break;}
				case (int)Buttons.YewTreeL:		{from.SendGump(new Trees4(m_Wand,from,4802,5500));break;}
				case (int)Buttons.Vines1:		{from.SendGump(new Trees4(m_Wand,from,3413,4000));break;}
				case (int)Buttons.Vines2:		{from.SendGump(new Trees4(m_Wand,from,3436,4000));break;}
				case (int)Buttons.Vines3:		{from.SendGump(new Trees4(m_Wand,from,3457,4000));break;}
				case (int)Buttons.Vines4:		{from.SendGump(new Trees4(m_Wand,from,3474,4000));break;}
				case (int)Buttons.Tree15T:		{from.SendGump(new Trees4(m_Wand,from,3286,5000));break;}
				case (int)Buttons.Leaves15N:	{from.SendGump(new Trees4(m_Wand,from,3287,4000));break;}
				case (int)Buttons.Tree16T:		{from.SendGump(new Trees4(m_Wand,from,3288,5000));break;}
				case (int)Buttons.Leaves16N:	{from.SendGump(new Trees4(m_Wand,from,3289,4000));break;}
/*				case (int)Buttons.:	{from.SendGump(new Trees4(from,,));break;}
				case (int)Buttons.:	{from.SendGump(new Trees4(from,,));break;}
*/			}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 17 );
		}
	}
}
