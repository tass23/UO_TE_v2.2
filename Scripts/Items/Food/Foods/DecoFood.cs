using System;

namespace Server.Items
{
	public class PileOfSpices : Item
	{
		[Constructable]
		public PileOfSpices() : base( 0x3BC6 )
		{
			Name = "Pile of Spices";
		}

		public PileOfSpices( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class PileOfFishSteaks : Item
	{
		[Constructable]
		public PileOfFishSteaks() : base( Utility.RandomList(0x3BC2, 0x3BC1) )
		{
			Name = "Pile of Fish Steaks";
		}

		public PileOfFishSteaks( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TrayOfCookies : Item
	{
		[Constructable]
		public TrayOfCookies() : base( Utility.RandomList(0x3BBC, 0x3BBB) )
		{
			Name = "Tray of Cookies";
		}

		public TrayOfCookies( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TrayOfSoupBread : Item
	{
		[Constructable]
		public TrayOfSoupBread() : base( Utility.RandomList(0x3BBA, 0x3BB9) )
		{
			Name = "Tray of Soup & Bread";
		}

		public TrayOfSoupBread( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TrayOfMeatPie : Item
	{
		[Constructable]
		public TrayOfMeatPie() : base( Utility.RandomList(0x3BB8, 0x3BB7) )
		{
			Name = "Tray of Meat Pie";
		}

		public TrayOfMeatPie( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class TrayOfBreadSticks : Item
	{
		[Constructable]
		public TrayOfBreadSticks() : base( Utility.RandomList(0x3BB6, 0x3BB5) )
		{
			Name = "Tray of Bread Sticks";
		}

		public TrayOfBreadSticks( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class VegableTray : Item
	{
		[Constructable]
		public VegableTray() : base( 0x3BB4 )
		{
			Name = "Vegable Tray";
		}

		public VegableTray( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class MeatTray : Item
	{
		[Constructable]
		public MeatTray() : base( 0x3BB3 )
		{
			Name = "Meat Tray";
		}

		public MeatTray( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}