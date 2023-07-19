using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server;
using Server.ContextMenus;
using Server.Mobiles;
using Server.Multis;

namespace Server.Items
{
	public class Mushroom_Lamp_Blue_v1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Blue_v1AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Blue_v1Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16639, 0, 0, 0, 0, 2, "Blue Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 1, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 2, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 0, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 8752, 0, 0, 1, 1152, -1, "Blue Mushroom lamp", 1);// 5
		}

		public Mushroom_Lamp_Blue_v1Addon( Serial serial ) : base( serial )
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
			{
                ac.Light = (LightType) lightsource;
			}
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( house.IsOwner( from ) || house.IsCoOwner( from ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3865 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16639)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 2 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Blue_v1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Blue_v1Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Blue_v1AddonDeed()
		{
			Name = "a Blue Mushroom lamp deed";
		}

		public Mushroom_Lamp_Blue_v1AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Red_v1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Red_v1AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Red_v1Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16638, 0, 0, 2, 0, 2, "Red Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 2, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 1, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 0, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 8750, 0, 0, 3, 2118, -1, "Red Mushroom lamp", 1);// 5
			
		}

		public Mushroom_Lamp_Red_v1Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( house.IsOwner( from ) || house.IsCoOwner( from ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3859 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16638)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Red_v1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Red_v1Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Red_v1AddonDeed()
		{
			Name = "a Red Mushroom lamp deed";
		}

		public Mushroom_Lamp_Red_v1AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Green_v1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Green_v1AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Green_v1Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16640, 0, 0, 0, 0, 2, "Green Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 0, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 8753, 0, 0, 1, 1400, -1, "Green Mushroom lamp", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 1, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 2, 0, -1, "Pull chain", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 3, 0, -1, "Pull chain", 1);// 6
		}

		public Mushroom_Lamp_Green_v1Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( house.IsOwner( from ) || house.IsCoOwner( from ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3856 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16640)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Green_v1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Green_v1Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Green_v1AddonDeed()
		{
			Name = "a Green Mushroom lamp deed";
		}

		public Mushroom_Lamp_Green_v1AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Purple_v1Addon : BaseAddon
	{  
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Purple_v1AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Purple_v1Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16641, 0, 0, 0, 0, 2, "Purple Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 8752, 0, 0, 1, 1460, -1, "Purple Mushroom lamp", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 2, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 3, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 4, 0, -1, "Pull chain", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 5, 0, -1, "Pull chain", 1);// 6
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 6, 0, -1, "Pull chain", 1);// 7
		}

		public Mushroom_Lamp_Purple_v1Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( house.IsOwner( from ) || house.IsCoOwner( from ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3862 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16641)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Purple_v1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Purple_v1Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Purple_v1AddonDeed()
		{
			Name = "a Purple Mushroom lamp deed";
		}

		public Mushroom_Lamp_Purple_v1AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Yellow_v1Addon : BaseAddon
	{  
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Yellow_v1AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Yellow_v1Addon()
		{
			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 0, 0, 2, "Yellow Mushroom lamp", 1);// 1 Invisible Yellow Light
			AddComplexComponent( (BaseAddon) this, 8753, 0, 0, 0, 1161, -1, "Yellow Mushroom lamp", 1);// 1
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 0, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 1, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 2, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 3, 0, -1, "Pull chain", 1);// 5
		}

		public Mushroom_Lamp_Yellow_v1Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( house.IsOwner( from ) || house.IsCoOwner( from ) || from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3885 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.Visible == false)
							{
								d.Visible = true;
								Point3D loc = GetWorldLocation();
								Effects.PlaySound( loc, Map, UnlitSound );
							}
							else if (d.Light == LightType.Circle150)
							{
								d.Light = LightType.Circle225;
								Point3D loc = GetWorldLocation();
								Effects.PlaySound( loc, Map, LitSound );
							}
							else if (d.Light == LightType.Circle225)
							{
								d.Light = LightType.Circle300;
								Point3D loc = GetWorldLocation();
								Effects.PlaySound( loc, Map, LitSound );
							}
							else if (d.Light == LightType.Circle300)
							{
								d.Visible = false;
								d.Light = LightType.Circle150;
								Point3D loc = GetWorldLocation();
								Effects.PlaySound( loc, Map, BurntOutSound );
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Yellow_v1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Yellow_v1Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Yellow_v1AddonDeed()
		{
			Name = "a Yellow Mushroom lamp deed";
		}

		public Mushroom_Lamp_Yellow_v1AddonDeed( Serial serial ) : base( serial )
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
//____________________________________________________GM USABLE LAMPS BELOW. CAN BE PLACED ANYWHERE. USED BY STAFF ONLY________________________________________________

	public class Mushroom_Lamp_Blue_v2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Blue_v2AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Blue_v2Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16639, 0, 0, 0, 0, 2, "Blue Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 1, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 2, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3865, 1, 0, 0, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 8752, 0, 0, 1, 1152, -1, "Blue Mushroom lamp", 1);// 5
		}

		public Mushroom_Lamp_Blue_v2Addon( Serial serial ) : base( serial )
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
			{
                ac.Light = (LightType) lightsource;
			}
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				//BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3865 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16639)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 2 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Blue_v2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Blue_v2Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Blue_v2AddonDeed()
		{
			Name = "a Blue Mushroom lamp deed(GM Only)";
		}

		public Mushroom_Lamp_Blue_v2AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Red_v2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Red_v2AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Red_v2Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16638, 0, 0, 2, 0, 2, "Red Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 2, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 1, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3859, 0, 0, 0, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 8750, 0, 0, 3, 2118, -1, "Red Mushroom lamp", 1);// 5
			
		}

		public Mushroom_Lamp_Red_v2Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				//BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3859 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16638)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Red_v2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Red_v2Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Red_v2AddonDeed()
		{
			Name = "a Red Mushroom lamp deed(GM Only)";
		}

		public Mushroom_Lamp_Red_v2AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Green_v2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Green_v2AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Green_v2Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16640, 0, 0, 0, 0, 2, "Green Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 0, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 8753, 0, 0, 1, 1400, -1, "Green Mushroom lamp", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 1, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 2, 0, -1, "Pull chain", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3856, 0, 0, 3, 0, -1, "Pull chain", 1);// 6
		}

		public Mushroom_Lamp_Green_v2Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				//BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3856 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16640)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Green_v2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Green_v2Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Green_v2AddonDeed()
		{
			Name = "a Green Mushroom lamp deed(GM Only)";
		}

		public Mushroom_Lamp_Green_v2AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Purple_v2Addon : BaseAddon
	{  
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Purple_v2AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Purple_v2Addon()
		{
			AddComplexComponent( (BaseAddon) this, 16641, 0, 0, 0, 0, 2, "Purple Mushroom lamp", 1);// 1 Colored Lantern
			AddComplexComponent( (BaseAddon) this, 8752, 0, 0, 1, 1460, -1, "Purple Mushroom lamp", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 2, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 3, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 4, 0, -1, "Pull chain", 1);// 5
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 5, 0, -1, "Pull chain", 1);// 6
			AddComplexComponent( (BaseAddon) this, 3862, 0, 1, 6, 0, -1, "Pull chain", 1);// 7
		}

		public Mushroom_Lamp_Purple_v2Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				//BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3862 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 16641)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Purple_v2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Purple_v2Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Purple_v2AddonDeed()
		{
			Name = "a Purple Mushroom lamp deed(GM Only)";
		}

		public Mushroom_Lamp_Purple_v2AddonDeed( Serial serial ) : base( serial )
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

	public class Mushroom_Lamp_Yellow_v2Addon : BaseAddon
	{  
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Mushroom_Lamp_Yellow_v2AddonDeed();
			}
		}

		//public int LitSound{ get { return 0x5B9; } }
		public int LitSound{ get { return 0x4D3; } }
		public int UnlitSound{ get { return 0x4D3; } }
		public int BurntOutSound{ get { return 0x4B8; } }

		[ Constructable ]
		public Mushroom_Lamp_Yellow_v2Addon()
		{
			AddComplexComponent( (BaseAddon) this, 5703, 0, 0, 0, 0, 2, "Yellow Mushroom lamp", 1);// 1 Invisible Yellow Light
			AddComplexComponent( (BaseAddon) this, 8753, 0, 0, 0, 1161, -1, "Yellow Mushroom lamp", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 0, 0, -1, "Pull chain", 1);// 2
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 1, 0, -1, "Pull chain", 1);// 3
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 2, 0, -1, "Pull chain", 1);// 4
			AddComplexComponent( (BaseAddon) this, 3885, 1, 0, 3, 0, -1, "Pull chain", 1);// 5
		}

		public Mushroom_Lamp_Yellow_v2Addon( Serial serial ) : base( serial )
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

		public override void OnComponentUsed( AddonComponent c, Mobile from )
		{
			if ( from.InRange( Location, 1 ))
			{
				//BaseHouse house = BaseHouse.FindHouseAt( this );
				if ( from.AccessLevel >= AccessLevel.GameMaster )
				{
					if (c.ItemID == 3885 )
					{
						foreach ( AddonComponent d in Components )
						{
							if (d.ItemID == 5703)
							{
								if (d.Visible == false)
								{
									d.Visible = true;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, UnlitSound );
								}
								else if (d.Light == LightType.Circle150)
								{
									d.Light = LightType.Circle225;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle225)
								{
									d.Light = LightType.Circle300;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, LitSound );
								}
								else if (d.Light == LightType.Circle300)
								{
									d.Visible = false;
									d.Light = LightType.Circle150;
									Point3D loc = GetWorldLocation();
									Effects.PlaySound( loc, Map, BurntOutSound );
								}
							}
							ReleaseWorldPackets();
							Delta(ItemDelta.Update);
						}
					}
				}
				else
					from.SendMessage("That does not belong to you!");
			}
			else
				from.SendLocalizedMessage( 1076766 ); // That is too far away.
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
			switch (version)
            {
                case 2:
				{
					goto case 1;
				}
                case 1:
				{
					goto case 0;
				}
                case 0:
				{
					break;
				}
            }
		}
	}

	public class Mushroom_Lamp_Yellow_v2AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Mushroom_Lamp_Yellow_v2Addon();
			}
		}

		[Constructable]
		public Mushroom_Lamp_Yellow_v2AddonDeed()
		{
			Name = "a Yellow Mushroom lamp deed(GM Only)";
		}

		public Mushroom_Lamp_Yellow_v2AddonDeed( Serial serial ) : base( serial )
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