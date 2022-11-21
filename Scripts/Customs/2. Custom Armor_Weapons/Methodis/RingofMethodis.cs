//Created by Blake Miller (wangchung)
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RingofMethodis: GoldRing
	{
		[Constructable]
		public RingofMethodis()
		{
			Weight = 1;
			Name = "Ring Of Methodis";
			Hue = 2101;
			Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 15;
			Attributes.RegenMana = 2;
			Attributes.SpellChanneling = 1;
			LootType = LootType.Cursed;
		}
		
		public RingofMethodis( Serial serial ) : base( serial )
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