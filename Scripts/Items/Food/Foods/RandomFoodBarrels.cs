using System;

namespace Server.Items
{
	public class RandomFoodBarrel : Item
	{
		[Constructable]
		public RandomFoodBarrel() : base( Utility.RandomList( 0x3B87, 0x3B88, 0x3B89, 0x3B8A, 0x3B8B, 0x3B8C, 0x3B8D, 0x3B8E, 0x3B8F, 0x3B90 ))
		{
			Name = "Barrel of food";
		}

		public RandomFoodBarrel( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}