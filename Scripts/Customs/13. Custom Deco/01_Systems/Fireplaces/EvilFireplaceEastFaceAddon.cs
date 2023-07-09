using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EvilFireplaceEastFaceAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {6942, 0, 3, 0}, {7393, 0, 0, 7}, {4653, 0, 3, 0}// 25	27	31	
			, {6922, 0, -1, 0}, {7374, 1, 1, 0}, {7384, 1, 0, 0}// 32	35	36	
			, {7376, 1, -1, 0}// 37	
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {6234, -1, -1, 17, 0, 14 }, {6234, 1, 2, 20, 0, 8 }, {6234, 1, -2, 20, 0, 8 }// 1	20	21	
			, {4012, 0, 0, 1, 0, 8 }, {3561, 0, -1, 2, 0, 0 }, {3561, 0, -1, 6, 0, 0 }// 28	33	34	
					};

	
		public override BaseAddonDeed Deed { get { return new EvilFireplaceEastFaceAddonDeed(); } }

		[ Constructable ]
		public EvilFireplaceEastFaceAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 383, 1, -2, 0, 1109, -1, "fire place" );// 2
			AddComplexComponent( (BaseAddon) this, 383, 1, 2, 0, 1109, -1, "fire place" );// 3
			AddComplexComponent( (BaseAddon) this, 373, 0, -2, 0, 1109, -1, "fire place" );// 4
			AddComplexComponent( (BaseAddon) this, 373, 0, 2, 0, 1109, -1, "fire place" );// 5
			AddComplexComponent( (BaseAddon) this, 323, 0, 0, 0, 1109, -1, "fire place" );// 6
			AddComplexComponent( (BaseAddon) this, 323, 0, -1, 0, 1109, -1, "fire place" );// 7
			AddComplexComponent( (BaseAddon) this, 323, 0, 1, 0, 1109, -1, "fire place" );// 8
			AddComplexComponent( (BaseAddon) this, 1301, 0, 0, 0, 0, -1, "fire place" );// 9
			AddComplexComponent( (BaseAddon) this, 1301, 0, -1, 0, 0, -1, "fire place" );// 10
			AddComplexComponent( (BaseAddon) this, 1301, 0, 1, 0, 0, -1, "fire place" );// 11
			AddComplexComponent( (BaseAddon) this, 379, 0, 2, 20, 1109, -1, "fire place" );// 12
			AddComplexComponent( (BaseAddon) this, 381, 0, -2, 20, 1109, -1, "fire place" );// 13
			AddComplexComponent( (BaseAddon) this, 377, 0, -1, 20, 1109, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 377, 0, 1, 20, 1109, -1, "fire place" );// 15
			AddComplexComponent( (BaseAddon) this, 377, 0, 0, 20, 1109, -1, "fire place" );// 16
			AddComplexComponent( (BaseAddon) this, 351, -1, -1, 0, 1109, -1, "fire place" );// 17
			AddComplexComponent( (BaseAddon) this, 351, -1, 1, 0, 1109, -1, "fire place" );// 18
			AddComplexComponent( (BaseAddon) this, 351, -1, 0, 0, 1109, -1, "fire place" );// 19
			AddComplexComponent( (BaseAddon) this, 14742, 0, -1, 0, 0, 8, "fire" );// 22
			AddComplexComponent( (BaseAddon) this, 14742, 0, 1, 0, 0, 8, "fire" );// 23
			AddComplexComponent( (BaseAddon) this, 14742, 0, 0, 0, 0, 8, "fire" );// 24
			AddComplexComponent( (BaseAddon) this, 8314, 0, 0, 1, 0, -1, "pot" );// 26
			AddComplexComponent( (BaseAddon) this, 7932, 1, 0, 25, 0, -1, "glowing skull" );// 29
			AddComplexComponent( (BaseAddon) this, 7021, 1, 3, 35, 0, -1, "fire place" );// 30

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
		public EvilFireplaceEastFaceAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class EvilFireplaceEastFaceAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new EvilFireplaceEastFaceAddon(); } }

		[Constructable]
		public EvilFireplaceEastFaceAddonDeed()
		{
			Name = "EvilFireplaceEastFace";
		}

		public EvilFireplaceEastFaceAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}