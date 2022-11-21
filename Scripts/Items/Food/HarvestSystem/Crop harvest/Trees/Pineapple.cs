using System;
using Server.Network;

namespace Server.Items
{
	public class Pineapple : Food
	{
		[Constructable]
		public Pineapple() : this( 1 )
		{
		}

		[Constructable]
		public Pineapple( int amount ) : base( amount, 3165 )
		{
			FillFactor = 2;
			Hue = 1130;
			Name = "Pineapple";
		}

		public Pineapple( Serial serial ) : base( serial )
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