using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class RawScaledFish : Item, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			if ( !Movable )
				return;

			base.ScissorHelper( from, new RawHeadlessFish(), 1 );
			from.AddToBackpack( new FishHeads( item.Amount ) );
		}

		[Constructable]
		public RawScaledFish() : this( 1 )
		{
		}

		[Constructable]
		public RawScaledFish( int amount ) : base( Utility.Random( 7701, 2 ) )
		{
			Stackable = true;
			Weight = 0.8;
			Amount = amount;
		}

		public RawScaledFish( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
