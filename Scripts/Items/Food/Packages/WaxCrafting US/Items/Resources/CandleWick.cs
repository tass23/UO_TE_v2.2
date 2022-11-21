using System;
using Server;

namespace Server.Items
{
	public class CandleWick : Item
	{
		[Constructable]
		public CandleWick() : this( 1 )
		{
		}

		[Constructable]
		public CandleWick( int amount ) : base( 0x979 )
		{
			Name = "Candle Wick";
			Stackable = true;
			Weight = 0.5;
			Amount = amount;
			Hue = 1052;
		}

		public CandleWick( Serial serial ) : base( serial )
		{
		}

		/*
		public override Item Dupe( int amount )
		{
			return base.Dupe( new CandleWick( amount ), amount );
		}
		*/
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