using System;
using Server.Targeting;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class RawBacon : CookableFood, Meat
	{
		[Constructable]
		public RawBacon() : this( 1 ){}

		[Constructable]
		public RawBacon( int amount ) : base( 0x979, 0 )
		{
			Name = "raw slice of bacon";
			Stackable = true;
			Amount = amount;
			Hue = 336;
		}

		public RawBacon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook()
		{
			return new Bacon();
		}
	}
}