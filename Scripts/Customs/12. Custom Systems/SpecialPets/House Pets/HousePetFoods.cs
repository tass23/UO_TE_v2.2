using System;
using Server;

namespace Server.Items
{
	public class PremiumDogFood : Item
	{		
		[Constructable]
		public PremiumDogFood() : base( 0x2FD6 )
		{
			Name = "Premium Dog Biscuits";
			Hue = 2967;
			Stackable = true;
		}
	
		public PremiumDogFood( Serial serial ) : base( serial )
		{
		}
		
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

	public class PremiumParrotFood : Item
	{		
		[Constructable]
		public PremiumParrotFood() : base( 0x2FD6 )
		{
			Name = "Premium Parrot Wafers";
			Hue = 0x38;
			Stackable = true;
		}
	
		public PremiumParrotFood( Serial serial ) : base( serial )
		{
		}
		
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