using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HornedLeatherBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Horned Leather Bag"; }
		}

		[Constructable]
		public HornedLeatherBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public HornedLeatherBag( int amount )
		{
			DropItem( new SewingKit() );
			DropItem( new Scissors() );
			DropItem( new HornedHides( 1000 ) );
		}
		
		public HornedLeatherBag( Serial serial ) : base( serial )
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