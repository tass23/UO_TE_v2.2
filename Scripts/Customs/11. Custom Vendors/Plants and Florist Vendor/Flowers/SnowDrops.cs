using System;

namespace Server.Items
{

public class SnowDrops : Item
	{
		[Constructable]
		public SnowDrops() : base( 3208 )
		{
			Name = "SnowDrops";
			Weight = 1.0;
		}

		public SnowDrops( Serial serial ) : base( serial )
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
