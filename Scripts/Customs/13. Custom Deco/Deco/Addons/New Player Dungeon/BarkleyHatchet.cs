using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class BarkleyHatchet : Item
	{
		[Constructable]
		public BarkleyHatchet( ) : base( 0x0F43  )
		{
			Name = "Barkley's Hatchet";
			Hue = 1266;
			Stackable = false;
			Weight = 1.0;
		}

		public BarkleyHatchet( Serial serial ) : base( serial )
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
