using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class BusterDrawer : Item
	{
		[Constructable]
		public BusterDrawer( ) : base( 0x0A46  )
		{
			Name = "Buster's Drawers";
			Hue = 1266;
			Stackable = false;
			Weight = 1.0;
		}

		public BusterDrawer( Serial serial ) : base( serial )
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
