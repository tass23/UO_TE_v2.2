using System;

namespace Server.Items
{
      public class DailyBucketOfWater : BaseDailyRare
	{
		[Constructable]
		public DailyBucketOfWater() : base( 0xFFA )
		{
			Weight = 1.0;
		}

		public DailyBucketOfWater( Serial serial ) : base( serial )
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
