using System;
using Server;

namespace Server.Items
{
	public class FracturedCrystal : Item
	{
		[Constructable]
		public FracturedCrystal() : this( 1 )
		{
		}

		[Constructable]
		public FracturedCrystal( int amount ) : base( 0x1BD3 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			Name = "Fractured Focusing Crystal";
			Hue = 2002;
		}

		public FracturedCrystal( Serial serial ) : base( serial )
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
	}
}