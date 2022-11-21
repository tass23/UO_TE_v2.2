using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ShadowAltarSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ShadowAltarSouthAddonDeed();
			}
		}

		[ Constructable ]
		public ShadowAltarSouthAddon()
		{
			AddComponent( new AddonComponent( 13915 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 13914 ), 1, 0, 0 );
			AddonComponent ac;
			ac = new AddonComponent( 13914 );
			AddComponent( ac, 1, 0, 0 );
			ac = new AddonComponent( 13915 );
			AddComponent( ac, 0, 0, 0 );

		}

		public ShadowAltarSouthAddon( Serial serial ) : base( serial )
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

	public class ShadowAltarSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ShadowAltarSouthAddon();
			}
		}

		[Constructable]
		public ShadowAltarSouthAddonDeed()
		{
			Name = "Shadow Altar south";
			Hue = 1109;
			LootType = LootType.Blessed;
		}

		public ShadowAltarSouthAddonDeed( Serial serial ) : base( serial )
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
	
	public class ShadowAltarEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new ShadowAltarEastAddonDeed();
			}
		}

		[ Constructable ]
		public ShadowAltarEastAddon()
		{
			AddComponent( new AddonComponent( 13981 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 13982 ), 0, 1, 0 );
			AddonComponent ac;
			ac = new AddonComponent( 13981 );
			AddComponent( ac, 0, 0, 0 );
			ac = new AddonComponent( 13982 );
			AddComponent( ac, 0, 1, 0 );

		}

		public ShadowAltarEastAddon( Serial serial ) : base( serial )
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

	public class ShadowAltarEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new ShadowAltarEastAddon();
			}
		}

		[Constructable]
		public ShadowAltarEastAddonDeed()
		{
			Name = "Shadow Altar east";
			Hue = 1109;
			LootType = LootType.Blessed;
		}

		public ShadowAltarEastAddonDeed( Serial serial ) : base( serial )
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
