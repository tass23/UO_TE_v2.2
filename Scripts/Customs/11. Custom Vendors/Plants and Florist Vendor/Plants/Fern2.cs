using System;

namespace Server.Items
{

public class Fern2 : Item
	{
		[Constructable]
		public Fern2() : base( 3232 )
		{
			Name = "Fern";
			Weight = 1.0;
		}

		public Fern2( Serial serial ) : base( serial )
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
