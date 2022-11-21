using System;
using Server;
using Server.Items;
using Server.Engines.Craft;
using Server.Mobiles;
using Server.Targeting;
using Reward = Server.Engines.Quests.BaseReward;

namespace Server.Engines.Craft
{
	public enum ArtyRecipes
	{		
		// magical
		RockArtifactRep					= 600,
		SkullCandleArtifactRep			= 601,
		BottleArtifactRep				= 602,
		DamagedBooksArtifactRep			= 603,
		StretchedHideArtifactRep		= 604,
		BrazierArtifactRep				= 605,
		LampPostArtifactRep				= 606,
		BooksNorthArtifactRep			= 607,
		BooksWestArtifactRep			= 608,
		BooksFaceDownArtifactRep		= 609,
		StuddedLeggingsArtifactRep		= 610,
		EggCaseArtifactRep				= 611,
		SkinnedGoatArtifactRep			= 612,
		GruesomeStandardArtifactRep		= 613,
		BloodyWaterArtifactRep			= 614,
		TarotCardsArtifactRep			= 615,
		BackpackArtifactRep				= 616,
		StuddedTunicArtifactRep			= 617,
		CocoonArtifactRep				= 618,
		SkinnedDeerArtifactRep			= 619,
		SaddleArtifactRep				= 620,
		LeatherTunicArtifactRep			= 621,
		ZyronicClawRep					= 622,
		TitansHammerRep					= 623,
		BladeOfTheRighteousRep			= 624,
		InquisitorsResolutionRep		= 625,
		RuinedPaintingArtifactRep		= 626,
		Basket1ArtifactRep				= 627,
		Basket2ArtifactRep				= 628,
		Basket4ArtifactRep				= 629,
		Basket5NorthArtifactRep			= 630,
		Basket5WestArtifactRep			= 631,
		Urn1ArtifactRep					= 632,
		Urn2ArtifactRep					= 633,
		Sculpture1ArtifactRep			= 634,
		Sculpture2ArtifactRep			= 635,
		TeapotNorthArtifactRep			= 636,
		TeapotWestArtifactRep			= 637,
		TowerLanternArtifactRep			= 638,
		ManStatuetteSouthArtifactRep	= 639,
		Basket3NorthArtifactRep			= 640,
		Basket3WestArtifactRep			= 641,
		Basket6ArtifactRep				= 642,
		ZenRock1ArtifactRep				= 643,
		FanNorthArtifactRep				= 644,
		FanWestArtifactRep				= 645,
		BowlsVerticalArtifactRep		= 646,
		ZenRock2ArtifactRep				= 647,
		ZenRock3ArtifactRep				= 648,
		Painting1NorthArtifactRep		= 649,
		Painting1WestArtifactRep		= 650,
		Painting2NorthArtifactRep		= 651,
		Painting2WestArtifactRep		= 652,
		TripleFanNorthArtifactRep		= 653,
		TripleFanWestArtifactRep		= 654,
		BowlArtifactRep					= 655,
		CupsArtifactRep					= 656,
		BowlsHorizontalArtifactRep		= 657,
		SakeArtifactRep					= 658,
		SwordDisplay1NorthArtifactRep	= 659,
		SwordDisplay1WestArtifactRep	= 660,
		Painting3ArtifactRep			= 661,
		Painting4NorthArtifactRep		= 662,
		Painting4WestArtifactRep		= 663,
		SwordDisplay2NorthArtifactRep	= 664,
		SwordDisplay2WestArtifactRep	= 665,
		FlowersArtifactRep				= 666,
		DolphinLeftArtifactRep			= 667,
		DolphinRightArtifactRep			= 668,
		SwordDisplay3SouthArtifactRep	= 669,
		SwordDisplay3EastArtifactRep	= 670,
		SwordDisplay4WestArtifactRep	= 671,
		Painting5NorthArtifactRep		= 672,	
		Painting5WestArtifactRep		= 673,
		SwordDisplay4NorthArtifactRep	= 674,
		SwordDisplay5NorthArtifactRep	= 675,
		SwordDisplay5WestArtifactRep	= 676,
		Painting6NorthArtifactRep		= 677,
		Painting6WestArtifactRep		= 678,
		ManStatuetteEastArtifactRep		= 679
	}
	public class DefArtyCraft : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.ItemID;	}
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>ARTIFACT CRAFTING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefArtyCraft();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefArtyCraft() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
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

        private static Type[] m_ArtyColorables = new Type[]
		{
			typeof( RockArtifactRep ), typeof( SkullCandleArtifactRep ),
			typeof( BottleArtifactRep ), typeof( DamagedBooksArtifactRep ),
			typeof( BrazierArtifactRep ), typeof( LampPostArtifactRep ),
			typeof( BooksNorthArtifactRep ), typeof( BooksWestArtifactRep ),
			typeof( BooksFaceDownArtifactRep ), typeof( EggCaseArtifactRep ),
			typeof( GruesomeStandardArtifactRep ),
			typeof( BloodyWaterArtifactRep ), typeof( TarotCardsArtifactRep ),
			typeof( BackpackArtifactRep ), typeof( StuddedTunicArtifactRep ),
			typeof( CocoonArtifactRep ), typeof( SkinnedDeerArtifactRep ),
			typeof( SaddleArtifactRep ), typeof( LeatherTunicArtifactRep ),
			typeof( ZyronicClawRep ), typeof( TitansHammerRep ),
			typeof( BladeOfTheRighteousRep ), typeof( InquisitorsResolutionRep ),
			typeof( RuinedPaintingArtifactRep ), typeof( Basket1ArtifactRep ),
			typeof( Basket2ArtifactRep ), typeof( Basket4ArtifactRep ),
			typeof( Basket5NorthArtifactRep ), typeof( Basket5WestArtifactRep ),
			typeof( Urn1ArtifactRep ), typeof( Urn2ArtifactRep ),
			typeof( Sculpture1ArtifactRep ),typeof( Sculpture2ArtifactRep ),
			typeof( TeapotNorthArtifactRep ), typeof( TeapotWestArtifactRep ),
			typeof( TowerLanternArtifactRep ), typeof( ManStatuetteSouthArtifactRep ),
			typeof( Basket3NorthArtifactRep ), typeof( Basket3WestArtifactRep ),
			typeof( Basket6ArtifactRep ), typeof( ZenRock1ArtifactRep ),
			typeof( FanNorthArtifactRep ), typeof( FanWestArtifactRep ),
			typeof( BowlsVerticalArtifactRep ), typeof( ZenRock2ArtifactRep ),
			typeof( ZenRock3ArtifactRep ), typeof( Painting1NorthArtifactRep ),
			typeof( Painting1WestArtifactRep ), typeof( Painting2NorthArtifactRep ),
			typeof( Painting2WestArtifactRep ), typeof( TripleFanNorthArtifactRep ),
			typeof( TripleFanWestArtifactRep ), typeof( BowlArtifactRep ),
			typeof( CupsArtifactRep ), typeof( BowlsHorizontalArtifactRep ),
			typeof( SakeArtifactRep ), typeof( SwordDisplay1NorthArtifactRep ),
			typeof( SwordDisplay1WestArtifactRep ), typeof( Painting3ArtifactRep ),
			typeof( Painting4NorthArtifactRep ), typeof( Painting4WestArtifactRep ),
			typeof( SwordDisplay2NorthArtifactRep ), typeof( SwordDisplay2WestArtifactRep ),
			typeof( FlowersArtifactRep ), typeof( DolphinLeftArtifactRep ),
			typeof( DolphinRightArtifactRep ), typeof( SwordDisplay3SouthArtifactRep ),
			typeof( SwordDisplay3EastArtifactRep ), typeof( SwordDisplay4WestArtifactRep ),
			typeof( Painting5NorthArtifactRep ), typeof( Painting5WestArtifactRep ),
			typeof( SwordDisplay4NorthArtifactRep ), typeof( SwordDisplay5NorthArtifactRep ),
			typeof( SwordDisplay5WestArtifactRep ), typeof( Painting6NorthArtifactRep ),
			typeof( Painting6WestArtifactRep ), typeof( ManStatuetteEastArtifactRep )
		};
		
        public override bool RetainsColorFrom(CraftItem item, Type type)
        {
            if (type != typeof(Log) && type != typeof(Board))
                return false;

            type = item.ItemType;

            bool contains = false;

            for (int i = 0; !contains && i < m_ArtyColorables.Length; ++i)
            {
                if (m_ArtyColorables[i] == type)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }
		
		public override void PlayCraftEffect( Mobile from )
		{
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

		public override void InitCraftList()
		{
			int index = -1;															//Skill Req.	//Exceptional Req.
      
			index = AddCraft( typeof( RockArtifactRep ), "Doom Stealables", "Rock", 35.0, 55.0, typeof( ArtyOre ), "Artifact Ore", 5 );								//1
				AddRes( index, typeof( Log ), 1044041, 1, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.RockArtifactRep );
			index = AddCraft( typeof( SkullCandleArtifactRep ), "Doom Stealables", "Skull Candle", 37.0, 56.0, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 1, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SkullCandleArtifactRep );
			index = AddCraft( typeof( BottleArtifactRep ), "Doom Stealables", "Bottle", 25.0, 45.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 1, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BottleArtifactRep );
			index = AddCraft( typeof( DamagedBooksArtifactRep ), "Doom Stealables", "Damaged Books", 40.0, 85.0, typeof( ArtyOre ), "Artifact Ore", 6 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.DamagedBooksArtifactRep );
				
			index = AddCraft( typeof( Basket1ArtifactRep ), "Tokuno Stealables", "Basket", 36.0, 56.0, typeof( ArtyOre ), "Artifact Ore", 8 );
				AddRes( index, typeof( Log ), 1044041, 10, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket1ArtifactRep );
			index = AddCraft( typeof( Basket2ArtifactRep ), "Tokuno Stealables", "Basket", 38.0, 58.0, typeof( ArtyOre ), "Artifact Ore", 9 );
				AddRes( index, typeof( Log ), 1044041, 9, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket2ArtifactRep );
			index = AddCraft( typeof( Basket3NorthArtifactRep ), "Tokuno Stealables", "Basket", 35.0, 55.0, typeof( ArtyOre ), "Artifact Ore", 8 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket3NorthArtifactRep );
			index = AddCraft( typeof( Basket3WestArtifactRep ), "Tokuno Stealables", "Basket", 37.5, 57.5, typeof( ArtyOre ), "Artifact Ore", 9 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket3WestArtifactRep );
				
			index = AddCraft( typeof( StretchedHideArtifactRep ), "Doom Stealables", "Stretched Hide", 50.0, 70.0, typeof( ArtyOre ), "Artifact Ore", 5 );				//2
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.StretchedHideArtifactRep );
			index = AddCraft( typeof( BrazierArtifactRep ), "Doom Stealables", "Brazier", 61.0, 81.0, typeof( ArtyOre ), "Artifact Ore", 12 );
				AddRes( index, typeof( Log ), 1044041, 10, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BrazierArtifactRep );
				
			index = AddCraft( typeof( Basket4ArtifactRep ), "Tokuno Stealables", "Basket", 48.0, 68.0, typeof( ArtyOre ), "Artifact Ore", 9 );
				AddRes( index, typeof( Log ), 1044041, 9, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket4ArtifactRep );
			index = AddCraft( typeof( Basket5NorthArtifactRep ), "Tokuno Stealables", "Basket", 51.0, 71.0, typeof( ArtyOre ), "Artifact Ore", 10 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket5NorthArtifactRep );
			index = AddCraft( typeof( Basket5WestArtifactRep ), "Tokuno Stealables", "Basket", 53.0, 73.0, typeof( ArtyOre ), "Artifact Ore", 11 );
				AddRes( index, typeof( Log ), 1044041, 7, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket5WestArtifactRep );
			index = AddCraft( typeof( Basket6ArtifactRep ), "Tokuno Stealables", "Basket", 49.9, 69.9, typeof( ArtyOre ), "Artifact Ore", 10 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Basket6ArtifactRep );
			index = AddCraft( typeof( ZenRock1ArtifactRep ), "Tokuno Stealables", "Zen Rock Garden", 55.2, 75.2, typeof( ArtyOre ), "Artifact Ore", 6 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ZenRock1ArtifactRep );
				
			index = AddCraft( typeof( LampPostArtifactRep ), "Doom Stealables", "Lamp Post", 62.0, 82.0, typeof( ArtyOre ), "Artifact Ore", 16 );						//3
				AddRes( index, typeof( Log ), 1044041, 9, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.LampPostArtifactRep );
			index = AddCraft( typeof( BooksNorthArtifactRep ), "Doom Stealables", "Books (North)", 63.0, 83.0, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BooksNorthArtifactRep );
			index = AddCraft( typeof( BooksWestArtifactRep ), "Doom Stealables", "Books (West)", 65.0, 85.0, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BooksWestArtifactRep );
			index = AddCraft( typeof( BooksFaceDownArtifactRep ), "Doom Stealables", "Books (Face Down)", 66.0, 86.0, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BooksFaceDownArtifactRep );
				
			index = AddCraft( typeof( Urn1ArtifactRep ), "Tokuno Stealables", "Urn", 61.0, 81.0, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Urn1ArtifactRep );
			index = AddCraft( typeof( Urn2ArtifactRep ), "Tokuno Stealables", "Urn", 62.5, 82.5, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Urn2ArtifactRep );
			index = AddCraft( typeof( Sculpture1ArtifactRep ), "Tokuno Stealables", "Sculpture", 64.3, 84.5, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Sculpture1ArtifactRep );
			index = AddCraft( typeof( Sculpture2ArtifactRep ), "Tokuno Stealables", "Sculpture", 63.2, 83.2, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Sculpture2ArtifactRep );
			index = AddCraft( typeof( TeapotNorthArtifactRep ), "Tokuno Stealables", "Teapot", 65.0, 85.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TeapotNorthArtifactRep );
			index = AddCraft( typeof( TeapotWestArtifactRep ), "Tokuno Stealables", "Teapot", 65.5, 85.5, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TeapotWestArtifactRep );
			index = AddCraft( typeof( TowerLanternArtifactRep ), "Tokuno Stealables", "Tower Lantern", 71.5, 91.5, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TowerLanternArtifactRep );
			index = AddCraft( typeof( FanNorthArtifactRep ), "Tokuno Stealables", "Fan", 60.1, 80.1, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.FanNorthArtifactRep );
			index = AddCraft( typeof( FanWestArtifactRep ), "Tokuno Stealables", "Fan", 64.5, 84.5, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.FanWestArtifactRep );
			index = AddCraft( typeof( BowlsVerticalArtifactRep ), "Tokuno Stealables", "Bowls", 66.2, 86.2, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BowlsVerticalArtifactRep );
			index = AddCraft( typeof( ZenRock2ArtifactRep ), "Tokuno Stealables", "Zen Rock Garden", 68.3, 88.3, typeof( ArtyOre ), "Artifact Ore", 10 );
				AddRes( index, typeof( Log ), 1044041, 7, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ZenRock2ArtifactRep );
			index = AddCraft( typeof( ZenRock3ArtifactRep ), "Tokuno Stealables", "Zen Rock Garden", 69.4, 89.4, typeof( ArtyOre ), "Artifact Ore", 8 );
				AddRes( index, typeof( Log ), 1044041, 6, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ZenRock3ArtifactRep );
				
			index = AddCraft( typeof( Painting1NorthArtifactRep ), "Tokuno Stealables", "Painting", 69.5, 89.5, typeof( ArtyOre ), "Artifact Ore", 5 );			//4
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting1NorthArtifactRep );
			index = AddCraft( typeof( Painting1WestArtifactRep ), "Tokuno Stealables", "Painting", 70.2, 90.2, typeof( ArtyOre ), "Artifact Ore", 6 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting1WestArtifactRep );
			index = AddCraft( typeof( Painting2NorthArtifactRep ), "Tokuno Stealables", "Painting", 71.3, 91.3, typeof( ArtyOre ), "Artifact Ore", 6 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting2NorthArtifactRep );
			index = AddCraft( typeof( Painting2WestArtifactRep ), "Tokuno Stealables", "Painting", 70.0, 90.0, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting2WestArtifactRep );
			index = AddCraft( typeof( TripleFanNorthArtifactRep ), "Tokuno Stealables", "Fan", 75.1, 95.1, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TripleFanNorthArtifactRep );
			index = AddCraft( typeof( TripleFanWestArtifactRep ), "Tokuno Stealables", "Fan", 77.0, 97.0, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TripleFanWestArtifactRep );
			index = AddCraft( typeof( BowlArtifactRep ), "Tokuno Stealables", "Bowl", 69.9, 89.9, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BowlArtifactRep );
			index = AddCraft( typeof( CupsArtifactRep ), "Tokuno Stealables", "Cups", 67.1, 87.1, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.CupsArtifactRep );
			index = AddCraft( typeof( BowlsHorizontalArtifactRep ), "Tokuno Stealables", "Bowls", 68.3, 88.3, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BowlsHorizontalArtifactRep );
			index = AddCraft( typeof( SakeArtifactRep ), "Tokuno Stealables", "Sake", 72.6, 92.6, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SakeArtifactRep );
				
			index = AddCraft( typeof( StuddedLeggingsArtifactRep ), "Doom Stealables", "Studded Leggings", 68.0, 88.0, typeof( ArtyOre ), "Artifact Ore", 8 );			//5
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.StuddedLeggingsArtifactRep );
			index = AddCraft( typeof( EggCaseArtifactRep ), "Doom Stealables", "Egg Case", 72.0, 92.0, typeof( ArtyOre ), "Artifact Ore", 12 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.EggCaseArtifactRep );
			index = AddCraft( typeof( SkinnedGoatArtifactRep ), "Doom Stealables", "Skinned Goat", 75.0, 95.0, typeof( ArtyOre ), "Artifact Ore", 6 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 2, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SkinnedGoatArtifactRep );
			index = AddCraft( typeof( GruesomeStandardArtifactRep ), "Doom Stealables", "Gruesome Standard", 78.0, 98.0, typeof( ArtyOre ), "Artifact Ore", 7 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.GruesomeStandardArtifactRep );		
			index = AddCraft( typeof( BloodyWaterArtifactRep ), "Doom Stealables", "Bloody Water", 79.0, 99.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BloodyWaterArtifactRep );
			index = AddCraft( typeof( TarotCardsArtifactRep ), "Doom Stealables", "Tarot Cards", 81.0, 101.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TarotCardsArtifactRep );
			index = AddCraft( typeof( BackpackArtifactRep ), "Doom Stealables", "Backpack", 86.0, 106.0, typeof( ArtyOre ), "Artifact Ore", 20 );
				AddRes( index, typeof( Log ), 1044041, 8, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 3, 1053098 );
				AddRes( index, typeof( Putrefication ), 1032678, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BackpackArtifactRep );
				
			index = AddCraft( typeof( SwordDisplay1NorthArtifactRep ), "Tokuno Stealables", "Sword Display", 89.3, 109.3, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay1NorthArtifactRep );
			index = AddCraft( typeof( SwordDisplay1WestArtifactRep ), "Tokuno Stealables", "Sword Display", 89.5, 109.5, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay1WestArtifactRep );
			index = AddCraft( typeof( Painting3ArtifactRep ), "Tokuno Stealables", "Painting", 90.1, 110.1, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting3ArtifactRep );
				
			index = AddCraft( typeof( Painting4NorthArtifactRep ), "Tokuno Stealables", "Painting", 92.1, 112.1, typeof( ArtyOre ), "Artifact Ore", 4 );			//6
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting4NorthArtifactRep );
			index = AddCraft( typeof( Painting4WestArtifactRep ), "Tokuno Stealables", "Painting", 92.5, 112.5, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting4WestArtifactRep );
			index = AddCraft( typeof( SwordDisplay2NorthArtifactRep ), "Tokuno Stealables", "Sword Display", 99.1, 115.1, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay2NorthArtifactRep );
			index = AddCraft( typeof( SwordDisplay2WestArtifactRep ), "Tokuno Stealables", "Sword Display", 99.3, 115.3, typeof( ArtyOre ), "Artifact Ore", 5 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay2WestArtifactRep );
				
			index = AddCraft( typeof( StuddedTunicArtifactRep ), "Doom Stealables", "Studded Tunic", 88.0, 108.0, typeof( ArtyOre ), "Artifact Ore", 12 );				//7
				AddRes( index, typeof( Log ), 1044041, 7, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.StuddedTunicArtifactRep );
			index = AddCraft( typeof( CocoonArtifactRep ), "Doom Stealables", "Cocoon", 90.0, 110.0, typeof( ArtyOre ), "Artifact Ore", 7 );
				AddRes( index, typeof( Log ), 1044041, 6, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.CocoonArtifactRep );
				
			index = AddCraft( typeof( FlowersArtifactRep ), "Tokuno Stealables", "Flowers", 91.2, 101.2, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.FlowersArtifactRep );
				
			index = AddCraft( typeof( SkinnedDeerArtifactRep ), "Doom Stealables", "Skinned Deer", 95.0, 115.0, typeof( ArtyOre ), "Artifact Ore", 3 );				//8
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SkinnedDeerArtifactRep );
				
			index = AddCraft( typeof( DolphinLeftArtifactRep ), "Tokuno Stealables", "Sculpture", 96.2, 116.2, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.DolphinLeftArtifactRep );
			index = AddCraft( typeof( DolphinRightArtifactRep ), "Tokuno Stealables", "Sculpture", 97.1, 117.1, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.DolphinRightArtifactRep );
			index = AddCraft( typeof( SwordDisplay3SouthArtifactRep ), "Tokuno Stealables", "Sword Display", 100.1, 120.0, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay3SouthArtifactRep );
			index = AddCraft( typeof( SwordDisplay3EastArtifactRep ), "Tokuno Stealables", "Sword Display", 100.1, 120.0, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay3EastArtifactRep );
			index = AddCraft( typeof( SwordDisplay4WestArtifactRep ), "Tokuno Stealables", "Sword Display", 102.3, 120.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay4WestArtifactRep );
			index = AddCraft( typeof( Painting5NorthArtifactRep ), "Tokuno Stealables", "Painting", 97.3, 117.3, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting5NorthArtifactRep );
			index = AddCraft( typeof( Painting5WestArtifactRep ), "Tokuno Stealables", "Painting", 98.1, 118.1, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting5WestArtifactRep );
				
			index = AddCraft( typeof( SaddleArtifactRep ), "Doom Stealables", "Saddle", 99.0, 119.0, typeof( ArtyOre ), "Artifact Ore", 3 );							//9
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRes( index, typeof( Putrefication ), 1032678, 1, 1053098 );				
				AddRecipe( index, (int) ArtyRecipes.SaddleArtifactRep );
			index = AddCraft( typeof( LeatherTunicArtifactRep ), "Doom Stealables", "Leather Tunic", 98.0, 118.0, typeof( ArtyOre ), "Artifact Ore", 12 );
				AddRes( index, typeof( Log ), 1044041, 5, 1044351 );
				AddRes( index, typeof( Taint ), 1032679, 3, 1053098 );
				AddRes( index, typeof( Putrefication ), 1032678, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.LeatherTunicArtifactRep );
				
			index = AddCraft( typeof( ManStatuetteSouthArtifactRep ), "Tokuno Stealables", "Statuette", 97.4, 117.4, typeof( ArtyOre ), "Artifact Ore", 2 );
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ManStatuetteSouthArtifactRep );
			index = AddCraft( typeof( SwordDisplay4NorthArtifactRep ), "Tokuno Stealables", "Sword Display", 112.2, 120.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay4NorthArtifactRep );
			index = AddCraft( typeof( SwordDisplay5NorthArtifactRep ), "Tokuno Stealables", "Sword Display", 112.2, 120.0, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay5NorthArtifactRep );
			index = AddCraft( typeof( SwordDisplay5WestArtifactRep ), "Tokuno Stealables", "Sword Display", 115.2, 120.0, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.SwordDisplay5WestArtifactRep );
			index = AddCraft( typeof( Painting6NorthArtifactRep ), "Tokuno Stealables", "Painting", 102.7, 118.2, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 4, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting6NorthArtifactRep );
			index = AddCraft( typeof( Painting6WestArtifactRep ), "Tokuno Stealables", "Painting", 102.8, 118.8, typeof( ArtyOre ), "Artifact Ore", 3 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.Painting6WestArtifactRep );
			index = AddCraft( typeof( ManStatuetteEastArtifactRep ), "Tokuno Stealables", "Sculpture", 104.6, 119.6, typeof( ArtyOre ), "Artifact Ore", 4 );
				AddRes( index, typeof( Log ), 1044041, 3, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 1, 1053098 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ManStatuetteEastArtifactRep );
				
			index = AddCraft( typeof( ZyronicClawRep ), "Doom Stealables", "Zyronic Claw", 100.0, 119.0, typeof( ArtyOre ), "Artifact Ore", 18 );						//10
				AddRes( index, typeof( Log ), 1044041, 10, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 6, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 4, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.ZyronicClawRep );
			index = AddCraft( typeof( TitansHammerRep ), "Doom Stealables", "Titan's Hammer", 105.0, 120.0, typeof( ArtyOre ), "Artifact Ore", 19 );
				AddRes( index, typeof( Log ), 1044041, 12, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 4, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 6, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.TitansHammerRep );
			index = AddCraft( typeof( BladeOfTheRighteousRep ), "Doom Stealables", "Blade Of The Righteous", 108.0, 120.0, typeof( ArtyOre ), "Artifact Ore", 20 );
				AddRes( index, typeof( Log ), 1044041, 12, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 5, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 5, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.BladeOfTheRighteousRep );
			index = AddCraft( typeof( InquisitorsResolutionRep ), "Doom Stealables", "The Inquisitor's Resolution", 110.0, 120.0, typeof( ArtyOre ), "Artifact Ore", 20 );	
				AddRes( index, typeof( Log ), 1044041, 14, 1044351 );
				AddRes( index, typeof( Scourge ), 1032677, 7, 1053098 );
				AddRes( index, typeof( Blight ), 1032675, 3, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.InquisitorsResolutionRep );
				
			index = AddCraft( typeof( RuinedPaintingArtifactRep ), "Doom Stealables", "Ruined Painting", 114.0, 120.0, typeof( ArtyOre ), "Artifact Ore", 5 );		//12
				AddRes( index, typeof( Log ), 1044041, 2, 1044351 );
				AddRes( index, typeof( Muculent ), 1032680, 1, 1053098 );
				AddRes( index, typeof( Taint ), 1032679, 2, 1053098 );
				AddRecipe( index, (int) ArtyRecipes.RuinedPaintingArtifactRep );
			
			MarkOption = true;
			//Repair = Core.AOS;
            SetSubRes(typeof(Log), 1072643);

            // Add every material you want the player to be able to choose from
            // This will override the overridable material	TODO: Verify the required skill amount
            AddSubRes(typeof(Log), 1072643, 00.0, 1044041, 1072652);
            AddSubRes(typeof(OakLog), 1072644, 65.0, 1044041, 1072652);
            AddSubRes(typeof(AshLog), 1072645, 80.0, 1044041, 1072652);
            AddSubRes(typeof(YewLog), 1072646, 95.0, 1044041, 1072652);
            AddSubRes(typeof(HeartwoodLog), 1072647, 100.0, 1044041, 1072652);
            AddSubRes(typeof(BloodwoodLog), 1072648, 100.0, 1044041, 1072652);
            AddSubRes(typeof(FrostwoodLog), 1072649, 100.0, 1044041, 1072652);
		}
	}
}