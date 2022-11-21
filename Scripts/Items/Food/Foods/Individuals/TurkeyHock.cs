using System;
using Server.Network;
using Server.Targeting;
namespace Server.Items
{
	public class TurkeyHock : Food
	{
		[Constructable]
		public TurkeyHock() : this( 1 ) { }
		[Constructable]
		public TurkeyHock( int amount ) : base( amount, 0x9D3 )
		{
			this.Stackable = true;
			this.Weight = 2.0;
			this.Amount = amount;
			this.Name = "Turkey Hock";
			this.Hue = 0x457;
		}
		public TurkeyHock( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}