using System;
using Server.Network;

namespace Server.Items
{
	public class Cranberry : Food
	{
		[Constructable]
		public Cranberry() : this( 1 )
		{
		}

		[Constructable]
		public Cranberry( int amount ) : base( amount, 0x9D1 )
		{
			FillFactor = 1;
			Hue = 0xE8;
			Name = "Cranberry";
		}

		public Cranberry( Serial serial ) : base( serial )
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