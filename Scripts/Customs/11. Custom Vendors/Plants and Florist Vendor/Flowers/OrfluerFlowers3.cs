using System;

namespace Server.Items
{

public class OrfluerFlowers3 : Item
	{
		[Constructable]
		public OrfluerFlowers3() : base( 3265 )
		{
			Name = "Orfluer Flowers";
			Weight = 1.0;
		}

		public OrfluerFlowers3( Serial serial ) : base( serial )
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
