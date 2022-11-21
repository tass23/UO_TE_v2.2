using System;

namespace Server.Items
{

public class BladePlant : Item
	{
		[Constructable]
		public BladePlant() : base( 3219 )
		{
			Name = "A Blade Plant";
			Weight = 1.0;
		}

		public BladePlant( Serial serial ) : base( serial )
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
