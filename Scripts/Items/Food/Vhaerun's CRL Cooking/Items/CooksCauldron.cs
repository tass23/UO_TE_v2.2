using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class CooksCauldron : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBoiling.CraftSystem; } }

		[Constructable]
		public CooksCauldron() : base( 0x9ED )
		{
			Name = "Cook's Cauldron";
			Weight = 4.0;
		}

		[Constructable]
		public CooksCauldron( int uses ) : base( uses, 0x9ED )
		{
			Name = "Cook's Cauldron";
			Weight = 4.0;
		}

		public CooksCauldron( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}