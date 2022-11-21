using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class CookedHeadlessFish : Food, ICarvable
	{
		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new FishSteak(), 4 );
		}

		[Constructable]
		public CookedHeadlessFish() : this( 1 )
		{
		}

		[Constructable]
		public CookedHeadlessFish( int amount ) : base( Utility.Random( 7708, 2 ) )
		{
			Stackable = true;
			Weight = 0.4;
			Amount = amount;
			this.FillFactor = 12;
		}

		public CookedHeadlessFish( Serial serial ) : base( serial )
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
