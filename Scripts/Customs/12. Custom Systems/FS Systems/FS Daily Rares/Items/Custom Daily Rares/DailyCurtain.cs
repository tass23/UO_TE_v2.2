using System;

namespace Server.Items
{
	[Flipable( 0x159E, 0x159F)]
	public class DailyCurtian : BaseDailyRare
	{
		[Constructable]
		public DailyCurtian() : base( 0x159E )
		{
		}

		public DailyCurtian( Serial serial ) : base( serial )
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