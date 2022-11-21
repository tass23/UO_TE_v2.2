using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class PeachCake : Food
	{
		[Constructable]
		public PeachCake() : base( 0x9E9 )
		{
			Name = "a peach cake";
			this.Weight = 1.0;
			this.FillFactor = 15;
			Hue = 46;
		}

		public PeachCake( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}