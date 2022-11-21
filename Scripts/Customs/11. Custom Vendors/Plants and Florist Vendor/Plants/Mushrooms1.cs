using System;

namespace Server.Items
{

public class Mushrooms1 : Item
	{
		[Constructable]
		public Mushrooms1() : base( 3347 )
		{
			Name = "Mushrooms";
			Weight = 1.0;
		}

		public Mushrooms1( Serial serial ) : base( serial )
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
