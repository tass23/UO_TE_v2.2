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
	public class AcklaySilk : Item
	{
		[Constructable]
		public AcklaySilk() : this( 1 )
		{
		}

		[Constructable]
		public AcklaySilk( int amount ) : base( 3567 )
		{
			Stackable = true;
			Amount = amount;
			Weight = 1.0;
			Hue = 1772;
			Name = "Acklay Silk";
		}

		public AcklaySilk( Serial serial ) : base( serial )
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