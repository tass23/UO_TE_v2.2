using System;

namespace Server.Items
{

public class Cactus4 : Item
	{
		[Constructable]
		public Cactus4() : base( 3368 )
		{
			Name = "Cactus";
			Weight = 1.0;
		}

		public Cactus4( Serial serial ) : base( serial )
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
