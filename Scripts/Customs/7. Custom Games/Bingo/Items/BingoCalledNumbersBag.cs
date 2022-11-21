using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	
	public class BingoCalledNumbersBag : Backpack
	{
		[Constructable]
		public BingoCalledNumbersBag ()
		{
			Name = "Bingo Called Numbers Bag";
		}

		public BingoCalledNumbersBag( Serial serial ) : base( serial )
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