using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class HoneydewMelonCake : Food
	{
		[Constructable]
		public HoneydewMelonCake() : base( 0x9E9 )
		{
			Name = "a honeydew melon cake";
			this.Weight = 1.0;
			this.FillFactor = 15;
			Hue = 61;
		}

		public HoneydewMelonCake( Serial serial ) : base( serial )
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