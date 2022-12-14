
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
	public class CrystalCave4Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1339, 5, -5, 1}, {1339, -2, -5, 1}, {1339, -2, -4, 1}// 1	2	3	
			, {1339, -1, -5, 1}, {1339, -1, -4, 1}, {1339, 0, -5, 1}// 4	5	6	
			, {1339, 0, -4, 1}, {1339, 1, -5, 1}, {1339, 1, -4, 1}// 7	8	9	
			, {1339, 2, -5, 1}, {1339, 2, -4, 1}, {1339, 3, -5, 1}// 10	11	12	
			, {1339, 3, -4, 1}, {1339, 4, -5, 1}, {1339, 4, -4, 1}// 13	14	15	
			, {1339, 5, -4, 1}, {1339, -6, -6, 1}, {1339, 5, -6, 1}// 16	17	18	
			, {1339, -5, -6, 1}, {1339, -4, -6, 1}, {1339, -3, -6, 1}// 19	20	21	
			, {1339, -2, -6, 1}, {1339, -1, -6, 1}, {1339, 0, -6, 1}// 22	23	24	
			, {1339, 1, -6, 1}, {1339, 2, -6, 1}, {1339, 3, -6, 1}// 25	26	27	
			, {1339, 4, -6, 1}, {610, -7, -7, 1}, {604, -7, -6, 1}// 28	29	30	
			, {606, -7, -5, 1}, {608, -7, -4, 1}, {605, -6, -7, 1}// 31	32	33	
			, {607, -5, -7, 1}, {609, -4, -7, 1}, {605, -3, -7, 1}// 34	35	36	
			, {607, -2, -7, 1}, {609, -1, -7, 1}, {605, 0, -7, 1}// 37	38	39	
			, {607, 1, -7, 1}, {609, 2, -7, 1}, {605, 3, -7, 1}// 40	41	42	
			, {607, 4, -7, 1}, {609, 5, -7, 1}, {4943, -6, -5, 1}// 43	44	45	
			, {4944, -5, -5, 1}, {4945, -5, -6, 1}, {6039, -5, -4, 1}// 46	47	48	
			, {6039, -4, -4, 1}, {6039, -5, -5, 1}, {6039, -4, -5, 1}// 49	50	51	
			, {6039, -6, -4, 1}, {6039, -3, -4, 1}, {954, -3, -4, 1}// 52	53	54	
			, {954, -6, -4, 1}, {6039, -3, -5, 1}, {964, -3, -5, 1}// 55	56	57	
			, {966, -3, -4, 1}, {965, -3, -6, 1}, {963, -4, -6, 1}// 58	59	60	
			, {6039, -6, -5, 1}, {964, -7, -5, 1}, {966, -7, -4, 1}// 61	62	63	
			, {13484, -4, -4, 19}, {13451, -5, -5, 16}, {13489, -5, -5, 3}// 64	65	66	
			, {3260, -4, -5, 1}, {3259, -6, -4, 1}, {3269, -4, -4, 1}// 67	68	69	
			, {2279, -3, -6, 0}, {1361, -2, -6, 1}, {1363, -2, -5, 1}// 70	71	72	
			, {3376, -6, -6, 3}, {3377, -5, -6, 2}, {3378, -2, -5, 1}// 73	74	75	
			, {611, 5, -6, 1}, {2274, 5, -5, 1}, {1339, -4, -2, 1}// 76	77	78	
			, {1339, 4, 5, 1}, {1339, -4, -1, 1}, {1339, -4, 0, 1}// 79	80	81	
			, {1339, -4, 1, 1}, {1339, -4, 2, 1}, {1339, -4, 3, 1}// 82	83	84	
			, {1339, -4, 4, 1}, {1339, -4, 5, 1}, {1339, -3, -2, 1}// 85	86	87	
			, {1339, -3, -1, 1}, {1339, -3, 0, 1}, {1339, -3, 1, 1}// 88	89	90	
			, {1339, -3, 2, 1}, {1339, -3, 3, 1}, {1339, -3, 4, 1}// 91	92	93	
			, {1339, -3, 5, 1}, {1339, -2, -2, 1}, {1339, -2, -1, 1}// 94	95	96	
			, {1339, -2, 0, 1}, {1339, -2, 1, 1}, {1339, -2, 2, 1}// 97	98	99	
			, {1339, -2, 3, 1}, {1339, -2, 4, 1}, {1339, -2, 5, 1}// 100	101	102	
			, {1339, -1, -2, 1}, {1339, -1, -1, 1}, {1339, -1, 0, 1}// 103	104	105	
			, {1339, -1, 1, 1}, {1339, -1, 2, 1}, {1339, -1, 3, 1}// 106	107	108	
			, {1339, -1, 4, 1}, {1339, -1, 5, 1}, {1339, 0, -2, 1}// 109	110	111	
			, {1339, 0, -1, 1}, {1339, 0, 0, 1}, {1339, 0, 1, 1}// 112	113	114	
			, {1339, 0, 2, 1}, {1339, 0, 3, 1}, {1339, 0, 4, 1}// 115	116	117	
			, {1339, 0, 5, 1}, {1339, 1, -2, 1}, {1339, 1, -1, 1}// 118	119	120	
			, {1339, 1, 0, 1}, {1339, 1, 1, 1}, {1339, 1, 2, 1}// 121	122	123	
			, {1339, 1, 3, 1}, {1339, 1, 4, 1}, {1339, 1, 5, 1}// 124	125	126	
			, {1339, 2, -2, 1}, {1339, 2, -1, 1}, {1339, 2, 0, 1}// 127	128	129	
			, {1339, 2, 1, 1}, {1339, 2, 2, 1}, {1339, 2, 3, 1}// 130	131	132	
			, {1339, 2, 4, 1}, {1339, 2, 5, 1}, {1339, 3, -2, 1}// 133	134	135	
			, {1339, 3, -1, 1}, {1339, 3, 0, 1}, {1339, 3, 1, 1}// 136	137	138	
			, {1339, 3, 2, 1}, {1339, 3, 3, 1}, {1339, 3, 4, 1}// 139	140	141	
			, {1339, 3, 5, 1}, {1339, 4, -2, 1}, {1339, 4, -1, 1}// 142	143	144	
			, {1339, 4, 0, 1}, {1339, 4, 1, 1}, {1339, 4, 2, 1}// 145	146	147	
			, {1339, 4, 3, 1}, {1339, 4, 4, 1}, {1339, -4, 6, 1}// 148	149	150	
			, {1339, 4, 7, 1}, {1339, -4, 7, 1}, {1339, -3, 6, 1}// 151	152	153	
			, {1339, -3, 7, 1}, {1339, -2, 6, 1}, {1339, -2, 7, 1}// 154	155	156	
			, {1339, -1, 6, 1}, {1339, -1, 7, 1}, {1339, 0, 6, 1}// 157	158	159	
			, {1339, 0, 7, 1}, {1339, 1, 6, 1}, {1339, 1, 7, 1}// 160	161	162	
			, {1339, 2, 6, 1}, {1339, 2, 7, 1}, {1339, 3, 6, 1}// 163	164	165	
			, {1339, 3, 7, 1}, {1339, 4, 6, 1}, {1339, 5, -2, 1}// 166	167	168	
			, {1339, 5, 7, 1}, {1339, 5, -1, 1}, {1339, 5, 0, 1}// 169	170	171	
			, {1339, 5, 1, 1}, {1339, 5, 2, 1}, {1339, 5, 3, 1}// 172	173	174	
			, {1339, 5, 4, 1}, {1339, 5, 5, 1}, {1339, 5, 6, 1}// 175	176	177	
			, {1339, 5, -3, 1}, {1339, -3, -3, 1}, {1339, -2, -3, 1}// 178	179	180	
			, {1339, -1, -3, 1}, {1339, 0, -3, 1}, {1339, 1, -3, 1}// 181	182	183	
			, {1339, 2, -3, 1}, {1339, 3, -3, 1}, {1339, 4, -3, 1}// 184	185	186	
			, {1339, -6, 7, 1}, {1339, -6, -3, 1}, {1339, -6, -2, 1}// 187	188	189	
			, {1339, -6, -1, 1}, {1339, -6, 0, 1}, {1339, -6, 1, 1}// 190	191	192	
			, {1339, -6, 2, 1}, {1339, -6, 3, 1}, {1339, -6, 4, 1}// 193	194	195	
			, {1339, -6, 5, 1}, {1339, -6, 6, 1}, {1339, -5, -2, 1}// 196	197	198	
			, {1339, -5, -1, 1}, {1339, -5, 0, 1}, {1339, -5, 1, 1}// 199	200	201	
			, {1339, -5, 2, 1}, {1339, -5, 3, 1}, {1339, -5, 4, 1}// 202	203	204	
			, {1339, -5, 5, 1}, {1339, -5, 6, 1}, {1339, -5, 7, 1}// 205	206	207	
			, {604, -7, -3, 1}, {606, -7, -2, 1}, {608, -7, -1, 1}// 208	209	210	
			, {604, -7, 0, 1}, {606, -7, 1, 1}, {608, -7, 2, 1}// 211	212	213	
			, {604, -7, 3, 1}, {606, -7, 4, 1}, {608, -7, 5, 1}// 214	215	216	
			, {604, -7, 6, 1}, {606, -7, 7, 1}, {1339, -6, 8, 1}// 217	218	219	
			, {1339, -5, 8, 1}, {1339, -4, 8, 1}, {1339, -1, 8, 1}// 220	221	222	
			, {1339, 0, 8, 1}, {1346, -3, 8, 1}, {1346, -2, 8, 1}// 223	224	225	
			, {1347, 2, 8, 1}, {1346, 3, 8, 1}, {1351, -7, 8, 1}// 226	227	228	
			, {1361, 1, 8, 1}, {1361, 4, 8, 1}, {1361, 5, 8, 1}// 229	230	231	
			, {6039, -4, -3, 1}, {6039, -5, -3, 1}, {959, -4, -3, 1}// 232	233	234	
			, {963, -5, -3, 1}, {4949, -6, -2, 1}, {4950, -6, -3, 1}// 235	236	237	
			, {612, 4, 6, 1}, {2278, -6, 6, 1}, {2275, 3, 5, 1}// 238	239	240	
			, {2274, 3, 6, 1}, {624, -1, 8, 1}, {624, 0, 8, 1}// 241	242	243	
			, {626, 3, 7, 1}, {628, 5, 0, 1}, {625, -3, -3, 1}// 244	245	246	
			, {1344, -6, -1, 1}, {2274, -5, 5, 1}, {3270, -5, -2, 1}// 247	248	249	
			, {3237, -3, -3, 1}, {1348, 6, -6, 1}, {1349, 6, -5, 1}// 250	251	252	
			, {1350, 6, -4, 1}, {605, 6, -7, 1}, {2280, 6, -6, 1}// 253	254	255	
			, {2275, 6, -5, 1}, {1339, 6, -3, 1}, {1339, 6, -2, 1}// 256	257	258	
			, {1339, 6, -1, 1}, {1339, 7, -2, 1}, {1339, 6, 2, 1}// 259	260	261	
			, {1339, 6, 3, 1}, {1344, 6, 4, 1}, {1344, 7, -1, 1}// 262	263	264	
			, {1353, 7, -3, 1}, {1363, 6, 1, 1}, {1361, 7, 3, 1}// 265	266	267	
			, {1363, 7, 2, 1}, {1340, 6, 6, 1}, {1344, 6, 7, 1}// 268	269	270	
			, {1352, 6, 5, 1}, {2277, 6, -2, 1}, {628, 6, 6, 1}// 271	272	273	
			, {628, 7, -2, 1}, {629, 7, -3, 1}// 274	275	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CrystalCave4AddonDeed();
			}
		}

		[ Constructable ]
		public CrystalCave4Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


		}

		public CrystalCave4Addon( Serial serial ) : base( serial )
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

	public class CrystalCave4AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CrystalCave4Addon();
			}
		}

		[Constructable]
		public CrystalCave4AddonDeed()
		{
			Name = "CrystalCave4";
		}

		public CrystalCave4AddonDeed( Serial serial ) : base( serial )
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