using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class WitchCauldron : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefWitchcraft.CraftSystem; } }

		[Constructable]
		public WitchCauldron() : base( 0x9ED )
		{
			Weight = 10.0;
			Hue = 0x3CC;
			Name = "Witch's Cauldron";
		}

		[Constructable]
		public WitchCauldron( int uses ) : base( uses, 0x9ED )
		{
			Weight = 10.0;
			Hue = 0x3CC;
			Name = "Witch's Cauldron";
		}

		public WitchCauldron( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}