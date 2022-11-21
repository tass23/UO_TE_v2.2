using System;
using Server.Network;

namespace Server.Items
{
	public class Kiwi : Food
	{
		[Constructable]
		public Kiwi() : this( 1 )
		{
		}

		[Constructable]
		public Kiwi( int amount ) : base( amount, 0xF8B )
		{
			FillFactor = 1;
			Hue = 0x458;
			Name = "Kiwi";
		}

		public Kiwi( Serial serial ) : base( serial )
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