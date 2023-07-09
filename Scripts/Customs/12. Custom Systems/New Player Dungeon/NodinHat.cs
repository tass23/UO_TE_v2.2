using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class NodinHat : Item
	{
		[Constructable]
		public NodinHat( ) : base( 0x1717  )
		{
			Name = "Nodin's Hat";
			Hue = 1266;
			Stackable = false;
			Weight = 1.0;
		}

		public NodinHat( Serial serial ) : base( serial )
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
