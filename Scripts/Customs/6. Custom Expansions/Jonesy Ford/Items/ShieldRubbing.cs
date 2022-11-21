using System;
using Server;

namespace Server.Items
{
	public class ShieldRubbing : Item
	{
		
		[Constructable]
		public ShieldRubbing() : base( 0x14F1 )
		{
			LootType = LootType.Blessed;
			Weight = 1;
			Name = "Shield Rubbing";
			Hue = 1089;
		}
		
		public ShieldRubbing( Serial serial ) : base( serial )
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