using System;

namespace Server.Items
{
	public class RandomFoodBasket : Item
	{
		[Constructable]
		public RandomFoodBasket() : base( Utility.RandomList( 0x3BC8, 0x3B91, 0x3B92, 0x3B93, 0x3B94, 0x3B95, 0x3B96, 0x3B97, 0x3B98, 0x3B99,
						0x3B9A, 0x3B9B, 0x3B9C, 0x3B9D, 0x3B9E, 0x3B9F, 0x3BA0, 0x3BA1, 0x3BA2, 0x3BA3, 0x3BA4, 0x3BA5 ))
		{
			Name = "Basket of food";
		}

		public RandomFoodBasket( Serial serial ) : base( serial ) { }
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}