using System;
using System.Collections;
using Server.Network;

namespace Server.Items
{
	public class BaconSlab : Food, Meat, ICarvable
	{
		[Constructable]
		public BaconSlab() : this( 1 ){}

		[Constructable]
		public BaconSlab( int amount ) : base( amount, 0x976 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new Bacon(), 5 );
			from.SendMessage( "You cut the slab into slices." );
		}

		public BaconSlab( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}