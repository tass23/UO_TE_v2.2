using System;
using Server.Network;

namespace Server.Items
{
	public class SnowPeas : Food
	{
		[Constructable]
		public SnowPeas() : this( 1 )
		{
		}

		[Constructable]
		public SnowPeas( int amount ) : base( amount, 0xF2F )
		{
			Weight = 0.1;
			FillFactor = 1;
			Hue = 0x29A;
			Name = "Snow Peas";
		}

		public SnowPeas( Serial serial ) : base( serial )
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