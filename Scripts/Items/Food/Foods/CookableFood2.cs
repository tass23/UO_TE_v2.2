using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawSteak : CookableFood
	{
		[Constructable]
		public RawSteak() : this( 1 ){}

		[Constructable]
		public RawSteak( int amount ) : base( 0x3BCE, 10 )
		{
			Name = "Raw Steak";
			Stackable = true;
			Amount = amount;
		}

		public RawSteak( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook()
		{
			return new CookedSteak();
		}
	}
}
