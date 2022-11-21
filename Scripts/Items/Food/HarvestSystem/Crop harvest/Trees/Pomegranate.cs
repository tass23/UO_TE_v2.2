using System;
using Server.Network;

namespace Server.Items
{
	public class Pomegranate : Food
	{
		[Constructable]
		public Pomegranate() : this( 1 )
		{
		}

		[Constructable]
		public Pomegranate( int amount ) : base( amount, 0x9D0 )
		{
			FillFactor = 2;
			Hue = 0x215;
			Name = "Pomegranate";
		}

		public Pomegranate( Serial serial ) : base( serial )
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