using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class FryingPan : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefGrilling.CraftSystem; } }

		[Constructable]
		public FryingPan() : base( 0x9E2 )
		{
			Name = "Frying Pan";
			Weight = 3.0;
		}

		[Constructable]
		public FryingPan( int uses ) : base( uses, 0x9E2 )
		{
			Name = "Frying Pan";
			Weight = 3.0;
		}

		public FryingPan( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}