using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BottleBag : Bag
	{
		public override string DefaultName
		{
			get { return "an Empty Bottle Supply bag"; }
		}

		[Constructable]
		public BottleBag() : this( 1 )
		{
			Movable = true;
			Hue = 0x250;
		}

		[Constructable]
		public BottleBag( int amount )
		{
			DropItem( new EmptyWineBottle( 500 ) );
			DropItem( new Bottle( 500 ) );
		}

		public BottleBag( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}