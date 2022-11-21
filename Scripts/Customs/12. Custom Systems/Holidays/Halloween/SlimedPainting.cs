using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SlimedPainting : Item
	{
		[Constructable]
		public SlimedPainting() : base( 0x2414 )
		{
			Weight = 10;
			Name = "a Slime-covered portrait of Wigo";
			Hue = 26;
		}

		public SlimedPainting( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
