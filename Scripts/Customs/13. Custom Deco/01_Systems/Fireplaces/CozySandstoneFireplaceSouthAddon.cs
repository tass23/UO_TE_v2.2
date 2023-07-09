using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CozySandstoneFireplaceSouthAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3562, 0, 0, 3}, {3562, -1, 0, 3}// 15	25	
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {3561, 0, 0, 1, 0, 0 }, {3561, -1, 0, 1, 0, 0 }// 20	28	
		};

	
		public override BaseAddonDeed Deed { get { return new CozySandstoneFireplaceSouthAddonDeed(); } }

		[ Constructable ]
		public CozySandstoneFireplaceSouthAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 968, 0, -1, 0, 0, -1, "fire place" );// 1
			AddComplexComponent( (BaseAddon) this, 968, -1, -1, 0, 0, -1, "fire place" );// 2
			AddComplexComponent( (BaseAddon) this, 968, 1, -1, 0, 0, -1, "fire place" );// 3
			AddComplexComponent( (BaseAddon) this, 2231, 3, -1, 0, 0, -1, "fire place" );// 4
			AddComplexComponent( (BaseAddon) this, 968, 2, -1, 0, 0, -1, "fire place" );// 5
			AddComplexComponent( (BaseAddon) this, 970, -3, -1, 0, 0, -1, "fire place" );// 6
			AddComplexComponent( (BaseAddon) this, 968, -2, -1, 0, 0, -1, "fire place" );// 7
			AddComplexComponent( (BaseAddon) this, 1993, -1, 0, 19, 0, -1, "mantle" );// 8
			AddComplexComponent( (BaseAddon) this, 1993, 1, 0, 19, 0, -1, "mantle" );// 9
			AddComplexComponent( (BaseAddon) this, 1181, -2, 0, 0, 0, -1, "fire place" );// 10
			AddComplexComponent( (BaseAddon) this, 5535, -2, 1, 2, 42, -1, "fire place" );// 11
			AddComplexComponent( (BaseAddon) this, 7131, -1, 0, 0, 0, -1, "fire wood" );// 12
			AddComplexComponent( (BaseAddon) this, 7131, 0, 0, 0, 0, -1, "fire wood" );// 13
			AddComplexComponent( (BaseAddon) this, 969, -2, 0, 0, 0, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 1993, -2, 0, 19, 0, -1, "mantle" );// 16
			AddComplexComponent( (BaseAddon) this, 1993, 2, 0, 19, 0, -1, "mantle" );// 17
			AddComplexComponent( (BaseAddon) this, 1181, 2, 0, 0, 0, -1, "fire place" );// 18
			AddComplexComponent( (BaseAddon) this, 967, 2, 0, 0, 0, -1, "fire place" );// 19
			AddComplexComponent( (BaseAddon) this, 969, -3, 0, 0, 0, -1, "fire place" );// 21
			AddComplexComponent( (BaseAddon) this, 969, 1, 0, 0, 0, -1, "fire place" );// 22
			AddComplexComponent( (BaseAddon) this, 2232, 3, 0, 0, 0, -1, "fire place" );// 23
			AddComplexComponent( (BaseAddon) this, 1993, 0, 0, 19, 0, -1, "mantle" );// 24
			AddComplexComponent( (BaseAddon) this, 1181, 3, 0, 0, 0, -1, "fire place" );// 26
			AddComplexComponent( (BaseAddon) this, 968, -2, 0, 0, 0, -1, "fire place" );// 27
			AddComplexComponent( (BaseAddon) this, 1301, -1, 0, 0, 0, -1, "rocks" );// 29
			AddComplexComponent( (BaseAddon) this, 7135, 3, 0, 0, 0, -1, "wood" );// 30
			AddComplexComponent( (BaseAddon) this, 2562, 2, 1, 12, 0, 5, "candle" );// 31
			AddComplexComponent( (BaseAddon) this, 3555, -1, 0, 3, 0, 5, "fire" );// 32
			AddComplexComponent( (BaseAddon) this, 2562, -2, 1, 12, 0, 5, "candle" );// 33
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 3, 0, 5, "fire" );// 34
			AddComplexComponent( (BaseAddon) this, 1301, 1, 0, 0, 0, -1, "rocks" );// 35
			AddComplexComponent( (BaseAddon) this, 1301, 0, 0, 0, 0, -1, "rocks" );// 36
			AddComplexComponent( (BaseAddon) this, 5535, 2, 1, 2, 42, -1, "fire place" );// 37

		}

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
		{
			AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null);
		}

		private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name)
		{
			AddonComponent ac;
			ac = new AddonComponent(item);
			if (name != null) ac.Name = name;
			if (hue != 0) ac.Hue = hue;
			if (lightsource != -1) ac.Light = (LightType) lightsource;
			addon.AddComponent(ac, xoffset, yoffset, zoffset);
		}
		public CozySandstoneFireplaceSouthAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class CozySandstoneFireplaceSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new CozySandstoneFireplaceSouthAddon(); } }

		[Constructable]
		public CozySandstoneFireplaceSouthAddonDeed()
		{
			Name = "CozySandstoneFireplaceSouth";
		}

		public CozySandstoneFireplaceSouthAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}