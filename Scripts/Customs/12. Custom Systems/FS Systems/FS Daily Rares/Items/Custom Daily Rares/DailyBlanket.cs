using System;

namespace Server.Items
{
	[Flipable( 0xA6C, 0xA6D, 0xA6E, 0xA6F)]
	public class DailyBlanket : BaseDailyRare
	{
		[Constructable]
		public DailyBlanket() : base( 0xA6C )
		{
			Hue = Utility.RandomNondyedHue();
		}

		public DailyBlanket( Serial serial ) : base( serial )
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