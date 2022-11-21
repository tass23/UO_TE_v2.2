using System;
using Server;

namespace Server.Items
{
	public class UmbrellaTableAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new UmbrellaTableDeed(); } }

		[Constructable]
		public UmbrellaTableAddon()
		{
			AddComponent( new AddonComponent( 4567 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 4566 ), 0, 1, 0 );
			
			AddComponent( new AddonComponent( 441 ), 0, 0, 0 );
			
			AddComponent( new AddonComponent( 4567 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 4566 ), 1, 1, 0 );
			
			AddComponent( new AddonComponent( 1414 ), 0, 2, 21 );
			AddComponent( new AddonComponent( 1418 ), 0, 0, 25 );
			AddComponent( new AddonComponent( 1414 ), 0, 1, 25 );
			AddComponent( new AddonComponent( 1414 ), 1, 1, 21 );
			AddComponent( new AddonComponent( 1415 ), 2, 0, 21 );
			AddComponent( new AddonComponent( 1415 ), 2, 1, 21 );
			AddComponent( new AddonComponent( 1492 ), 2, 2, 25 );
			AddComponent( new AddonComponent( 1497 ), 1, 2, 25 );
			AddComponent( new AddonComponent( 1415 ), 1, 0, 25 );
			AddComponent( new AddonComponent( 1414 ), 1, 1, 25 );
		}

		public UmbrellaTableAddon( Serial serial ) : base( serial )
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

	public class UmbrellaTableDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new UmbrellaTableAddon(); } }

		[Constructable]
		public UmbrellaTableDeed()
		{
		Name = "Umbrella Table";
		}

		public UmbrellaTableDeed( Serial serial ) : base( serial )
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