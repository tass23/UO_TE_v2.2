
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class bottlerackAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new bottlerackAddonDeed(); } }

		[ Constructable ]
		public bottlerackAddon()
		{
			AddComponent( new AddonComponent( 2182 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 2182 ), 0, 2, 12 );
			AddComponent( new AddonComponent( 2182 ), 0, 2, 6 );
			AddComponent( new AddonComponent( 2182 ), 0, 1, 12 );
			AddComponent( new AddonComponent( 2182 ), 0, 1, 6 );
			AddComponent( new AddonComponent( 2182 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 2182 ), 0, 0, 12 );
			AddComponent( new AddonComponent( 2182 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 2182 ), 0, 0, 6 );
			AddComponent( new AddonComponent( 2182 ), 0, -1, 12 );
			AddComponent( new AddonComponent( 2182 ), 0, -1, 6 );
			AddComponent( new AddonComponent( 2182 ), 0, -1, 0 );
			AddonComponent ac = null;
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 1, 12 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 0, 12 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, -1, 12 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 1, 6 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 2, 12 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, -1, 6 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 0, 6 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, 2, 6 );
			ac = new AddonComponent( 2182 );
			AddComponent( ac, 0, -1, 0 );

		}

		public bottlerackAddon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class bottlerackAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new bottlerackAddon(); } }

		[Constructable]
		public bottlerackAddonDeed()
		{
			Name = "Bottle Rack";
		}

		public bottlerackAddonDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}