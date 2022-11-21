using System;

namespace Server.Items
{
	public class DailySushi : BaseDailyRare
	{
		[Constructable]
		public DailySushi() : base( 0x283E )
		{
		}

		public DailySushi( Serial serial ) : base( serial )
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