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
	public class PowerCell : Item
	{
		[Constructable]
		public PowerCell() : this( 1 )
		{
		}

		[Constructable]
		public PowerCell( int amount ) : base( 3903 )
		{
			Name = "Diatium Power Cell";
			Hue = 2936;
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
		}

		public PowerCell( Serial serial ) : base( serial )
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