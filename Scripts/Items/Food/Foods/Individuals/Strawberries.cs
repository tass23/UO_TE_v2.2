using System;
using Server.Network;
namespace Server.Items
{
	[Flipable( 0xf2a, 0xf2b )]
	public class Strawberries : Food
	{
		[Constructable]
		public Strawberries() : this( 1 ) { }
		[Constructable]
		public Strawberries( int amount ) : base( amount, 0xf2a )
		{
			this.Name = "strawberries";
			this.Hue = 33;
			this.Weight = 1.0;
			this.FillFactor = 1;
		}
		public Strawberries( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}