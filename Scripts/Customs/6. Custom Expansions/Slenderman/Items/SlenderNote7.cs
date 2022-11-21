using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class SlenderScroll7 : Item
	{

		[Constructable]
		public SlenderScroll7() : base( 3636 )
		{
			Hue = 1089;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "Slenderman Info Page 7";
		}

		public SlenderScroll7( Serial serial ) : base( serial )
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
			from.AddToBackpack( new SlenderNote7() );
			from.CloseGump( typeof( SlenderScroll7Gump ) );
			from.SendGump( new SlenderScroll7Gump( from, this ) );
			this.Delete();
		}
	}
	
	public class SlenderNote7 : Item
	{

		[Constructable]
		public SlenderNote7() : base( 5359 )
		{
			Hue = 1089;
			Weight = 1.0;
			LootType = LootType.Blessed;
			Name = "Slenderman Info Page 7";
		}

		public SlenderNote7( Serial serial ) : base( serial )
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
			from.CloseGump( typeof( SlenderNote7Gump ) );
			from.SendGump( new SlenderNote7Gump( from, this ) );
		}
	}

	public class SlenderScroll7Gump : Gump
	{
		private Mobile m_From;
		private SlenderScroll7 m_Deed;

		public SlenderScroll7Gump( Mobile from, SlenderScroll7 deed ) : base( 0, 0 )
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
			AddImage(86, 119, 30510, 1088); //Tarot Card
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
			AddHtml( 165, 86, 146, 30, @"Slenderman Info Page 7", (bool)false, (bool)false); //Quest Title
			AddHtml( 207, 131, 177, 154, @"LEAVE ME ALONE", (bool)false, (bool)false); //Notestring
		}
	}
	
	public class SlenderNote7Gump : Gump
	{
		private Mobile m_From;
		private SlenderNote7 m_Deed;

		public SlenderNote7Gump( Mobile from, SlenderNote7 deed ) : base( 0, 0 )
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
			AddImage(86, 119, 30510, 1088); //Tarot Card
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
			AddHtml( 165, 86, 146, 30, @"Slenderman Info Page 7", (bool)false, (bool)false); //Quest Title
			AddHtml( 207, 131, 177, 154, @"LEAVE ME ALONE", (bool)false, (bool)false); //Notestring
		}
	}
}