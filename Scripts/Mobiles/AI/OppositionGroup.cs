using System;
using Server;
using Server.Mobiles;

namespace Server
{
	public class OppositionGroup
	{
		private Type[][] m_Types;

		public OppositionGroup( Type[][] types )
		{
			m_Types = types;
		}

		public bool IsEnemy( object from, object target )
		{
			int fromGroup = IndexOf( from );
			int targGroup = IndexOf( target );

			return ( fromGroup != -1 && targGroup != -1 && fromGroup != targGroup );
		}

		public int IndexOf( object obj )
		{
			if ( obj == null )
				return -1;

			Type type = obj.GetType();

			for ( int i = 0; i < m_Types.Length; ++i )
			{
				Type[] group = m_Types[i];

				bool contains = false;

				for ( int j = 0; !contains && j < group.Length; ++j )
					contains = ( type == group[j] );

				if ( contains )
					return i;
			}

			return -1;
		}

		private static OppositionGroup m_TerathansAndOphidians = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( TerathanAvenger ),
					typeof( TerathanDrone ),
					typeof( TerathanMatriarch ),
					typeof( TerathanWarrior )
				},
				new Type[]
				{
					typeof( OphidianArchmage ),
					typeof( OphidianKnight ),
					typeof( OphidianMage ),
					typeof( OphidianMatriarch ),
					typeof( OphidianWarrior )
				}
			} );

		public static OppositionGroup TerathansAndOphidians
		{
			get{ return m_TerathansAndOphidians; }
		}

		private static OppositionGroup m_SavagesAndOrcs = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( Orc ),
					typeof( OrcBomber ),
					typeof( OrcBrute ),
					typeof( OrcCaptain ),
					typeof( OrcishLord ),
					typeof( OrcishMage ),
					typeof( SpawnedOrcishLord ),
					typeof( OrcChopper ),
					typeof( OrcScout )
				},
				new Type[]
				{
					typeof( Savage ),
					typeof( SavageRider ),
					typeof( SavageRidgeback ),
					typeof( SavageShaman )
				}
			} );

		public static OppositionGroup SavagesAndOrcs
		{
			get{ return m_SavagesAndOrcs; }
		}
		
		private static OppositionGroup m_FeyAndUndead = new OppositionGroup( new Type[][]
			{
				new Type[]
				{
					typeof( Centaur ),
					typeof( EtherealWarrior ),
					typeof( Kirin ),
					typeof( LordOaks ),
					typeof( Pixie ),
					typeof( Silvani ),
					typeof( Unicorn ),
					typeof( Wisp ),
					#region Mondain's Legacy
					typeof( Treefellow ),
				    typeof( MLDryad ),
				    #endregion
                    typeof( Twaulo ),
                    typeof( FeralTreefellow )
                },
				new Type[]
				{
					typeof( AncientLich ),
					typeof( Bogle ),
					typeof( LichLord ),
					typeof( Shade ),
					typeof( Spectre ),
					typeof( Wraith ),
					typeof( BoneKnight ),
					typeof( Ghoul ),
					typeof( Mummy ),
					typeof( SkeletalKnight ),
					typeof( Skeleton ),
					typeof( Zombie ),
					typeof( ShadowKnight ),
					typeof( DarknightCreeper ),
					typeof( RevenantLion ),
					typeof( LadyOfTheSnow ),
					typeof( RottingCorpse ),
					typeof( SkeletalDragon ),
					typeof( Lich ),
				    typeof( GiantBlackWidow ),

//This following block is part of Starlazer's Vampire Pack1
	        			typeof( Vampire0 ),
	        			typeof( Vampire1 ),
	        			typeof( Vampire2 ),
	        			typeof( Vampire3 ),
	        			typeof( Vampire4 ),
	        			typeof( Vampire5 ),
//The above block is part of Starlazer's Vampire Pack1

				    #region Mondain's Legacy
                	typeof( Gnaw ),
					typeof( LadyLissith ),
					typeof( Guile ),
					typeof( LadySabrix ),
					typeof( Silk ),
					typeof( Virulent ),
					typeof( Irk ),
					typeof( Malefic ),
					typeof( Spite ),
				    typeof( DireWolf ),
                    typeof( Quagmire ),
                    typeof( SwampTentacle ),
                    typeof( WhippingVine ),
                    typeof( Corpser ),
                    typeof( Changeling )
                    #endregion
                }
			} );

		public static OppositionGroup FeyAndUndead
		{
			get{ return m_FeyAndUndead; }
		}
	}
}