using System;
using Server.Network;

namespace Server.Items
{
	public class Grapefruit : Food
	{
		[Constructable]
		public Grapefruit() : this( 1 )
		{
		}

		[Constructable]
		public Grapefruit( int amount ) : base( amount, 0x9D0 )
		{
			FillFactor = 2;
			Hue = 0x15E;
			Name = "Grapefruit";
		}

		public Grapefruit( Serial serial ) : base( serial )
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