//Created by Blake Miller (wangchung)
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BraceletofMethodis: GoldBracelet
	{
		[Constructable]
		public BraceletofMethodis()
		{
			Weight = 1;
			Name = "Bracelet of Methodis";
			Hue = 2101;
			Attributes.CastRecovery = 2;
			Attributes.CastSpeed = 1;
			Attributes.Luck = 25;
			Attributes.RegenMana = 3;
			Attributes.SpellChanneling = 1;
			LootType = LootType.Cursed; 
		}

		public BraceletofMethodis( Serial serial ) : base( serial )
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