using System;
using Server.Network;

namespace Server.Items
{
	public class RedPepper : Food
	{
		[Constructable]
		public RedPepper() : this( 1 )
		{
		}

		[Constructable]
		public RedPepper( int amount ) : base( amount, 0xC75 )
		{
			FillFactor = 1;
			Hue = 37;
			Name = "Red Pepper";
		}

		public RedPepper( Serial serial ) : base( serial )
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