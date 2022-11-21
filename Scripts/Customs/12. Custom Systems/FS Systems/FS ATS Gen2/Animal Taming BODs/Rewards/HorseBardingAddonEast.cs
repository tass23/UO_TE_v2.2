using System;
using Server;

namespace Server.Items
{
	public class BardedHorseEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BardedHorseEastDeed(); } }

		[Constructable]
		public BardedHorseEastAddon()
		{
			AddComponent( new AddonComponent( 0x1379 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x1378 ), 0, 1, 0 );
		}

		public BardedHorseEastAddon( Serial serial ) : base( serial )
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

	public class BardedHorseEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BardedHorseEastAddon(); } }

		[Constructable]
		public BardedHorseEastDeed()
		{
			Name = "horse barding addon deed [east]";
		}

		public BardedHorseEastDeed( Serial serial ) : base( serial )
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