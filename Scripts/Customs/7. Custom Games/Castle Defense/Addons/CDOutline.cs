using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CDOutline : BaseAddon
	{    
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CDOutlineDeed();
			}
		}

		[ Constructable ]
		public CDOutline()
		{
			AddComplexComponent( (BaseAddon) this, 2755, -2, -2, 0, 32, -1, "Defense Weaponry", 1);// 1
			AddComplexComponent( (BaseAddon) this, 2806, -2, -1, 0, 32, -1, "Defense Weaponry", 1);// 2
			AddComplexComponent( (BaseAddon) this, 2806, -2, 0, 0, 32, -1, "Defense Weaponry", 1);// 3
			AddComplexComponent( (BaseAddon) this, 2806, -2, 1, 0, 32, -1, "Defense Weaponry", 1);// 4
			AddComplexComponent( (BaseAddon) this, 2807, -1, -2, 0, 32, -1, "Defense Weaponry", 1);// 5
			AddComplexComponent( (BaseAddon) this, 2807, 0, -2, 0, 32, -1, "Defense Weaponry", 1);// 6
			AddComplexComponent( (BaseAddon) this, 2756, -2, 2, 0, 32, -1, "Defense Weaponry", 1);// 7
			AddComplexComponent( (BaseAddon) this, 2809, -1, 2, 0, 32, -1, "Defense Weaponry", 1);// 8
			AddComplexComponent( (BaseAddon) this, 2809, 0, 2, 0, 32, -1, "Defense Weaponry", 1);// 9
			AddComplexComponent( (BaseAddon) this, 2757, 2, -2, 0, 32, -1, "Defense Weaponry", 1);// 10
			AddComplexComponent( (BaseAddon) this, 2807, 1, -2, 0, 32, -1, "Defense Weaponry", 1);// 11
			AddComplexComponent( (BaseAddon) this, 2808, 2, -1, 0, 32, -1, "Defense Weaponry", 1);// 12
			AddComplexComponent( (BaseAddon) this, 2808, 2, 0, 0, 32, -1, "Defense Weaponry", 1);// 13
			AddComplexComponent( (BaseAddon) this, 2808, 2, 1, 0, 32, -1, "Defense Weaponry", 1);// 14
			AddComplexComponent( (BaseAddon) this, 2754, 2, 2, 0, 32, -1, "Defense Weaponry", 1);// 15
			AddComplexComponent( (BaseAddon) this, 2809, 1, 2, 0, 32, -1, "Defense Weaponry", 1);// 16
		}
		public CDOutline( Serial serial ) : base( serial )
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
	public class CDOutlineDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CDOutline();
			}
		}
		[Constructable]
		public CDOutlineDeed()
		{
			Name = "CDOutline";
		}
		public CDOutlineDeed( Serial serial ) : base( serial )
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