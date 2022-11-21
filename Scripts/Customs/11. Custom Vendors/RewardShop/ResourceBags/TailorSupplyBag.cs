using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TailorSupplyBag : Bag
	{
		public override string DefaultName
		{
			get { return "a Tailoring Supply bag"; }
		}

		[Constructable]
		public TailorSupplyBag() : this( 1 )
		{
			Movable = true;
			Hue = 0x315;
		}

		[Constructable]
		public TailorSupplyBag( int amount )
		{
			DropItem( new Hides( 500 ) );
			DropItem( new BarbedHides( 500 ) );
			DropItem( new HornedHides( 500 ) );
			DropItem( new SpinedHides( 500 ) );
			DropItem( new BoltOfCloth( 100 ) );
			DropItem( new Wool( 500 ) );
		}
		
		public TailorSupplyBag( Serial serial ) : base( serial )
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