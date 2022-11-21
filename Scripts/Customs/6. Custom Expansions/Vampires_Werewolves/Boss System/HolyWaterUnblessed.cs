using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class HolyWaterUnblessed : Item
	{
		[Constructable]
		public HolyWaterUnblessed() : base( 0x0F0E )
		{
			Weight = 1.0;
			Hue = 1295;
			Name = "plain holy water";
			Stackable = true;
		}
		
		public HolyWaterUnblessed( Serial serial ) : base( serial )
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