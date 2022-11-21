using System;
using Server;

namespace Server.Items
{
	public class FishTank : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new FishTankDeed(); } }

		[Constructable]
		public FishTank()
		{
 
		 AddComponent( new AddonComponent( 498 ), 0, 1, 0 );
		 AddComponent( new AddonComponent( 498 ), 5, 1, 0 );

		 AddComponent( new AddonComponent( 7846 ), 6, 2, 10 );
		 
		 AddComponent( new AddonComponent( 498 ), 0, 0, 0 );
		 AddComponent( new AddonComponent( 498 ), 5, 0, 0 );
	
		 AddComponent( new AddonComponent( 496 ), 0, 2, 0 );
		 AddComponent( new AddonComponent( 496 ), 5, 2, 0 );
		 
		 AddComponent( new AddonComponent( 500 ), 4, 2, 0 );
		 
		 AddComponent( new AddonComponent( 183 ), 0, 1, 0 );
		 AddComponent( new AddonComponent( 183 ), 1, 1, 0 );
		 AddComponent( new AddonComponent( 183 ), 2, 1, 0 );
		 AddComponent( new AddonComponent( 183 ), 3, 1, 0 );
		 AddComponent( new AddonComponent( 183 ), 4, 1, 0 );

		 AddComponent( new AddonComponent( 3530 ), 0, 1, 5 );
		 AddComponent( new AddonComponent( 3530 ), 1, 1, 5 );
		 AddComponent( new AddonComponent( 3530 ), 2, 1, 5 );
		 AddComponent( new AddonComponent( 3530 ), 3, 1, 5 );
		 AddComponent( new AddonComponent( 3530 ), 4, 1, 5 );

		 AddComponent( new AddonComponent( 0x3515 ), 0, 0, 5 );
		 AddComponent( new AddonComponent( 0x3515 ), 1, 0, 5 );
		 AddComponent( new AddonComponent( 0x3515 ), 2, 0, 5 );
		 AddComponent( new AddonComponent( 5363 ), 2, 1, 15 );
		 AddComponent( new AddonComponent( 0x3515 ), 3, 0, 5 );
		 AddComponent( new AddonComponent( 0x3515 ), 4, 0, 5 );

		 AddComponent( new AddonComponent( 6370 ), 1, 1, 6 );
		 
		 
		 AddComponent( new AddonComponent( 4038 ), 1, 1, 6 );
		 AddComponent( new AddonComponent( 6816 ), 0, 1, 0 );
		 AddComponent( new AddonComponent( 6814 ), 4, 1, 0 );
		 AddComponent( new AddonComponent( 6881 ), 2, 1, 6 );
		 AddComponent( new AddonComponent( 6928 ), 2, 1, 6 );
		 
		 AddComponent( new AddonComponent( 3544 ), 3, 1, 8 );
		 AddComponent( new AddonComponent( 3545 ), 1, 1, 8 );
		 AddComponent( new AddonComponent( 5367 ), 1, 2, 0 );
		 
		 AddComponent( new AddonComponent( 5370 ), 3, 2, 0 );
		 
		 
		 
		 AddComponent( new AddonComponent( 3167 ), 0, 0, 20 );
		 AddComponent( new AddonComponent( 3166 ), 1, 0, 20 );
		 AddComponent( new AddonComponent( 3167 ), 2, 0, 20 );
		 AddComponent( new AddonComponent( 3168 ), 3, 0, 20 );
		 AddComponent( new AddonComponent( 3167 ), 4, 0, 20 );
		 		}

		public FishTank( Serial serial ) : base( serial )
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

	public class FishTankDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new FishTank(); } }

		[Constructable]
		public FishTankDeed()
		{
	 Name = "Fish Tank";
		}

		public FishTankDeed( Serial serial ) : base( serial )
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