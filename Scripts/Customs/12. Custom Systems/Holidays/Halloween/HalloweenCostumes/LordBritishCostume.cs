using System;
using Server;

namespace Server.Items
{
	public class LordBritishCostume : BaseSuit
	{
		[Constructable]
		public LordBritishCostume() : base( AccessLevel.Player, 0x0, 0x2042 )
		{
			Name = "Lord British Costume";
			Weight = 1.0;
			Movable = true;
			LootType = LootType.Blessed;
			Layer = Layer.OuterTorso;

		}

		public LordBritishCostume( Serial serial ) : base( serial )
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