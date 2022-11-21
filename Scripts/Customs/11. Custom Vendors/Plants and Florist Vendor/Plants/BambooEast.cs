using System;

namespace Server.Items
{

public class BambooEast : Item
	{
		[Constructable]
		public BambooEast() : base( 9327 )
		{
			Name = "Bamboo";
			Weight = 1.0;
		}

		public BambooEast( Serial serial ) : base( serial )
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
