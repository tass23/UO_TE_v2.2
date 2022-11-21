using System;

namespace Server.Items
{
	/*[Flipable( 0x222E, 0x222F, 0x2230, 0x2231)]*/
	public class DailyMushroom : BaseDailyRare
	{
		[Constructable]
		public DailyMushroom() : base(Utility.RandomList( 0x222E, 0x222F, 0x2230, 0x2231) )
		{
		}

		public DailyMushroom( Serial serial ) : base( serial )
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