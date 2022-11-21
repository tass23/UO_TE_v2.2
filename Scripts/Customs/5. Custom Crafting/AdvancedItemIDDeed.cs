///////////////////////////////////////////////
///                                        ///
///	  Originaly made by System 32    	  ///
///        Edits made by KaNJi           ///
///                                     ///
//////////////////////////////////////////
/*Enhanced by  _____         
*	  		   \_   \___ ___ 
*			    / /\/ __/ _ \
*		     /\/ /_| (_|  __/
*			 \____/ \___\___|
*/
using System;
using Server;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Prompts;
using Server.Targeting;
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	public class ItemIDDeed : Item
	{

		[Constructable]
		public ItemIDDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
			Hue = 1086;
			Name = "Advanced Item ID Deed";
		}

		public ItemIDDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else
			{
				from.CloseGump( typeof( ItemIDDeedGump));
				from.SendGump( new ItemIDDeedGump() );
			}
		}
	}
}
namespace Server.Gumps
{
	public class ItemIDDeedGump : Gump
	{
		public ItemIDDeedGump()
			: base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=true;
			AddPage(1);			
			AddBackground(73, 38, 334, 236, 5054);
			AddBackground(83, 45, 312, 73, 5054);
			AddLabel(164, 58, 0, @"Advanced ItemID Deed");
			//AddLabel(218, 78, 0, "By Ice");
			AddButton(103, 199, 1149, 1148, 0, GumpButtonType.Page, 100);//smithy
			AddButton(301, 199, 1149, 1148, 0, GumpButtonType.Page, 200);//tailoring
			AddLabel(99, 240, 0, "Smithy Items");
			AddLabel(293, 240, 0, "Tailoring Items");
			AddBackground(103, 132, 63, 60, 5054);
			AddBackground(301, 132, 63, 60, 5054);
			AddImage(98, 60, 2274);
			AddImage(339, 60, 2274);
			AddItem(115, 155, 5091);
			AddItem(305, 152, 3997);
			AddItem(200, 153, 2981);
			AddItem(227, 154, 3016);
			AddItem(198, 136, 2971);
			AddItem(230, 136, 2972);
			AddButton(200, 199, 1149, 1148, 0, GumpButtonType.Page, 300);//misc
			AddLabel(202, 235, 0, @"Misc Items");

			AddPage(100);
			AddBackground(51, 25, 216, 315, 5054);
			AddButton(80, 55, 2076, 2075, 0, GumpButtonType.Page, 101);
			AddLabel(142, 55, 0, "Ringmail");
			AddButton(80, 90, 2076, 2075, 0, GumpButtonType.Page, 102);
			AddLabel(142, 90, 0, "Chainmail");
			AddButton(80, 125, 2076, 2075, 0, GumpButtonType.Page, 103);
			AddLabel(142, 125, 0, "Platemail");
			AddButton(80, 160, 2076, 2075, 0, GumpButtonType.Page, 104);
			AddLabel(142, 160, 0, "Helmets");
			AddButton(80, 195, 2076, 2075, 0, GumpButtonType.Page, 105);
			AddLabel(142, 195, 0, "Shields");
			AddButton(80, 230, 2076, 2075, 0, GumpButtonType.Page, 106);
			AddLabel(142, 230, 0, "Dragon Armor");
			AddButton(129, 279, 1112, 1111, 0, GumpButtonType.Page, 1);
						
			AddPage(101);//RingMail
			AddBackground(2, 2, 227, 267, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,2);
			AddItem(75, 20, 5099);
			AddLabel(145, 24, 0, @"Gloves");
			this.AddCheck(20, 65, 2152, 2153, false,3);
			AddItem(75, 65, 5104);
			AddLabel(145, 67, 0, @"Leggings");
			this.AddCheck(20, 110, 2152, 2153, false,4);
			AddItem(75, 115, 5102);
			AddLabel(145, 114, 0, @"Sleeves");
			this.AddCheck(20, 155, 2152, 2153, false,5);
			AddItem(75, 155, 5100);
			AddLabel(145, 162, 0, @"Tunic");
			this.AddButton(24, 225, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(140, 225, 242, 241, 0, GumpButtonType.Page, 100);//cancel
			
			AddPage(102);
			AddBackground(2, 2, 227, 209, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,6);
			AddItem(66, 26, 5051);
			AddLabel(145, 24, 0, @"Coif");
			this.AddCheck(20, 65, 2152, 2153, false,7);
			AddItem(70, 64, 5054);
			AddLabel(145, 67, 0, @"Leggings");
			this.AddCheck(20, 110, 2152, 2153, false,8);
			AddItem(63, 109, 5055);
			AddLabel(145, 114, 0, @"Tunic");
			this.AddButton(24, 167, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(140, 167, 242, 241, 0, GumpButtonType.Page, 100);//cancel

			AddPage(103);
			AddBackground(2, 2, 227, 446, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,9);
			AddItem(60, 19, 5136);
			AddLabel(113, 22, 0, @"Arms");
			this.AddCheck(20, 55, 2152, 2153, false,10);
			AddItem(60, 57, 5140);
			AddLabel(113, 56, 0, @"Gloves");
			this.AddCheck(20, 90, 2152, 2153, false,11);
			AddItem(55, 85, 5137);
			AddLabel(113, 88, 0, @"Leggings");
			this.AddCheck(20, 125, 2152, 2153, false,12);
			AddItem(58, 122, 5141);
			AddLabel(113, 125, 0, @"Tunic");
			this.AddCheck(20, 160, 2152, 2153, false,13);
			AddItem(49, 162, 7172);
			AddLabel(113, 162, 0, @"Female Tunic");
			this.AddCheck(20, 195, 2152, 2153, false,14);
			AddItem(52, 198, 10105);
			AddLabel(113, 197, 0, @"Mempo");
			this.AddCheck(20, 230, 2152, 2153, false,15);
			AddItem(54, 227, 10109);
			AddLabel(113, 230, 0, @"Do");
			this.AddCheck(20, 265, 2152, 2153, false,16);
			AddItem(54, 263, 10112);
			AddLabel(110, 267, 0, @"Hiro Sode");
			this.AddCheck(20, 300, 2152, 2153, false,17);
			AddItem(55, 301, 10120);
			AddLabel(113, 301, 0, @"Suneate");
			this.AddCheck(20, 335, 2152, 2153, false,18);
			AddItem(59, 333, 10125);
			AddLabel(113, 333, 0, @"Haidate");
			this.AddButton(24, 400, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(140, 400, 242, 241, 0, GumpButtonType.Page, 100);//cancel

AddPage(104);
			AddBackground(0, 0, 438, 374, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,19);
			AddItem(47, 23, 5132);
			AddLabel(91, 23, 0, @"Bascinet");
			this.AddCheck(20, 55, 2152, 2153, false,20);
			AddItem(48, 61, 5128);
			AddLabel(91, 58, 0, @"Close Helmet");
			this.AddCheck(20, 90, 2152, 2153, false,21);
			AddItem(48, 95, 5130);
			AddLabel(91, 92, 0, @"Helmet");
			this.AddCheck(20, 125, 2152, 2153, false,22);
			AddItem(49, 127, 5134);
			AddLabel(91, 124, 0, @"Norse Helm");
			this.AddCheck(20, 160, 2152, 2153, false,23);
			AddItem(49, 163, 5138);
			AddLabel(91, 159, 0, @"Plate Helm");
			this.AddCheck(20, 195, 2152, 2153, false,24);
			AddItem(48, 198, 10100);
			AddLabel(91, 196, 0, @"Chain Hatsuburi");
			this.AddCheck(20, 230, 2152, 2153, false,25);
			AddItem(49, 233, 10101);
			AddLabel(91, 233, 0, @"Plate Hatsuburi");
			this.AddCheck(20, 265, 2152, 2153, false,26);
			AddItem(52, 270, 10103);
			AddLabel(94, 265, 0, @"Heavy Jingasa");
			this.AddCheck(217, 20, 2152, 2153, false,27);
			AddItem(250, 25, 10113);
			AddLabel(305, 23, 0, @"Light Jingasa");
			this.AddCheck(217, 55, 2152, 2153, false,28);
			AddItem(249, 60, 10116);
			AddLabel(305, 56, 0, @"Small Jingasa");
			this.AddCheck(217, 90, 2152, 2153, false,29);
			AddItem(249, 92, 10104);
			AddLabel(305, 92, 0, @"Decorative Kabuto");
			this.AddCheck(217, 125, 2152, 2153, false,30);
			AddItem(248, 128, 10117);
			AddLabel(305, 126, 0, @"Battle Kabuto");
			this.AddCheck(217, 160, 2152, 2153, false,31);
			AddItem(249, 160, 10121);
			AddLabel(305, 158, 0, @"Standard Kabuto");
			this.AddCheck(217, 195, 2152, 2153, false,32);
			AddItem(250, 201, 11118);
			AddLabel(305, 197, 0, @"Circlet");
			this.AddCheck(217, 230, 2152, 2153, false,33);
			AddItem(249, 235, 11119);
			AddLabel(305, 230, 0, @"Royal Circlet");
			this.AddCheck(217, 265, 2152, 2153, false,34);
			AddItem(248, 273, 11120);
			AddLabel(305, 266, 0, @"Gemmed Circlet");
			this.AddButton(102, 315, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(238, 315, 242, 241, 0, GumpButtonType.Page, 100);//cancel

AddPage(105);
			AddBackground(0, 0, 227, 407, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,35);
			AddItem(52, 25, 7027);
			AddLabel(105, 22, 0, @"Buckler");
			this.AddCheck(20, 55, 2152, 2153, false,36);
			AddItem(52, 57, 7026);
			AddLabel(105, 55, 0, @"Bronze Shield");
			this.AddCheck(20, 90, 2152, 2153, false,37);
			AddItem(50, 86, 7030);
			AddLabel(105, 87, 0, @"Heater Shield");
			this.AddCheck(20, 125, 2152, 2153, false,38);
			AddItem(50, 127, 7035);
			AddLabel(105, 123, 0, @"Metal Shield");
			this.AddCheck(20, 160, 2152, 2153, false,39);
			AddItem(46, 154, 7028);
			AddLabel(105, 159, 0, @"Kite Shield");
			this.AddCheck(20, 195, 2152, 2153, false,40);
			AddItem(55, 193, 7033);
			AddLabel(105, 194, 0, @"Tear Kite Shield");
			this.AddCheck(20, 230, 2152, 2153, false,41);
			AddItem(50, 232, 7107);
			AddLabel(105, 228, 0, @"Chaos Shield");
			this.AddCheck(20, 265, 2152, 2153, false,42);
			AddItem(44, 262, 7108);
			AddLabel(105, 264, 0, @"Order Shield");
			this.AddCheck(20, 300, 2152, 2153, false,43);
			AddItem(49, 298, 11009);
			AddLabel(105, 301, 0, @"Dupre's Sheild");
			this.AddButton(31, 355, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(131, 355, 242, 241, 0, GumpButtonType.Page, 100);//cancel
			
AddPage(106);
			AddBackground(0, 0, 227, 264, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,44);
			AddItem(54, 19, 9795);
			AddLabel(115, 20, 0, @"Gloves");
			this.AddCheck(20, 55, 2152, 2153, false,45);
			AddItem(53, 56, 9797);
			AddLabel(115, 57, 0, @"Helm");
			this.AddCheck(20, 90, 2152, 2153, false,46);
			AddItem(57, 87, 9799);
			AddLabel(115, 92, 0, @"Leggings");
			this.AddCheck(20, 125, 2152, 2153, false,47);
			AddItem(56, 127, 9815);
			AddLabel(115, 128, 0, @"Sleeves");
			this.AddCheck(20, 160, 2152, 2153, false,48);
			AddItem(53, 156, 9793);
			AddLabel(115, 163, 0, @"Breastplate");	
			this.AddButton(35, 214, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(131, 215, 242, 241, 0, GumpButtonType.Page, 100);//cancel	

AddPage(200);
			AddBackground(0, 0, 216, 315, 5054);
			AddButton(20, 20, 2076, 2075, 0, GumpButtonType.Page, 201);
			AddLabel(90, 20, 0, @"Hats");
			AddButton(20, 45, 2076, 2075, 0, GumpButtonType.Page, 202);
			AddLabel(90, 45, 0, @"Shirts");
			AddButton(20, 70, 2076, 2075, 0, GumpButtonType.Page, 203);
			AddLabel(90, 70, 0, @"Pants");
			AddButton(20, 95, 2076, 2075, 0, GumpButtonType.Page, 204);
			AddLabel(90, 95, 0, @"Miscellaneous");
			AddButton(20, 120, 2076, 2075, 0, GumpButtonType.Page, 205);
			AddLabel(90, 120, 0, @"Footwear");
			AddButton(20, 145, 2076, 2075, 0, GumpButtonType.Page, 206);
			AddLabel(90, 145, 0, @"Leather Armor");
			AddButton(20, 170, 2076, 2075, 0, GumpButtonType.Page, 207);
			AddLabel(90, 170, 0, @"Studded Armor");
			AddButton(20, 195, 2076, 2075, 0, GumpButtonType.Page, 208);
			AddLabel(90, 195, 0, @"Female Armor");
			AddButton(20, 220, 2076, 2075, 0, GumpButtonType.Page, 209);
			AddLabel(90, 220, 0, @"Bone Armor");
			AddButton(71, 259, 1112, 1111, 0, GumpButtonType.Page, 1);
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
AddPage(201);
			AddBackground(0, 0, 424, 329, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,49);
			AddItem(41, 25, 5444);
			AddLabel(95, 20, 0, @"Skullcap");
			this.AddCheck(20, 55, 2152, 2153, false,50);
			AddItem(46, 61, 5440);
			AddLabel(95, 55, 0, @"Bandana");
			this.AddCheck(20, 90, 2152, 2153, false,51);
			AddItem(50, 93, 5907);
			AddLabel(95, 90, 0, @"Floppy Hat");
			this.AddCheck(20, 125, 2152, 2153, false,52);
			AddItem(48, 130, 5909);
			AddLabel(95, 125, 0, @"Cap");
			this.AddCheck(20, 160, 2152, 2153, false,53);
			AddItem(55, 166, 5908);
			AddLabel(95, 160, 0, @"Wide-brim Hat");
			this.AddCheck(20, 195, 2152, 2153, false,54);
			AddItem(50, 198, 5911);
			AddLabel(100, 195, 0, @"Straw Hat");
			this.AddCheck(20, 230, 2152, 2153, false,55);
			AddItem(51, 230, 5910);
			AddLabel(100, 230, 0, @"Tall Straw Hat");
			this.AddCheck(20, 265, 2152, 2153, false,56);
			AddItem(51, 271, 5912);
			AddLabel(100, 265, 0, @"Wizard's Hat");
			this.AddCheck(223, 20, 2152, 2153, false,57);
			AddItem(251, 25, 5913);
			AddLabel(299, 20, 0, @"Bonnet");
			this.AddCheck(223, 55, 2152, 2153, false,58);
			AddItem(253, 60, 5914);
			AddLabel(300, 60, 0, @"Feathered Hat");
			this.AddCheck(223, 90, 2152, 2153, false,59);
			AddItem(251, 94, 5915);
			AddLabel(300, 90, 0, @"Tricorne Hat");
			this.AddCheck(223, 125, 2152, 2153, false,60);
			AddItem(253, 130, 5916);
			AddLabel(300, 125, 0, @"Jester Hat");
			this.AddCheck(223, 160, 2152, 2153, false,61);
			AddItem(238, 165, 8966);
			AddLabel(300, 165, 0, @"Flower Garland");
			this.AddCheck(223, 195, 2152, 2153, false,62);
			AddItem(254, 198, 10127);
			AddLabel(300, 195, 0, @"Cloth Ninja Hood");
			this.AddCheck(223, 230, 2152, 2153, false,63);
			AddItem(255, 236, 10136);
			AddLabel(300, 230, 0, @"Kasa");
			this.AddButton(217, 281, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(316, 281, 242, 241, 0, GumpButtonType.Page, 200);//cancel	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
AddPage(202);
			AddBackground(0, 0, 445, 405, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,64);
			AddItem(28, 21, 8059);
			AddLabel(100, 22, 0, @"Doublet");
			this.AddCheck(20, 55, 2152, 2153, false,65);
			AddItem(46, 56, 5399);
			AddLabel(100, 55, 0, @"Shirt");
			this.AddCheck(20, 90, 2152, 2153, false,66);
			AddItem(44, 94, 7933);
			AddLabel(100, 90, 0, @"Fancy Shirt");
			this.AddCheck(20, 125, 2152, 2153, false,67);
			AddItem(35, 121, 8097);
			AddLabel(100, 125, 0, @"Tunic");
			this.AddCheck(20, 160, 2152, 2153, false,68);
			AddItem(34, 154, 8189);
			AddLabel(100, 155, 0, @"Surcoat");
			this.AddCheck(20, 195, 2152, 2153, false,69);
			AddItem(46, 183, 7937);
			AddLabel(100, 190, 0, @"Plain Dress");
			this.AddCheck(20, 230, 2152, 2153, false,70);
			AddItem(46, 208, 7936);
			AddLabel(100, 230, 0, @"Fancy Dress");
			this.AddCheck(20, 265, 2152, 2153, false,71);
			AddItem(47, 259, 5397);
			AddLabel(103, 265, 0, @"Cloak");
			this.AddCheck(20, 300, 2152, 2153, false,72);
			AddItem(46, 292, 7939);
			AddLabel(100, 300, 0, @"Robe");
			this.AddCheck(20, 335, 2152, 2153, false,73);
			AddItem(34, 335, 8095);
			AddLabel(100, 335, 0, @"Jester Suit");
			this.AddCheck(223, 20, 2152, 2153, false,74);
			AddItem(245, 18, 8970);
			AddLabel(310, 20, 0, @"Fur Cape");
			this.AddCheck(223, 55, 2152, 2153, false,75);
			AddItem(246, 48, 8974);
			AddLabel(310, 55, 0, @"Gilded Dress");
			this.AddCheck(223, 90, 2152, 2153, false,76);
			AddItem(253, 86, 8976);
			AddLabel(310, 90, 0, @"Formal Shirt");
			this.AddCheck(223, 125, 2152, 2153, false,77);
			AddItem(257, 124, 10132);
			AddLabel(310, 125, 0, @"Cloth Ninja Jacket");
			this.AddCheck(223, 160, 2152, 2153, false,78);
			AddItem(258, 144, 10137);
			AddLabel(310, 165, 0, @"Kamishimo");
			this.AddCheck(223, 195, 2152, 2153, false,79);
			AddItem(259, 195, 10140);
			AddLabel(310, 200, 0, @"Hakama-Shita");
			this.AddCheck(223, 230, 2152, 2153, false,80);
			AddItem(257, 224, 10114);
			AddLabel(310, 235, 0, @"Male Kimono");
			this.AddCheck(223, 265, 2152, 2153, false,81);
			AddItem(258, 258, 10115);
			AddLabel(310, 265, 0, @"Female Kimono");
			this.AddCheck(223, 300, 2152, 2153, false,82);
			AddItem(254, 300, 10145);
			AddLabel(310, 300, 0, @"Jin-Baori");
			this.AddButton(216, 351, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(349, 351, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(203);
			AddBackground(0, 0, 235, 340, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,83);
			AddItem(51, 23, 5422);
			AddLabel(100, 20, 0, @"Short Pants");
			this.AddCheck(20, 55, 2152, 2153, false,84);
			AddItem(48, 54, 5433);
			AddLabel(100, 55, 0, @"Long Pants");
			this.AddCheck(20, 90, 2152, 2153, false,85);
			AddItem(53, 93, 5431);
			AddLabel(100, 90, 0, @"Kilt");
			this.AddCheck(20, 125, 2152, 2153, false,86);
			AddItem(44, 125, 5398);
			AddLabel(100, 125, 0, @"Skirt");
			this.AddCheck(20, 160, 2152, 2153, false,87);
			AddItem(47, 162, 8972);
			AddLabel(100, 165, 0, @"Fur Sarong");
			this.AddCheck(20, 195, 2152, 2153, false,88);
			AddItem(52, 191, 10138);
			AddLabel(100, 195, 0, @"Hakama");
			this.AddCheck(20, 230, 2152, 2153, false,89);
			AddItem(48, 232, 10139);
			AddLabel(100, 230, 0, @"Tattsuke-Hakama");
			this.AddButton(30, 287, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(133, 287, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(204);
			AddBackground(0, 0, 235, 186, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,90);
			AddItem(50, 22, 5441);
			AddLabel(100, 20, 0, @"Body Sash");
			this.AddCheck(20, 55, 2152, 2153, false,91);
			AddItem(53, 53, 5435);
			AddLabel(100, 55, 0, @"Half Apron");
			this.AddCheck(20, 90, 2152, 2153, false,92);
			AddItem(51, 87, 5437);
			AddLabel(100, 90, 0, @"Full Apron");
			this.AddButton(36, 138, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(136, 138, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(205);
			AddBackground(0, 0, 235, 338, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,93);
			AddItem(58, 24, 8967);
			AddLabel(100, 20, 0, @"Fur Boots");
			this.AddCheck(20, 55, 2152, 2153, false,94);
			AddItem(42, 55, 10135);
			AddLabel(100, 55, 0, @"Ninja Tabi");
			this.AddCheck(20, 90, 2152, 2153, false,95);
			AddItem(58, 90, 10134);
			AddLabel(100, 90, 0, @"Waraji And Tabi");
			this.AddCheck(20, 125, 2152, 2153, false,96);
			AddItem(56, 126, 5901);
			AddLabel(100, 125, 0, @"Sandals");
			this.AddCheck(20, 160, 2152, 2153, false,97);
			AddItem(53, 163, 5903);
			AddLabel(100, 160, 0, @"Shoes");
			this.AddCheck(20, 195, 2152, 2153, false,98);
			AddItem(50, 196, 5899);
			AddLabel(100, 200, 0, @"Boots");
			this.AddCheck(20, 230, 2152, 2153, false,99);
			AddItem(53, 229, 5905);
			AddLabel(100, 235, 0, @"Thigh Boots");
			this.AddButton(31, 285, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(133, 285, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(206);
			AddBackground(0, 0, 441, 355, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,400);
			AddItem(58, 25, 5063);
			AddLabel(100, 20, 0, @"Gorget");
			this.AddCheck(20, 55, 2152, 2153, false,401);
			AddItem(51, 59, 7609);
			AddLabel(100, 55, 0, @"Cap");
			this.AddCheck(20, 90, 2152, 2153, false,402);
			AddItem(50, 92, 5062);
			AddLabel(100, 90, 0, @"Gloves");
			this.AddCheck(20, 125, 2152, 2153, false,403);
			AddItem(51, 126, 5069);
			AddLabel(100, 125, 0, @"Sleeves");
			this.AddCheck(20, 160, 2152, 2153, false,404);
			AddItem(40, 158, 5067);
			AddLabel(100, 160, 0, @"Leggings");
			this.AddCheck(20, 195, 2152, 2153, false,405);
			AddItem(42, 192, 5068);
			AddLabel(100, 195, 0, @"Tunic");
			this.AddCheck(20, 230, 2152, 2153, false,406);
			AddItem(49, 233, 10102);
			AddLabel(100, 230, 0, @"Jingasa");
			this.AddCheck(20, 265, 2152, 2153, false,407);
			AddItem(48, 267, 10106);
			AddLabel(100, 265, 0, @"Mempo");
			this.AddCheck(20, 300, 2152, 2153, false,408);
			AddItem(49, 298, 10182);
			AddLabel(100, 300, 0, @"Do");
			this.AddCheck(196, 20, 2152, 2153, false,409);
			AddItem(231, 19, 10110);
			AddLabel(285, 20, 0, @"Hiro Sode");
			this.AddCheck(196, 55, 2152, 2153, false,410);
			AddItem(228, 56, 10118);
			AddLabel(285, 55, 0, @"Suneate");
			this.AddCheck(196, 90, 2152, 2153, false,412);
			AddItem(232, 89, 10122);
			AddLabel(285, 93, 0, @"Haidate");
			this.AddCheck(196, 125, 2152, 2153, false,413);
			AddItem(219, 123, 10129);
			AddLabel(285, 125, 0, @"Ninja Pants");
			this.AddCheck(196, 160, 2152, 2153, false,414);
			AddItem(233, 162, 10131);
			AddLabel(285, 160, 0, @"Ninja Jacket");
			this.AddCheck(196, 195, 2152, 2153, false,415);
			AddItem(228, 199, 10128);
			AddLabel(285, 195, 0, @"Ninja Belt");
			this.AddCheck(196, 230, 2152, 2153, false,416);
			AddItem(226, 230, 10130);
			AddLabel(285, 230, 0, @"Ninja Mitts");
			this.AddCheck(196, 265, 2152, 2153, false,417);
			AddItem(226, 270, 10126);
			AddLabel(285, 265, 0, @"Ninja Hood");
			this.AddButton(209, 306, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(323, 306, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(207);
			AddBackground(0, 0, 381, 265, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,418);
			AddItem(54, 27, 5078);
			AddLabel(100, 20, 0, @"Gorget");
			this.AddCheck(20, 55, 2152, 2153, false,419);
			AddItem(52, 55, 5077);
			AddLabel(100, 55, 0, @"Gloves");
			this.AddCheck(20, 90, 2152, 2153, false,420);
			AddItem(51, 92, 5084);
			AddLabel(100, 90, 0, @"Sleeves");
			this.AddCheck(20, 125, 2152, 2153, false,421);
			AddItem(43, 123, 5082);
			AddLabel(100, 125, 0, @"Leggings");
			this.AddCheck(20, 160, 2152, 2153, false,422);
			AddItem(41, 157, 5083);
			AddLabel(100, 160, 0, @"Tunic");
			this.AddCheck(197, 20, 2152, 2153, false,423);
			AddItem(226, 22, 10141);
			AddLabel(280, 20, 0, @"Mempo");
			this.AddCheck(197, 55, 2152, 2153, false,424);
			AddItem(226, 51, 10183);
			AddLabel(280, 55, 0, @"Do");
			this.AddCheck(197, 90, 2152, 2153, false,425);
			AddItem(231, 88, 10111);
			AddLabel(280, 90, 0, @"Hiro Sode");
			this.AddCheck(197, 125, 2152, 2153, false,426);
			AddItem(230, 130, 10194);
			AddLabel(280, 130, 0, @"Suneate");
			this.AddCheck(197, 160, 2152, 2153, false,427);
			AddItem(232, 159, 10123);
			AddLabel(280, 160, 0, @"Haidate");
			this.AddButton(66, 210, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(214, 210, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(208);
			AddBackground(0, 0, 228, 310, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,428);
			AddItem(61, 22, 7168);
			AddLabel(110, 20, 0, @"Shorts");
			this.AddCheck(20, 55, 2152, 2153, false,429);
			AddItem(56, 59, 7176);
			AddLabel(110, 55, 0, @"Skirt");
			this.AddCheck(20, 90, 2152, 2153, false,430);
			AddItem(51, 93, 7178);
			AddLabel(110, 90, 0, @"Bustier");
			this.AddCheck(20, 125, 2152, 2153, false,431);
			AddItem(50, 128, 7180);
			AddLabel(110, 125, 0, @"Studded Bustier");
			this.AddCheck(20, 160, 2152, 2153, false,432);
			AddItem(53, 161, 7174);
			AddLabel(110, 160, 0, @"Female Armor");
			this.AddCheck(20, 195, 2152, 2153, false,433);
			AddItem(51, 192, 7170);
			AddLabel(110, 200, 0, @"Studded Armor");	
			this.AddButton(35, 254, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(131, 254, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(209);
			AddBackground(0, 0, 228, 278, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,434);
			AddItem(52, 25, 5201);
			AddLabel(105, 20, 0, @"Helmet");
			this.AddCheck(20, 55, 2152, 2153, false,435);
			AddItem(51, 59, 5200);
			AddLabel(105, 55, 0, @"Gloves");
			this.AddCheck(20, 90, 2152, 2153, false,436);
			AddItem(52, 96, 5198);
			AddLabel(105, 90, 0, @"Arms");
			this.AddCheck(20, 125, 2152, 2153, false,437);
			AddItem(53, 123, 5202);
			AddLabel(105, 125, 0, @"Leggings");
			this.AddCheck(20, 160, 2152, 2153, false,438);
			AddItem(45, 152, 5199);
			AddLabel(105, 160, 0, @"Breastplate");
			this.AddButton(30, 226, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(131, 226, 242, 241, 0, GumpButtonType.Page, 200);//cancel	

			AddPage(300);
			AddBackground(0, 0, 504, 444, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,439);
			AddItem(50, 16, 5445);
			AddLabel(110, 20, 0, @"Bear Mask");
			this.AddCheck(20, 55, 2152, 2153, false,440);
			AddItem(47, 49, 5447);
			AddLabel(110, 55, 0, @"Deer Mask");
			this.AddCheck(20, 90, 2152, 2153, false,441);
			AddItem(54, 92, 5449);
			AddLabel(110, 94, 0, @"Tribal Mask 0");
			this.AddCheck(20, 125, 2152, 2153, false,442);
			AddItem(54, 133, 5451);
			AddLabel(110, 125, 0, @"Tribal Mask 1");
			this.AddCheck(20, 160, 2152, 2153, false,443);
			AddItem(46, 165, 7947);
			AddLabel(110, 165, 0, @"Orc Helm");
			this.AddCheck(20, 200, 2152, 2153, false,444);
			AddItem(56, 190, 9859);
			AddLabel(110, 200, 0, @"Hooded Shroud");
			this.AddCheck(20, 235, 2152, 2153, false,445);
			AddItem(49, 235, 9865);
			AddLabel(110, 235, 0, @"Winged Helmet");
			this.AddCheck(20, 271, 2152, 2153, false,446);
			AddItem(52, 259, 9889);
			AddLabel(110, 270, 0, @"Feathered Mask");
			this.AddCheck(241, 20, 2152, 2153, false,447);
			AddItem(263, 21, 10219);
			AddLabel(325, 20, 0, @"Obi");
			this.AddCheck(241, 55, 2152, 2153, false,448);
			AddItem(241, 48, 11010);
			AddLabel(325, 50, 0, @"Quiver");
			this.AddCheck(241, 90, 2152, 2153, false,449);
			AddItem(242, 80, 11012);
			AddLabel(325, 90, 0, @"Cloak Of Humility");
			this.AddCheck(241, 125, 2152, 2153, false,450);
			AddItem(239, 125, 11014);
			AddLabel(325, 125, 0, @"Legs Of Honor");
			this.AddCheck(241, 160, 2152, 2153, false,451);
			AddItem(243, 154, 11016);
			AddLabel(325, 160, 0, @"Breastplate Of Jus");
			this.AddCheck(241, 200, 2152, 2153, false,452);
			AddItem(276, 201, 11018);
			AddLabel(325, 200, 0, @"Arms Of Compassion");
			this.AddCheck(241, 235, 2152, 2153, false,453);
			AddItem(270, 236, 11020);
			AddLabel(325, 235, 0, "Gauntlets Of Valor");
			this.AddCheck(242, 271, 2152, 2153, false,454);
			AddItem(267, 277, 11022);
			AddLabel(325, 270, 0, "Gorget Of Truth");
			this.AddCheck(242, 305, 2152, 2153, false,455);
			AddItem(269, 312, 11024);
			AddLabel(325, 310, 0, "Helm Of Spirituality");
			this.AddCheck(242, 340, 2152, 2153, false,456);
			AddItem(265, 347, 11026);
			AddLabel(325, 345, 0, "Solaretes Of Sacrifice");
			this.AddCheck(20, 305, 2152, 2153, false,457);
			AddItem(48, 308, 11112);
			AddLabel(110, 300, 0, "Elven Belt");
			this.AddButton(248, 396, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(403, 396, 242, 241, 0, GumpButtonType.Page, 1);//cancel	
			AddButton(85, 375, 1234, 1233, 0, GumpButtonType.Page, 700);
			AddLabel(94, 354, 0, "MORE");

AddPage(700);
			AddBackground(0, 0, 499, 385, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,458);
			AddItem(50, 20, 12120);
			AddLabel(105, 20, 0, @"Talisman 0");
			this.AddCheck(20, 55, 2152, 2153, false,459);
			AddItem(49, 53, 12121);
			AddLabel(105, 55, 0, @"Talisman 1");
			this.AddCheck(20, 90, 2152, 2153, false,460);
			AddItem(51, 89, 12122);
			AddLabel(105, 90, 0, @"Talisman 2");
			this.AddCheck(20, 125, 2152, 2153, false,461);
			AddItem(51, 127, 12123);
			AddLabel(105, 125, 0, @"Talisman 3");
			this.AddCheck(20, 160, 2152, 2153, false,462);
			AddItem(45, 154, 12217);
			AddLabel(105, 160, 0, @"Elven Robe 0");
			this.AddCheck(20, 195, 2152, 2153, false,463);
			AddItem(46, 187, 12218);
			AddLabel(105, 195, 0, @"Elven Robe 1");
			this.AddCheck(20, 230, 2152, 2153, false,464);
			AddItem(47, 227, 12227);
			AddLabel(105, 235, 0, @"Elven Pants");
			this.AddCheck(20, 265, 2152, 2153, false,465);
			AddItem(47, 270, 12228);
			AddLabel(105, 265, 0, @"Elven Boots");
			this.AddCheck(20, 300, 2152, 2153, false,466);
			AddItem(52, 299, 12229);
			AddLabel(105, 300, 0, @"Leaf Tunic");
			this.AddCheck(20, 335, 2152, 2153, false,467);
			AddItem(58, 339, 12230);
			AddLabel(105, 330, 0, @"Leaf Gloves");
			this.AddCheck(214, 20, 2152, 2153, false,468);
			AddItem(242, 26, 12231);
			AddLabel(290, 20, 0, @"Leaf Gorget");
			this.AddCheck(214, 55, 2152, 2153, false,469);
			AddItem(244, 53, 12232);
			AddLabel(290, 55, 0, @"Leaf Arms");
			this.AddCheck(214, 90, 2152, 2153, false,470);
			AddItem(240, 89, 12233);
			AddLabel(290, 90, 0, @"Leaf Leggings");
			this.AddCheck(214, 125, 2152, 2153, false,471);
			AddItem(240, 123, 12234);
			AddLabel(290, 128, 0, @"Leaf Tonlet");
			this.AddCheck(214, 160, 2152, 2153, false,472);
			AddItem(247, 159, 12235);
			AddLabel(290, 160, 0, @"Female Leaf Tunic");
			this.AddCheck(214, 195, 2152, 2153, false,473);
			AddItem(247, 203, 12648);
			AddLabel(295, 200, 0, @"Raven Helm");
			this.AddCheck(214, 230, 2152, 2153, false,474);
			AddItem(241, 237, 12658);
			AddLabel(295, 235, 0, @"Glasses");
			this.AddCheck(214, 265, 2152, 2153, false,475);
			AddItem(241, 267, 12657);
			AddLabel(295, 265, 0, @"Elven Quiver");
			this.AddButton(311, 335, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(416, 335, 242, 241, 0, GumpButtonType.Page, 300);//cancel
			AddButton(212, 329, 1234, 1233, 0, GumpButtonType.Page, 701);
			AddLabel(221, 312, 0, @"MORE");

			AddPage(701);
			AddBackground(0, 0, 342, 515, 5054);
			this.AddCheck(20, 20, 2152, 2153, false,476);
			AddItem(49, 16, 12638);
			AddLabel(115, 20, 0, @"Woodland Breastplate");
			this.AddCheck(20, 55, 2152, 2153, false,477);
			AddItem(52, 60, 12640);
			AddLabel(115, 55, 0, @"Woodland Gorget");
			this.AddCheck(20, 90, 2152, 2153, false,478);
			AddItem(49, 95, 12641);
			AddLabel(120, 90, 0, @"Woodland Gloves");
			this.AddCheck(20, 125, 2152, 2153, false,479);
			AddItem(54, 123, 12642);
			AddLabel(120, 125, 0, @"Woodland Leggings");
			this.AddCheck(20, 160, 2152, 2153, false,480);
			AddItem(57, 157, 12643);
			AddLabel(120, 160, 0, @"Woodland Arms");
			this.AddCheck(20, 195, 2152, 2153, false,481);
			AddItem(53, 193, 12644);
			AddLabel(120, 195, 0, @"Female Woodland Breastplate");
			this.AddCheck(20, 230, 2152, 2153, false,482);
			AddItem(55, 229, 12662);
			AddLabel(120, 230, 0, @"Male Elven Shirt");
			this.AddCheck(20, 265, 2152, 2153, false,483);
			AddItem(55, 265, 12663);
			AddLabel(120, 265, 0, @"Female Elven Shirt");
			this.AddCheck(20, 299, 2152, 2153, false,484);
			AddItem(59, 308, 7943);
			AddLabel(120, 300, 0, @"Earrings");
			this.AddCheck(20, 335, 2152, 2153, false,485);
			AddItem(50, 341, 7944);
			AddLabel(120, 335, 0, @"Necklace");
			this.AddCheck(20, 370, 2152, 2153, false,486);
			AddItem(50, 379, 4234);
			AddLabel(120, 370, 0, @"Ring");
			this.AddCheck(20, 405, 2152, 2153, false,487);
			AddItem(57, 413, 4230);
			AddLabel(120, 405, 0, @"Bracelet");
			this.AddButton(60, 460, 239, 240, 1, GumpButtonType.Reply, 0);
			AddButton(214, 460, 242, 241, 0, GumpButtonType.Page, 700);//cancel
		}

public enum Buttons
		{
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m = state.Mobile;
			int m_ItemID;
			
			switch( info.ButtonID )
			{
				case 1:
				{
					if( info.IsSwitched ( 2 )  )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5099;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 3 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5104;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 4 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5102;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 5 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5100;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}else if ( info.IsSwitched ( 6 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5051;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 7 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5054;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 8 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5055;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 9 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5136;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 10 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5140;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 11 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5137;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 12 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5141;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 13 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7172;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 14 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10105;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 15 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10109;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 16 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10112;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 17 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10120;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 18 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10125;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 19 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5132;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 20 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5128;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 21 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5130;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 22 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5134;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 23 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5138;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 24 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10100;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 25 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10101;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 26 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10103;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 27 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10113;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 28 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10116;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 29 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10104;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 30 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10117;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 31 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10121;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 32 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11118;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 33 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11119;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 34 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11120;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 35 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7027;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 36 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7026;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 37 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7030;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 38 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7035;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 39 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7028;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 40 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7033;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 41 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7107;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 42 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7108;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 43 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11009;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 44 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9795;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 45 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9797;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 46 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9799;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 47 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9815;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 48 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9793;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 49 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5444;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 50 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5440;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 51 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5907;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 52 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5909;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 53 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5908;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 54 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5911;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 55 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5910;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 56 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5912;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 57 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5913;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 58 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5914;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 59 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5915;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 60 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5916;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 61 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8966;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 62 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10127;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 63 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10136;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 64 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8059;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 65 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5399;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 66 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7933;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 67 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8097;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 68 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8189;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 69 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7937;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 70 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7936;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 71 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5397;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 72 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7939;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 73 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8095;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 74 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8970;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 75 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8974;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 76 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8976;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 77 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10132;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 78 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10137;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 79 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10140;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 80 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10114;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 81 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10115;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 82 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10145;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 83 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5422;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 84 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5433;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					
					else if ( info.IsSwitched ( 85 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5431;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 86 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5398;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 87 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8972;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 88 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10138;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 89 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10139;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 90 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5441;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 91 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5435;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 92 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5437;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 93 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 8967;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 94 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10135;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 95 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10134;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 96 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5901;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 97 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5903;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 98 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5899;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 99 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5905;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 400 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5063;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 401 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7609;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 402 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5062;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 403 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5069;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 404 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5067;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 405 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5068;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 406 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10102;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 407 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10106;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 408 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10182;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 409 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10110;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 410 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10118;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 412 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10122;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 413 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10129;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 414 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10131;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 415 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10128;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 416 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10130;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 417 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10126;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 418 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5078;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 419 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5077;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 420 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5084;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 421 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5082;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 422 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5083;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 423 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10141;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 424 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10183;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 425 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10111;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 426 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10194;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 427 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10123;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 428 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7168;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 429 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7176;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 430 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7178;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 431 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7180;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 432 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7174;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 433 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7170;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 434 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5201;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 435 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5200;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 436 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5198;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 437 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5202;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 438 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5199;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 439 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5445;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 440 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5447;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 441 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5449;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 442 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 5451;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 443 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7947;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 444 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9859;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 445 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9865;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 446 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 9889;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 447 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 10219;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 448 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11010;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 449 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11012;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 450 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11014;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 451 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11016;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 452 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11018;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 453 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11020;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 454 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11022;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 455 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11024;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 456 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11026;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 457 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 11112;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 458 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12120;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 459 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12121;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 460 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12122;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 461 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12123;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 462 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12217;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 463 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12218;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 464 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12227;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 465 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12228;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 466 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12229;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 467 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12230;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 468 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12231;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 469 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12232;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 470 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12233;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 471 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12234;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 472 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12235;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 473 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12648;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 474 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12658;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 475 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12657;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 476 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12638;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 477 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12640;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 478 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12641;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 479 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12642;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 480 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12643;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 481 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12644;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 482 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12662;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 483 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 12663;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 484 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7943;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 485 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 7944;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 486 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 4234;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}
					else if ( info.IsSwitched ( 487 ) )
					{
						if( info.Switches.Length == 1 )
						{
							m_ItemID = 4230;
							m.Target = new ItemIDTarget( m_ItemID );
						}
						else 
						{
							m.SendMessage( 38,"You can't do this");
						}
					}					
					else
					{
						m.SendMessage( 38,"You can't do this");
					}
					break;
				}
				
				case 0:
				{
					m.SendMessage( 38,"Cancelled" ); 
					break;
				}
			}
		}					
					public class ItemIDTarget : Target
		{
			int m_ItemID;
			
			public ItemIDTarget( int itemid ) : base( -1, true, TargetFlags.None )
			{
				m_ItemID = itemid;
			}	
		
			protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
			{
				Item a = from.Backpack.FindItemByType( typeof( ItemIDDeed ) );
			
				if( target is BaseJewel || target is BaseArmor || target is BaseClothing ||target is BaseShield  )
				{
					if( a != null )
					{
						Item item = (Item)target;
					
							if( item.RootParent == from ) // Make sure its in their pack or they are wearing it
							{
								item.ItemID = m_ItemID;
								a.Delete();
								from.SendMessage( "You have changed the item id" ); 
							}
						
							else
							{
								from.SendMessage( 38,"It should be in your backpack");
							}
					}
					
					else
					{
						from.SendMessage( 38," You dont have a item id deed in your backpack ");
						from.CloseGump( typeof (ItemIDDeedGump));
					}
				}
			
				else
				{
					from.SendMessage( 38,"You can change only armor, jewelry and clothing Item ID !");
				}
			}
		}
	}	
}			