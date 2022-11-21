using System;
using Server;

namespace Server.Items
{
	public class RingOfTheMagicianBlessed : GoldRing
	{
		public override int LabelNumber{ get{ return 1061105; } } // Ring of the Magician
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public RingOfTheMagicianBlessed()
		{
			LootType = LootType.Blessed;
			Name = "Ring of the Magician";
			Hue = 0x554;
			Attributes.CastRecovery = 6;
			Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 10;
			Resistances.Energy = 15;
			Attributes.RegenMana = 10;
		}

		public RingOfTheMagicianBlessed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x12B )
				Hue = 0x554;
		}
	}
}