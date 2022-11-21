using System;

namespace Server.Items
{

public class Cactus5 : Item
	{
		[Constructable]
		public Cactus5() : base( 3370 )
		{
			Name = "Cactus";
			Weight = 1.0;
		}

		public Cactus5( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
