using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawHamSlices : CookableFood, Meat
	{
		[Constructable]
		public RawHamSlices() : this( 1 ){}

		[Constructable]
		public RawHamSlices( int amount ) : base( 0x1E1F, 0 )
		{
			Name = "raw sliced ham";
			Stackable = true;
			Amount = amount;
			Hue = 336;
		}

		public RawHamSlices( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook()
		{
			return new HamSlices();
		}
	}
}