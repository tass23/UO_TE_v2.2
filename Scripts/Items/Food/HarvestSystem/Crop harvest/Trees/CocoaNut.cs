using System;
using Server.Network;

namespace Server.Items
{
	public class CocoaNut : Item
	{
		[Constructable]
		public CocoaNut() : this( 1 ) { }

		[Constructable]
		public CocoaNut( int amount ) : base( 0x1726 )
		{
			Name = "Coconut";
			Hue = 0x422;
			Stackable = true;
			Amount = amount;
		}

		public void Carve( Mobile from, Item item )
		{
			if ( !Movable ) return;
			base.ScissorHelper( from, new CocoaBean(), 1 );
			from.SendMessage( "You cut away the husk to get the bean." );
		}

		public CocoaNut( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}