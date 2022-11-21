using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class FruitCake : Food
	{
		[Constructable]
		public FruitCake() : this( 0 )
		{
		}

		[Constructable]
		public FruitCake( int Color ) : base( 0x9E9 )
		{
			Name = "a fruit cake";
			this.Weight = 1.0;
			this.FillFactor = 15;
			Hue = Color;
		}

		public FruitCake( Serial serial ) : base( serial )
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