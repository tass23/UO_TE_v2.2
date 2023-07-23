
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
	public class RainbowTamer3Addon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {8636, -7, -2, 1}, {8636, -11, -4, 1}, {4109, -10, -5, 0}// 1	2	3	
			, {1395, -11, -4, 0}, {1395, -11, 3, 0}, {8636, -9, -1, 1}// 4	5	6	
			, {351, -13, 2, 0}, {8636, -9, 0, 1}, {8636, -11, 2, 1}// 7	8	9	
			, {8636, -8, 2, 1}, {8636, -7, 1, 1}, {351, -13, -1, 0}// 10	11	12	
			, {8636, -8, 1, 1}, {3892, -12, -3, 0}, {8636, -6, -1, 1}// 13	14	15	
			, {1392, -12, -5, 0}, {8636, -11, -2, 1}, {8636, -12, 1, 1}// 16	17	18	
			, {2148, -10, 0, 0}, {8636, -8, 3, 1}, {8636, -6, 0, 1}// 19	20	21	
			, {8636, -9, 4, 1}, {2147, -7, -4, 0}, {8636, -12, 2, 1}// 22	23	24	
			, {8636, -8, -5, 1}, {351, -13, -2, 0}, {2147, -5, -4, 0}// 25	26	27	
			, {8636, -8, -4, 1}, {4014, -12, -1, 0}, {8636, -9, 2, 1}// 28	29	30	
			, {8636, -7, 4, 1}, {5193, -11, -3, 0}, {2147, -8, -4, 0}// 31	32	33	
			, {11599, -12, 3, 5}, {1395, -11, 1, 0}, {8636, -9, 3, 1}// 34	35	36	
			, {1395, -10, 0, 0}, {1395, -7, -4, 0}, {352, -9, -7, 0}// 37	38	39	
			, {1392, -12, 0, 0}, {1394, -12, -6, 0}, {352, -7, -7, 0}// 40	41	42	
			, {1395, -10, 2, 0}, {1393, -5, -6, 0}, {8636, -6, -4, 1}// 43	44	45	
			, {8636, -12, 0, 1}, {351, -13, -3, 0}, {351, -13, 1, 0}// 46	47	48	
			, {352, -6, -7, 0}, {351, -13, -4, 0}, {352, -8, -7, 0}// 49	50	51	
			, {351, -13, -6, 0}, {8636, -9, -6, 1}, {1392, -12, -4, 0}// 52	53	54	
			, {8636, -6, -3, 1}, {1393, -6, -6, 0}, {1393, -7, -6, 0}// 55	56	57	
			, {8636, -7, 3, 1}, {8636, -10, -4, 1}, {8636, -8, 0, 1}// 58	59	60	
			, {1392, -12, -1, 0}, {1393, -8, -6, 0}, {1398, -6, -5, 0}// 61	62	63	
			, {2147, -12, 2, 0}, {352, -5, -7, 0}, {1395, -10, 1, 0}// 64	65	66	
			, {352, -12, -7, 0}, {1395, -11, 4, 0}, {3707, -9, -3, 0}// 67	68	69	
			, {2451, -5, -6, 2}, {1395, -11, -3, 0}, {8636, -12, -4, 1}// 70	71	72	
			, {1395, -10, 4, 0}, {1393, -9, -6, 0}, {1392, -12, 4, 0}// 73	74	75	
			, {1393, -10, -6, 0}, {1395, -11, 2, 0}, {1393, -11, -6, 0}// 76	77	78	
			, {8636, -10, -6, 1}, {3892, -11, 4, 0}, {8636, -6, -6, 1}// 79	80	81	
			, {2147, -8, 3, 0}, {8636, -6, -5, 1}, {2147, -7, 3, 0}// 82	83	84	
			, {8636, -7, -6, 1}, {1392, -12, 2, 0}, {3892, -7, 0, 0}// 85	86	87	
			, {1395, -8, -4, 0}, {8636, -7, -5, 1}, {8636, -9, -4, 1}// 88	89	90	
			, {8636, -11, 4, 1}, {8636, -11, 1, 1}, {8636, -9, -3, 1}// 91	92	93	
			, {2149, -13, 2, 0}, {8636, -11, 0, 1}, {1395, -11, -1, 0}// 94	95	96	
			, {8636, -10, -3, 1}, {8636, -11, -1, 1}, {1392, -12, 3, 0}// 97	98	99	
			, {3892, -5, 1, 0}, {1392, -12, -2, 0}, {2149, -13, 0, 0}// 100	101	102	
			, {8636, -12, 4, 1}, {1395, -10, -4, 0}, {8636, -10, 4, 1}// 103	104	105	
			, {8636, -7, 2, 1}, {8636, -11, -3, 1}, {8636, -11, -6, 1}// 106	107	108	
			, {2148, -10, 1, 0}, {1395, -10, 3, 0}, {7595, -8, -6, 0}// 109	110	111	
			, {8636, -11, 3, 1}, {1395, -9, -4, 0}, {1395, -5, 4, 0}// 112	113	114	
			, {1395, -10, -2, 0}, {351, -13, 4, 0}, {1392, -12, 1, 0}// 115	116	117	
			, {1395, -11, -2, 0}, {1398, -5, -5, 0}, {2148, -10, -1, 0}// 118	119	120	
			, {8636, -8, 4, 1}, {1395, -10, -3, 0}, {1395, -11, 0, 0}// 121	122	123	
			, {352, -11, -7, 0}, {351, -13, 0, 0}, {352, -10, -7, 0}// 124	125	126	
			, {353, -13, -7, 0}, {1395, -7, 4, 0}, {351, -13, 3, 0}// 127	128	129	
			, {1395, -5, -4, 0}, {1395, -8, 4, 0}, {1395, -6, 4, 0}// 130	131	132	
			, {8636, -10, -5, 1}, {1395, -6, -4, 0}, {8636, -7, 0, 1}// 133	134	135	
			, {8636, -11, -5, 1}, {4109, -11, -4, 0}, {8636, -12, -2, 1}// 136	137	138	
			, {8636, -9, 1, 1}, {8636, -12, -6, 1}, {2147, -12, -3, 0}// 139	140	141	
			, {8636, -12, -1, 1}, {8636, -12, -3, 1}, {1395, -9, 4, 0}// 142	143	144	
			, {3000, -12, -1, 0}, {8636, -12, 3, 1}, {1392, -12, -3, 0}// 145	146	147	
			, {3720, -8, -6, 5}, {1395, -10, -1, 0}, {1398, -8, -5, 0}// 148	149	150	
			, {3894, -12, -4, 0}, {8636, -12, -5, 1}, {8636, -7, -1, 1}// 151	152	153	
			, {8636, -10, 0, 1}, {2147, -6, -4, 0}, {7596, -7, -6, 0}// 154	155	156	
			, {5193, -7, 2, 0}, {1398, -9, -5, 0}, {3894, -11, -5, 0}// 157	158	159	
			, {8636, -10, -1, 1}, {8636, -9, -2, 1}, {5193, -5, -1, 0}// 160	161	162	
			, {1398, -7, -5, 0}, {8636, -6, -2, 1}, {3892, -9, 1, 0}// 163	164	165	
			, {5933, -11, 4, 0}, {5931, -11, 4, 0}, {3817, -6, -6, 2}// 166	167	168	
			, {4104, -7, -6, 2}, {8636, -5, 4, 1}, {8636, -5, 3, 1}// 169	170	171	
			, {8636, -5, 2, 1}, {8636, -5, 1, 1}, {8636, -5, 0, 1}// 172	173	174	
			, {8636, -5, -1, 1}, {8636, -5, -2, 1}, {8636, -5, -3, 1}// 175	176	177	
			, {8636, -5, -4, 1}, {8636, -5, -5, 1}, {8636, -5, -6, 1}// 178	179	180	
			, {8636, -6, 4, 1}, {8636, -6, 3, 1}, {8636, -6, 2, 1}// 181	182	183	
			, {8636, -8, -2, 1}, {3892, -10, -4, 0}, {3892, -9, -6, 0}// 184	185	186	
			, {3893, -7, -5, 0}, {2149, -13, -3, 0}, {3894, -11, -6, 0}// 187	188	189	
			, {1398, -11, -5, 0}, {2147, -5, 3, 0}, {351, -13, -5, 0}// 190	191	192	
			, {8636, -6, 1, 1}, {7595, -6, -6, 0}, {7596, -9, -6, 0}// 193	194	195	
			, {8636, -8, -6, 1}, {8636, -7, -3, 1}, {2451, -12, 4, 1}// 196	197	198	
			, {3894, -12, -6, 0}, {1398, -10, -5, 0}, {8636, -10, 2, 1}// 199	200	201	
			, {8636, -10, 1, 1}, {5193, -12, -5, 0}, {7596, -5, -6, 0}// 202	203	204	
			, {7869, -11, -5, 0}, {2148, -10, 2, 0}, {2148, -10, -2, 0}// 205	206	207	
			, {3893, -7, -2, 0}, {8636, -8, -1, 1}, {8636, -8, -3, 1}// 208	209	210	
			, {4014, -12, 3, 0}, {4014, -12, -2, 0}, {8636, -7, -4, 1}// 211	212	213	
			, {3894, -12, -5, 0}, {8636, -9, -5, 1}, {2147, -6, 3, 0}// 214	215	216	
			, {8636, -10, -2, 1}, {2149, -10, -4, 0}, {8636, -10, 3, 1}// 217	218	219	
			, {5453, -12, 1, 0}, {3894, -10, -6, 0}, {2147, -12, 0, 0}// 220	221	222	
			, {12788, -9, -3, 0}, {12788, -9, -2, 0}, {12788, -9, -1, 0}// 223	224	225	
			, {12788, -9, 0, 0}, {12788, -9, 1, 0}, {12788, -9, 2, 0}// 226	227	228	
			, {12788, -9, 3, 0}, {12788, -8, -3, 0}, {12788, -8, -2, 0}// 229	230	231	
			, {12788, -8, -1, 0}, {12788, -8, 0, 0}, {12788, -8, 1, 0}// 232	233	234	
			, {12788, -8, 2, 0}, {12788, -8, 3, 0}, {12788, -7, -3, 0}// 235	236	237	
			, {12788, -7, -2, 0}, {12788, -7, -1, 0}, {12788, -7, 0, 0}// 238	239	240	
			, {12788, -7, 1, 0}, {12788, -7, 2, 0}, {12788, -7, 3, 0}// 241	242	243	
			, {12788, -6, -3, 0}, {12788, -6, -2, 0}, {12788, -6, -1, 0}// 244	245	246	
			, {12788, -6, 0, 0}, {12788, -6, 1, 0}, {12788, -6, 2, 0}// 247	248	249	
			, {12788, -6, 3, 0}, {12788, -5, -3, 0}, {12788, -5, -2, 0}// 250	251	252	
			, {12788, -5, -1, 0}, {12788, -5, 0, 0}, {12788, -5, 1, 0}// 253	254	255	
			, {12788, -5, 2, 0}, {12788, -5, 3, 0}, {2147, -9, -4, 0}// 256	257	277	
			, {2148, -10, -3, 0}, {2148, -10, 3, 0}, {2147, -9, 3, 0}// 278	279	280	
			, {7595, -6, 4, 1}, {4979, -6, 4, 6}, {7596, -7, 4, 1}// 281	282	283	
			, {4980, -7, 4, 3}, {352, -5, 7, 0}, {352, -6, 7, 0}// 284	285	286	
			, {1392, -12, 5, 0}, {352, -7, 7, 0}, {8636, -10, 6, 1}// 287	288	289	
			, {352, -8, 7, 0}, {8636, -9, 6, 1}, {352, -9, 7, 0}// 290	291	292	
			, {8636, -8, 7, 1}, {8636, -12, 7, 1}, {8636, -7, 7, 1}// 293	294	295	
			, {1395, -10, 5, 0}, {1395, -11, 5, 0}, {8636, -11, 5, 1}// 296	297	298	
			, {352, -10, 7, 0}, {1395, -5, 5, 0}, {8636, -7, 6, 1}// 299	300	301	
			, {8636, -10, 7, 1}, {1395, -7, 5, 0}, {1389, -6, 7, 0}// 302	303	304	
			, {352, -12, 7, 0}, {352, -11, 7, 0}, {1395, -8, 6, 0}// 305	306	307	
			, {5193, -10, 5, 0}, {351, -13, 5, 0}, {1395, -10, 6, 0}// 308	309	310	
			, {8636, -8, 5, 1}, {3892, -7, 5, 0}, {8636, -7, 5, 1}// 311	312	313	
			, {8636, -12, 5, 1}, {351, -13, 7, 0}, {1395, -9, 6, 0}// 314	315	316	
			, {1392, -12, 6, 0}, {1390, -12, 7, 0}, {1389, -5, 7, 0}// 317	318	319	
			, {1389, -7, 7, 0}, {1389, -8, 7, 0}, {1389, -9, 7, 0}// 320	321	322	
			, {1389, -10, 7, 0}, {1389, -11, 7, 0}, {8636, -8, 6, 1}// 323	324	325	
			, {8636, -11, 6, 1}, {1395, -9, 5, 0}, {8636, -11, 7, 1}// 326	327	328	
			, {351, -13, 6, 0}, {1395, -6, 6, 0}, {1395, -6, 5, 0}// 329	330	331	
			, {1395, -7, 6, 0}, {8636, -9, 5, 1}, {1395, -5, 6, 0}// 332	333	334	
			, {1395, -11, 6, 0}, {1395, -8, 5, 0}, {8636, -5, 7, 1}// 335	336	337	
			, {8636, -5, 6, 1}, {8636, -5, 5, 1}, {8636, -6, 7, 1}// 338	339	340	
			, {8636, -6, 6, 1}, {8636, -6, 5, 1}, {8636, -10, 5, 1}// 341	342	343	
			, {8636, -9, 7, 1}, {8636, -12, 6, 1}, {492, 3, 2, 5}// 344	345	346	
			, {361, 7, -2, 0}, {1393, 5, -1, 5}, {8512, 7, 1, 0}// 347	348	349	
			, {1395, 0, 4, 0}, {2147, -4, -4, 0}, {362, 11, -5, 0}// 351	352	353	
			, {363, 7, -5, 0}, {1395, 3, 0, 5}, {2160, -3, 0, 1}// 354	355	356	
			, {362, 8, -5, 0}, {388, 7, 3, 0}, {5109, -4, 2, 4}// 357	358	359	
			, {1395, -1, -2, 0}, {1395, -1, -4, 0}, {1395, -2, 2, 0}// 360	361	362	
			, {352, 0, -7, 0}, {1393, 6, -1, 5}, {361, 7, -3, 0}// 363	364	365	
			, {4075, 11, -1, 0}, {8512, 7, 3, 0}, {1395, -2, 4, 0}// 366	367	368	
			, {362, 2, -2, 0}, {1395, 5, 0, 5}, {351, 1, 3, 0}// 369	370	371	
			, {1393, -4, -6, 0}, {1398, -4, -5, 0}, {352, -3, -7, 0}// 372	373	374	
			, {1395, -1, 1, 0}, {351, 1, -2, 0}, {4072, 10, -1, 0}// 375	376	377	
			, {1389, 6, 2, 5}, {4076, 10, 1, 0}, {492, 5, 2, 5}// 378	379	380	
			, {1395, -1, 3, 0}, {351, 1, -6, 0}, {352, 1, -7, 0}// 381	382	383	
			, {1902, 7, -1, 0}, {1395, -3, 4, 0}, {1395, 0, 0, 0}// 384	385	386	
			, {1395, 5, 1, 5}, {1393, 10, -4, 0}, {3893, -3, -3, 0}// 387	389	390	
			, {1393, -1, -6, 0}, {362, 9, -5, 0}, {1395, 0, 3, 0}// 391	392	393	
			, {2146, -3, 3, 0}, {362, 10, -5, 0}, {492, 3, -2, 5}// 394	395	396	
			, {1395, -2, 3, 0}, {1393, 3, -1, 5}, {8512, 7, 2, 0}// 397	398	399	
			, {1902, 7, 0, 0}, {1393, 0, -6, 0}, {1393, -2, -6, 0}// 400	401	402	
			, {1393, 9, -4, 0}, {1393, -3, -6, 0}, {351, 1, 4, 0}// 403	404	405	
			, {1395, -1, 4, 0}, {1387, 1, -3, 0}, {1395, -1, -3, 0}// 406	407	408	
			, {1395, 0, -1, 0}, {388, 2, 3, 0}, {8512, 7, 0, 0}// 409	410	411	
			, {1395, -4, 4, 0}, {1395, 0, 1, 0}, {1387, 1, -4, 0}// 412	413	414	
			, {1395, 0, 2, 0}, {492, 6, -2, 5}, {1387, 1, 4, 0}// 415	416	417	
			, {361, 7, 3, 0}, {1395, -4, -4, 0}, {1395, 4, 0, 5}// 418	420	421	
			, {1395, -2, -4, 0}, {1395, -2, -3, 0}, {351, 1, -3, 0}// 422	423	424	
			, {1395, -2, 1, 0}, {1395, -1, 2, 0}, {1395, 0, -3, 0}// 425	426	427	
			, {1395, 1, 1, 0}, {352, -1, -7, 0}, {1392, 8, -2, 0}// 428	429	430	
			, {1395, -3, -4, 0}, {1370, 1, -1, 0}, {352, -2, -7, 0}// 431	432	433	
			, {1389, 4, 2, 5}, {1395, 1, 0, 0}, {1393, 11, -4, 0}// 434	435	436	
			, {1394, 8, -4, 0}, {1395, 8, 1, 0}, {1395, 8, 0, 0}// 437	438	439	
			, {1395, 8, 2, 0}, {1395, 8, -1, 0}, {1395, 9, -2, 0}// 440	441	442	
			, {1395, 9, -1, 0}, {1395, 9, 0, 0}, {1395, 9, 1, 0}// 443	444	445	
			, {1395, 9, 2, 0}, {1395, 9, 3, 0}, {1395, 10, -2, 0}// 446	447	448	
			, {1395, 10, -1, 0}, {1395, 10, 0, 0}, {1395, 10, 1, 0}// 449	450	451	
			, {1395, 10, 2, 0}, {1395, 10, 3, 0}, {1395, 11, -2, 0}// 452	453	454	
			, {1395, 11, -1, 0}, {1395, 11, 0, 0}, {1395, 11, 1, 0}// 455	456	457	
			, {1395, 11, 2, 0}, {1395, 11, 3, 0}, {1387, 1, -5, 0}// 458	459	461	
			, {1398, -2, -5, 0}, {3893, -3, 0, 0}, {352, -4, -7, 0}// 462	463	464	
			, {1395, -1, -1, 0}, {1395, -1, 0, 0}, {1387, 1, -2, 0}// 465	466	467	
			, {1395, 0, -4, 0}, {1902, 7, 2, 0}, {351, 1, -5, 0}// 468	469	470	
			, {1395, -2, 0, 0}, {1395, 0, -2, 0}, {1392, 8, 3, 0}// 471	472	473	
			, {1395, -2, -2, 0}, {1395, -2, -1, 0}, {1388, 1, -6, 0}// 474	475	476	
			, {1904, 2, -1, 0}, {361, 7, 4, 0}, {4074, 10, 0, 0}// 477	479	481	
			, {1395, 6, 0, 5}, {1395, 1, 3, 1}, {4070, 9, 0, 0}// 482	484	486	
			, {7685, -3, 0, 1}, {7685, -2, 0, 0}, {1395, 6, 1, 5}// 487	488	490	
			, {8512, 2, 0, 0}, {8512, 2, 1, 0}, {8512, 2, 2, 0}// 491	492	493	
			, {8512, 2, -1, 0}, {4077, 11, 1, 0}, {8636, 1, 4, 1}// 494	495	496	
			, {8636, 1, 3, 1}, {8636, 1, 2, 1}, {8636, 1, 1, 1}// 497	498	499	
			, {8636, 1, 0, 1}, {8636, 1, -1, 1}, {8636, 1, -2, 1}// 500	501	502	
			, {8636, 1, -3, 1}, {8636, 1, -4, 1}, {8636, 1, -5, 1}// 503	504	505	
			, {8636, 1, -6, 1}, {8636, 0, 4, 1}, {8636, 0, 3, 1}// 506	507	508	
			, {8636, 0, 2, 1}, {8636, 0, 1, 1}, {8636, 0, 0, 1}// 509	510	511	
			, {8636, 0, -1, 1}, {8636, 0, -2, 1}, {8636, 0, -3, 1}// 512	513	514	
			, {8636, 0, -4, 1}, {8636, 0, -5, 1}, {8636, 0, -6, 1}// 515	516	517	
			, {8636, -1, 4, 1}, {8636, -1, 3, 1}, {8636, -1, 2, 1}// 518	519	520	
			, {8636, -1, 1, 1}, {8636, -1, 0, 1}, {8636, -1, -1, 1}// 521	522	523	
			, {8636, -1, -2, 1}, {8636, -1, -3, 1}, {8636, -1, -4, 1}// 524	525	526	
			, {8636, -1, -5, 1}, {8636, -1, -6, 1}, {8636, -2, 4, 1}// 527	528	529	
			, {8636, -2, 3, 1}, {8636, -2, 2, 1}, {8636, -2, 1, 1}// 530	531	532	
			, {8636, -2, 0, 1}, {8636, -2, -1, 1}, {8636, -2, -2, 1}// 533	534	535	
			, {8636, -2, -3, 1}, {8636, -2, -4, 1}, {8636, -2, -5, 1}// 536	537	538	
			, {8636, -2, -6, 1}, {8636, -3, 4, 1}, {8636, -3, 3, 1}// 539	540	541	
			, {8636, -3, 2, 1}, {8636, -3, 1, 1}, {8636, -3, 0, 1}// 542	543	544	
			, {8636, -3, -1, 1}, {8636, -3, -2, 1}, {8636, -3, -3, 1}// 545	546	547	
			, {8636, -3, -4, 1}, {8636, -3, -5, 1}, {8636, -3, -6, 1}// 548	549	550	
			, {8636, -4, 4, 1}, {8636, -4, 3, 1}, {8636, -4, 2, 1}// 551	552	553	
			, {8636, -4, 1, 1}, {8636, -4, 0, 1}, {8636, -4, -1, 1}// 554	555	556	
			, {8636, -4, -2, 1}, {8636, -4, -3, 1}, {8636, -4, -4, 1}// 557	558	559	
			, {8636, -4, -5, 1}, {8636, -4, -6, 1}, {2148, -3, -2, 0}// 560	561	562	
			, {2149, -3, 1, 0}, {1902, 7, 1, 0}, {492, 4, 2, 5}// 563	564	565	
			, {2168, -3, -1, 0}, {8512, 7, -1, 0}, {7595, -4, -6, 0}// 566	567	568	
			, {4073, 9, 1, 0}, {492, 6, 2, 5}, {4078, 11, 0, 0}// 569	570	571	
			, {362, 7, -2, 0}, {1395, 3, 1, 5}, {351, 1, -4, 0}// 572	573	574	
			, {1398, -3, -5, 0}, {3893, -4, 4, 0}, {1398, 0, -5, 0}// 575	577	578	
			, {1395, 1, 2, 1}, {1904, 2, 0, 0}, {1389, 5, 2, 5}// 579	580	581	
			, {492, 5, -2, 5}, {1398, -1, -5, 0}, {1389, 3, 2, 5}// 582	583	584	
			, {4071, 9, -1, 0}, {2168, -3, 1, 0}, {1904, 2, 1, 0}// 585	586	587	
			, {1904, 2, 2, 0}, {1393, 4, -1, 5}, {1395, 4, 1, 5}// 588	589	590	
			, {492, 4, -2, 5}, {1392, 8, 4, 0}, {1392, 8, -3, 0}// 592	594	595	
			, {1395, 9, -3, 0}, {1395, 10, -3, 0}, {1395, 11, -3, 0}// 596	597	598	
			, {1395, 9, 4, 0}, {1395, 10, 4, 0}, {1395, 11, 4, 0}// 599	600	601	
			, {361, 7, -4, 0}, {12788, -4, -3, 0}, {12788, -4, -2, 0}// 602	603	604	
			, {12788, -4, -1, 0}, {12788, -4, 0, 0}, {12788, -4, 1, 0}// 605	606	607	
			, {12788, -4, 2, 0}, {12788, -4, 3, 0}, {12788, -3, -3, 0}// 608	609	610	
			, {12788, -3, -2, 0}, {12788, -3, -1, 0}, {12788, -3, 0, 0}// 611	612	613	
			, {12788, -3, 1, 0}, {12788, -3, 2, 0}, {12788, -3, 3, 0}// 614	615	616	
			, {2147, -3, -4, 0}, {2148, -3, -3, 0}, {2147, -4, 3, 0}// 630	631	632	
			, {2148, -3, 2, 0}, {352, -3, 7, 0}, {352, -4, 7, 0}// 633	634	635	
			, {3896, 0, 6, 1}, {1395, -2, 6, 0}, {1395, -4, 6, 0}// 636	637	638	
			, {1395, -3, 5, 0}, {1395, 0, 6, 0}, {1387, 1, 5, 0}// 639	640	641	
			, {351, 1, 6, 0}, {1387, 1, 6, 0}, {351, 1, 5, 0}// 642	643	644	
			, {1395, -4, 5, 0}, {1389, -1, 7, 0}, {1389, -2, 7, 0}// 645	646	647	
			, {1389, -3, 7, 0}, {1389, -4, 7, 0}, {1395, -3, 6, 0}// 648	649	650	
			, {352, -1, 7, 0}, {352, 0, 7, 0}, {352, -2, 7, 0}// 651	652	653	
			, {1395, 0, 5, 0}, {1395, -2, 5, 0}, {350, 1, 7, 0}// 654	655	656	
			, {1395, -1, 6, 0}, {1395, -1, 5, 0}, {1389, 0, 7, 0}// 657	658	659	
			, {1391, 1, 7, 0}, {3896, -1, 6, 1}, {3896, -2, 6, 1}// 660	661	662	
			, {8636, 1, 7, 1}, {8636, 1, 6, 1}, {8636, 1, 5, 1}// 663	664	665	
			, {8636, 0, 7, 1}, {8636, 0, 6, 1}, {8636, 0, 5, 1}// 666	667	668	
			, {8636, -1, 7, 1}, {8636, -1, 6, 1}, {8636, -1, 5, 1}// 669	670	671	
			, {8636, -2, 7, 1}, {8636, -2, 6, 1}, {8636, -2, 5, 1}// 672	673	674	
			, {8636, -3, 7, 1}, {8636, -3, 6, 1}, {8636, -3, 5, 1}// 675	676	677	
			, {8636, -4, 7, 1}, {8636, -4, 6, 1}, {8636, -4, 5, 1}// 678	679	680	
			, {362, 8, 5, 0}, {362, 9, 5, 0}, {362, 10, 5, 0}// 681	682	683	
			, {362, 11, 5, 0}, {1390, 8, 5, 0}, {1389, 9, 5, 0}// 684	685	686	
			, {1389, 10, 5, 0}, {1389, 11, 5, 0}, {361, 7, 5, 0}// 687	688	689	
			, {1387, 13, -1, 0}, {362, 12, -5, 0}, {1387, 13, 3, 0}// 690	691	693	
			, {1388, 13, -4, 0}, {1387, 13, -2, 0}, {1387, 13, 0, 0}// 694	695	696	
			, {1387, 13, 2, 0}, {1393, 12, -4, 0}, {361, 13, 2, 0}// 697	698	699	
			, {361, 13, 1, 0}, {361, 13, -1, 0}, {361, 13, -2, 0}// 700	701	702	
			, {361, 13, -3, 0}, {1395, 12, -2, 0}, {1395, 12, -1, 0}// 703	705	706	
			, {1395, 12, 0, 0}, {1395, 12, 1, 0}, {1395, 12, 2, 0}// 707	708	709	
			, {1395, 12, 3, 0}, {361, 13, 0, 0}, {1387, 13, 1, 0}// 710	711	712	
			, {361, 13, 3, 0}, {362, 13, -5, 0}, {1387, 13, 4, 0}// 713	714	717	
			, {1387, 13, -3, 0}, {1395, 12, -3, 0}, {1395, 12, 4, 0}// 718	719	720	
			, {361, 13, -4, 0}, {361, 13, 4, 0}, {362, 12, 5, 0}// 721	722	723	
			, {360, 13, 5, 0}, {1389, 12, 5, 0}, {1391, 13, 5, 0}// 724	725	726	
					};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new RainbowTamer3AddonDeed();
			}
		}

		[ Constructable ]
		public RainbowTamer3Addon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 12960, -10, 4, 0, 1854, -1, "", 1);// 258
			AddComplexComponent( (BaseAddon) this, 12972, -9, 4, 0, 1854, -1, "", 1);// 259
			AddComplexComponent( (BaseAddon) this, 12972, -7, 4, 0, 1854, -1, "", 1);// 260
			AddComplexComponent( (BaseAddon) this, 12972, -6, 4, 0, 1854, -1, "", 1);// 261
			AddComplexComponent( (BaseAddon) this, 12973, -5, 4, 0, 1854, -1, "", 1);// 262
			AddComplexComponent( (BaseAddon) this, 12973, -8, 4, 0, 1854, -1, "", 1);// 263
			AddComplexComponent( (BaseAddon) this, 12974, -9, -4, 0, 1854, -1, "", 1);// 264
			AddComplexComponent( (BaseAddon) this, 12974, -8, -4, 0, 1854, -1, "", 1);// 265
			AddComplexComponent( (BaseAddon) this, 12974, -6, -4, 0, 1854, -1, "", 1);// 266
			AddComplexComponent( (BaseAddon) this, 12974, -5, -4, 0, 1854, -1, "", 1);// 267
			AddComplexComponent( (BaseAddon) this, 12975, -7, -4, 0, 1854, -1, "", 1);// 268
			AddComplexComponent( (BaseAddon) this, 12968, -10, -4, 0, 1854, -1, "", 1);// 269
			AddComplexComponent( (BaseAddon) this, 12970, -10, -3, 0, 1854, -1, "", 1);// 270
			AddComplexComponent( (BaseAddon) this, 12970, -10, -1, 0, 1854, -1, "", 1);// 271
			AddComplexComponent( (BaseAddon) this, 12970, -10, 0, 0, 1854, -1, "", 1);// 272
			AddComplexComponent( (BaseAddon) this, 12970, -10, 1, 0, 1854, -1, "", 1);// 273
			AddComplexComponent( (BaseAddon) this, 12970, -10, 3, 0, 1854, -1, "", 1);// 274
			AddComplexComponent( (BaseAddon) this, 12971, -10, -2, 0, 1854, -1, "", 1);// 275
			AddComplexComponent( (BaseAddon) this, 12971, -10, 2, 0, 1854, -1, "", 1);// 276
			AddComplexComponent( (BaseAddon) this, 6571, 8, -4, 7, 0, 1, "", 1);// 350
			AddComplexComponent( (BaseAddon) this, 14713, 2, 2, 0, 0, 0, "", 1);// 388
			AddComplexComponent( (BaseAddon) this, 14678, 1, -1, 0, 0, 1, "", 1);// 419
			AddComplexComponent( (BaseAddon) this, 6571, 8, 4, 7, 0, 1, "", 1);// 460
			AddComplexComponent( (BaseAddon) this, 3633, 8, -4, 0, 0, 1, "", 1);// 478
			AddComplexComponent( (BaseAddon) this, 14713, 2, -1, 0, 0, 0, "", 1);// 480
			AddComplexComponent( (BaseAddon) this, 3633, 8, 4, 0, 0, 1, "", 1);// 483
			AddComplexComponent( (BaseAddon) this, 14678, 1, 2, 0, 0, 1, "", 1);// 485
			AddComplexComponent( (BaseAddon) this, 14713, 2, 1, 0, 0, 0, "", 1);// 489
			AddComplexComponent( (BaseAddon) this, 14678, 1, 1, 0, 0, 1, "", 1);// 576
			AddComplexComponent( (BaseAddon) this, 14713, 2, 0, 0, 0, 0, "", 1);// 591
			AddComplexComponent( (BaseAddon) this, 14678, 1, 0, 0, 0, 1, "", 1);// 593
			AddComplexComponent( (BaseAddon) this, 12958, -2, -4, 0, 1854, -1, "", 1);// 617
			AddComplexComponent( (BaseAddon) this, 12961, -2, 4, 0, 1854, -1, "", 1);// 618
			AddComplexComponent( (BaseAddon) this, 12972, -4, 4, 0, 1854, -1, "", 1);// 619
			AddComplexComponent( (BaseAddon) this, 12972, -3, 4, 0, 1854, -1, "", 1);// 620
			AddComplexComponent( (BaseAddon) this, 12974, -3, -4, 0, 1854, -1, "", 1);// 621
			AddComplexComponent( (BaseAddon) this, 12975, -4, -4, 0, 1854, -1, "", 1);// 622
			AddComplexComponent( (BaseAddon) this, 12976, -2, -3, 0, 1854, -1, "", 1);// 623
			AddComplexComponent( (BaseAddon) this, 12976, -2, -1, 0, 1854, -1, "", 1);// 624
			AddComplexComponent( (BaseAddon) this, 12976, -2, 0, 0, 1854, -1, "", 1);// 625
			AddComplexComponent( (BaseAddon) this, 12976, -2, 1, 0, 1854, -1, "", 1);// 626
			AddComplexComponent( (BaseAddon) this, 12976, -2, 3, 0, 1854, -1, "", 1);// 627
			AddComplexComponent( (BaseAddon) this, 12977, -2, -2, 0, 1854, -1, "", 1);// 628
			AddComplexComponent( (BaseAddon) this, 12977, -2, 2, 0, 1854, -1, "", 1);// 629
			AddComplexComponent( (BaseAddon) this, 3633, 12, 4, 0, 0, 1, "", 1);// 692
			AddComplexComponent( (BaseAddon) this, 6571, 12, -4, 7, 0, 1, "", 1);// 704
			AddComplexComponent( (BaseAddon) this, 3633, 12, -4, 0, 0, 1, "", 1);// 715
			AddComplexComponent( (BaseAddon) this, 6571, 12, 4, 7, 0, 1, "", 1);// 716

		}

		public RainbowTamer3Addon( Serial serial ) : base( serial )
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

	public class RainbowTamer3AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new RainbowTamer3Addon();
			}
		}

		[Constructable]
		public RainbowTamer3AddonDeed()
		{
			Name = "RainbowTamer3";
		}

		public RainbowTamer3AddonDeed( Serial serial ) : base( serial )
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