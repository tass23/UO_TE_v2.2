using System;
using Server.Network;

namespace Server.Items
{
	public class Blueberry : Food
	{
		[Constructable]
		public Blueberry() : this( 1 )
		{
		}

		[Constructable]
		public Blueberry( int amount ) : base( amount, 0x9D1 )
		{
			FillFactor = 1;
			Hue = 0x62;
			Name = "Blueberry";
		}

		public Blueberry( Serial serial ) : base( serial )
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