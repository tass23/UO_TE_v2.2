using System;
using Server.Network;

namespace Server.Items
{
	public class Eggplant : Food
	{
		[Constructable]
		public Eggplant() : this( 1 )
		{
		}

		[Constructable]
		public Eggplant( int amount ) : base( amount, 0xD3A )
		{
			FillFactor = 2;
			Name = "Eggplant";
			Hue = 0x72;
		}

		public Eggplant( Serial serial ) : base( serial )
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