using System;
using Server;

namespace Server.Items
{
	public class PaintBallPellets : Item
	{
		[Constructable]
		public PaintBallPellets() : this( 1 )
		{
		}

		[Constructable]
		public PaintBallPellets( int amount ) : base( 0xF21 )
		{
			Name = "Paint Ball Pellets";
			Hue = 2364;
			Stackable = true;
			Weight = 0.01;
			Amount = amount;
		}

		public PaintBallPellets( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}