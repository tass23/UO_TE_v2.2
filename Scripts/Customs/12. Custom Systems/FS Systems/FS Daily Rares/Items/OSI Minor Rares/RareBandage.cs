using System;

namespace Server.Items
{
	public class RareBandage : Bandage
	{
		[Constructable]
		public RareBandage()
		{
			int chance = Utility.Random( 100 );

			if ( chance <= 5 )
				ItemID = 0xEE9;
		}

		public RareBandage( Serial serial ) : base( serial )
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