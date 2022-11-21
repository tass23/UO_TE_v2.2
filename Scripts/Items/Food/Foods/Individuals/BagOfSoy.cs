using System;
using Server.Network;

namespace Server.Items
{
	public class BagOfSoy : Item
	{
		[Constructable]
		public BagOfSoy() : base( 0x1045 )
		{
			Weight = 5.0;
			Stackable = true;
			Hue = 0x3D5;
			Name = "Bag of Soy";
		}

		public BagOfSoy( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}