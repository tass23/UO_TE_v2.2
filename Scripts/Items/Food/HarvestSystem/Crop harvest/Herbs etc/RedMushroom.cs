using System;
using Server.Network;

namespace Server.Items
{
	public class RedMushroom : Food
	{
		[Constructable]
		public RedMushroom() : this( 1 ){}

		[Constructable]
		public RedMushroom( int amount ) : base( amount, 0xD16 )
		{
			FillFactor = 4;
		}

		public RedMushroom( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
} 