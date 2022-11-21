using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class VegePie : Food
	{
		[Constructable]
		public VegePie() : base( 0x1041 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
			Name = "vegetable pie";
		}

		public VegePie( Serial serial ) : base( serial )
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