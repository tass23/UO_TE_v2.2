using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class JuicersTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefJuicing.CraftSystem; } }

		[Constructable]
		public JuicersTools() : base( 0xE4F )
		{
			Name = "Juicer's Tools";
			Weight = 2.0;
		}

		[Constructable]
		public JuicersTools( int uses ) : base( uses, 0xE4F )
		{
			Name = "Juicer's Tools";
			Weight = 2.0;
		}

		public JuicersTools( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}