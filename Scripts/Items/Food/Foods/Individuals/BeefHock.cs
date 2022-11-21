using System;
using Server.Network;
using Server.Targeting;
namespace Server.Items
{
	public class BeefHock : Food
	{
		[Constructable]
		public BeefHock() : this( 1 ) { }
		[Constructable]
		public BeefHock( int amount ) : base( amount, 0x9D3 )
		{
			this.Stackable = true;
			this.Weight = 1.0;
			this.Amount = amount;
			this.Name = "Beef Hock";
			this.Hue = 0x459;
		}
		public BeefHock( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}