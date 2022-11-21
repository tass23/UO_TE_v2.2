using System;
using Server.Items;

namespace Server.Engines.Craft
{
	#region Mondain's Legacy
	public enum BowRecipes
	{		
		//magical
		BarbedLongbow 			= 200,
		SlayerLongbow			= 201,
		FrozenLongbow 			= 202,
		LongbowOfMight 			= 203,
		RangersShortbow 		= 204,
		LightweightShortbow		= 205,
		MysticalShortbow 		= 206,
		AssassinsShortbow 		= 207,
		
		// arties
		BlightGrippedLongbow 	= 250,
		FaerieFire 				= 251,
		SilvanisFeywoodBow 		= 252,
		MischiefMaker			= 253,
		TheNightReaper 			= 254,
	}
	#endregion

	public class DefBowFletching : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fletching; }
		}

		public override int GumpTitleNumber
		{
			get { return 1044006; } // <CENTER>BOWCRAFT AND FLETCHING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBowFletching();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefBowFletching() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		/*
		private static Type[] m_FletcherColorables = new Type[]
		{
			typeof( BaseRanged ), typeof( Kindling ),
			typeof( Shaft ), typeof( Arrow ),
			typeof( Bolt )
		};
		
		public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (type != typeof(Board) && type != typeof(Log))
                return false;

            type = item.ItemType;

            bool contains = false;

            for (int i = 0; !contains && i < m_FletcherColorables.Length; ++i)
            {
                if (m_FletcherColorables[i] == type)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
		*/
		public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            return true;
        }
		
		public override void PlayCraftEffect( Mobile from )
		{
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 33, 5, 1, true, false, 0 );

			from.PlaySound( 0x55 );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				if ( quality == 0 )
					return 502785; // You were barely able to make this item.  It's quality is below average.
				else if ( makersMark && quality == 2 )
					return 1044156; // You create an exceptional quality item and affix your maker's mark.
				else if ( quality == 2 )
					return 1044155; // You create an exceptional quality item.
				else				
					return 1044154; // You create the item.
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.FiftyPercentChanceMinusTenPercent; } }

		public override void InitCraftList()
		{
			int index = -1;

			// Materials
			AddCraft( typeof( Kindling ), 1044457, 1023553, 0.0, 00.0, typeof( Board ), 1044041, 1, 1044351 );

			index = AddCraft( typeof( Shaft ), 1044457, 1027124, 0.0, 40.0, typeof( Board ), 1044041, 1, 1044351 );
			SetUseAllRes( index, true );

			// Ammunition
			index = AddCraft( typeof( Arrow ), 1044565, 1023903, 0.0, 40.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );
			
            index = AddCraft(typeof(FlamingArrow), 1044565, "flaming arrow", 50.0, 75.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes(index, typeof(SmallFireRock), "small Fire Rock", 2, "You don't have enough small Fire Rock.");
            AddRes(index, typeof(PigIron), "Pig Iron", 1, "You don't have enough Pig Iron.");
            SetUseAllRes(index, true);
			
			index = AddCraft( typeof( Bolt ), 1044565, 1027163, 0.0, 40.0, typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes( index, typeof( Feather ), 1044562, 1, 1044563 );
			SetUseAllRes( index, true );
			
            index = AddCraft(typeof(FireBolt), 1044565, "fire bolt", 50.0, 75.0,  typeof( Shaft ), 1044560, 1, 1044561 );
			AddRes(index, typeof(SmallFireRock), "small Fire Rock", 1, "You don't have enough small Fire Rock.");
            AddRes(index, typeof(PigIron), "Pig Iron", 1, "You don't have enough Pig Iron.");
            SetUseAllRes(index, true);
			
			if( Core.SE )
			{
				index = AddCraft( typeof( FukiyaDarts ), 1044565, 1030246, 50.0, 90.0, typeof( Board ), 1044041, 1, 1044351 );
				SetUseAllRes( index, true );
				SetNeededExpansion( index, Expansion.SE );
			}

			// Weapons
			AddCraft( typeof( Bow ), 1044566, 1025042, 30.0, 70.0, typeof( Board ), 1044041, 7, 1044351 );
			AddCraft( typeof( Crossbow ), 1044566, 1023919, 60.0, 100.0, typeof( Board ), 1044041, 7, 1044351 );
			AddCraft( typeof( HeavyCrossbow ), 1044566, 1025117, 80.0, 120.0, typeof( Board ), 1044041, 10, 1044351 );

			if ( Core.AOS )
			{
				AddCraft( typeof( CompositeBow ), 1044566, 1029922, 70.0, 110.0, typeof( Board ), 1044041, 7, 1044351 );
				AddCraft( typeof( RepeatingCrossbow ), 1044566, 1029923, 90.0, 130.0, typeof( Board ), 1044041, 10, 1044351 );
			}

			if( Core.SE )
			{
				index = AddCraft( typeof( Yumi ), 1044566, 1030224, 90.0, 130.0, typeof( Board ), 1044041, 10, 1044351 );
				SetNeededExpansion( index, Expansion.SE );
			}
			
			#region Mondain's Legacy
			if ( Core.ML )
			{
				index = AddCraft( typeof( ElvenCompositeLongbow ), 1044566, 1031562, 95.0, 145.0, typeof( Board ), 1044041, 20, 1044351 );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( MagicalShortbow ), 1044566, 1031551, 85.0, 135.0, typeof( Board ), 1044041, 15, 1044351 );	
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( BlightGrippedLongbow ), 1044566, 1072907, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( LardOfParoxysmus ), 1032681, 1, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 10, 1053098 );
				AddRes( index, typeof( Corruption ), 1032676, 10, 1053098 );
				AddRecipe( index, (int) BowRecipes.BlightGrippedLongbow );
				ForceNonExceptional( index );		
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( FaerieFire ), 1044566, 1072908, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( LardOfParoxysmus ), 1032681, 1, 1053098 );
				AddRes( index, typeof( Putrefication ), 1032678, 10, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 10, 1053098 );
				AddRecipe( index, (int) BowRecipes.FaerieFire );	
				ForceNonExceptional( index );			
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( SilvanisFeywoodBow ), 1044566, 1072955, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( LardOfParoxysmus ), 1032681, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 10, 1053098 );
				AddRes( index, typeof( Muculent ), 1032680, 10, 1053098 );
				AddRecipe( index, (int) BowRecipes.SilvanisFeywoodBow );
				ForceNonExceptional( index );			
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( MischiefMaker ), 1044566, 1072910, 75.0, 125.0, typeof( Board ), 1044041, 15, 1044351 );
				AddRes( index, typeof( DreadHornMane ), 1032682, 1, 1053098 );
				AddRes( index, typeof( Corruption ), 1032676, 10, 1053098 );
				AddRes( index, typeof( Putrefication ), 1032678, 10, 1053098 );
				AddRecipe( index, (int) BowRecipes.MischiefMaker );			
				ForceNonExceptional( index );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( TheNightReaper ), 1044566, 1072912, 75.0, 125.0, typeof( Board ), 1044041, 10, 1044351 );
				AddRes( index, typeof( DreadHornMane ), 1032682, 1, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 10, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 10, 1053098 );
				AddRecipe( index, (int) BowRecipes.TheNightReaper );
				ForceNonExceptional( index );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( BarbedLongbow ), 1044566, 1073505, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( FireRuby ), 1026254, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.BarbedLongbow );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( SlayerLongbow ), 1044566, 1073506, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( BrilliantAmber ), 1026256, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.SlayerLongbow );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( FrozenLongbow ), 1044566, 1073507, 75.0, 125.0, typeof( Board ), 1044041, 20, 1044351 );
				AddRes( index, typeof( Turquoise ), 1026250, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.FrozenLongbow );	
				SetNeededExpansion( index, Expansion.ML );			
				
				index = AddCraft( typeof( LongbowOfMight ), 1044566, 1073508, 75.0, 125.0, typeof( Board ), 1044041, 10, 1044351 );
				AddRes( index, typeof( BlueDiamond ), 1026255, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.LongbowOfMight );	
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( RangersShortbow ), 1044566, 1073509, 75.0, 125.0, typeof( Board ), 1044041, 15, 1044351 );
				AddRes( index, typeof( PerfectEmerald ), 1026251, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.RangersShortbow );	
				SetNeededExpansion( index, Expansion.ML );	
				
				index = AddCraft( typeof( LightweightShortbow ), 1044566, 1073510, 75.0, 125.0, typeof( Board ), 1044041, 15, 1044351 );
				AddRes( index, typeof( WhitePearl ), 1026253, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.LightweightShortbow );	
				SetNeededExpansion( index, Expansion.ML );	
				
				index = AddCraft( typeof( MysticalShortbow ), 1044566, 1073511, 75.0, 125.0, typeof( Board ), 1044041, 15, 1044351 );
				AddRes( index, typeof( EcruCitrine ), 1026252, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.MysticalShortbow );	
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( AssassinsShortbow ), 1044566, 1073512, 75.0, 125.0, typeof( Board ), 1044041, 15, 1044351 );
				AddRes( index, typeof( DarkSapphire ), 1026249, 1, 1053098 );
				AddRecipe( index, (int) BowRecipes.AssassinsShortbow );
				SetNeededExpansion( index, Expansion.ML );
				
				AddCraft(typeof(FlameThrower), 1044566, "flame thrower", 75.0, 85.0, typeof(SmallFireRock), "small Fire Rock", 4, "You don't have enough small Fire Rock.");
				AddRes( index, typeof( FireRuby ), 1032695, 3, 1044240 );
			
				AddCraft(typeof(FireStorm), 1044566, "fire storm", 85.0, 95.0, typeof(SmallFireRock), "small Fire Rock", 6, "You don't have enough small Fire Rock.");
				AddRes( index, typeof( FireRuby ), 1032695, 4, 1044240 );
			}
			
			SetSubRes( typeof( Board ), 1072643 );
			
			AddSubRes( typeof( Board ),			1021848, 0.0, 1072653 );
			AddSubRes( typeof( OakBoard ),		1072533, 65.0, 1072653 );
			AddSubRes( typeof( AshBoard ),		1072534, 80.0, 1072653 );
			AddSubRes( typeof( YewBoard ),		1072535, 95.0, 1072653 );
			AddSubRes( typeof( HeartwoodBoard ),	1072536, 100.0, 1072653 );
			AddSubRes( typeof( BloodwoodBoard ),	1072538, 100.0, 1072653 );
			AddSubRes( typeof( FrostwoodBoard ),	1072539, 100.0, 1072653 );
			#endregion

			MarkOption = true;
			Repair = Core.AOS;
		}
	}
}