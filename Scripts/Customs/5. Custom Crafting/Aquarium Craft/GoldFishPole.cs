using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class GoldFishPole : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefAquarium.CraftSystem; } }

		[Constructable]
		public GoldFishPole() : base( 0xDC0 )
		{
			Name = "A Golden Rod";
			Hue = 50;
			Weight = 1.0;
		}

		[Constructable]
		public GoldFishPole( int uses ) : base( uses, 50 )
		{
			Weight = 1.0;
		}

		public GoldFishPole( Serial serial ) : base( serial )
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