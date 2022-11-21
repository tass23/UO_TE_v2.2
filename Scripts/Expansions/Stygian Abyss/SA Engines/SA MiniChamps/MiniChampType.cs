using System;
using Server;
using Server.Mobiles;

namespace Server.Engines.SAMiniChamps
{
	public enum MiniChampType
	{
		SecretGarden,
		StygianDragonLair,
		CrimsonVeins,
		AbyssalLair,
		FireTemple,
		LandsOfTheLich,
		SkeletalDragon,
		EnslavedGoblins,
		LavaCaldera,
        PassageOfTears,
		ClanChitter,
        ClanRibbon,
		ClanScratch
	}

	public class MiniChampSpawnInfo
	{
		private string m_MiniName;
		private Type m_MiniChamp;
		private Type[][] m_MiniSpawnTypes;

		public string MiniName { get { return m_MiniName; } }
		public Type MiniChamp { get { return m_MiniChamp; } }
		public Type[][] MiniSpawnTypes { get { return m_MiniSpawnTypes; } }

		public MiniChampSpawnInfo( string name, Type minichamp, Type[][] minispawnTypes )
		{
			m_MiniName = name;
			m_MiniChamp = minichamp;
			m_MiniSpawnTypes = minispawnTypes;
		}

		public static MiniChampSpawnInfo[] Table{ get { return m_Table; } }

		private static readonly MiniChampSpawnInfo[] m_Table = new MiniChampSpawnInfo[]
			{
				new MiniChampSpawnInfo( "Secret Garden", typeof( PixieRenowned ), new Type[][]				// Secret Garden
				{																																		// Secret Garden
					new Type[]{ typeof( SAPixie )},																										// Level 1
					new Type[]{ typeof( Wisp )},																										// Level 2
					new Type[]{ typeof( DarkWisp )}																										// Level 3
				} ),
				new MiniChampSpawnInfo( "Stygian Dragon Lair", typeof( WyvernRenowned ), new Type[][]		// Stygian Dragon Lair
				{																																		// Stygian Dragon Lair
					new Type[]{ typeof( FairyDragon1 ) },																								// Level 1
					new Type[]{ typeof( Wyvern ) },																										// Level 2
					new Type[]{ typeof( ForgottenServant ) }																							// Level 3
				} ),
				new MiniChampSpawnInfo( "Crimson Veins", typeof( FireElementalRenowned ), new Type[][]		// Crimson Veins
				{																																		// Crimson Veins
					new Type[]{ typeof( LavaSnake ), typeof( LavaLizard ), typeof( FireAnt ) },															// Level 1
					new Type[]{ typeof( FireGargoyle ), typeof( Efreet ) },																				// Level 2
					new Type[]{ typeof( FireDaemon ), typeof( LavaElemental ) }																			// Level 3
				} ),
				new MiniChampSpawnInfo( "Abyssal Lair", typeof( DevourerRenowned ), new Type[][]			// Abyssal Lair
				{																																		// Abyssal Lair
					new Type[]{ typeof( GreaterMongbat ), typeof( Imp ) },																				// Level 1
					new Type[]{ typeof( Daemon ) },																										// Level 2
					new Type[]{ typeof( PitFiend ) }																									// Level 3
				} ),
				new MiniChampSpawnInfo( "Fire Temple", typeof( FireDaemonRenowned ), new Type[][]			// Fire Temple
				{																																		// Fire Temple
					new Type[]{ typeof( LavaSnake ), typeof( LavaLizard ), typeof( FireAnt ) },															// Level 1
					new Type[]{ typeof( LavaSerpent ), typeof( HellCat ), typeof( HellHound ) },														// Level 2
					new Type[]{ typeof( FireDaemon ), typeof( LavaElemental ) }																			// Level 3
				} ),
				new MiniChampSpawnInfo( "Lands Of The Lich", typeof( AncientLichRenowned ), new Type[][]	// Lands Of The Lich
				{																																		// Lands Of The Lich
					new Type[]{ typeof( Wraith ), typeof( Spectre ), typeof( Shade ), typeof( Skeleton ), typeof( Zombie ) },							// Level 1
					new Type[]{ typeof( BoneMagi ), typeof( SkeletalMage ), typeof( BoneKnight ), typeof( SkeletalKnight ), typeof( WailingBanshee )},	// Level 2
					new Type[]{ typeof( SkeletalLich ), typeof( RottingCorpse ) }																		// Level 3
				} ),
				new MiniChampSpawnInfo( "Skeletal Dragon", typeof( SkeletalDragonRenowned ), new Type[][]	// Skeletal Dragon
				{																																		// Skeletal Dragon
					new Type[]{ typeof( PatchworkSkeleton ), typeof( Skeleton ) },																		// Level 1
					new Type[]{ typeof( BoneKnight ), typeof( SkeletalKnight ) },																		// Level 2
					new Type[]{ typeof( BoneMagi ), typeof( SkeletalMage ) },																			// Level 3
					new Type[]{ typeof( SkeletalLich ) }																								// Level 4
				} ),
				new MiniChampSpawnInfo( "Enslaved Goblins", typeof( GrayGoblinMageRenowned ), new Type[][]		// Enslaved Goblins
				{																																		// Enslaved Goblins
					new Type[]{ typeof( EnslavedGrayGoblin ), typeof( EnslavedGreenGoblin ) },															// Level 1
					new Type[]{ typeof( EnslavedGoblinScout ), typeof( EnslavedGoblinKeeper ) },														// Level 2
					new Type[]{ typeof( EnslavedGoblinMage ), typeof( EnslavedGoblinAlchemist ) },														// Level 3
					new Type[]{ typeof( GreenGoblinAlchemistRenowned )}
				} ),
				new MiniChampSpawnInfo( "Lava Caldera", typeof( FireDaemonRenowned ), new Type[][]			// Lava Caldera
				{																																		// Lava Caldera
					new Type[]{ typeof( LavaSnake ), typeof( LavaLizard ), typeof( FireAnt ) },															// Level 1
					new Type[]{ typeof( LavaSerpent ), typeof( HellCat ), typeof( HellHound ) },														// Level 2
					new Type[]{ typeof( FireDaemon ), typeof( LavaElemental ) }																			// Level 3
				} ),
                new MiniChampSpawnInfo( "Passage Of Tears", typeof( AcidElementalRenowned ), new Type[][]	// Passage Of Tears
				{																																		// PassageOfTears
					new Type[]{ typeof( AcidSlug ), typeof( CorrosiveSlime ) },																			// Level 1
					new Type[]{ typeof( AcidElemental ) },																								// Level 2
					new Type[]{ typeof( InterredGrizzle )}																							// Level 3
				} ),
				new MiniChampSpawnInfo( "Clan Chitter", typeof( RakktaviRenowned ), new Type[][]			// Clan Chitter
				{																																		// Clan Chitter
					new Type[]{ typeof( ClockworkScorpion ), typeof( ClanChitterAssistant ) },															// Level 1
					new Type[]{ typeof( ClockworkScorpion ), typeof( ClanChitterTinkerer ) }															// Level 2
				} ),
				new MiniChampSpawnInfo( "Clan Ribbon", typeof( VitaviRenowned ), new Type[][]				// Clan Ribbon
				{																																		// Clan Ribbon
					new Type[]{ typeof( ClanRibbonPlagueRat ), typeof( ClanRibbonSupplicant ) },														// Level 1
					new Type[]{ typeof( ClanRibbonPlagueRat ), typeof( ClanRibbonCourtier ) }															// Level 2
				} ),
				new MiniChampSpawnInfo( "Clan Scratch", typeof( TikitaviRenowned ), new Type[][]			// Clan Scratch
				{																																		// Clan Scratch
					new Type[]{ typeof( ClanScratchSavageWolf ), typeof( ClanScratchScrounger ) },														// Level 1
					new Type[]{ typeof( ClanScratchSavageWolf ), typeof( ClanScratchHenchrat ) }														// Level 2
				} )
			};

		public static MiniChampSpawnInfo GetInfo( MiniChampType type )
		{
			int v = (int)type;

			if( v < 0 || v >= m_Table.Length )
				v = 0;

			return m_Table[v];
		}
	}
}