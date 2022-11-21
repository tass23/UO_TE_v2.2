using System;

namespace Server.Items
{
	public class DailyFlax : BaseDailyRare
	{
		[Constructable]
		public DailyFlax() : base( 0x1A9B )
		{
		}

		public DailyFlax( Serial serial ) : base( serial )
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