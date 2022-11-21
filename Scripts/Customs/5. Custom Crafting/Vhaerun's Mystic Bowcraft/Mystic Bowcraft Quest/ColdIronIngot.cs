using System;

namespace Server.Items
{
	public class ColdIronIngot : Item
	{
		[Constructable]
		public ColdIronIngot() : this( 1 )
		{
		}

		[Constructable]
		public ColdIronIngot( int amount ) : base( 0x1BF5 )
		{
			Stackable = true;
			Amount = amount;
		      	Weight = 2.0;
			Name = "cold iron ingot";
            	}

		public ColdIronIngot( Serial serial ) : base( serial )
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