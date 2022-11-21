using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4 )]
	public class Skull : Item
	{
		[Constructable]
		public Skull( ) : base( 0x1AE0 + Utility.Random( 5 ) )
		{
			Stackable = false;
			Weight = 1.0;
		}

		public Skull( Serial serial ) : base( serial )
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
