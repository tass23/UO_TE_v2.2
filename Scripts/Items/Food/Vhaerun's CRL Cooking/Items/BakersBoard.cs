using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class BakersBoard : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefBaking.CraftSystem; } }

		[Constructable]
		public BakersBoard() : base( 0x14EA )
		{
			Name = "Baker's Board";
			Weight = 4.0;
		}

		[Constructable]
		public BakersBoard( int uses ) : base( uses, 0x14EA )
		{
			Name = "Baker's Board";
			Weight = 4.0;
		}

		public BakersBoard( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}