using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class Necronomicon : Item
	{
		[Constructable]
		public Necronomicon( ) : base( 17080 )
		{
			Stackable = false;
			Weight = 5.0;
			Name = "Necronomicon - The Book of the Dead";
		}

		public Necronomicon( Serial serial ) : base( serial )
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