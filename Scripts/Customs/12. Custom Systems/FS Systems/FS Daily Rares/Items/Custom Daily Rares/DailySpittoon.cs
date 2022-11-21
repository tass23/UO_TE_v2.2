using System;

namespace Server.Items
{
	public class DailySpittoon : BaseDailyRare
	{
		[Constructable]
		public DailySpittoon() : base( 0x1003 )
		{
		}

		public DailySpittoon( Serial serial ) : base( serial )
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