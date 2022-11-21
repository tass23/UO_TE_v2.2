using System;

namespace Server.Items
{
	public class Wasabi : Item
	{
		[Constructable]
		public Wasabi() : base( 0x24E8 ) { }

		public Wasabi( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class EmptyBentoBox : Item
	{
		[Constructable]
		public EmptyBentoBox() : base( 0x2834 )
		{
			Weight = 5.0;
		}

		public EmptyBentoBox( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class GreenTeaBasket : Item
	{
		[Constructable]
		public GreenTeaBasket() : base( 0x284B )
		{
			Weight = 10.0;
		}

		public GreenTeaBasket( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}