using System;
using Server.Network;

namespace Server.Items
{
	public class EdibleSun : Food
	{
		[Constructable]
		public EdibleSun() : this( 1 ) { }

		[Constructable]
		public EdibleSun( int amount ) : base( amount, 0xF27 )
		{
			Weight = 0.1;
			Stackable = true;
			FillFactor = 1;
			Hue = 1089;
			Name = "Sunflower Seeds";
		}

		public EdibleSun( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}