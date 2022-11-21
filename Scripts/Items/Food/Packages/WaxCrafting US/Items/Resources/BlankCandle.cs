using System;
using Server;

namespace Server.Items
{
	public class BlankCandle : Item
	{

		[Constructable]
		public BlankCandle() : base( 0x1433 )
		{
			Name = "Blank Candle";
			Weight = 0.5;
			Hue = 1150;
		}

		public BlankCandle( Serial serial ) : base( serial )
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