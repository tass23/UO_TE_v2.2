using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class HamSlices : Food, Meat
	{
		[Constructable]
		public HamSlices() : this( 1 ){}

		[Constructable]
		public HamSlices( int amount ) : base( amount, 0x1E1F )
		{
			this.Weight = 0.2;
			this.FillFactor = 1;
		}

		public HamSlices( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}