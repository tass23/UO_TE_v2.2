using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class RawHeadlessFish : CookableFood, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 4 );
		}

		[Constructable]
		public RawHeadlessFish() : this( 1 )
		{
		}

		[Constructable]
		public RawHeadlessFish( int amount ) : base( Utility.Random( 7703, 2 ), 20 )
		{
			Stackable = true;
			Weight = 0.6;
			Amount = amount;
		}

		public override Food Cook()
		{
			return new CookedHeadlessFish();
		}

		public RawHeadlessFish( Serial serial ) : base( serial )
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
