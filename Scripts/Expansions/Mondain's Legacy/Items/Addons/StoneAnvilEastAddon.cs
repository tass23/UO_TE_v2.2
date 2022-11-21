using System;
using Server;

namespace Server.Items
{
	public class StoneAnvilEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new StoneAnvilEastDeed(); } }
		public override bool RetainDeedHue{ get{ return true; } }

		[Constructable]
		public StoneAnvilEastAddon()
		{
			AddComponent( new AnvilComponent( 0x2DD6 ), 0, 0, 0 );
		}

		public StoneAnvilEastAddon( Serial serial ) : base( serial )
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

	public class StoneAnvilEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new StoneAnvilEastAddon(); } }
		public override int LabelNumber{ get{ return 1073392; } } // stone anvil (east)

		[Constructable]
		public StoneAnvilEastDeed()
		{
		}

		public StoneAnvilEastDeed( Serial serial ) : base( serial )
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