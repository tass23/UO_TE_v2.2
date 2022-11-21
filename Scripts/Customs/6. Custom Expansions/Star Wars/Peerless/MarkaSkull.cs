using System;
using Server;

namespace Server.Items
{
	public class MarkaSkull : Item
	{
		[Constructable]
		public MarkaSkull() : base( 0x2251 )
		{
			Name = "the skull of Marka Ragnos";
            ItemID = Utility.RandomList(0x2251, 0x224F);
			Hue = 1786;
		}

		public MarkaSkull( Serial serial ) : base( serial )
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