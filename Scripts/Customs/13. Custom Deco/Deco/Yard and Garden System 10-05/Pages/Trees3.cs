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
	public class Trees3 : Gump
	{
		int price = 0;
		int itemid;
		string TGold = "";
		YardWand m_Wand;

		public Trees3(YardWand wand, Mobile owner, int id, int p)
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

			this.AddLabel(107, 91, 537, @"Tree 9T");
			this.AddButton(89, 96, 5032, 2361, (int)Buttons.Tree9T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 111, 172, @"Leaves 9N");
			this.AddButton(89, 116, 5032, 2361, (int)Buttons.Leaves9N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 131, 167, @"Leaves 9O");
			this.AddButton(89, 136, 5032, 5032, (int)Buttons.Leaves9O, GumpButtonType.Reply, 0);
			this.AddLabel(107, 151, 43, @"Leaves 9F");
			this.AddButton(89, 156, 5032, 5032, (int)Buttons.Leaves9F, GumpButtonType.Reply, 0);
			this.AddLabel(107, 171, 537, @"Tree 10T");
			this.AddButton(89, 176, 5032, 5032, (int)Buttons.Tree10T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 191, 172, @"Leaves 10N");
			this.AddButton(89, 196, 5032, 5032, (int)Buttons.Leaves10N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 211, 167, @"Leaves 10O");
			this.AddButton(89, 216, 5032, 5032, (int)Buttons.Leaves10O, GumpButtonType.Reply, 0);
			this.AddLabel(107, 231, 43, @"Leaves 10F");
			this.AddButton(89, 236, 5032, 5032, (int)Buttons.Leaves10F, GumpButtonType.Reply, 0);
			this.AddLabel(107, 251, 537, @"Tree 11T");
			this.AddButton(89, 256, 5032, 5032, (int)Buttons.Tree11T, GumpButtonType.Reply, 0);
			this.AddLabel(107, 271, 172, @"Leaves 11N");
			this.AddButton(89, 276, 5032, 5032, (int)Buttons.Leaves11N, GumpButtonType.Reply, 0);
			this.AddLabel(107, 291, 167, @"Leaves 11O");
			this.AddButton(89, 296, 5032, 5032, (int)Buttons.Leaves11O, GumpButtonType.Reply, 0);
			this.AddLabel(107, 311, 43, @"Leaves 11F");
			this.AddButton(89, 316, 5032, 5032, (int)Buttons.Leaves11F, GumpButtonType.Reply, 0);


			this.AddLabel(250, 91, 537, @"Tree 12T");
			this.AddButton(232, 96, 5032, 5032, (int)Buttons.Tree12T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 111, 172, @"Leaves 12N");
			this.AddButton(232, 116, 5032, 5032, (int)Buttons.Leaves12N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 131, 167, @"Leaves 12O");
			this.AddButton(232, 136, 5032, 5032, (int)Buttons.Leaves12O, GumpButtonType.Reply, 0);
			this.AddLabel(250, 151, 43, @"Leaves 12F");
			this.AddButton(232, 156, 5032, 5032, (int)Buttons.Leaves12F, GumpButtonType.Reply, 0);
			this.AddLabel(250, 171, 537, @"Tree 13T");
			this.AddButton(232, 176, 5032, 5032, (int)Buttons.Tree13T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 191, 172, @"Leaves 13N");
			this.AddButton(232, 196, 5032, 5032, (int)Buttons.Leaves13N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 211, 167, @"Leaves 13O");
			this.AddButton(232, 216, 5032, 5032, (int)Buttons.Leaves13O, GumpButtonType.Reply, 0);
			this.AddLabel(250, 231, 43, @"Leaves 13F");
			this.AddButton(232, 236, 5032, 5032, (int)Buttons.Leaves13F, GumpButtonType.Reply, 0);
			this.AddLabel(250, 251, 537, @"Tree 14T");
			this.AddButton(232, 256, 5032, 5032, (int)Buttons.Tree14T, GumpButtonType.Reply, 0);
			this.AddLabel(250, 271, 172, @"Leaves 14N");
			this.AddButton(232, 276, 5032, 5032, (int)Buttons.Leaves14N, GumpButtonType.Reply, 0);
			this.AddLabel(250, 291, 167, @"Leaves 14O");
			this.AddButton(232, 296, 5032, 5032, (int)Buttons.Leaves14O, GumpButtonType.Reply, 0);
			this.AddLabel(250, 311, 43, @"Leaves 14F");
			this.AddButton(232, 316, 5032, 5032, (int)Buttons.Leaves14F, GumpButtonType.Reply, 0);

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

			Tree9T, Leaves9N, Leaves9O, Leaves9F, Tree10T, Leaves10N, Leaves10O, Leaves10F,
			Tree11T, Leaves11N, Leaves11O, Leaves11F, Tree12T, Leaves12N, Leaves12O, Leaves12F,
			Tree13T, Leaves13N, Leaves13O, Leaves13F, Tree14T, Leaves14N, Leaves14O, Leaves14F,
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

				case (int)Buttons.Next:			{from.SendGump(new Trees4(m_Wand,from,itemid,price));break;}
				case (int)Buttons.Previous:		{from.SendGump(new Trees2(m_Wand,from,itemid,price));break;}
				//Pages
				case (int)Buttons.Tree9T:		{from.SendGump(new Trees3(m_Wand,from,3476,5000));break;}
				case (int)Buttons.Leaves9N:		{from.SendGump(new Trees3(m_Wand,from,3477,5000));break;}
				case (int)Buttons.Leaves9O:		{from.SendGump(new Trees3(m_Wand,from,3478,5000));break;}
				case (int)Buttons.Leaves9F:		{from.SendGump(new Trees3(m_Wand,from,3479,5000));break;}
				case (int)Buttons.Tree10T:		{from.SendGump(new Trees3(m_Wand,from,3480,5000));break;}
				case (int)Buttons.Leaves10N:	{from.SendGump(new Trees3(m_Wand,from,3481,5000));break;}
				case (int)Buttons.Leaves10O:	{from.SendGump(new Trees3(m_Wand,from,3482,5000));break;}
				case (int)Buttons.Leaves10F:	{from.SendGump(new Trees3(m_Wand,from,3483,5000));break;}
				case (int)Buttons.Tree11T:		{from.SendGump(new Trees3(m_Wand,from,3484,5000));break;}
				case (int)Buttons.Leaves11N:	{from.SendGump(new Trees3(m_Wand,from,3485,5000));break;}
				case (int)Buttons.Leaves11O:	{from.SendGump(new Trees3(m_Wand,from,3486,5000));break;}
				case (int)Buttons.Leaves11F:	{from.SendGump(new Trees3(m_Wand,from,3487,5000));break;}
				case (int)Buttons.Tree12T:		{from.SendGump(new Trees3(m_Wand,from,3488,5000));break;}
				case (int)Buttons.Leaves12N:	{from.SendGump(new Trees3(m_Wand,from,3489,5000));break;}
				case (int)Buttons.Leaves12O:	{from.SendGump(new Trees3(m_Wand,from,3490,5000));break;}
				case (int)Buttons.Leaves12F:	{from.SendGump(new Trees3(m_Wand,from,3491,5000));break;}
				case (int)Buttons.Tree13T:		{from.SendGump(new Trees3(m_Wand,from,3492,5000));break;}
				case (int)Buttons.Leaves13N:	{from.SendGump(new Trees3(m_Wand,from,3493,5000));break;}
				case (int)Buttons.Leaves13O:	{from.SendGump(new Trees3(m_Wand,from,3494,5000));break;}
				case (int)Buttons.Leaves13F:	{from.SendGump(new Trees3(m_Wand,from,3495,5000));break;}
				case (int)Buttons.Tree14T:		{from.SendGump(new Trees3(m_Wand,from,3496,5000));break;}
				case (int)Buttons.Leaves14N:	{from.SendGump(new Trees3(m_Wand,from,3497,5000));break;}
				case (int)Buttons.Leaves14O:	{from.SendGump(new Trees3(m_Wand,from,3498,5000));break;}
				case (int)Buttons.Leaves14F:	{from.SendGump(new Trees3(m_Wand,from,3499,5000));break;}
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
			from.Target = new YardTarget( m_Wand, from, itemid, p, 16 );
		}
	}
}
