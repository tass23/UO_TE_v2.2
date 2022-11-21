using System;
using Server.Network;

namespace Server.Items
{
	public class GreenBean : Food
	{
		[Constructable]
		public GreenBean() : this( 1 )
		{
		}

		[Constructable]
		public GreenBean( int amount ) : base( amount, 0xF80 )
		{
			Weight = 0.1;
			FillFactor = 1;
			Hue = 0x483;
			Name = "Green Bean";
		}

		public GreenBean( Serial serial ) : base( serial )
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