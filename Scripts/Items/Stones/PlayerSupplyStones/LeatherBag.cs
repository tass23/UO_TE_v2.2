using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class LeatherBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Leather Bag"; }
		}

		[Constructable]
		public LeatherBag() : this( 1 )
		{
			Movable = true;
		}

		[Constructable]
		public LeatherBag( int amount )
		{
			DropItem( new SewingKit() );
			DropItem( new Scissors() );
			DropItem( new Hides( 1000 ) );
		}
		
		public LeatherBag( Serial serial ) : base( serial )
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