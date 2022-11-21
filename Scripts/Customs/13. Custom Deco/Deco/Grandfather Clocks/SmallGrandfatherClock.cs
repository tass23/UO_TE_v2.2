using System;
using Server;

namespace Server.Items
{

	[Flipable( 0x44D5, 0x44D9 )]
	public class SmallGrandfatherClock : Clock
	{
		[Constructable]
		public SmallGrandfatherClock() : base( 0x44D5 )
		{
		}

		public SmallGrandfatherClock( Serial serial ) : base( serial )
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