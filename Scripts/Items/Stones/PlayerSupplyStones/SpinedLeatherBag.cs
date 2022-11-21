using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SpinedLeatherBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Spined Leather Bag"; }
		}

		[Constructable]
		public SpinedLeatherBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public SpinedLeatherBag( int amount )
		{
			DropItem( new SewingKit() );
			DropItem( new Scissors() );
			DropItem( new SpinedHides( 1000 ) );
		}
		
		public SpinedLeatherBag( Serial serial ) : base( serial )
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