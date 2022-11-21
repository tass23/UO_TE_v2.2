using System;
using Server.Network;
namespace Server.Items
{
	public class Spam : Food
	{
		[Constructable]
		public Spam() : this( 1 ) { }
		[Constructable]
		public Spam( int amount ) : base( amount, 0x1044 )
		{
			FillFactor = 7;
		}
		public Spam( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}