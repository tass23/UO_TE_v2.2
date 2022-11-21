using System;
using Server.Network;
namespace Server.Items
{
	public class BlueberryMuffins : Food
	{
		[Constructable]
		public BlueberryMuffins() : this( 1 ) { }
		[Constructable]
		public BlueberryMuffins( int amount ) : base( amount, 0x9EB)
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
			this.Hue = 0x1FC;
			this.Name = "Blueberry Muffins";
		}
		public BlueberryMuffins( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class PumpkinMuffins : Food
	{
		[Constructable]
		public PumpkinMuffins() : this( 1 ) { }
		[Constructable]
		public PumpkinMuffins( int amount ) : base( amount, 0x9EB )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
			this.Hue = 0x1C0;
			this.Name = "Pumpkin Muffins";
		}
		public PumpkinMuffins( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class ChocSunflowerSeeds : Food
	{
		[Constructable]
		public ChocSunflowerSeeds() : this( 1 ) { }
		[Constructable]
		public ChocSunflowerSeeds( int amount ) : base( amount, 0x9B4 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 0x41B;
			this.Name = "Chocolate Sunflower Seeds";
		}
		public ChocSunflowerSeeds( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class RiceKrispTreat : Food
	{
		[Constructable]
		public RiceKrispTreat() : this( 1 ) { }
		[Constructable]
		public RiceKrispTreat( int amount ) : base( amount, 0x1044 )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 0x9B;
			this.Name = "Rice Krisp Treat";
		}
		public RiceKrispTreat( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class Brownies : Food
	{
		[Constructable]
		public Brownies() : this( 1 ) { }
		[Constructable]
		public Brownies( int amount ) : base( amount, 0x160B )
		{
			this.Weight = 1.0;
			this.FillFactor = 2;
			this.Hue = 0x47D;
			this.Name = "Brownies";
		}
		public Brownies( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
	public class ChocolateCake : Food
	{
		[Constructable]
		public ChocolateCake() : this( 1 ) { }
		[Constructable]
		public ChocolateCake( int amount ) : base( amount, 0x9E9 )
		{
			this.Weight = 2.0;
			this.FillFactor = 3;
			this.Hue = 0x45D;
			this.Name = "Chocolate Cake";
		}
		public ChocolateCake( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}