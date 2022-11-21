using System;

namespace Server.Items
{
	public class WasabiClumps : Food
	{
		[Constructable]
		public WasabiClumps() : base( 0x24EB )
		{
			FillFactor = 2;
		}

		public WasabiClumps( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class BentoBox : Food
	{
		[Constructable]
		public BentoBox() : base( 0x2836 )
		{
			Weight = 5.0;
			FillFactor = 2;
		}

		public override bool Eat( Mobile from )
		{
			if ( !base.Eat( from ) ) return false;
			from.AddToBackpack( new EmptyBentoBox() );
			return true;
		}

		public BentoBox( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SushiRolls : Food
	{
		[Constructable]
		public SushiRolls() : base( 0x283E )
		{
			Weight = 3.0;
			FillFactor = 2;
		}

		public SushiRolls( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class SushiPlatter : Food
	{
		[Constructable]
		public SushiPlatter() : base( 0x2840 )
		{
			Weight = 3.0;
			FillFactor = 2;
		}

		public SushiPlatter( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class GreenTea : Food
	{
		[Constructable]
		public GreenTea() : base( 0x284C )
		{
			Weight = 4.0;
			FillFactor = 2;
		}

		public GreenTea( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class MisoSoup : Food
	{
		[Constructable]
		public MisoSoup() : base( 0x284D )
		{
			Weight = 4.0;
			FillFactor = 2;
		}

		public MisoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class WhiteMisoSoup : Food
	{
		[Constructable]
		public WhiteMisoSoup() : base( 0x284E )
		{
			Weight = 4.0;
			FillFactor = 2;
		}

		public WhiteMisoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class RedMisoSoup : Food
	{
		[Constructable]
		public RedMisoSoup() : base( 0x284F )
		{
			Weight = 4.0;
			FillFactor = 2;
		}

		public RedMisoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class AwaseMisoSoup : Food
	{
		[Constructable]
		public AwaseMisoSoup() : base( 0x2850 )
		{
			Weight = 4.0;
			FillFactor = 2;
		}

		public AwaseMisoSoup( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}