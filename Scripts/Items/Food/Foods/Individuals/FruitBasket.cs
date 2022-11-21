using System;
using Server.Network;
namespace Server.Items
{
	public class FruitBasket : Food
	{
		[Constructable]
		public FruitBasket() : this( 1 ) { }
		[Constructable]
		public FruitBasket( int amount ) : base( amount, 0x993 )
		{
			this.Weight = 2.0;
			this.FillFactor = 3;
			this.Name = "basket of fruit";
		}
		public FruitBasket( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}