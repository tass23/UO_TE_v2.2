using System;

namespace Server.Items
{

public class Fern1 : Item
	{
		[Constructable]
		public Fern1() : base( 3231 )
		{
			Name = "Fern";
			Weight = 1.0;
		}

		public Fern1( Serial serial ) : base( serial )
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
