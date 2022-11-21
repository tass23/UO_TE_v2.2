//Created by Blake Miller (wangchung)
using System;
using Server;
using Server.Items;

namespace Server.Items
{

	public class NecklaceofMethodis: GoldNecklace
	{
		[Constructable]
		public NecklaceofMethodis()
		{
			Weight = 4;
			Name = "Necklace Of Methodis";
			Hue = 2101;
			Attributes.CastSpeed = 3;
			Attributes.Luck = 15;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.SpellDamage = 5;
			LootType = LootType.Cursed;
		}
		
		public NecklaceofMethodis( Serial serial ) : base( serial )
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