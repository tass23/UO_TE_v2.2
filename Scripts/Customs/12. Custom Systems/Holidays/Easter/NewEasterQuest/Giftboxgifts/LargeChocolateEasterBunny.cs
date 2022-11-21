using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class LargeChocolateEasterBunny : Food
	{

		[Constructable]
		public  LargeChocolateEasterBunny() : this( 1 )
		{
		}
		
		[Constructable]
		public  LargeChocolateEasterBunny( int amount ) : base( 0xF8F, amount )
		{
			this.Hue = 1854;
			this.Name = "Large Chocolate Easter Bunny";
			this.Movable = true;
			this.Stackable = true;
			this.ItemID = 9762;
			this.Amount = amount;	
			this.Weight = 1;
			this.FillFactor = 2;
		}

		public LargeChocolateEasterBunny( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new LargeChocolateEasterBunny( amount ), amount );
		//}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}