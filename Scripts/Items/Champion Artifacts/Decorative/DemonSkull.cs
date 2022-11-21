using System;
using Server;

namespace Server.Items
{
	public class DemonSkull : Item
	{
		[Constructable]
		public DemonSkull() : base( 0x2251 )
		{
            ItemID = Utility.RandomList(0x2251, 0x224F);
		}

		public DemonSkull( Serial serial ) : base( serial )
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
