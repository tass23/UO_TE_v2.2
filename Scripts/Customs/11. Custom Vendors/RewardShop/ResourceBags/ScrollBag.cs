using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ScrollBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Scroll Supply bag"; }
		}

		[Constructable]
		public ScrollBag() : this( 1 )
		{
			Movable = true;
			Hue = 0x105;
		}

		[Constructable]
		public ScrollBag( int amount )
		{
			DropItem( new BlankScroll( 1000 ) );
		}

		public ScrollBag( Serial serial ) : base( serial )
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