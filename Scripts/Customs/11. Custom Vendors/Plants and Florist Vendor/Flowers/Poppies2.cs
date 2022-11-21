using System;

namespace Server.Items
{

public class Poppies2 : Item
	{
		[Constructable]
		public Poppies2() : base( 3263 )
		{
			Name = "Poppies";
			Weight = 1.0;
		}

		public Poppies2( Serial serial ) : base( serial )
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
