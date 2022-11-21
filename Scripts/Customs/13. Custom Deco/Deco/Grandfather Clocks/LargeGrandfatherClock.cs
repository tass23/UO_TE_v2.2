using System;
using Server;

namespace Server.Items
{

	[Flipable( 0x44DD, 0x44E1 )]
	public class LargeGrandfatherClock : Clock
	{
		[Constructable]
		public LargeGrandfatherClock() : base( 0x44DD )
		{
		}

		public LargeGrandfatherClock( Serial serial ) : base( serial )
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