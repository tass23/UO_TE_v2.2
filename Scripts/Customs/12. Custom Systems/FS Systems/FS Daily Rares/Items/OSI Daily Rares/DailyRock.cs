using System;

namespace Server.Items
{
	public class DailyRock : BaseDailyRare
	{
		[Constructable]
		public DailyRock() : base( 0x1368 )
		{
		}

		public DailyRock( Serial serial ) : base( serial )
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