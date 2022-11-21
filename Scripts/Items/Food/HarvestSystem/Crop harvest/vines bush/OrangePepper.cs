using System;
using Server.Network;

namespace Server.Items
{
	public class OrangePepper : Food
	{
		[Constructable]
		public OrangePepper() : this( 1 )
		{
		}

		[Constructable]
		public OrangePepper( int amount ) : base( amount, 0xC75 )
		{
			FillFactor = 1;
			Hue = 455;
			Name = "Orange Pepper";
		}

		public OrangePepper( Serial serial ) : base( serial )
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