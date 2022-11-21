using System;
using Server.Network;

namespace Server.Items
{
	public class Cherry : Food
	{
		[Constructable]
		public Cherry() : this( 1 )
		{
		}

		[Constructable]
		public Cherry( int amount ) : base( amount, 0x9D1 )
		{
			FillFactor = 2;
			Hue = 0x85;
			Name = "Cherry";
		}

		public Cherry( Serial serial ) : base( serial )
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