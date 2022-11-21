
using System;

namespace Server.Items
{
	public class EmptyJuiceBottle : Item
	{
		[Constructable]
		public EmptyJuiceBottle() : this( 1 ) { }

		[Constructable]
		public EmptyJuiceBottle( int amount ) : base( 3854 )
		{
			Stackable = true;
			Name = "Empty Juice Bottle";
			Amount = amount;
		}

		public EmptyJuiceBottle( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}