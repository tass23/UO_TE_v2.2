using System;
using Server.Network;

namespace Server.Items
{
	public class Peanut : Food
	{
		[Constructable]
		public Peanut() : this( 1 )
		{
		}

		[Constructable]
		public Peanut( int amount ) : base( amount, 0x14FD )
		{
			Weight = 0.1;
			FillFactor = 1;
			Hue = 0x224;
			Name = "Peanut";
		}

		public Peanut( Serial serial ) : base( serial )
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