using System;
using Server;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Gumps;
using Server.Targeting;

namespace Server.Items
{
	public class LightsaberHilt : Item
	{
		private static int[] m_Hues = new int[] 
		{
			1466, 1486, 1491, 1075, 1102, 1151, 1154, 1301, 1100, 2909, 2471, 1556, 1173, 1366, 1391, 2908, 1168, 1398, 1468, 2906,
			1161, 1259, 1358, 2907, 1081, 1169, 1281, 2911, 39, 233, 1172, 2910, 1163, 1170, 1460, 1001, 1150, 1153, 2955 /*UO-The Expanse Custom Lightsaber Hues*/
		};

		[Constructable]
		public LightsaberHilt() : base( 0x0F92 )
		{
			Name = "Lightsaber Hilt";
			Stackable = false;
			Weight = 2.0;
			Hue = Utility.RandomList( m_Hues );
		}

		public LightsaberHilt( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}