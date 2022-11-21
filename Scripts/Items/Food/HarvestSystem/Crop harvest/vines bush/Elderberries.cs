using System;
using Server.Network;

namespace Server.Items
{
	public class Elderberries : Food
	{
		[Constructable]
		public Elderberries() : this( 1 )
		{
		}

		[Constructable]
		public Elderberries( int amount ) : base( amount, 0x9D1 )
		{
			FillFactor = 1;
			Hue = 0x200;
			Name = "Elderberries";
		}

		public Elderberries( Serial serial ) : base( serial )
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