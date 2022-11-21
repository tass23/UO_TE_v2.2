using System;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	public class FishHeads : Food
	{
		[Constructable]
		public FishHeads() : this( 1 )
		{
		}

		[Constructable]
		public FishHeads( int amount ) : base( Utility.Random( 7705, 2 ) )
		{
			Weight = 0.1;
			Amount = amount;
			this.FillFactor = 0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "*ugh*! That's cat food!" );
			return;
		}

		public FishHeads( Serial serial ) : base( serial )
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
