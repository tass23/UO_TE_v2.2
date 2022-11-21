using System;
using Server.Network;

namespace Server.Items
{
	public class YellowPepper : Food
	{
		[Constructable]
		public YellowPepper() : this( 1 )
		{
		}

		[Constructable]
		public YellowPepper( int amount ) : base( amount, 0xC75 )
		{
			FillFactor = 1;
			Hue = 435;
			Name = "Yellow Pepper";
		}

		public YellowPepper( Serial serial ) : base( serial )
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