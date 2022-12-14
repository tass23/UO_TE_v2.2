
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
	public class NecrocityMoonGateNEWAddon : BaseAddon
	{
         
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new NecrocityMoonGateNEWAddonDeed();
			}
		}

		[ Constructable ]
		public NecrocityMoonGateNEWAddon()
		{



			AddComplexComponent( (BaseAddon) this, 1811, 2, 1, 0, 1108, -1, "", 1);// 1
			AddComplexComponent( (BaseAddon) this, 1801, -1, -1, 0, 1108, -1, "", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1801, -1, 0, 0, 1108, -1, "", 1);// 3
			AddComplexComponent( (BaseAddon) this, 1801, -1, 1, 0, 1108, -1, "", 1);// 4
			AddComplexComponent( (BaseAddon) this, 1801, 0, -1, 0, 1108, -1, "", 1);// 5
			AddComplexComponent( (BaseAddon) this, 1801, 0, 0, 0, 1108, -1, "", 1);// 6
			AddComplexComponent( (BaseAddon) this, 1801, 0, 1, 0, 1108, -1, "", 1);// 7
			AddComplexComponent( (BaseAddon) this, 1801, 1, -1, 0, 1108, -1, "", 1);// 8
			AddComplexComponent( (BaseAddon) this, 1801, 1, 0, 0, 1108, -1, "", 1);// 9
			AddComplexComponent( (BaseAddon) this, 1801, 1, 1, 0, 1108, -1, "", 1);// 10
			AddComplexComponent( (BaseAddon) this, 1811, 1, 2, 0, 1108, -1, "", 1);// 11
			AddComplexComponent( (BaseAddon) this, 1812, -1, 2, 0, 1108, -1, "", 1);// 12
			AddComplexComponent( (BaseAddon) this, 1810, -2, -1, 0, 1108, -1, "", 1);// 13
			AddComplexComponent( (BaseAddon) this, 1812, -2, 1, 0, 1108, -1, "", 1);// 14
			AddComplexComponent( (BaseAddon) this, 1813, 1, -2, 0, 1108, -1, "", 1);// 15
			AddComplexComponent( (BaseAddon) this, 4070, -1, 0, 5, 1108, -1, "", 1);// 16
			AddComplexComponent( (BaseAddon) this, 4071, -1, -1, 5, 1108, -1, "", 1);// 17
			AddComplexComponent( (BaseAddon) this, 4072, 0, -1, 5, 1108, -1, "", 1);// 18
			AddComplexComponent( (BaseAddon) this, 4073, -1, 1, 5, 1108, -1, "", 1);// 19
			AddComplexComponent( (BaseAddon) this, 4074, 0, 0, 5, 1108, -1, "", 1);// 20
			AddComplexComponent( (BaseAddon) this, 4075, 1, -1, 5, 1108, -1, "", 1);// 21
			AddComplexComponent( (BaseAddon) this, 4076, 0, 1, 5, 1108, -1, "", 1);// 22
			AddComplexComponent( (BaseAddon) this, 4077, 1, 1, 5, 1108, -1, "", 1);// 23
			AddComplexComponent( (BaseAddon) this, 4078, 1, 0, 5, 1108, -1, "", 1);// 24
			AddComplexComponent( (BaseAddon) this, 1810, -1, -2, 0, 1107, -1, "", 1);// 25
			AddComplexComponent( (BaseAddon) this, 1813, 2, -1, 0, 1108, -1, "", 1);// 26
			AddComplexComponent( (BaseAddon) this, 13834, 2, 0, 0, 1102, -1, "", 1);// 27
			AddComplexComponent( (BaseAddon) this, 13837, 0, -2, 0, 1102, -1, "", 1);// 28
			AddComplexComponent( (BaseAddon) this, 13836, -2, 0, 0, 1102, -1, "", 1);// 29
			AddComplexComponent( (BaseAddon) this, 13835, 0, 2, 0, 1102, -1, "", 1);// 30

		}

		public NecrocityMoonGateNEWAddon( Serial serial ) : base( serial )
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

	public class NecrocityMoonGateNEWAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new NecrocityMoonGateNEWAddon();
			}
		}

		[Constructable]
		public NecrocityMoonGateNEWAddonDeed()
		{
			Name = "NecrocityMoonGateNEW";
		}

		public NecrocityMoonGateNEWAddonDeed( Serial serial ) : base( serial )
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