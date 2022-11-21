using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class CookedSteak : Food
	{
		[Constructable]
		public CookedSteak() : this( 1 ){}

		[Constructable]
		public CookedSteak( int amount ) : base( amount, 0x3BCD )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
			Name = "Cooked Steak";
		}

		public CookedSteak( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WeddingCake : Food
	{
		[Constructable]
		public WeddingCake() : base( 0x3BCC )
		{
			Name = "Wedding Cake";
			this.Weight = 10.0;
			this.FillFactor = 10;
		}

		public WeddingCake( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SliceOfWeddingCake : Food
	{
		[Constructable]
		public SliceOfWeddingCake() : this( 1 ){}

		[Constructable]
		public SliceOfWeddingCake( int amount ) : base( amount, 0x3BCB )
		{
			Name = "Slice of Wedding Cake";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public SliceOfWeddingCake( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class PennyCandy : Food
	{
		[Constructable]
		public PennyCandy() : this( 1 ){}

		[Constructable]
		public PennyCandy( int amount ) : base( amount, 0x3BC7 )
		{
			Name = "Candy";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public PennyCandy( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0x3BC5, 0x3BC4 )]
	public class SliceOfCake : Food
	{
		[Constructable]
		public SliceOfCake() : this( 1 ){}

		[Constructable]
		public SliceOfCake( int amount ) : base( amount, 0x3BC5 )
		{
			Name = "Slice of Cake";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public SliceOfCake( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	[FlipableAttribute( 0x3BC0, 0x3BBF )]
	public class RoastHam : Food
	{
		[Constructable]
		public RoastHam() : base( 0x3BC0 )
		{
			Name = "Roast Ham";
			this.Weight = 10.0;
			this.FillFactor = 10;
		}

		public RoastHam( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class BirthdayCake : Food
	{
		[Constructable]
		public BirthdayCake() : base( 0x3BBD )
		{
			Name = "Birthday Cake";
			this.Weight = 10.0;
			this.FillFactor = 10;
		}

		public BirthdayCake( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}