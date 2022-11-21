using System;

namespace Server.Items
{

public class BambooCluster : Item
	{
		[Constructable]
		public BambooCluster() : base( 9324 )
		{
			Name = "A Cluster of Bamboo";
			Weight = 1.0;
		}

		public BambooCluster( Serial serial ) : base( serial )
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
