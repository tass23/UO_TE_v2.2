using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class MeatCake : Food
	{
		[Constructable]
		public MeatCake() : this( 0 )
		{
		}

		[Constructable]
		public MeatCake( int Color ) : base( 0x9E9 )
		{
			Name = "a meat cake";
			this.Weight = 1.0;
			this.FillFactor = 15;
			Hue = Color;
		}

		public MeatCake( Serial serial ) : base( serial )
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