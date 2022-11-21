using System;

namespace Server.Items
{

public class FoxgloveFlowers1 : Item
	{
		[Constructable]
		public FoxgloveFlowers1() : base( 3204 )
		{
			Name = "Foxgloves";
			Weight = 1.0;
		}

		public FoxgloveFlowers1( Serial serial ) : base( serial )
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
