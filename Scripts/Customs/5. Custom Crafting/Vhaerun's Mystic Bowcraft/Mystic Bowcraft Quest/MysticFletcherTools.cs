using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	[FlipableAttribute( 0x1022, 0x1023 )]
	public class MysticFletcherTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefMysticBowcraft.CraftSystem; } }

		[Constructable]
		public MysticFletcherTools() : base( 0x1022 )
		{
			Hue = 0x965;
			Name = "mystic fletcher tools";
			Weight = 2.0;
			ShowUsesRemaining = true;
		}

		[Constructable]
		public MysticFletcherTools( int uses ) : base( uses, 0x1022 )
		{
			Hue = 0x965;
			Name = "mystic fletcher tools";
			Weight = 2.0;
		}

		public MysticFletcherTools( Serial serial ) : base( serial )
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