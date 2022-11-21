using System;
using System.Collections;
using Server.Network;
namespace Server.Items
{
	public class CheeseWheel : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;
			if ( this.Amount > 1 )
			{
				from.SendMessage( "You can only cut up one wheel at a time." );
				return;
			}
			base.ScissorHelper( from, new CheeseWedge(), 3 );
			from.AddToBackpack( new CheeseWedgeSmall() );
			from.SendMessage( "You cut a wedge out of the wheel." );
		}
		[Constructable]
		public CheeseWheel() : this( 1 ) { }
		[Constructable]
		public CheeseWheel( int amount ) : base( amount, 0x97E )
		{
			this.Weight = 0.4;
			this.FillFactor = 12;
		}
		public CheeseWheel( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}