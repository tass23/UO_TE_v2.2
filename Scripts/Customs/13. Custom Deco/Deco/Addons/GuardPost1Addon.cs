
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
	public class GuardPost1Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {1404, -4, -6, 0}, {1404, -4, -5, 0}, {1404, -3, -6, 0}// 1	2	3	
			, {1404, -3, -5, 0}, {1404, -2, -6, 0}, {1404, -2, -5, 0}// 4	5	6	
			, {1404, -1, -6, 0}, {1404, -1, -5, 0}, {1404, 0, -6, 0}// 7	8	9	
			, {1404, 0, -5, 0}, {6013, 1, -6, 0}, {6013, 1, -5, 0}// 10	11	12	
			, {6015, 2, -6, 0}, {6015, 2, -5, 0}, {6015, 3, -6, 0}// 13	14	15	
			, {6015, 3, -5, 0}, {6015, 4, -6, 0}, {6015, 4, -5, 0}// 16	17	18	
			, {6015, 5, -6, 0}, {6015, 5, -5, 0}, {1267, -4, -6, 20}// 19	20	21	
			, {1267, -4, -5, 20}, {1267, -3, -6, 20}, {1267, -3, -5, 20}// 22	23	24	
			, {1267, -2, -6, 20}, {1267, -2, -5, 20}, {1267, -1, -6, 20}// 25	26	27	
			, {1267, -1, -5, 20}, {1267, 0, -6, 20}, {1267, 0, -5, 20}// 28	29	30	
			, {1200, 1, -6, 20}, {1198, 1, -5, 20}, {1200, 2, -6, 20}// 31	32	33	
			, {1198, 2, -5, 20}, {1200, 3, -6, 20}, {1198, 3, -5, 20}// 34	35	36	
			, {1200, 4, -6, 20}, {1198, 4, -5, 20}, {1200, 5, -6, 20}// 37	38	39	
			, {1198, 5, -5, 20}, {1216, -4, -6, 40}, {1209, -4, -5, 40}// 40	41	42	
			, {1212, -3, -6, 40}, {1206, -3, -5, 40}, {1212, -2, -6, 40}// 43	44	45	
			, {1206, -2, -5, 40}, {1212, -1, -6, 40}, {1206, -1, -5, 40}// 46	47	48	
			, {1215, 0, -6, 40}, {1211, 0, -5, 40}, {1216, -4, -6, 60}// 49	50	51	
			, {1209, -4, -5, 60}, {1212, -3, -6, 60}, {1192, -3, -5, 60}// 52	53	54	
			, {1212, -2, -6, 60}, {1192, -2, -5, 60}, {1212, -1, -6, 60}// 55	56	57	
			, {1192, -1, -5, 60}, {1215, 0, -6, 60}, {1211, 0, -5, 60}// 58	59	60	
			, {970, -5, -7, 0}, {969, -5, -6, 0}, {968, -4, -7, 0}// 61	62	64	
			, {968, -1, -7, 0}, {968, 0, -7, 0}, {968, 1, -7, 0}// 67	68	71	
			, {965, 1, -5, 0}, {2141, 2, -7, 0}, {2141, 3, -7, 0}// 72	73	74	
			, {2141, 4, -7, 0}, {2233, 4, -5, 0}, {2141, 5, -7, 0}// 75	76	77	
			, {2231, 5, -5, 0}, {970, -5, -7, 20}, {969, -5, -6, 20}// 78	79	80	
			, {969, -5, -5, 20}, {968, -4, -7, 20}, {968, -1, -7, 20}// 81	82	85	
			, {968, 0, -7, 20}, {960, 1, -7, 20}, {2141, 1, -5, 20}// 86	87	88	
			, {2141, 2, -7, 20}, {2141, 2, -5, 20}, {2141, 3, -7, 20}// 89	90	91	
			, {2141, 3, -5, 20}, {2141, 4, -7, 20}, {2141, 4, -5, 20}// 92	93	94	
			, {960, 5, -7, 20}, {2141, 5, -5, 20}, {970, -5, -7, 40}// 95	96	97	
			, {969, -5, -6, 40}, {961, -5, -5, 40}, {968, -4, -7, 40}// 98	99	100	
			, {960, -3, -7, 40}, {960, -2, -7, 40}, {960, -1, -7, 40}// 101	102	103	
			, {968, 0, -7, 40}, {969, 0, -6, 40}, {961, 0, -5, 40}// 104	105	106	
			, {970, -5, -7, 60}, {969, -5, -6, 60}, {969, -5, -5, 60}// 107	108	109	
			, {968, -4, -7, 60}, {968, -3, -7, 60}, {968, -2, -7, 60}// 110	111	112	
			, {968, -1, -7, 60}, {968, 0, -7, 60}, {969, 0, -6, 60}// 113	114	115	
			, {969, 0, -5, 60}, {1404, -4, -4, 0}, {1404, -4, -3, 0}// 116	117	118	
			, {1404, -4, -2, 0}, {1404, -4, -1, 0}, {1404, -4, 0, 0}// 119	120	121	
			, {1267, -4, 1, 0}, {1403, -4, 6, 0}, {1403, -4, 7, 0}// 122	123	124	
			, {6013, -4, 8, 0}, {1404, -3, -4, 0}, {1404, -3, -3, 0}// 125	126	127	
			, {1404, -3, -2, 0}, {1404, -3, -1, 0}, {1404, -3, 0, 0}// 128	129	130	
			, {1404, -3, 1, 0}, {1403, -3, 6, 0}, {1403, -3, 7, 0}// 131	132	133	
			, {6013, -3, 8, 0}, {1404, -2, -4, 0}, {1404, -2, -3, 0}// 134	135	136	
			, {1404, -2, -2, 0}, {1404, -2, -1, 0}, {1404, -2, 0, 0}// 137	138	139	
			, {1404, -2, 1, 0}, {6014, -2, 2, 0}, {6014, -2, 3, 0}// 140	141	142	
			, {6014, -2, 4, 0}, {6013, -2, 5, 0}, {1403, -2, 6, 0}// 143	144	145	
			, {1403, -2, 7, 0}, {6013, -2, 8, 0}, {1404, -1, -4, 0}// 146	147	148	
			, {1404, -1, -3, 0}, {1404, -1, -2, 0}, {1404, -1, -1, 0}// 149	150	151	
			, {1404, -1, 0, 0}, {1404, -1, 1, 0}, {6014, -1, 2, 0}// 152	153	154	
			, {6014, -1, 3, 0}, {6013, -1, 4, 0}, {6014, -1, 5, 0}// 155	156	157	
			, {1403, -1, 6, 0}, {1403, -1, 7, 0}, {6013, -1, 8, 0}// 158	159	160	
			, {1404, 0, -4, 0}, {1404, 0, -3, 0}, {1404, 0, -2, 0}// 161	162	163	
			, {1404, 0, -1, 0}, {1404, 0, 0, 0}, {1404, 0, 1, 0}// 164	165	166	
			, {6015, 0, 2, 0}, {6014, 0, 3, 0}, {6013, 0, 4, 0}// 167	168	169	
			, {6014, 0, 5, 0}, {1403, 0, 6, 0}, {1403, 0, 7, 0}// 170	171	172	
			, {6013, 0, 8, 0}, {1404, 1, -4, 0}, {1404, 1, -3, 0}// 173	174	175	
			, {1404, 1, -2, 0}, {1404, 1, -1, 0}, {1404, 1, 0, 0}// 176	177	178	
			, {1404, 1, 1, 0}, {1403, 1, 2, 0}, {1403, 1, 3, 0}// 179	180	181	
			, {1403, 1, 4, 0}, {1403, 1, 5, 0}, {1403, 1, 6, 0}// 182	183	184	
			, {1403, 1, 7, 0}, {6013, 1, 8, 0}, {6015, 2, -4, 0}// 185	186	187	
			, {6015, 2, -3, 0}, {6015, 2, -2, 0}, {6015, 2, -1, 0}// 188	189	190	
			, {6015, 2, 0, 0}, {6015, 2, 1, 0}, {6015, 2, 2, 0}// 191	192	193	
			, {6015, 2, 3, 0}, {6015, 2, 4, 0}, {6015, 2, 5, 0}// 194	195	196	
			, {1403, 2, 6, 0}, {1403, 2, 7, 0}, {6013, 2, 8, 0}// 197	198	199	
			, {6015, 3, -4, 0}, {6015, 3, -3, 0}, {6015, 3, -2, 0}// 200	201	202	
			, {6015, 3, -1, 0}, {6015, 3, 0, 0}, {6015, 3, 1, 0}// 203	204	205	
			, {6013, 3, 2, 0}, {6013, 3, 3, 0}, {6013, 3, 4, 0}// 206	207	208	
			, {6014, 3, 5, 0}, {6014, 3, 6, 0}, {1403, 3, 7, 0}// 209	210	211	
			, {1403, 3, 8, 0}, {6015, 4, -4, 0}, {6015, 4, -3, 0}// 212	213	214	
			, {6015, 4, -2, 0}, {6015, 4, -1, 0}, {6015, 4, 0, 0}// 215	216	217	
			, {6015, 4, 1, 0}, {6013, 4, 2, 0}, {6013, 4, 3, 0}// 218	219	220	
			, {6013, 4, 4, 0}, {6013, 4, 5, 0}, {6014, 4, 6, 0}// 221	222	223	
			, {1403, 4, 7, 0}, {1403, 4, 8, 0}, {6015, 5, -4, 0}// 224	225	226	
			, {6015, 5, -3, 0}, {6015, 5, -2, 0}, {6015, 5, -1, 0}// 227	228	229	
			, {6015, 5, 0, 0}, {6015, 5, 1, 0}, {6013, 5, 2, 0}// 230	231	232	
			, {6013, 5, 3, 0}, {6013, 5, 4, 0}, {6013, 5, 5, 0}// 233	234	235	
			, {6013, 5, 8, 0}, {1267, -4, -4, 20}, {1267, -4, -3, 20}// 236	237	238	
			, {1267, -4, -2, 20}, {1197, -4, -1, 20}, {1197, -4, 0, 20}// 239	240	241	
			, {1202, -4, 1, 20}, {1267, -3, -4, 20}, {1267, -3, -3, 20}// 242	243	244	
			, {1267, -3, -2, 20}, {1190, -3, -1, 20}, {1193, -3, 0, 20}// 245	246	247	
			, {1198, -3, 1, 20}, {1267, -2, -4, 20}, {1267, -2, -3, 20}// 248	249	250	
			, {1267, -2, -2, 20}, {1196, -2, -1, 20}, {1195, -2, 0, 20}// 251	252	253	
			, {1198, -2, 1, 20}, {1267, -1, -4, 20}, {1267, -1, -3, 20}// 254	255	256	
			, {1267, -1, -2, 20}, {1190, -1, -1, 20}, {1190, -1, 0, 20}// 257	258	259	
			, {1198, -1, 1, 20}, {1267, 0, -4, 20}, {1267, 0, -3, 20}// 260	261	262	
			, {1267, 0, -2, 20}, {1193, 0, -1, 20}, {1193, 0, 0, 20}// 263	264	265	
			, {1198, 0, 1, 20}, {1199, 1, -1, 20}, {1199, 1, 0, 20}// 266	267	268	
			, {1201, 1, 1, 20}, {1209, -4, -4, 40}, {1209, -4, -3, 40}// 269	270	271	
			, {1209, -4, -2, 40}, {1206, -3, -4, 40}, {1206, -3, -3, 40}// 272	273	274	
			, {1192, -3, -2, 40}, {1206, -2, -4, 40}, {1206, -2, -3, 40}// 275	276	277	
			, {1192, -2, -2, 40}, {1206, -1, -4, 40}, {1192, -1, -2, 40}// 278	279	280	
			, {1211, 0, -4, 40}, {1211, 0, -3, 40}, {1213, 0, -2, 40}// 281	282	283	
			, {1209, -4, -4, 60}, {1209, -4, -3, 60}, {1214, -4, -2, 60}// 284	285	286	
			, {1192, -3, -4, 60}, {1210, -3, -2, 60}, {1192, -2, -4, 60}// 287	288	289	
			, {1192, -2, -3, 60}, {1210, -2, -2, 60}, {1192, -1, -4, 60}// 290	291	292	
			, {1192, -1, -3, 60}, {1210, -1, -2, 60}, {1211, 0, -4, 60}// 293	294	295	
			, {1211, 0, -3, 60}, {1213, 0, -2, 60}, {969, -5, -3, 0}// 296	297	299	
			, {969, -5, -2, 0}, {969, -5, -1, 0}, {969, -5, 0, 0}// 300	301	302	
			, {969, -5, 1, 0}, {994, -5, 2, 0}, {969, -5, 3, 0}// 303	304	305	
			, {966, -5, 4, 0}, {2142, -5, 5, 0}, {2142, -5, 6, 0}// 306	307	308	
			, {2142, -5, 7, 0}, {2142, -5, 8, 0}, {993, -4, 1, 0}// 309	310	311	
			, {1006, -4, 2, 0}, {1006, -4, 3, 0}, {1006, -4, 4, 0}// 312	313	314	
			, {1007, -4, 5, 0}, {2141, -4, 8, 0}, {1006, -3, 2, 0}// 315	316	318	
			, {1006, -3, 3, 0}, {1006, -3, 4, 0}, {1007, -3, 5, 0}// 319	320	321	
			, {2141, -3, 8, 0}, {968, -2, 1, 0}, {2141, -2, 8, 0}// 322	323	324	
			, {2141, -1, 8, 0}, {969, 0, -2, 0}, {967, 0, 1, 0}// 326	327	330	
			, {2141, 0, 8, 0}, {2141, 1, 8, 0}, {2141, 2, 8, 0}// 331	332	333	
			, {2233, 4, -3, 0}, {2231, 5, -3, 0}, {2143, 5, 8, 0}// 334	335	336	
			, {969, -5, -2, 20}, {2142, -5, -1, 20}, {2142, -5, 0, 20}// 339	340	341	
			, {2142, -5, 1, 20}, {968, -4, -2, 20}, {2141, -3, 1, 20}// 342	343	344	
			, {2141, -2, 1, 20}, {968, -1, -2, 20}, {2141, -1, 1, 20}// 345	346	347	
			, {969, 0, -4, 20}, {967, 0, -2, 20}, {2141, 0, 1, 20}// 348	350	351	
			, {2141, 1, -2, 20}, {2142, 1, -1, 20}, {2142, 1, 0, 20}// 352	353	354	
			, {2140, 1, 1, 20}, {961, -5, -4, 40}, {961, -5, -3, 40}// 355	356	357	
			, {969, -5, -2, 40}, {968, -4, -2, 40}, {960, -3, -2, 40}// 358	359	360	
			, {960, -2, -2, 40}, {960, -1, -2, 40}, {961, 0, -4, 40}// 361	362	363	
			, {961, 0, -3, 40}, {967, 0, -2, 40}, {969, -5, -4, 60}// 364	365	366	
			, {969, -5, -3, 60}, {969, -5, -2, 60}, {968, -4, -2, 60}// 367	368	369	
			, {968, -3, -2, 60}, {968, -2, -2, 60}, {968, -1, -2, 60}// 370	371	372	
			, {969, 0, -4, 60}, {969, 0, -3, 60}, {967, 0, -2, 60}// 373	374	375	
			, {1007, -3, 2, 15}, {1007, -4, 2, 15}, {1007, -3, 3, 10}// 376	377	378	
			, {1007, -4, 3, 10}, {1006, -3, 2, 10}, {1006, -4, 2, 10}// 379	380	381	
			, {1007, -3, 4, 5}, {1007, -4, 4, 5}, {1006, -3, 3, 5}// 382	383	384	
			, {1006, -4, 3, 5}, {1006, -3, 2, 5}, {1006, -4, 2, 5}// 385	386	387	
			, {6015, 6, -6, 0}, {6015, 6, -5, 0}, {1203, 6, -6, 20}// 388	389	390	
			, {1201, 6, -5, 20}, {968, 6, -7, 0}, {969, 6, -6, 0}// 391	392	393	
			, {2142, 6, -5, 0}, {960, 6, -7, 20}, {961, 6, -6, 20}// 394	395	396	
			, {958, 6, -5, 20}, {6015, 6, -4, 0}, {6015, 6, -3, 0}// 397	398	399	
			, {6015, 6, -2, 0}, {6015, 6, -1, 0}, {6015, 6, 0, 0}// 400	401	402	
			, {6015, 6, 1, 0}, {6013, 6, 2, 0}, {6013, 6, 3, 0}// 403	404	405	
			, {6013, 6, 4, 0}, {6013, 6, 5, 0}, {6013, 6, 6, 0}// 406	407	408	
			, {6015, 6, 7, 0}, {6015, 6, 8, 0}, {2142, 6, -4, 0}// 409	410	411	
			, {2142, 6, -3, 0}, {2142, 6, -2, 0}, {2142, 6, -1, 0}// 412	413	414	
			, {2142, 6, 0, 0}, {967, 6, 1, 0}, {2142, 6, 2, 0}// 415	416	417	
			, {2142, 6, 3, 0}, {2142, 6, 4, 0}, {2142, 6, 5, 0}// 418	419	420	
			, {2142, 6, 6, 0}, {2142, 6, 7, 0}, {2140, 6, 8, 0}// 421	422	423	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new GuardPost1AddonDeed();
			}
		}

		[ Constructable ]
		public GuardPost1Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 991, -5, -5, 0, 0, 0, "", 1);// 63
			AddComplexComponent( (BaseAddon) this, 990, -3, -7, 0, 0, 0, "", 1);// 65
			AddComplexComponent( (BaseAddon) this, 990, -2, -7, 0, 0, 0, "", 1);// 66
			AddComplexComponent( (BaseAddon) this, 991, 0, -6, 0, 0, 0, "", 1);// 69
			AddComplexComponent( (BaseAddon) this, 991, 0, -5, 0, 0, 0, "", 1);// 70
			AddComplexComponent( (BaseAddon) this, 990, -3, -7, 20, 0, 0, "", 1);// 83
			AddComplexComponent( (BaseAddon) this, 990, -2, -7, 20, 0, 0, "", 1);// 84
			AddComplexComponent( (BaseAddon) this, 991, -5, -4, 0, 0, 0, "", 1);// 298
			AddComplexComponent( (BaseAddon) this, 990, -3, 1, 0, 0, 0, "", 1);// 317
			AddComplexComponent( (BaseAddon) this, 990, -1, 1, 0, 0, 0, "", 1);// 325
			AddComplexComponent( (BaseAddon) this, 991, 0, -1, 0, 0, 0, "", 1);// 328
			AddComplexComponent( (BaseAddon) this, 991, 0, 0, 0, 0, 0, "", 1);// 329
			AddComplexComponent( (BaseAddon) this, 991, -5, -4, 20, 0, 0, "", 1);// 337
			AddComplexComponent( (BaseAddon) this, 991, -5, -3, 20, 0, 0, "", 1);// 338
			AddComplexComponent( (BaseAddon) this, 991, 0, -3, 20, 0, 0, "", 1);// 349

		}

		public GuardPost1Addon( Serial serial ) : base( serial )
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

	public class GuardPost1AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new GuardPost1Addon();
			}
		}

		[Constructable]
		public GuardPost1AddonDeed()
		{
			Name = "GuardPost1";
		}

		public GuardPost1AddonDeed( Serial serial ) : base( serial )
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