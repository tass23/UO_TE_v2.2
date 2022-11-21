using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1428, 0x1429 )]
	public class DippingStick : Item
	{

		[Constructable]
		public DippingStick() : base( 0x1428 )
		{
			Name = "Dipping Stick";
			Weight = 0.5;
		}

		public DippingStick( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}