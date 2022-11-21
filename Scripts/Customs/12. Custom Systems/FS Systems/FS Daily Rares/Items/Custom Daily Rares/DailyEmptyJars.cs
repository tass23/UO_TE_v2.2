using System;

namespace Server.Items
{
	public class DailyEmptyJars : BaseDailyRare
	{
		[Constructable]
		public DailyEmptyJars() : base( 0xE47 )
		{
		}

		public DailyEmptyJars( Serial serial ) : base( serial )
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