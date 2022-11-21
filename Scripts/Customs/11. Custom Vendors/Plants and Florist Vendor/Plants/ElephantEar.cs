using System;

namespace Server.Items
{

public class ElephantEar : Item
	{
		[Constructable]
		public ElephantEar() : base( 3223 )
		{
			Name = "Elephant Ear Plant";
			Weight = 1.0;
		}

		public ElephantEar( Serial serial ) : base( serial )
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
