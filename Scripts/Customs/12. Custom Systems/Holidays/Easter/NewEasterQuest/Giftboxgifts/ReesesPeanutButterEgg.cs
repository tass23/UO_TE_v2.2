using System;
using System.Collections;
using Server;
using Server.Network;

namespace Server.Items
{
	public class ReesesPeanutButterEgg : Food
	{

		[Constructable]
		public  ReesesPeanutButterEgg() : this( 1 )
		{
		}
		
		[Constructable]
		public  ReesesPeanutButterEgg( int amount ) : base( 0xF8F, amount )
		{
			this.Hue = 1718;
			this.Name = "Reese's Peanut Butter Egg";
			this.Movable = true;
			this.Stackable = true;
			this.ItemID = 5928;
			this.Amount = amount;	
			this.Weight = 1;
			this.FillFactor = 2;
		}

		public ReesesPeanutButterEgg( Serial serial ) : base( serial )
		{
		}

		//public override Item Dupe( int amount )
		//{
			//return base.Dupe( new ReesesPeanutButterEgg( amount ), amount );
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