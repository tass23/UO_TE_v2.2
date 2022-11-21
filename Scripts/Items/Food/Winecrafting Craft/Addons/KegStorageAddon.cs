
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class kegstorageAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new kegstorageAddonDeed(); } }

		[ Constructable ]
		public kegstorageAddon()
		{
			AddComponent( new AddonComponent( 23 ), 1, 1, 0 );
			AddComponent( new AddonComponent( 2823 ), 1, 1, 5 );
			AddComponent( new AddonComponent( 7608 ), 1, 1, 8 );
			AddComponent( new AddonComponent( 2823 ), 1, 1, 14 );
			AddComponent( new AddonComponent( 7608 ), 1, 1, 17 );
			AddComponent( new AddonComponent( 23 ), 0, 1, 0 );
			AddComponent( new AddonComponent( 8654 ), 0, 1, 9 );
			AddComponent( new AddonComponent( 23 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 8654 ), 0, 0, 9 );
			AddComponent( new AddonComponent( 23 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 2823 ), 1, 0, 14 );
			AddComponent( new AddonComponent( 7608 ), 1, 0, 17 );
			AddComponent( new AddonComponent( 7608 ), 1, 0, 8 );
			AddComponent( new AddonComponent( 2823 ), 1, 0, 5 );
			AddComponent( new AddonComponent( 23 ), 1, 2, 0 );
			AddComponent( new AddonComponent( 2822 ), 1, 2, 5 );
			AddComponent( new AddonComponent( 7608 ), 1, 2, 8 );
			AddComponent( new AddonComponent( 2822 ), 1, 2, 14 );
			AddComponent( new AddonComponent( 7608 ), 1, 2, 17 );
			AddComponent( new AddonComponent( 23 ), 0, 2, 0 );
			AddComponent( new AddonComponent( 23 ), 0, 2, 5 );
			AddComponent( new AddonComponent( 23 ), 0, 2, 10 );
			AddComponent( new AddonComponent( 23 ), 1, -1, 0 );
			AddComponent( new AddonComponent( 2824 ), 1, -1, 5 );
			AddComponent( new AddonComponent( 7608 ), 1, -1, 8 );
			AddComponent( new AddonComponent( 2824 ), 1, -1, 14 );
			AddComponent( new AddonComponent( 7608 ), 1, -1, 17 );
			AddComponent( new AddonComponent( 23 ), 0, -1, 0 );
			AddComponent( new AddonComponent( 8654 ), 0, -1, 9 );
			AddComponent( new AddonComponent( 23 ), 0, -2, 0 );
			AddComponent( new AddonComponent( 23 ), 1, -2, 0 );
			AddComponent( new AddonComponent( 23 ), 1, -2, 5 );
			AddComponent( new AddonComponent( 23 ), 1, -2, 10 );
			AddonComponent ac = null;
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, -1, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, -2, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, -1, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, -2, 0 );
			ac = new AddonComponent( 2824 );
			AddComponent( ac, 1, -1, 5 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, -1, 8 );
			ac = new AddonComponent( 2823 );
			AddComponent( ac, 1, 0, 14 );
			ac = new AddonComponent( 2824 );
			AddComponent( ac, 1, -1, 14 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 0, 17 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, -1, 17 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 0, 8 );
			ac = new AddonComponent( 2823 );
			AddComponent( ac, 1, 0, 5 );
			ac = new AddonComponent( 8654 );
			AddComponent( ac, 0, 0, 9 );
			ac = new AddonComponent( 8654 );
			AddComponent( ac, 0, -1, 9 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, -2, 5 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, -2, 10 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, 2, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, 1, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 1, 1, 0 );
			ac = new AddonComponent( 8654 );
			AddComponent( ac, 0, 1, 9 );
			ac = new AddonComponent( 2822 );
			AddComponent( ac, 1, 2, 5 );
			ac = new AddonComponent( 2823 );
			AddComponent( ac, 1, 1, 5 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 2, 8 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 1, 8 );
			ac = new AddonComponent( 2823 );
			AddComponent( ac, 1, 1, 14 );
			ac = new AddonComponent( 2822 );
			AddComponent( ac, 1, 2, 14 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 1, 17 );
			ac = new AddonComponent( 7608 );
			AddComponent( ac, 1, 2, 17 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, 2, 0 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, 2, 5 );
			ac = new AddonComponent( 23 );
			AddComponent( ac, 0, 2, 10 );

		}

		public kegstorageAddon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class kegstorageAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new kegstorageAddon(); } }

		[Constructable]
		public kegstorageAddonDeed()
		{
			Name = "Keg Storage";
		}

		public kegstorageAddonDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }

		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}