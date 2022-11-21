using System;

namespace Server.Items
{

public class OrfluerFlowers1 : Item
	{
		[Constructable]
		public OrfluerFlowers1() : base( 3205 )
		{
			Name = "Orfluer Flowers";
			Weight = 1.0;
		}

		public OrfluerFlowers1( Serial serial ) : base( serial )
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
