////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HoverBikeNorth : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new HoverBikeNorthDeed();
			}
		}

		[ Constructable ]
		public HoverBikeNorth()
		{
			AddComplexComponent( (BaseAddon) this, 11750, 0, 0, 0, 1762, -1, "Hoverbike", 1);// 1
			AddComplexComponent( (BaseAddon) this, 1899, -1, 1, 0, 1761, -1, "Hoverbike", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1900, 0, 1, 0, 1761, -1, "Hoverbike", 1);// 3
			AddComplexComponent( (BaseAddon) this, 1898, 1, 1, 0, 1761, -1, "Hoverbike", 1);// 4
			AddComplexComponent( (BaseAddon) this, 1897, 0, 2, 0, 1761, -1, "Hoverbike", 1);// 5
			AddComplexComponent( (BaseAddon) this, 1909, -1, -1, 0, 1761, -1, "Hoverbike", 1);// 6
			AddComplexComponent( (BaseAddon) this, 1912, 1, -1, 0, 1761, -1, "Hoverbike", 1);// 7
			AddComplexComponent( (BaseAddon) this, 1290, 0, 0, 0, 1762, -1, "Hoverbike", 1);// 8
			AddComplexComponent( (BaseAddon) this, 2173, 0, -1, 0, 2117, -1, "Hoverbike", 1);// 9
			AddComplexComponent( (BaseAddon) this, 9440, 1, 1, 5, 1761, -1, "Hoverbike", 1);// 10
			AddComplexComponent( (BaseAddon) this, 9440, -1, 1, 5, 1761, -1, "Hoverbike", 1);// 11
			AddComplexComponent( (BaseAddon) this, 3629, 0, 1, 5, 1761, -1, "Hoverbike", 1);// 12
			AddComplexComponent( (BaseAddon) this, 7882, 0, 2, 10, 1762, -1, "Hoverbike", 1);// 13
			AddComplexComponent( (BaseAddon) this, 7885, 0, 2, 5, 1479, 1, "Hoverbike", 1);// 14
		}

		public HoverBikeNorth( Serial serial ) : base( serial )
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

	public class HoverBikeNorthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new HoverBikeNorth();
			}
		}

		[Constructable]
		public HoverBikeNorthDeed()
		{
			Name = "Hover Bike(North)";
		}

		public HoverBikeNorthDeed( Serial serial ) : base( serial )
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