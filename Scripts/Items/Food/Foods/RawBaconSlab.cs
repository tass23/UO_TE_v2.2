using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawBaconSlab : CookableFood, Meat, ICarvable
	{
		[Constructable]
		public RawBaconSlab() : this( 1 ){}

		[Constructable]
		public RawBaconSlab( int amount ) : base( 0x976, 0 )
		{
			Name = "raw slab of bacon";
			Stackable = true;
			Hue = 41;
			Amount = amount;
		}

		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new RawBacon(), 5 );
			from.SendMessage( "You cut the slab into slices." );
		}

		public RawBaconSlab( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook()
		{
			return new BaconSlab();
		}
	}
}