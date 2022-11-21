using System;

namespace Server.Items
{

public class LargeFern : Item
	{
		[Constructable]
		public LargeFern() : base( 3233 )
		{
			Name = "Large Fern";
			Weight = 1.0;
		}

		public LargeFern( Serial serial ) : base( serial )
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
