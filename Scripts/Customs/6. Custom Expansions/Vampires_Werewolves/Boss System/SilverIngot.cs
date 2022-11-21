using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class SilverIngot : Item
	{
		[Constructable]
		public SilverIngot() : base( 0x1BE3 )
		{
			Weight = 1.0;
			Hue = 2955;
			Name = "silver ingot";
			Stackable = true;
		}
		
		public SilverIngot( Serial serial ) : base( serial )
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