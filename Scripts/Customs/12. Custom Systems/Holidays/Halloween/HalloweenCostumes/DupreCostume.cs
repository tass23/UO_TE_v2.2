using System;
using Server;

namespace Server.Items
{
	public class DupreCostume : BaseSuit
	{
		[Constructable]
		public DupreCostume() : base( AccessLevel.Player, 0x0, 0x2050 )
		{
			Name = "Dupre Costume";
			Weight = 1.0;
			Movable = true;
			LootType = LootType.Blessed;
			Layer = Layer.OuterTorso;

		}

		public DupreCostume( Serial serial ) : base( serial )
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