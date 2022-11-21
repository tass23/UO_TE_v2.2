using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public class StabilizingRing : Item
	{
		[Constructable]
		public StabilizingRing() : base( 5439 )
		{
			Name = "Magnetic Stabilizing Ring";
			Stackable = false;
			Weight = 1.0;
			Hue = 2103;
		}

		public StabilizingRing( Serial serial ) : base( serial )
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