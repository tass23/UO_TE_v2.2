using System;

namespace Server.Items
{

public class FanPlant : Item
	{
		[Constructable]
		public FanPlant() : base( 3224 )
		{
			Name = "Fan Plant";
			Weight = 1.0;
		}

		public FanPlant( Serial serial ) : base( serial )
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
