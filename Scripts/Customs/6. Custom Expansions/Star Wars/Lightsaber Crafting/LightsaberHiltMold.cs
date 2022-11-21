using System;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;
using Server.Gumps;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server;
using Server.Items;
using Server.Engines.Craft;

namespace Server.Items
{
	public class LightsaberHiltMold : BaseTool
	{
		public override CraftSystem CraftSystem{ get{ return DefForging.CraftSystem; } }

		[Constructable]
		public LightsaberHiltMold() : base( 0x194C )
		{
			Weight = 25.0;
			//ShowUsesRemaining = true;
			//UsesRemaining = 4;
			Name = "Lightsaber Hilt Mold";
			Hue = 1086;
		}

		[Constructable]
		public LightsaberHiltMold( int uses ) : base( uses, 0x194C )
		{
			Weight = 1.0;
			Hue = 1086;
		}

		public LightsaberHiltMold( Serial serial ) : base( serial )
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