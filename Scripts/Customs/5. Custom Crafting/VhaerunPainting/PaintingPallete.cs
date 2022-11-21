
using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class PaintingPallete : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefPainting.CraftSystem; } }

		[Constructable]
		public PaintingPallete() : base( 0xFC1 )
		{
			Name = "Hand-Power Miter Saw";
			Weight = 2.0;
			Hue = 1195;
		}


		public PaintingPallete( Serial serial ) : base( serial )
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