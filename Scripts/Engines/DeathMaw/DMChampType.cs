using System;
using Server;
using Server.Mobiles;
using Server.Engines.DMChamps;

namespace Server.Engines.DMChamps
{
	public enum DMChampType
	{
		EntryWing,
		SpiderWing,
		UnholyWing,
		DragonWing,
		FeyWing,
		ElementalWing,
		DemonWing
	}

	public class DMChampSpawnInfo
	{
		private string m_DMName;
		private Type m_DMChamp;
		private Type m_DMChamp2;
		private Type[][] m_DMSpawnTypes;

		public string DMName { get { return m_DMName; } }
		public Type DMChamp { get { return m_DMChamp; } }
		public Type DMChamp2 { get { return m_DMChamp2; } }
		public Type[][] DMSpawnTypes { get { return m_DMSpawnTypes; } }

		public DMChampSpawnInfo( string name, Type dmchamp, Type dmchamp2, Type[][] dmspawnTypes )
		{
			m_DMName = name;
			m_DMChamp = dmchamp;
			m_DMChamp2 = dmchamp2;
			m_DMSpawnTypes = dmspawnTypes;
		}

		public static DMChampSpawnInfo[] Table{ get { return m_Table; } }

		private static readonly DMChampSpawnInfo[] m_Table = new DMChampSpawnInfo[]
			{
				new DMChampSpawnInfo( "Entry Wing", typeof( Semidar2 ), typeof( Centrilla ), new Type[][]						// Entry Wing
				{																																		// Entry Wing
					new Type[]{ typeof( Gargoyle ), typeof( FireRockGargoyle )},																		// Level 1
					new Type[]{ typeof( GargoyleDestroyer ), typeof( GargoyleEnforcer )},																// Level 2
					new Type[]{ typeof( UndeadGargoyle ), typeof( StoneGargoyle )},																		// Level 3
				} ),
				new DMChampSpawnInfo( "Spider Wing", typeof( Mephitis2 ), typeof( WhiteWidow ), new Type[][]					// Spider Wing
				{																																		// Spider Wing
					new Type[]{ typeof( GiantSpider ), typeof( FrostSpider )},																			// Level 1
					new Type[]{ typeof( DreadSpider ), typeof( GiantSpider )},																			// Level 2
					new Type[]{ typeof( WolfSpider ), typeof( TrapdoorSpider )},																		// Level 3
				} ),
				new DMChampSpawnInfo( "Unholy Wing", typeof( Neira2 ), typeof( Pestilence ), new Type[][]						// Unholy Wing
				{																																		// Unholy Wing
					new Type[]{ typeof( Ghoul ), typeof( Lich )},																						// Level 1
					new Type[]{ typeof( Ghoul ), typeof( LichLord )},																					// Level 2
					new Type[]{ typeof( PrimevalLich2 ), typeof( Lich )}																				// Level 3
				} ),
				new DMChampSpawnInfo( "Dragon Wing", typeof( Rikktor2 ), typeof( Rocky ), new Type[][]							// Dragon Wing
				{																																		// Dragon Wing
					new Type[]{ typeof( GiantSerpent ), typeof( Coil )},																				// Level 1
					new Type[]{ typeof( Drake ), typeof( Dragon ) },																					// Level 2
					new Type[]{ typeof( GiantSerpent ), typeof( GreaterDragon)}																			// Level 3
				} ),
				new DMChampSpawnInfo( "Fey Wing", typeof( LordOaks2 ), typeof( Motarom ), new Type[][]							// Fey Wing
				{																																		// Fey Wing
					new Type[]{ typeof( Pixie ), typeof( Centaur )},																					// Level 1
					new Type[]{ typeof( DarkWisp ), typeof( Unicorn )},																					// Level 2
					new Type[]{ typeof( Pixie ), typeof( SerpentineDragon )}																			// Level 3
				} ),
				new DMChampSpawnInfo( "Elemental Wing", typeof( AbyssalInfernal2 ), typeof( TheHellishChampion ), new Type[][]	// Elemental Wing
				{																																		// Elemental Wing
					new Type[]{ typeof( Slime ), typeof( AcidSlug ), typeof( BloodElemental )},															// Level 1
					new Type[]{ typeof( EarthElemental ), typeof( EnragedEarthElemental )},																// Level 2
					new Type[]{ typeof( PoisonElemental ), typeof( GreaterPoisonElemental )}															// Level 3
				} ),
				new DMChampSpawnInfo( "Demon Wing", typeof( Harrower2 ), typeof( Daemon ), new Type[][]							// Demon Wing
				{																																		// Demon Wing
					new Type[]{ typeof( Daemon ), typeof( Balron )},																					// Level 1
					new Type[]{ typeof( Daemon ), typeof( BlackgateDaemon )},																			// Level 2
					new Type[]{ typeof( Daemon ), typeof( AbyssalAbomination2 )},																		// Level 3
				} ),
			};

		public static DMChampSpawnInfo GetInfo( DMChampType type )
		{
			int v = (int)type;

			if( v < 0 || v >= m_Table.Length )
				v = 0;

			return m_Table[v];
		}
	}
}