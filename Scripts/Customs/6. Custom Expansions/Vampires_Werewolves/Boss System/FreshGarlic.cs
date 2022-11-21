using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class FreshGarlic : Item
	{
		[Constructable]
		public FreshGarlic() : base( 0x0C6E )
		{
			Weight = 1.0;
			Hue = 2952;
			Name = "fresh garlic";
			Stackable = true;			
		}
		
		public FreshGarlic( Serial serial ) : base( serial )
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