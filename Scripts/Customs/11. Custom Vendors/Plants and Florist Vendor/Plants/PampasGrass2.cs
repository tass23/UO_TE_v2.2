using System;

namespace Server.Items
{

public class PampasGrass2 : Item
	{
		[Constructable]
		public PampasGrass2() : base( 3268 )
		{
			Name = "Pampas Grass";
			Weight = 1.0;
		}

		public PampasGrass2( Serial serial ) : base( serial )
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
