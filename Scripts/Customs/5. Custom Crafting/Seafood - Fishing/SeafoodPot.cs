using System;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{

	public class SeafoodPot : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefCooking.CraftSystem; } }

		[Constructable]
		public SeafoodPot() : base( 0x42BE )
		{
			Name = "Seafood Pot";
			Hue = 896;
			Weight = 2.0;
		}

		[Constructable]
		public SeafoodPot( int uses ) : base( uses, 0x42BE )
		{
			Weight = 2.0;
		}

		public SeafoodPot( Serial serial ) : base( serial )
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
