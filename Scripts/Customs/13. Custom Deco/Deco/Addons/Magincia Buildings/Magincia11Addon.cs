
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
	public class Magincia11Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2860, -4, -1, 20}, {2861, -1, -4, 20}, {2861, 0, -4, 20}// 1	2	3	
			, {1900, 5, -1, 0}, {1900, 5, -1, 5}, {1900, 4, -1, 5}// 4	5	6	
			, {1900, 4, -1, 0}, {1900, 4, -1, 10}, {1900, 6, -1, 0}// 7	8	9	
			, {1902, 6, -1, 5}, {1902, 7, -1, 0}, {1902, 4, -1, 15}// 10	11	12	
			, {1902, 5, -1, 10}, {351, -5, -4, 0}, {351, 3, -1, 0}// 13	14	15	
			, {351, -5, -1, 0}, {351, 3, -4, 0}, {352, -4, -5, 0}// 16	17	18	
			, {352, 3, -5, 0}, {352, -1, -5, 0}, {352, 0, -5, 0}// 19	20	21	
			, {353, -5, -5, 0}, {398, -3, -5, 0}, {398, 1, -5, 0}// 22	23	24	
			, {399, 3, -3, 0}, {399, -5, -3, 0}, {400, -5, -2, 0}// 25	26	27	
			, {400, 3, -2, 0}, {402, -2, -5, 0}, {402, 2, -5, 0}// 28	29	30	
			, {1387, 3, -1, 20}, {1387, 3, -3, 20}, {1387, 3, -2, 20}// 31	32	33	
			, {1388, 3, -4, 20}, {1392, -4, -2, 20}, {1392, -4, -3, 20}// 34	35	36	
			, {1392, -4, -1, 20}, {1393, -1, -4, 20}, {1393, 0, -4, 20}// 37	38	39	
			, {1393, 2, -4, 20}, {1393, 1, -4, 20}, {1393, -2, -4, 20}// 40	41	42	
			, {1393, -3, -4, 20}, {1394, -4, -4, 20}, {1395, -3, -3, 20}// 43	44	45	
			, {1395, 1, -3, 20}, {1395, -1, -3, 20}, {1396, -3, -2, 20}// 46	47	48	
			, {1396, 1, -2, 20}, {1396, -1, -2, 20}, {1397, -1, -1, 20}// 49	50	51	
			, {1397, -2, -1, 20}, {1397, 0, -1, 20}, {1397, -3, -1, 20}// 52	53	54	
			, {1397, 1, -1, 20}, {1397, 2, -1, 20}, {1398, -2, -2, 20}// 55	56	57	
			, {1398, 2, -2, 20}, {1398, 0, -2, 20}, {1399, 0, -3, 20}// 58	59	60	
			, {1399, -2, -3, 20}, {1399, 2, -3, 20}, {3236, 5, -5, 0}// 61	62	63	
			, {3236, -6, -6, 0}, {3235, -6, -5, 0}, {3235, 4, -4, 0}// 64	65	66	
			, {3234, 4, -5, 0}, {3233, 4, -5, 0}, {3233, -5, -6, 0}// 67	68	69	
			, {3221, -6, -6, 0}, {3221, 4, -5, 0}, {3205, 3, -3, 31}// 70	71	72	
			, {2860, -4, 0, 20}, {1900, 0, 4, 10}, {1900, 0, 4, 5}// 73	74	75	
			, {1900, 0, 5, 4}, {1900, 5, 0, 5}, {1900, 5, 0, 0}// 76	77	78	
			, {1900, 0, 5, 0}, {1900, 0, 4, 0}, {1900, -1, 6, 0}// 79	80	81	
			, {1900, 4, 0, 10}, {1900, 4, 0, 5}, {1900, 4, 0, 0}// 82	83	84	
			, {1900, -1, 5, 4}, {1900, -1, 4, 5}, {1900, 6, 0, 0}// 85	86	87	
			, {1900, 0, 6, 0}, {1900, -1, 5, 0}, {1900, -1, 4, 10}// 88	89	90	
			, {1901, -1, 7, 0}, {1901, -1, 5, 9}, {1901, -1, 6, 5}// 91	92	93	
			, {1901, -1, 4, 15}, {1901, 0, 7, 0}, {1901, 0, 5, 9}// 94	95	96	
			, {1901, 0, 6, 5}, {1901, 0, 4, 15}, {1902, 7, 0, 0}// 97	98	99	
			, {1902, 4, 0, 15}, {1902, 6, 0, 5}, {1902, 5, 0, 10}// 100	101	102	
			, {350, 3, 3, 0}, {351, 3, 0, 0}, {351, -5, 0, 0}// 103	104	105	
			, {351, -5, 3, 0}, {352, -4, 3, 0}, {352, -1, 3, 0}// 106	107	108	
			, {352, 0, 3, 0}, {398, -3, 3, 0}, {398, 1, 3, 0}// 109	110	111	
			, {399, 3, 1, 0}, {399, -5, 1, 0}, {400, 3, 2, 0}// 112	113	114	
			, {400, -5, 2, 0}, {402, -2, 3, 0}, {402, 2, 3, 0}// 115	116	117	
			, {1387, 3, 2, 20}, {1387, 3, 0, 20}, {1387, 3, 1, 20}// 118	119	120	
			, {1389, 2, 3, 20}, {1389, -1, 3, 20}, {1389, 0, 3, 20}// 121	122	123	
			, {1389, -2, 3, 20}, {1389, 1, 3, 20}, {1389, -3, 3, 20}// 124	125	126	
			, {1390, -4, 3, 20}, {1391, 3, 3, 20}, {1392, -4, 2, 20}// 127	128	129	
			, {1392, -4, 0, 20}, {1392, -4, 1, 20}, {1395, 2, 2, 20}// 130	131	132	
			, {1395, -2, 2, 20}, {1395, 0, 2, 20}, {1396, 2, 1, 20}// 133	134	135	
			, {1396, 0, 1, 20}, {1396, -2, 1, 20}, {1397, -1, 0, 20}// 136	137	138	
			, {1397, -2, 0, 20}, {1397, 0, 0, 20}, {1397, -3, 0, 20}// 139	140	141	
			, {1397, 1, 0, 20}, {1397, 2, 0, 20}, {1398, -3, 1, 20}// 142	143	144	
			, {1398, 1, 1, 20}, {1398, -1, 1, 20}, {1399, -1, 2, 20}// 145	146	147	
			, {1399, 1, 2, 20}, {1399, -3, 2, 20}, {3205, -3, 3, 31}// 148	149	150	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new Magincia11AddonDeed();
			}
		}

		[ Constructable ]
		public Magincia11Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public Magincia11Addon( Serial serial ) : base( serial )
		{
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

	public class Magincia11AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new Magincia11Addon();
			}
		}

		[Constructable]
		public Magincia11AddonDeed()
		{
			Name = "Magincia11";
		}

		public Magincia11AddonDeed( Serial serial ) : base( serial )
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