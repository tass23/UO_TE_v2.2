using System;

namespace Server.Items
{

public class SnakePlant : Item
	{
		[Constructable]
		public SnakePlant() : base( 3241 )
		{
			Name = "A Snake Plant";
			Weight = 1.0;
		}

		public SnakePlant( Serial serial ) : base( serial )
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
