using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1403, 0x1402 )]
	public class SilverStakeUnblessed : Item
	{
		[Constructable]
		public SilverStakeUnblessed() : base( 0x1403 )
		{
			Weight = 5.0;
			Hue = 2028;
			Name = "an unblessed silver stake";		
		}
		
		public SilverStakeUnblessed( Serial serial ) : base( serial )
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