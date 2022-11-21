using System;
using Server.Network;

namespace Server.Items
{
	public class TanMushroom : Food
	{
		[Constructable]
		public TanMushroom() : this( 1 ){}

		[Constructable]
		public TanMushroom( int amount ) : base( amount, 0xD19 )
		{
			FillFactor = 4;
		}

		public TanMushroom( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 