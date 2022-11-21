using System;

namespace Server.Items
{
	public class Paints : Item
	{
		[Constructable]
		public Paints() : base( 0xE47 )
		{
		      	Weight = 2.0;
			Stackable = true;
			Name = "Paints";
			Hue = 0x2C2;
			
		}

		public Paints( Serial serial ) : base( serial )
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