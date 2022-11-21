using System;
using Server;
using Server.Items;

namespace Server.Items
{
	[Server.Engines.Craft.LightsaberForge]
	public class LightsaberForgeEast : BaseAddon
	{ 
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LightsaberForgeEastDeed();
			}
		}

		[ Constructable ]
		public LightsaberForgeEast()
		{
			AddComplexComponent( (BaseAddon) this, 8704, 0, 1, 1, 1173, -1, "Lightsaber Forge", 1);// 1
			AddComplexComponent( (BaseAddon) this, 10989, 0, 1, 0, 1081, 1, "Lightsaber Forge", 1);// 2
			AddComplexComponent( (BaseAddon) this, 12563, 0, 2, 1, 1173, -1, "Lightsaber Forge", 1);// 3
			AddComplexComponent( (BaseAddon) this, 12560, 0, -1, 1, 1173, -1, "Lightsaber Forge", 1);// 4
			AddComplexComponent( (BaseAddon) this, 12566, 0, 0, 1, 1173, -1, "Lightsaber Forge", 1);// 5
			AddComplexComponent( (BaseAddon) this, 17607, 0, 1, 21, 1173, -1, "Lightsaber Forge", 1);// 6
			AddComplexComponent( (BaseAddon) this, 17987, 0, 2, 15, 1173, -1, "Lightsaber Forge", 1);// 7
			AddComplexComponent( (BaseAddon) this, 17987, 0, 0, 15, 1173, -1, "Lightsaber Forge", 1);// 8
			AddComplexComponent( (BaseAddon) this, 20316, 1, 1, 1, 1182, -1, "Lightsaber Forge", 1);// 9
		}

		public LightsaberForgeEast( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;

            if (hue != 0)
                ac.Hue = hue;

            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }

            if (lightsource != -1)
                ac.Light = (LightType) lightsource;

            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class LightsaberForgeEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LightsaberForgeEast();
			}
		}

		[Constructable]
		public LightsaberForgeEastDeed()
		{
			Name = "Lightsaber Forge (East)";
		}

		public LightsaberForgeEastDeed( Serial serial ) : base( serial )
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
	
	public class LightsaberForgeSouth : BaseAddon
	{
         
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new LightsaberForgeSouthDeed();
			}
		}

		[ Constructable ]
		public LightsaberForgeSouth()
		{
			AddComplexComponent( (BaseAddon) this, 8703, 1, 0, 1, 1173, -1, "Lightsaber Forge", 1);// 1
			AddComplexComponent( (BaseAddon) this, 10973, 1, 0, 0, 1081, 1, "Lightsaber Forge", 1);// 2
			AddComplexComponent( (BaseAddon) this, 12566, -1, 0, 1, 1173, -1, "Lightsaber Forge", 1);// 3
			AddComplexComponent( (BaseAddon) this, 12560, 0, 0, 1, 1173, -1, "Lightsaber Forge", 1);// 4
			AddComplexComponent( (BaseAddon) this, 17607, 1, 0, 21, 1173, -1, "Lightsaber Forge", 1);// 5
			AddComplexComponent( (BaseAddon) this, 17987, 0, 0, 15, 1173, -1, "Lightsaber Forge", 1);// 6
			AddComplexComponent( (BaseAddon) this, 20319, 1, 1, 1, 1182, -1, "Lightsaber Forge", 1);// 7
			AddComplexComponent( (BaseAddon) this, 12563, 2, 0, 1, 1173, -1, "Lightsaber Forge", 1);// 8
			AddComplexComponent( (BaseAddon) this, 17987, 2, 0, 15, 1173, -1, "Lightsaber Forge", 1);// 9
		}

		public LightsaberForgeSouth( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;

            if (hue != 0)
                ac.Hue = hue;

            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }

            if (lightsource != -1)
                ac.Light = (LightType) lightsource;

            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class LightsaberForgeSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new LightsaberForgeSouth();
			}
		}

		[Constructable]
		public LightsaberForgeSouthDeed()
		{
			Name = "Lightsaber Forge (South)";
		}

		public LightsaberForgeSouthDeed( Serial serial ) : base( serial )
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