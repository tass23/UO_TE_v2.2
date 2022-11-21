using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BarbedLeatherBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Barbed Leather Bag"; }
		}

		[Constructable]
		public BarbedLeatherBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public BarbedLeatherBag( int amount )
		{
			DropItem( new SewingKit() );
			DropItem( new Scissors() );
			DropItem( new BarbedHides( 1000 ) );
		}
		
		public BarbedLeatherBag( Serial serial ) : base( serial )
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