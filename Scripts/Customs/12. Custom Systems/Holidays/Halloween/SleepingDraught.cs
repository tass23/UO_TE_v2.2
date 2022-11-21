using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SleepingDraught : Item
	{
		[Constructable]
		public SleepingDraught( ) : base( 3848 )
		{
			Stackable = false;
			Weight = 1.0;
			Name = "a sleeping draught";
		}

		public SleepingDraught( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
