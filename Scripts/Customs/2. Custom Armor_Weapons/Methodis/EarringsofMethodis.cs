//Created by Blake Miller (wangchung)
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EarringsofMethodis: GoldEarrings
	{
		[Constructable]
		public EarringsofMethodis()
		{
			Weight = 2;
			Name = "Earrings Of Methodis";
			Hue = 2101;
			Attributes.BonusMana = 3;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.SpellChanneling = 1;
			LootType = LootType.Cursed;
        }

		public EarringsofMethodis( Serial serial ) : base( serial )
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
		}
	}
}