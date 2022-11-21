using System;
using Server.Network;
using Server.Gumps;
using Server.Multis;
using System.Collections.Generic;
using Server.ContextMenus; 

namespace Server.Items
{
	public class HorsemanHead : Item
	{
		[Constructable]
		public HorsemanHead() : base( 0x4691 )
		{
			LootType = LootType.Blessed;
			Weight = 10;
			Light = LightType.Circle150;
		}

		public HorsemanHead( Serial serial ) : base( serial )
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
