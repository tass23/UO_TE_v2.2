using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class GoldenIdol : Item
	{
		[Constructable]
		public GoldenIdol( ) : base( 0x4688  )
		{
			Name = "The Golden Idol";
			Hue = 1161;
			Stackable = false;
			Weight = 1.0;
		}

		public GoldenIdol( Serial serial ) : base( serial )
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