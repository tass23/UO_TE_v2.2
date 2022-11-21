using System;
using Server;

namespace Server.Items
{
	public class CraftedGoldBricks : Item
	{
		public override int LabelNumber{ get{ return 1063489; } }
		
		[Constructable]
		public CraftedGoldBricks() : base( 0x1BEB )
		{

			Weight = 0.1;
			LootType = LootType.Blessed;

		}

		public CraftedGoldBricks( Serial serial ) : base( serial )
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