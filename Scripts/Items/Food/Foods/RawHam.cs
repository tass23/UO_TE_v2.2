using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class RawHam : CookableFood, Meat, ICarvable
	{
		[Constructable]
		public RawHam() : this( 1 ){}

		[Constructable]
		public RawHam( int amount ) : base( 0x9C9, 0 )
		{
			Name = "raw ham";
			Stackable = true;
			Hue = 41;
			Amount = amount;
		}

		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new RawHamSlices(), 5 );
			from.SendMessage( "You slice the ham." );
		}

		public RawHam( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}

		public override Food Cook()
		{
			return new Ham();
		}
	}
}