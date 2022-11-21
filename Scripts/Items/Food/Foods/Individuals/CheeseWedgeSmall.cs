using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class CheeseWedgeSmall : Food
	{
		[Constructable]
		public CheeseWedgeSmall() : this( 1 ) { }
		[Constructable]
		public CheeseWedgeSmall( int amount ) : base( amount, 0x97C )
		{
			this.Weight = 0.1;
			this.FillFactor = 3;
		}
		public CheeseWedgeSmall( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}