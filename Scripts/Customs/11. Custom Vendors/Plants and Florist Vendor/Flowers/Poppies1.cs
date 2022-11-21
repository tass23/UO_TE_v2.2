using System;

namespace Server.Items
{

public class Poppies1 : Item
	{
		[Constructable]
		public Poppies1() : base( 3262 )
		{
			Name = "Poppies";
			Weight = 1.0;
		}

		public Poppies1( Serial serial ) : base( serial )
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
