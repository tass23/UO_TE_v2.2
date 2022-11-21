using System;
using Server.Network;

namespace Server.Items
{
	public class ChiliPepper : Food
	{
		[Constructable]
		public ChiliPepper() : this( 1 )
		{
		}

		[Constructable]
		public ChiliPepper( int amount ) : base( amount, 0xC6D )
		{
			Weight = 0.5;
			FillFactor = 1;
			Hue = 460;
			Name = "Chili Pepper";
		}

		public ChiliPepper( Serial serial ) : base( serial )
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