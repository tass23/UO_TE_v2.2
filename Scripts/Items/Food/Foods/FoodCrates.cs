using System;

namespace Server.Items
{
	public class CrateOfBananas : Item
	{
		[Constructable]
		public CrateOfBananas() : base( Utility.RandomList(0x3BB2, 0x3BB1, 0x3BB0) )
		{
			Name = "Crate of Bananas";
		}

		public CrateOfBananas( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfGords : Item
	{
		[Constructable]
		public CrateOfGords() : base( 0x3BAF )
		{
			Name = "Crate of Gords";
		}

		public CrateOfGords( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfMeat : Item
	{
		[Constructable]
		public CrateOfMeat() : base( 0x3BAE )
		{
			Name = "Crate of Meat";
		}

		public CrateOfMeat( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfGrapes : Item
	{
		[Constructable]
		public CrateOfGrapes() : base( Utility.RandomList(0x3BAD, 0x3BAC, 0x3BAB) )
		{
			Name = "Crate of Grapes";
		}

		public CrateOfGrapes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfLemons : Item
	{
		[Constructable]
		public CrateOfLemons() : base( 0x3BAA )
		{
			Name = "Crate of Lemons";
		}

		public CrateOfLemons( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfLimes : Item
	{
		[Constructable]
		public CrateOfLimes() : base( 0x3BA9 )
		{
			Name = "Crate of Limes";
		}

		public CrateOfLimes( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfTurnips : Item
	{
		[Constructable]
		public CrateOfTurnips() : base( Utility.RandomList(0x3BA7, 0x3BA6) )
		{
			Name = "Crate of Turnips";
		}

		public CrateOfTurnips( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}

	public class CrateOfPumpkins : Item
	{
		[Constructable]
		public CrateOfPumpkins() : base( 0x3BA8 )
		{
			Name = "Crate of Pumpkins";
		}

		public CrateOfPumpkins( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}