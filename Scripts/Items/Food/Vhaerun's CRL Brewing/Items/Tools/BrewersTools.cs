using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class BrewersTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBrewing.CraftSystem; } }

		[Constructable]
		public BrewersTools() : base( 0x1EBC )
		{
			Weight = 2.0;
			Name = "Brewer's Tools";
			Hue = 0x3EF;
		}

		[Constructable]
		public BrewersTools( int uses ) : base( uses, 0xE7F )
		{
			Weight = 2.0;
			Name = "Brewer's Tools";
			Hue = 0x3EF;
		}

		public BrewersTools( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}