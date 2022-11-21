using System;
using Server.Network;

namespace Server.Items
{
	public class BagOfOats : Item
	{
		[Constructable]
		public BagOfOats() : base( 0x1045 )
		{
			Weight = 5.0;
			Stackable = true;
			Hue = 0x45E;
			Name = "Bag of Oats";
		}

		public BagOfOats( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}