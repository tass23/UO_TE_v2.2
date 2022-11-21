using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShadowBannerEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ShadowBannerEastAddonDeed();
			}
		}

		[ Constructable ]
		public ShadowBannerEastAddon()
		{
			AddComponent( new AddonComponent( 13918 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 13919 ), 0, 1, 0 );

		}

		public ShadowBannerEastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class ShadowBannerEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ShadowBannerEastAddon();
			}
		}

		[Constructable]
		public ShadowBannerEastAddonDeed()
		{
			Name = "Shadow Banner East";
			Hue = 1109;
			LootType = LootType.Blessed;
		}

		public ShadowBannerEastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
	
	public class ShadowBannerSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ShadowBannerSouthAddonDeed();
			}
		}

		[ Constructable ]
		public ShadowBannerSouthAddon()
		{
			AddComponent( new AddonComponent( 13916 ), 1, 0, 0 );
			AddComponent( new AddonComponent( 13917 ), 0, 0, 0 );

		}

		public ShadowBannerSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class ShadowBannerSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ShadowBannerSouthAddon();
			}
		}

		[Constructable]
		public ShadowBannerSouthAddonDeed()
		{
			Name = "Shadow Banner South";
			Hue = 1109;
			LootType = LootType.Blessed;
		}

		public ShadowBannerSouthAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
