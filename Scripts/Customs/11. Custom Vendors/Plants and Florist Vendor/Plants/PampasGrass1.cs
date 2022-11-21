using System;

namespace Server.Items
{

public class PampasGrass1 : Item
	{
		[Constructable]
		public PampasGrass1() : base( 3237 )
		{
			Name = "Pampas Grass";
			Weight = 1.0;
		}

		public PampasGrass1( Serial serial ) : base( serial )
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
