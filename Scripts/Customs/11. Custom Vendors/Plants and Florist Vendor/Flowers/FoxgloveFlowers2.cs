using System;

namespace Server.Items
{

public class FoxgloveFlowers2 : Item
	{
		[Constructable]
		public FoxgloveFlowers2() : base( 3210 )
		{
			Name = "Foxgloves";
			Weight = 1.0;
		}

		public FoxgloveFlowers2( Serial serial ) : base( serial )
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
