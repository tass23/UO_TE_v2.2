using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public class WinecraftersTools : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefWinecrafting.CraftSystem; } }

		[Constructable]
		public WinecraftersTools() : base( 0xF00 )
		{
			Weight = 2.0;
			Name = "Winecrafter Tools";
			Hue = 0x530;
		}

		[Constructable]
		public WinecraftersTools( int uses ) : base( uses, 0xF00 )
		{
			Weight = 2.0;
			Name = "Winecrafter Tools";
			Hue = 0x530;
		}

		public WinecraftersTools( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}