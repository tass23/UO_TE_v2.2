using System;

namespace Server.Items
{

public class Rushes : Item
	{
		[Constructable]
		public Rushes() : base( 3239 )
		{
			Name = "Rushes";
			Weight = 1.0;
		}

		public Rushes( Serial serial ) : base( serial )
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
