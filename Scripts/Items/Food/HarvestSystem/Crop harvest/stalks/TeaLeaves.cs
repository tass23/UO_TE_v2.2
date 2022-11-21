using System;
using Server.Network;

namespace Server.Items
{
	public class TeaLeaves : Item
	{
		[Constructable]
		public TeaLeaves() : this( 1 ) {}

		[Constructable]
		public TeaLeaves(int amount) : base( 3976 )
		{
			Weight = 0.1;
			Stackable = true;
			Amount = amount;
			Hue = 0x44;
			Name = "Tea Leaves";
		}

		public TeaLeaves( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}