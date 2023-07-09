using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RumpleCharms : Item
	{
		[Constructable]
		public RumpleCharms( ) : base( 0x0FB6  )
		{
			Name = "Rumplestiltskin's Lucky Charms";
			Hue = 1266;
			Stackable = false;
			Weight = 1.0;
		}

		public RumpleCharms( Serial serial ) : base( serial )
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
