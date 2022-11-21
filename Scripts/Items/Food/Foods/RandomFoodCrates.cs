using System;

namespace Server.Items
{
	public class RandomFoodCrate : Item
	{
		[Constructable]
		public RandomFoodCrate() : base( Utility.RandomList( 0x3BB2, 0x3BB1, 0x3BB0, 0x3BAF, 0x3BAE, 0x3BAD, 0x3BAC, 0x3BAB, 0x3BAA, 0x3BA9, 0x3BA7, 0x3BA6, 0x3BA8 ))
		{
			Name = "Crate of food";
		}

		public RandomFoodCrate( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}