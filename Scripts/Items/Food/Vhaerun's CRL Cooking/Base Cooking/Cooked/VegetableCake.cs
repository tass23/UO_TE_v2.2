using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class VegetableCake : Food
	{
		[Constructable]
		public VegetableCake() : this( 0 )
		{
		}

		[Constructable]
		public VegetableCake( int Color ) : base( 0x9E9 )
		{
			Name = "a vegetable cake";
			this.Weight = 1.0;
			this.FillFactor = 15;
			Hue = Color;
		}

		public VegetableCake( Serial serial ) : base( serial )
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