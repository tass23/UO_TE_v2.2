using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class CheeseWedge : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;
			base.ScissorHelper( from, new CheeseWedgeSmall(), 3 );
			from.SendMessage( "You cut the wheel into 3 wedges." );
		}
		[Constructable]
		public CheeseWedge() : this( 1 ) { }
		[Constructable]
		public CheeseWedge( int amount ) : base( amount, 0x97D )
		{
			this.Weight = 0.3;
			this.FillFactor = 9;
		}
		public CheeseWedge( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}