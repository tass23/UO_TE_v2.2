
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
	public class CTFbase2Addon : BaseAddon
	{
         
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CTFbase2AddonDeed();
			}
		}

		[ Constructable ]
		public CTFbase2Addon()
		{



			AddComplexComponent( (BaseAddon) this, 1891, 1, -1, 0, 37, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 1889, -1, -1, 0, 37, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1890, 1, 1, 0, 37, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 1892, -1, 1, 0, 37, -1, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 1884, 1, -2, 0, 37, -1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 1882, 1, 2, 0, 37, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 1881, -1, -2, 0, 37, -1, "", 1);// 7
			AddComplexComponent( (BaseAddon) this, 1883, -1, 2, 0, 37, -1, "", 1);// 8
			AddComplexComponent( (BaseAddon) this, 1881, -2, -1, 0, 37, -1, "", 1);// 9
			AddComplexComponent( (BaseAddon) this, 1883, -2, 1, 0, 37, -1, "", 1);// 10
			AddComplexComponent( (BaseAddon) this, 1179, 1, 0, 0, 235, -1, "", 1);// 11
			AddComplexComponent( (BaseAddon) this, 1179, -1, 0, 0, 235, -1, "", 1);// 12
			AddComplexComponent( (BaseAddon) this, 1179, -2, 0, 0, 235, -1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 1179, 0, 2, 0, 235, -1, "", 1);// 14
			AddComplexComponent( (BaseAddon) this, 1179, 0, 1, 0, 235, -1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 1179, 0, -1, 0, 235, -1, "", 1);// 16
			AddComplexComponent( (BaseAddon) this, 1179, 0, -2, 0, 235, -1, "", 1);// 17
			AddComplexComponent( (BaseAddon) this, 6099, 0, 0, 0, 235, -1, "", 1);// 18
			AddComplexComponent( (BaseAddon) this, 1882, 2, 1, 0, 37, -1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 1884, 2, -1, 0, 37, -1, "", 1);// 20
			AddComplexComponent( (BaseAddon) this, 1179, 2, 0, 0, 235, -1, "", 1);// 21

		}

		public CTFbase2Addon( Serial serial ) : base( serial )
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

	public class CTFbase2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CTFbase2Addon();
			}
		}

		[Constructable]
		public CTFbase2AddonDeed()
		{
			Name = "CTFbase2";
		}

		public CTFbase2AddonDeed( Serial serial ) : base( serial )
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