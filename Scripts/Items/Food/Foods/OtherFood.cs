using System;
using Server.Network;
namespace Server.Items
{
	public class Meatballs : Food
	{
		[Constructable]
		public Meatballs() : this( 1 ) { }
		[Constructable]
		public Meatballs( int amount ) : base( amount, 0xE74 )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x475;
			this.FillFactor = 4;
			this.Name = "Meatballs";
		}
		public Meatballs( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Meatloaf : Food
	{
		[Constructable]
		public Meatloaf() : this( 1 ) { }
		[Constructable]
		public Meatloaf( int amount ) : base( amount, 0xE79 )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x475;
			this.FillFactor = 5;
			this.Name = "Meatloaf";
		}
		public Meatloaf( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PotatoStrings : Food
	{
		[Constructable]
		public PotatoStrings() : this( 1 ) { }
		[Constructable]
		public PotatoStrings( int amount ) : base( amount, 0x1B8D )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x225;
			this.FillFactor = 3;
			this.Name = "Potato Strings";
		}
		public PotatoStrings( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Pancakes : Food
	{
		[Constructable]
		public Pancakes() : this( 1 ) { }
		[Constructable]
		public Pancakes( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x22A;
			this.FillFactor = 5;
			this.Name = "Pancakes";
		}
		public Pancakes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Waffles : Food
	{
		[Constructable]
		public Waffles() : this( 1 ) { }
		[Constructable]
		public Waffles( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x1C7;
			this.FillFactor = 4;
			this.Name = "Waffles";
		}
		public Waffles( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class GrilledHam : Food
	{
		[Constructable]
		public GrilledHam() : this( 1 ) { }
		[Constructable]
		public GrilledHam( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x33D;
			this.FillFactor = 4;
			this.Name = "Grilled Ham";
		}
		public GrilledHam( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Hotwings : Food
	{
		[Constructable]
		public Hotwings() : this( 1 ) { }
		[Constructable]
		public Hotwings( int amount ) : base( amount, 0x1608 )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x21A;
			this.FillFactor = 3;
			this.Name = "Hotwings";
		}
		public Hotwings( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Taco : Food
	{
		[Constructable]
		public Taco() : this( 1 ) { }
		[Constructable]
		public Taco( int amount ) : base( amount, 0x1370 )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x465;
			this.FillFactor = 3;
			this.Name = "Taco";
		}
		public Taco( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class CornOnCob : Food
	{
		[Constructable]
		public CornOnCob() : this( 1 ) { }
		[Constructable]
		public CornOnCob( int amount ) : base( amount, 0xC81 )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x0;
			this.FillFactor = 3;
			this.Name = "Cooked Corn on the Cob";
		}
		public CornOnCob( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Hotdog : Food
	{
		[Constructable]
		public Hotdog() : this( 1 ) { }
		[Constructable]
		public Hotdog( int amount ) : base( amount, 0xC7F )
		{
			this.Weight = 1.0;
			this.Stackable = true;
			this.Hue = 0x457;
			this.FillFactor = 3;
			this.Name = "Hotdog";
		}
		public Hotdog( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class CookedSunflowerSeeds : Food
	{
		[Constructable]
		public CookedSunflowerSeeds() : this( 1 ) { }
		[Constructable]
		public CookedSunflowerSeeds( int amount ) : base( amount, 0xF27 )
		{
			this.Weight = 0.1;
			this.Stackable = true;
			this.Hue = 0x44F;
			this.FillFactor = 2;
			this.Name = "Sunflower Seeds";
		}
		public CookedSunflowerSeeds( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}