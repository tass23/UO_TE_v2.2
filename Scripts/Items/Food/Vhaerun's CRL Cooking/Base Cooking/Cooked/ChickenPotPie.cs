using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class ChickenPotPie : Food
	{
		[Constructable]
		public ChickenPotPie() : base( 0x1041 )
		{
			this.Name = "baked chicken pot pie";
			this.Weight = 1.0;
			this.FillFactor = 10;
		}

		public ChickenPotPie( Serial serial ) : base( serial )
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