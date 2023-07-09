using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfAmmunition : Bag
	{
		[Constructable]
		public BagOfAmmunition() : this( 25 )
		{
			Hue = 0x30;
		}

		[Constructable]
		public BagOfAmmunition( int amount )
		{
			DropItem( new Arrow   ( amount ) );
                        DropItem( new Bolt   ( amount ) );
			
		}

		public BagOfAmmunition( Serial serial ) : base( serial )
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