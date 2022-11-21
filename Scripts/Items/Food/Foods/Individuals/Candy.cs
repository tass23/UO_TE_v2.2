
using System;
using Server.Network;
namespace Server.Items{
	[FlipableAttribute( 0x9B5, 0x9B5 )]
	public class Candy : Food
	{
		[Constructable]
		public Candy() : this( 1 ) { }
		[Constructable]
		public Candy( int amount ) : base( amount, 0x9B5 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
			this.Hue = 24;
			this.Name = "Candy";
		}
		public Candy( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }

	}
}