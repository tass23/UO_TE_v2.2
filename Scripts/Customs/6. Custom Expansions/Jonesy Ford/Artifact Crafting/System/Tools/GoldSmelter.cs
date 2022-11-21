using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class GoldSmelter : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefArtyCraft.CraftSystem; } }

		[Constructable]
		public GoldSmelter() : base( 0x1421 )
		{
			Name = "Transmogrifier";
			Weight = 2.0;
			ShowUsesRemaining = true;
			Hue = 1195;
		}

		[Constructable]
		public GoldSmelter( int uses ) : base( uses, 0x1421 )
		{
			Weight = 2.0;
		}

		public GoldSmelter( Serial serial ) : base( serial )
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