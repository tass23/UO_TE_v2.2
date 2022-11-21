using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;

namespace Server.Items
{
	public class TrainingDragonSpawner : Spawner
	{
		[Constructable]
		public TrainingDragonSpawner() : base( "TrainingDragon" )
		{
			HomeRange = 1;
			MinDelay = TimeSpan.FromMinutes( 0 );
			MaxDelay = TimeSpan.FromMinutes( 1 );
			Spawn();
		}

		public TrainingDragonSpawner( Serial serial ) : base( serial )
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