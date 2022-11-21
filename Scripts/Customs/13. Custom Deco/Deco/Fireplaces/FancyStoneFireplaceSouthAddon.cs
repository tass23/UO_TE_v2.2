using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FancyStoneFireplaceSouthAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3553, 0, 0, 2}, {3553, -1, 0, 2}, {3562, 0, 0, 2}// 6	7	10	
			, {3562, -1, 0, 2}, {3553, 1, 0, 2}, {3562, 1, 0, 2}// 11	47	49	
			, {29, 1, 0, 0}// 57	
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {3561, -1, 0, 1, 0, 0 }, {3561, 0, 0, 1, 0, 0 }, {3561, 1, 0, 1, 0, 0 }// 8	9	48	
			, {3555, 1, 0, 2, 0, 5 }// 52	
		};

	
		public override BaseAddonDeed Deed { get { return new FancyStoneFireplaceSouthAddonDeed(); } }

		[ Constructable ]
		public FancyStoneFireplaceSouthAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 7682, 1, 0, 0, 0, -1, "dirt" );// 1
			AddComplexComponent( (BaseAddon) this, 7682, -1, 0, 0, 0, -1, "dirt" );// 2
			AddComplexComponent( (BaseAddon) this, 27, -2, 0, 0, 0, -1, "fire place" );// 3
			AddComplexComponent( (BaseAddon) this, 7131, 1, 0, 0, 0, -1, "fire wood" );// 4
			AddComplexComponent( (BaseAddon) this, 7131, -1, 0, 0, 0, -1, "fire wood" );// 5
			AddComplexComponent( (BaseAddon) this, 7135, 3, 0, 0, 0, -1, "fire place" );// 12
			AddComplexComponent( (BaseAddon) this, 2232, 3, 0, 1, 0, -1, "fire place" );// 13
			AddComplexComponent( (BaseAddon) this, 2231, 3, -1, 1, 0, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 1305, 3, 0, 0, 0, -1, "fire place" );// 15
			AddComplexComponent( (BaseAddon) this, 1993, -1, 0, 19, 0, -1, "mantle" );// 16
			AddComplexComponent( (BaseAddon) this, 1993, -2, 0, 19, 0, -1, "mantle" );// 17
			AddComplexComponent( (BaseAddon) this, 1993, 0, 0, 19, 0, -1, "mantle" );// 18
			AddComplexComponent( (BaseAddon) this, 1993, 1, 0, 19, 0, -1, "mantle" );// 19
			AddComplexComponent( (BaseAddon) this, 1993, 2, 0, 19, 0, -1, "mantle" );// 20
			AddComplexComponent( (BaseAddon) this, 29, 1, 0, 0, 0, -1, "fire place" );// 21
			AddComplexComponent( (BaseAddon) this, 28, -2, 0, 0, 0, -1, "fire place" );// 22
			AddComplexComponent( (BaseAddon) this, 29, -3, -1, 0, 0, -1, "fire place" );// 23
			AddComplexComponent( (BaseAddon) this, 28, 0, -1, 0, 0, -1, "fire place" );// 24
			AddComplexComponent( (BaseAddon) this, 28, 1, -1, 0, 0, -1, "fire place" );// 25
			AddComplexComponent( (BaseAddon) this, 28, 2, -1, 0, 0, -1, "fire place" );// 26
			AddComplexComponent( (BaseAddon) this, 28, -1, -1, 0, 0, -1, "fire place" );// 27
			AddComplexComponent( (BaseAddon) this, 28, -2, -1, 0, 0, -1, "fire place" );// 28
			AddComplexComponent( (BaseAddon) this, 27, -3, 0, 0, 0, -1, "fire place" );// 29
			AddComplexComponent( (BaseAddon) this, 26, 2, 0, 0, 0, -1, "fire place" );// 30
			AddComplexComponent( (BaseAddon) this, 1305, 2, 0, 0, 0, -1, "fire place" );// 31
			AddComplexComponent( (BaseAddon) this, 1305, 1, 0, 0, 0, -1, "fire place" );// 32
			AddComplexComponent( (BaseAddon) this, 1305, 0, 0, 0, 0, -1, "fire place" );// 33
			AddComplexComponent( (BaseAddon) this, 1305, -1, 0, 0, 0, -1, "fire place" );// 34
			AddComplexComponent( (BaseAddon) this, 1305, -2, 0, 0, 0, -1, "fire place" );// 35
			AddComplexComponent( (BaseAddon) this, 26, 2, 0, 0, 0, -1, "fire place" );// 36
			AddComplexComponent( (BaseAddon) this, 1305, 2, 0, 0, 0, -1, "fire place" );// 37
			AddComplexComponent( (BaseAddon) this, 2562, 2, 1, 10, 0, 5, "candle" );// 38
			AddComplexComponent( (BaseAddon) this, 5535, 2, 1, 0, 0, -1, "fire place" );// 39
			AddComplexComponent( (BaseAddon) this, 2562, -2, 1, 10, 0, 5, "candle" );// 40
			AddComplexComponent( (BaseAddon) this, 5535, -2, 1, 0, 0, -1, "fire place" );// 41
			AddComplexComponent( (BaseAddon) this, 1305, -2, 0, 0, 0, -1, "fire place" );// 42
			AddComplexComponent( (BaseAddon) this, 3555, -1, 0, 3, 0, 5, "fire" );// 43
			AddComplexComponent( (BaseAddon) this, 1305, -1, 0, 0, 0, -1, "fire place" );// 44
			AddComplexComponent( (BaseAddon) this, 3555, 0, 0, 2, 0, 5, "fire" );// 45
			AddComplexComponent( (BaseAddon) this, 7131, 0, 0, 0, 0, -1, "fire wood" );// 46
			AddComplexComponent( (BaseAddon) this, 1305, 0, 0, 0, 0, -1, "fire place" );// 50
			AddComplexComponent( (BaseAddon) this, 1305, 1, 0, 0, 0, -1, "fire place" );// 51
			AddComplexComponent( (BaseAddon) this, 2231, -1, 0, 1, 1893, -1, "fire place" );// 53
			AddComplexComponent( (BaseAddon) this, 2231, 0, 0, 1, 1893, -1, "fire place" );// 54
			AddComplexComponent( (BaseAddon) this, 2231, 1, 0, 1, 1893, -1, "fire place" );// 55
			AddComplexComponent( (BaseAddon) this, 7681, 0, 0, 0, 0, -1, "dirt" );// 56

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
		public FancyStoneFireplaceSouthAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class FancyStoneFireplaceSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new FancyStoneFireplaceSouthAddon(); } }

		[Constructable]
		public FancyStoneFireplaceSouthAddonDeed()
		{
			Name = "FancyStoneFireplaceSouth";
		}

		public FancyStoneFireplaceSouthAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}