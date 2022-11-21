using System;

namespace Server.Items
{
	public class DailyFullJars : BaseDailyRare
	{
		[Constructable]
		public DailyFullJars() : base( 0xe48 )
		{
		}

		public DailyFullJars( Serial serial ) : base( serial )
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