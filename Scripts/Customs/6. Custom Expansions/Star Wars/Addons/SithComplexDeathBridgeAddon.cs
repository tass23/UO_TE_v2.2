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
	public class SithComplexDeathBridgeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SithComplexDeathBridgeAddonDeed();
			}
		}

		[ Constructable ]
		public SithComplexDeathBridgeAddon()
		{
			AddComplexComponent( (BaseAddon) this, 2327, 4, -1, 0, 2051, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 2327, 1, 1, 0, 2051, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 2327, -2, -1, 0, 2051, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 2327, 4, 1, 0, 2051, -1, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 2327, 2, -1, 0, 2051, -1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 2327, -1, -1, 0, 2051, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 2327, -1, 1, 0, 2051, -1, "", 1);// 7
			AddComplexComponent( (BaseAddon) this, 2327, 0, 1, 0, 2051, -1, "", 1);// 8
			AddComplexComponent( (BaseAddon) this, 2327, -3, -1, 0, 2051, -1, "", 1);// 9
			AddComplexComponent( (BaseAddon) this, 2327, 3, -1, 0, 2051, -1, "", 1);// 10
			AddComplexComponent( (BaseAddon) this, 2327, 3, 1, 0, 2051, -1, "", 1);// 11
			AddComplexComponent( (BaseAddon) this, 2327, 0, -1, 0, 2051, -1, "", 1);// 12
			AddComplexComponent( (BaseAddon) this, 2327, -2, 1, 0, 2051, -1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 2327, 2, 1, 0, 2051, -1, "", 1);// 14
			AddComplexComponent( (BaseAddon) this, 2327, 1, -1, 0, 2051, -1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 2327, -3, 1, 0, 2051, -1, "", 1);// 16
			AddComplexComponent( (BaseAddon) this, 2328, -3, 0, 0, 1478, -1, "", 1);// 17
			AddComplexComponent( (BaseAddon) this, 2328, -2, 0, 0, 1478, -1, "", 1);// 18
			AddComplexComponent( (BaseAddon) this, 2328, -1, 0, 0, 1478, -1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 2328, 0, 0, 0, 1478, -1, "", 1);// 20
			AddComplexComponent( (BaseAddon) this, 2328, 1, 0, 0, 1478, -1, "", 1);// 21
			AddComplexComponent( (BaseAddon) this, 2328, 2, 0, 0, 1478, -1, "", 1);// 22
			AddComplexComponent( (BaseAddon) this, 2328, 3, 0, 0, 1478, -1, "", 1);// 23
			AddComplexComponent( (BaseAddon) this, 2328, 4, 0, 0, 1478, -1, "", 1);// 24
		}

		public SithComplexDeathBridgeAddon( Serial serial ) : base( serial )
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

	public class SithComplexDeathBridgeAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SithComplexDeathBridgeAddon();
			}
		}

		[Constructable]
		public SithComplexDeathBridgeAddonDeed()
		{
			Name = "Sith Complex Death Bridge";
		}

		public SithComplexDeathBridgeAddonDeed( Serial serial ) : base( serial )
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