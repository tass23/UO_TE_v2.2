using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EvilFireplaceSouthFaceAddon : BaseAddon
	{
		private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {7373, -1, 1, 0}, {7374, 1, 1, 0}, {7379, 0, 1, 0}// 3	4	5	
			, {4653, 3, 0, 0}, {6941, 3, 0, 0}, {6922, -1, 0, 0}// 7	8	21	
			, {7393, 0, 0, 7}// 30	
		};

 
		private static int[,] m_AddOnComplexComponents = new int[,] {
			  {3561, -1, 0, 6, 0, 0 }, {6234, 2, 1, 20, 0, 5 }, {6234, -2, 1, 20, 0, 5 }// 1	23	24	
			, {3561, -1, 0, 2, 0, 0 }, {4012, 0, 0, 1, 0, 5 }, {6234, -1, -1, 17, 0, 11 }// 25	33	38	
					};

	
		public override BaseAddonDeed Deed { get { return new EvilFireplaceSouthFaceAddonDeed(); } }

		[ Constructable ]
		public EvilFireplaceSouthFaceAddon()
		{
			for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
				AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );
			for (int i = 0; i < m_AddOnComplexComponents.Length / 6; i++)
				AddComplexComponent( (BaseAddon)this, m_AddOnComplexComponents[i,0], m_AddOnComplexComponents[i,1], m_AddOnComplexComponents[i,2], m_AddOnComplexComponents[i,3], m_AddOnComplexComponents[i,4], m_AddOnComplexComponents[i,5] );
			AddComplexComponent( (BaseAddon) this, 7021, 2, 1, 27, 0, -1, "fire place" );// 2
			AddComplexComponent( (BaseAddon) this, 7914, 0, 1, 25, 0, -1, "glowing skull" );// 6
			AddComplexComponent( (BaseAddon) this, 322, 1, 0, 0, 1109, -1, "fire place" );// 9
			AddComplexComponent( (BaseAddon) this, 322, 0, 0, 0, 1109, -1, "fire place" );// 10
			AddComplexComponent( (BaseAddon) this, 322, -1, 0, 0, 1109, -1, "fire place" );// 11
			AddComplexComponent( (BaseAddon) this, 1301, 0, 0, 0, 0, -1, "fire place" );// 12
			AddComplexComponent( (BaseAddon) this, 373, 2, 0, 0, 1109, -1, "fire place" );// 13
			AddComplexComponent( (BaseAddon) this, 382, 2, 1, 0, 1109, -1, "fire place" );// 14
			AddComplexComponent( (BaseAddon) this, 352, -1, -1, 0, 1109, -1, "fire place" );// 15
			AddComplexComponent( (BaseAddon) this, 1301, 1, 0, 0, 0, -1, "fire place" );// 16
			AddComplexComponent( (BaseAddon) this, 1301, -1, 0, 0, 0, -1, "fire place" );// 17
			AddComplexComponent( (BaseAddon) this, 380, -2, 0, 20, 1109, -1, "fire place" );// 18
			AddComplexComponent( (BaseAddon) this, 378, 0, 0, 20, 1109, -1, "fire place" );// 19
			AddComplexComponent( (BaseAddon) this, 373, -2, 0, 0, 1109, -1, "fire place" );// 20
			AddComplexComponent( (BaseAddon) this, 382, -2, 1, 0, 1109, -1, "fire place" );// 22
			AddComplexComponent( (BaseAddon) this, 352, 0, -1, 0, 1109, -1, "fire place" );// 26
			AddComplexComponent( (BaseAddon) this, 379, 2, 0, 20, 1109, -1, "fire place" );// 27
			AddComplexComponent( (BaseAddon) this, 378, 1, 0, 20, 1109, -1, "fire place" );// 28
			AddComplexComponent( (BaseAddon) this, 378, -1, 0, 20, 1109, -1, "fire place" );// 29
			AddComplexComponent( (BaseAddon) this, 14732, -1, 0, 0, 0, 5, "fire" );// 31
			AddComplexComponent( (BaseAddon) this, 8296, 0, 0, 1, 0, -1, "pot" );// 32
			AddComplexComponent( (BaseAddon) this, 14732, 0, 0, 0, 0, 5, "fire" );// 34
			AddComplexComponent( (BaseAddon) this, 14732, -1, 0, 1, 0, 5, "fire" );// 35
			AddComplexComponent( (BaseAddon) this, 352, 1, -1, 0, 1109, -1, "fire place" );// 36
			AddComplexComponent( (BaseAddon) this, 7023, 0, 2, 23, 0, -1, "fire place" );// 37

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
		public EvilFireplaceSouthFaceAddon(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class EvilFireplaceSouthFaceAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new EvilFireplaceSouthFaceAddon(); } }

		[Constructable]
		public EvilFireplaceSouthFaceAddonDeed()
		{
			Name = "EvilFireplaceSouthFace";
		}

		public EvilFireplaceSouthFaceAddonDeed(Serial serial) : base(serial){}
		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( 0 ); }
		public override void	Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt(); }
	}
}