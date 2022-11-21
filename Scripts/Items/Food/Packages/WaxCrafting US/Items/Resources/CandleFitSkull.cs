using System;
using Server;

namespace Server.Items
{
	public class CandleFitSkull : Item
	{

		[Constructable]
		public CandleFitSkull() : base( 0x1AE3 )
		{
			Name = "A Candle Fit Skull";
			Weight = 0.5;
		}

		public CandleFitSkull( Serial serial ) : base( serial )
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