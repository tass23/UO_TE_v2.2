using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FancyStoneFireplaceEastAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3562, 0, 0, 2}, {3553, 0, 0, 0}, {3562, 0, -1, 2}// 7	9	31	
			, {3562, 0, 1, 2}, {3553, 0, 1, 0}, {3553, 0, -1, 2}// 32	35	36	
					};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {3561, 0, 0, 1, 0, 0 }, {3561, 0, -1, 1, 0, 0 }, {3561, 0, 1, 1, 0, 0 }// 8	33	34	
					};

	
		public override BaseAddonDeed Deed { get { return new FancyStoneFireplaceEastAddonDeed(); } }

		[ Constructable ]
		public FancyStoneFireplaceEastAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 29, -1, -3, 0, 0, -1, "fire place" );// 1
			AddComplexComponent( (BaseAddon) this, 28, 0, -3, 0, 0, -1, "fire place" );// 2
			AddComplexComponent( (BaseAddon) this, 5534, 1, 2, 0, 0, -1, "fire place" );// 3
			AddComplexComponent( (BaseAddon) this, 2557, 1, 2, 10, 0, 8, "candle" );// 4
			AddComplexComponent( (BaseAddon) this, 5534, 1, -2, 0, 0, -1, "fire place" );// 5
			AddComplexComponent( (BaseAddon) this, 2557, 1, -2, 10, 0, 8, "candle" );// 6
			AddComplexComponent( (BaseAddon) this, 7128, 0, 0, 0, 0, -1, "fire wood" );// 10
			AddComplexComponent( (BaseAddon) this, 3555, 0, 1, 2, 0, 8, "fire" );// 11
			AddComplexComponent( (BaseAddon) this, 3555, 0, -1, 2, 0, 8, "fire" );// 12
			AddComplexComponent( (BaseAddon) this, 27, -1, 2, 0, 0, -1, "fire place" );// 13
			AddComplexComponent( (BaseAddon) this, 27, -1, -2, 0, 0, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 27, -1, -1, 0, 0, -1, "fire place" );// 15
			AddComplexComponent( (BaseAddon) this, 27, -1, 1, 0, 0, -1, "fire place" );// 16
			AddComplexComponent( (BaseAddon) this, 27, -1, 0, 0, 0, -1, "fire place" );// 17
			AddComplexComponent( (BaseAddon) this, 2232, -1, 3, 1, 0, -1, "fire place" );// 18
			AddComplexComponent( (BaseAddon) this, 1305, 0, 1, 0, 0, -1, "fire place" );// 19
			AddComplexComponent( (BaseAddon) this, 1305, 0, 0, 0, 0, -1, "fire place" );// 20
			AddComplexComponent( (BaseAddon) this, 1305, 0, -1, 0, 0, -1, "fire place" );// 21
			AddComplexComponent( (BaseAddon) this, 1305, 0, -2, 0, 0, -1, "fire place" );// 22
			AddComplexComponent( (BaseAddon) this, 26, 0, 2, 0, 0, -1, "fire place" );// 23
			AddComplexComponent( (BaseAddon) this, 27, 0, -2, 0, 0, -1, "fire place" );// 24
			AddComplexComponent( (BaseAddon) this, 29, 0, 1, 0, 0, -1, "fire place" );// 25
			AddComplexComponent( (BaseAddon) this, 1997, 0, 2, 19, 0, -1, "mantle" );// 26
			AddComplexComponent( (BaseAddon) this, 1997, 0, 1, 19, 0, -1, "mantle" );// 27
			AddComplexComponent( (BaseAddon) this, 1997, 0, 0, 19, 0, -1, "mantle" );// 28
			AddComplexComponent( (BaseAddon) this, 1997, 0, -1, 19, 0, -1, "mantle" );// 29
			AddComplexComponent( (BaseAddon) this, 1997, 0, -2, 19, 0, -1, "mantle" );// 30
			AddComplexComponent( (BaseAddon) this, 7128, 0, 1, 0, 0, -1, "fire wood" );// 37
			AddComplexComponent( (BaseAddon) this, 7128, 0, -1, 0, 0, -1, "fire wood" );// 38
			AddComplexComponent( (BaseAddon) this, 28, 0, -2, 0, 0, -1, "fire place" );// 39
			AddComplexComponent( (BaseAddon) this, 7682, 0, 0, 0, 0, -1, "dirt6" );// 40
			AddComplexComponent( (BaseAddon) this, 7682, 0, -1, 0, 0, -1, "dirt6" );// 41
			AddComplexComponent( (BaseAddon) this, 7681, 0, 1, 0, 0, -1, "dirt6" );// 42
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 2, 0, 8, "fire" );// 43
			AddComplexComponent( (BaseAddon) this, 2232, 0, 1, 1, 1893, -1, "fire place" );// 44
			AddComplexComponent( (BaseAddon) this, 2232, 0, -1, 1, 1893, -1, "fire place" );// 45
			AddComplexComponent( (BaseAddon) this, 2232, 0, 0, 1, 1893, -1, "fire place" );// 46
			AddComplexComponent( (BaseAddon) this, 1305, 0, 3, 0, 0, -1, "fire place" );// 47
			AddComplexComponent( (BaseAddon) this, 2231, 0, 3, 1, 0, -1, "fire place" );// 48
			AddComplexComponent( (BaseAddon) this, 7138, 0, 3, 0, 0, -1, "fire place" );// 49

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
		public FancyStoneFireplaceEastAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class FancyStoneFireplaceEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new FancyStoneFireplaceEastAddon(); } }

		[Constructable]
		public FancyStoneFireplaceEastAddonDeed()
		{
			Name = "FancyStoneFireplaceEast";
		}

		public FancyStoneFireplaceEastAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}