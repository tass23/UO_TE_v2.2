using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class SlenderScroll1 : Item
	{

		[Constructable]
		public SlenderScroll1() : base( 3636 )
		{
			Hue = 1089;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "Slenderman Info Page 1";
		}

		public SlenderScroll1( Serial serial ) : base( serial )
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

			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.AddToBackpack( new SlenderNote1() );
			from.CloseGump( typeof( SlenderScroll1Gump ) );
			from.SendGump( new SlenderScroll1Gump( from, this ) );
			this.Delete();
		}
	}
	
	public class SlenderNote1 : Item
	{

		[Constructable]
		public SlenderNote1() : base( 5359 )
		{
			Hue = 1089;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "Slenderman Info Page 1";
		}

		public SlenderNote1( Serial serial ) : base( serial )
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

			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.CloseGump( typeof( SlenderNote1Gump ) );
			from.SendGump( new SlenderNote1Gump( from, this ) );
		}
	}

	public class SlenderScroll1Gump : Gump
	{
		private Mobile m_From;
		private SlenderScroll1 m_Deed;

		public SlenderScroll1Gump( Mobile from, SlenderScroll1 deed ) : base( 0, 0 )
		{
			m_From = from;
			m_Deed = deed;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImageTiled(54, 33, 369, 400, 2604); //Main Background
			//AddAlphaRegion(54, 33, 369, 400);
			AddImageTiled(416, 39, 44, 389, 203); //Right side border (underneath tiled boxes)
			AddImage(97, 49, 9005); //Quest ribbon
			AddImageTiled(58, 39, 29, 390, 10460); //Left side border
			//AddImageTiled(87, 119, 326, 181, 5058); //Notestring Background
			//AddImageTiled(164, 86, 148, 33, 5058); //Quest Title Background
			AddImageTiled(412, 37, 31, 389, 10460); //Right side border
			AddImage(86, 119, 30501, 1089); //Tarot Card
			AddImage(430, 9, 10441); //Right side Dragon
			AddImageTiled(40, 38, 17, 391, 9263); //Left side border
			AddImage(6, 25, 10421); //Dragon body (Left side)
			AddImage(34, 12, 10420); //Dragon head (left side)
			AddImageTiled(94, 25, 342, 15, 10304); //Top border
			AddImageTiled(40, 414, 415, 16, 10304); //Bottom border
			AddImage(-10, 314, 10402); //End of Dragon Tail (Left side)
			AddImage(56, 150, 10411); //Dragon Tail inside window
			AddImage(136, 84, 96); //Blue Bar near ribbon
			AddImage(372, 57, 1417); //Green circle
			AddImage(381, 66, 5576); //Hero Icon in green circle
			AddImageTiled(90, 34, 322, 5, 5214); //Underneath top border
			AddImage(146, 91, 2103); //Bullet by Quest Title
			AddHtml( 165, 86, 146, 30, @"Slenderman Info Page 1", (bool)false, (bool)false); //Quest Title
			AddHtml( 207, 131, 177, 154, @"FOLLOWS", (bool)false, (bool)false); //Notestring
		}
	}
	
	public class SlenderNote1Gump : Gump
	{
		private Mobile m_From;
		private SlenderNote1 m_Deed;

		public SlenderNote1Gump( Mobile from, SlenderNote1 deed ) : base( 0, 0 )
		{
			m_From = from;
			m_Deed = deed;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImageTiled(54, 33, 369, 400, 2604); //Main Background
			//AddAlphaRegion(54, 33, 369, 400);
			AddImageTiled(416, 39, 44, 389, 203); //Right side border (underneath tiled boxes)
			AddImage(97, 49, 9005); //Quest ribbon
			AddImageTiled(58, 39, 29, 390, 10460); //Left side border
			//AddImageTiled(87, 119, 326, 181, 5104); //Notestring Background
			//AddImageTiled(164, 86, 148, 33, 5104); //Quest Title Background
			AddImageTiled(412, 37, 31, 389, 10460); //Right side border
			AddImage(86, 119, 30501, 1089); //Tarot Card
			AddImage(430, 9, 10441); //Right side Dragon
			AddImageTiled(40, 38, 17, 391, 9263); //Left side border
			AddImage(6, 25, 10421); //Dragon body (Left side)
			AddImage(34, 12, 10420); //Dragon head (left side)
			AddImageTiled(94, 25, 342, 15, 10304); //Top border
			AddImageTiled(40, 414, 415, 16, 10304); //Bottom border
			AddImage(-10, 314, 10402); //End of Dragon Tail (Left side)
			AddImage(56, 150, 10411); //Dragon Tail inside window
			AddImage(136, 84, 96); //Blue Bar near ribbon
			AddImage(372, 57, 1417); //Green circle
			AddImage(381, 66, 5576); //Hero Icon in green circle
			AddImageTiled(90, 34, 322, 5, 5214); //Underneath top border
			AddImage(146, 91, 2103); //Bullet by Quest Title
			AddHtml( 165, 86, 146, 30, @"Slenderman Info Page 1", (bool)false, (bool)false); //Quest Title
			AddHtml( 207, 131, 177, 154, @"FOLLOWS", (bool)false, (bool)false); //Notestring
		}
	}
}