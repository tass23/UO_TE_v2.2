using System;

namespace Server.Items
{

public class Bulrushes : Item
	{
		[Constructable]
		public Bulrushes() : base( 3220 )
		{
			Name = "Bulrushes";
			Weight = 1.0;
		}

		public Bulrushes( Serial serial ) : base( serial )
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
