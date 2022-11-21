using System;

namespace Server.Items
{

public class Fern5 : Item
	{
		[Constructable]
		public Fern5() : base( 3236 )
		{
			Name = "Fern";
			Weight = 1.0;
		}

		public Fern5( Serial serial ) : base( serial )
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
