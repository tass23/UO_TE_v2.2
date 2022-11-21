using System;

namespace Server.Items
{
	public class DailyStump : BaseDailyRare
	{
		[Constructable]
		public DailyStump() : base( 0xE56 )
		{
		}

		public DailyStump( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}