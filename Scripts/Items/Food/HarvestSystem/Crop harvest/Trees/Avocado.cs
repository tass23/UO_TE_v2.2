using System;
using Server.Network;

namespace Server.Items
{
	public class Avocado : Food
	{
		[Constructable]
		public Avocado() : this( 1 )
		{
		}

		[Constructable]
		public Avocado( int amount ) : base( amount, 0xC80 )
		{
			Weight = 0.5;
			FillFactor = 1;
			Stackable = true;
			Hue = 0x483;
			Name = "Avocado";
		}

		public Avocado( Serial serial ) : base( serial )
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