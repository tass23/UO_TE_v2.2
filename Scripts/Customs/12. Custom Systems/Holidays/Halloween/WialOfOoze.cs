using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class WialOfOoze : Item
	{
		[Constructable]
		public WialOfOoze( ) : base( 3620 )
		{
			Stackable = false;
			Weight = 1.0;
			Name = "a Wial of ooze";
			Hue = 26;
		}

		public WialOfOoze( Serial serial ) : base( serial )
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
