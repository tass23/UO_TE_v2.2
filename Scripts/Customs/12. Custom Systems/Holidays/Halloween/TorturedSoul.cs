using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class TorturedSoul : Item
	{
		[Constructable]
		public TorturedSoul( ) : base( 14088 )
		{
			Stackable = false;
			Weight = 1.0;
			Name = "a tortured soul";
		}

		public TorturedSoul( Serial serial ) : base( serial )
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
