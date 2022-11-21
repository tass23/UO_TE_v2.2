using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CozySandstoneFireplaceEastAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3562, 0, 0, 3}, {3562, 0, -1, 3}// 21	26	
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {3561, 0, -1, 1, 0, 0 }, {3561, 0, 0, 1, 0, 0 }// 20	30	
		};

	
		public override BaseAddonDeed Deed { get { return new CozySandstoneFireplaceEastAddonDeed(); } }

		[ Constructable ]
		public CozySandstoneFireplaceEastAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 1997, 0, 2, 19, 0, -1, "mantle" );// 1
			AddComplexComponent( (BaseAddon) this, 1997, 0, -2, 19, 0, -1, "mantle" );// 2
			AddComplexComponent( (BaseAddon) this, 1301, 0, 1, 0, 0, -1, "rocks" );// 3
			AddComplexComponent( (BaseAddon) this, 5534, 1, -2, 2, 42, -1, "fire place" );// 4
			AddComplexComponent( (BaseAddon) this, 5534, 1, 2, 2, 42, -1, "fire place" );// 5
			AddComplexComponent( (BaseAddon) this, 1997, 0, 1, 19, 0, -1, "mantle" );// 6
			AddComplexComponent( (BaseAddon) this, 7128, 0, 0, 0, 0, -1, "fire wood" );// 7
			AddComplexComponent( (BaseAddon) this, 7128, 0, -1, 0, 0, -1, "fire wood" );// 8
			AddComplexComponent( (BaseAddon) this, 969, 0, -2, 0, 0, -1, "fire place" );// 9
			AddComplexComponent( (BaseAddon) this, 1181, 0, 2, 0, 0, -1, "fire place" );// 10
			AddComplexComponent( (BaseAddon) this, 1181, 0, -2, 0, 0, -1, "fire place" );// 11
			AddComplexComponent( (BaseAddon) this, 1997, 0, 0, 19, 0, -1, "mantle" );// 12
			AddComplexComponent( (BaseAddon) this, 7138, 0, 3, 0, 0, -1, "wood" );// 13
			AddComplexComponent( (BaseAddon) this, 969, -1, 1, 0, 0, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 969, -1, 0, 0, 0, -1, "fire place" );// 15
			AddComplexComponent( (BaseAddon) this, 968, 0, 1, 0, 0, -1, "fire place" );// 16
			AddComplexComponent( (BaseAddon) this, 969, -1, -1, 0, 0, -1, "fire place" );// 17
			AddComplexComponent( (BaseAddon) this, 969, -1, -2, 0, 0, -1, "fire place" );// 18
			AddComplexComponent( (BaseAddon) this, 967, 0, 2, 0, 0, -1, "fire place" );// 19
			AddComplexComponent( (BaseAddon) this, 2232, -1, 3, 1, 0, -1, "fire place" );// 22
			AddComplexComponent( (BaseAddon) this, 1301, 0, 0, 0, 0, -1, "rocks" );// 23
			AddComplexComponent( (BaseAddon) this, 969, -1, 2, 0, 0, -1, "fire place" );// 24
			AddComplexComponent( (BaseAddon) this, 968, 0, -3, 0, 0, -1, "fire place" );// 25
			AddComplexComponent( (BaseAddon) this, 1181, 0, 3, 0, 0, -1, "fire place" );// 27
			AddComplexComponent( (BaseAddon) this, 970, -1, -3, 0, 0, -1, "fire place" );// 28
			AddComplexComponent( (BaseAddon) this, 968, 0, -2, 0, 0, -1, "fire place" );// 29
			AddComplexComponent( (BaseAddon) this, 1997, 0, -1, 19, 0, -1, "mantle" );// 31
			AddComplexComponent( (BaseAddon) this, 1301, 0, -1, 0, 0, -1, "rocks" );// 32
			AddComplexComponent( (BaseAddon) this, 2231, 0, 3, 1, 0, -1, "fire place" );// 33
			AddComplexComponent( (BaseAddon) this, 2557, 1, -2, 12, 0, 8, "candle" );// 34
			AddComplexComponent( (BaseAddon) this, 2557, 1, 2, 12, 0, 8, "candle" );// 35
			AddComplexComponent( (BaseAddon) this, 3555, 0, -1, 3, 0, 8, "fire" );// 36
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 4, 0, 8, "fire" );// 37

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
		public CozySandstoneFireplaceEastAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class CozySandstoneFireplaceEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new CozySandstoneFireplaceEastAddon(); } }

		[Constructable]
		public CozySandstoneFireplaceEastAddonDeed()
		{
			Name = "CozySandstoneFireplaceEast";
		}

		public CozySandstoneFireplaceEastAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}