using System;
using Server.Network;

namespace Server.Items
{
	public class TanGinger : Food
	{
		[Constructable]
		public TanGinger() : this( 1 ) { }

		[Constructable]
		public TanGinger( int amount ) : base( amount, 0xF85 )
		{
			FillFactor = 1;
			Hue = 0x413;
			Name = "Tan Ginger";
		}

		public TanGinger( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}