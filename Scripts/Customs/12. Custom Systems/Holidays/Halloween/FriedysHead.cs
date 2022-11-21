using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class FriedysHead : Item
	{
		[Constructable]
		public FriedysHead( ) : base( 0x1AE0 )
		{
			Stackable = false;
			Weight = 1.0;
			Name = "The Remains of Friedy Bruger";
			Hue = 1772;
		}

		public FriedysHead( Serial serial ) : base( serial )
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
