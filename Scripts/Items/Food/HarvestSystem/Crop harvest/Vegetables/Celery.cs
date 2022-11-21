using System;
using Server.Network;

namespace Server.Items
{
	public class Celery : Food
	{
		[Constructable]
		public Celery() : this( 1 )
		{
		}

		[Constructable]
		public Celery( int amount ) : base( amount, 0xC77 )
		{
			Weight = 0.5;
			FillFactor = 1;
			Name = "Celery";
			Hue = 0xAA;
		}

		public Celery( Serial serial ) : base( serial )
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