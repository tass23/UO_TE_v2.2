using System;
using Server;

namespace Server.Items
{
	public class PileOfBlankCandles : Item
	{

		[Constructable]
		public PileOfBlankCandles() : base( 0x1BD6 )
		{
			Name = "Pile of Blank Candles";
			Weight = 3.0;
			Hue = 1153;
		}

		public PileOfBlankCandles( Serial serial ) : base( serial )
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