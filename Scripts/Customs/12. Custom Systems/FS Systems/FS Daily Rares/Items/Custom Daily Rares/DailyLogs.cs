using System;

namespace Server.Items
{
	public class DailyLogs : BaseDailyRare
	{
		[Constructable]
		public DailyLogs() : base( 0x1BE2 )
		{
		}

		public DailyLogs( Serial serial ) : base( serial )
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