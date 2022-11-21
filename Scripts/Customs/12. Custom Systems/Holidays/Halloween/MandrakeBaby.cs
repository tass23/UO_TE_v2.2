using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class MandrakeBaby : Item
	{
		[Constructable]
		public MandrakeBaby( ) : base( 0xF86 )
		{
			Stackable = false;
			Weight = 1.0;
		}

		public MandrakeBaby( Serial serial ) : base( serial )
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
