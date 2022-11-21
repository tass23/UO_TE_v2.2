using System;
using Server;
using System.Collections.Generic;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Engines.Craft
{
    #region Recipes
    public enum CookRecipes
    {
        // magical
        RotWormStew = 500
        
    }
    #endregion
	public class DefCooking : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Cooking;	} }

		public override int GumpTitleNumber { get { return 1044003; } }

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem { get { if ( m_CraftSystem == null ) m_CraftSystem = new DefCooking(); return m_CraftSystem; } }

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item ) { return 0.0; }

		private DefCooking() : base( 1, 1, 1.25 ){}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			else if ( !BaseTool.CheckAccessible( tool, from ) ) return 1044263;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from ){}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken ) from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial ) return 1044043;
				else return 1044157;
			}
			else
			{
				if ( quality == 0 ) return 502785;
				else if ( makersMark && quality == 2 ) return 1044156;
				else if ( quality == 2 ) return 1044155;
				else return 1044154;
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft( typeof( SackFlour ), 1044495, 1024153, 0.0, 100.0, typeof( WheatSheaf ), 1044489, 2, 1044490 );
			SetNeedMill( index, true );
			index = AddCraft( typeof( Dough ), 1044495, 1024157, 0.0, 100.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			index = AddCraft( typeof( SweetDough ), 1044495, 1041340, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Jar of Honey", 1, "You need a Jar of Honey" );
			index = AddCraft( typeof( Batter ), 1044495, "batter", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			index = AddCraft( typeof( Butter ), 1044495, "butter", 0.0, 100.0, typeof( Cream ), "Cream", 1, 1044253 );
			index = AddCraft( typeof( Cream ), 1044495, "cream", 0.0, 100.0, typeof( BaseBeverage ), "Milk", 1, 1044253 );
			index = AddCraft( typeof( CookingOil ), 1044495, "cooking oil", 0.0, 100.0, typeof( Peanut ), "Peanut", 10, 1044253 );
			index = AddCraft( typeof( Vinegar ), 1044495, "vinegar", 0.0, 100.0, typeof( Apple ), "apples", 5, 1044253 );
			AddRes( index, typeof( BottleOfWine ), "Wine", 1, 1044253 );
			index = AddCraft( typeof( CakeMix ), 1044495, 1041002, 0.0, 100.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );
			index = AddCraft( typeof( CookieMix ), 1044495, 1024159, 0.0, 100.0, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( SweetDough ), 1044475, 1, 1044253 );

			index = AddCraft( typeof( GroundBeef ), 1044496, "ground beef", 0.0, 100.0, typeof( BeefHock ), "Beef Hock", 1, 1044253 );
			index = AddCraft( typeof( GroundPork ), 1044496, "ground pork", 0.0, 100.0, typeof( PorkHock ), "Pork Hock", 1, 1044253 );
			index = AddCraft( typeof( SlicedTurkey ), 1044496, "sliced turkey", 0.0, 100.0, typeof( TurkeyHock ), "Turkey Hock", 1, 1044253 );
			index = AddCraft( typeof( PastaNoodles ), 1044496, "pasta noodles", 0.0, 100.0, typeof( SackFlour ), "Sack of Flour", 1, 1044253 );
			AddRes( index, typeof( Eggs ), "eggs", 5, 1044253 );
			index = AddCraft( typeof( PeanutButter ), 1044496, "peanut butter", 0.0, 100.0, typeof( Peanut ), "Peanuts", 30, 1044253 );
			index = AddCraft( typeof( Tortilla ), 1044496, "tortilla", 0.0, 100.0, typeof( BagOfCornmeal ), "Bag of Cornmeal", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			if ( Core.ML )
			{
				index = AddCraft( typeof( CocoaButter ), 1044495, 1079998, 0.0, 100.0, typeof( CocoaPulp ), 1080530, 1, 1044253 );
				SetItemHue( index, 0x457 );
				SetNeededExpansion( index, Expansion.ML );
				SetNeedOven( index, true );

				index = AddCraft( typeof( CocoaLiquor ), 1044495, 1079999, 0.0, 100.0, typeof( CocoaPulp ), 1080530, 1, 1044253 );
				AddRes( index, typeof( EmptyPewterBowl ), 1025629, 1, 1044253 );
				SetItemHue( index, 0x46A );
				SetNeededExpansion( index, Expansion.ML );
				SetNeedOven( index, true );
			}
			index = AddCraft( typeof( UnbakedQuiche ), 1044496, 1041339, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), 1044477, 1, 1044253 );
			index = AddCraft( typeof( UnbakedMeatPie ), 1044496, 1041338, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( RawRibs ), 1044482, 1, 1044253 );
			index = AddCraft( typeof( UncookedSausagePizza ), 1044496, 1041337, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Sausage ), 1044483, 1, 1044253 );
			index = AddCraft( typeof( UncookedCheesePizza ), 1044496, 1041341, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), 1044486, 1, 1044253 );
			index = AddCraft( typeof( UnbakedFruitPie ), 1044496, 1041334, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Pear ), 1044481, 1, 1044253 );
			index = AddCraft( typeof( UnbakedPeachCobbler ), 1044496, 1041335, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Peach ), 1044480, 1, 1044253 );
			index = AddCraft( typeof( UnbakedApplePie ), 1044496, 1041336, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Apple ), 1044479, 1, 1044253 );
			index = AddCraft( typeof( UnbakedPumpkinPie ), 1044496, 1041342, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), 1044484, 1, 1044253 );
			index = AddCraft( typeof( WoodPulp ), 1044496, 1113136, 60.0, 100.0, typeof(BarkFragment), 1032687, 1, 1044253); 	
            AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			index = AddCraft( typeof( GreenTea ), 1044496, 1030315, 80.0, 130.0, typeof( GreenTeaBasket ), 1030316, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			SetNeedOven( index, true );
			index = AddCraft( typeof( WasabiClumps ), 1044496, 1029451, 70.0, 120.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( WoodenBowlPea ), 1025633, 3, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( SushiRolls ), 1044496, 1030303, 90.0, 120.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 5, 1044253 );
			AddRes( index, typeof( RawShrimp ), "Raw Shrimp", 5, "You need more Raw Shrimp" );
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( SushiPlatter ), 1044496, 1030305, 90.0, 120.0, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 5, 1044253 );
			AddRes( index, typeof( RawShrimp ), "Raw Shrimp", 5, "You need more Raw Shrimp" );
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( TribalPaint ), 1044496, 1040000, 80.0, 80.0, typeof( SackFlour ), 1044468, 1, 1044253 );
			AddRes( index, typeof( TribalBerry ), 1046460, 1, 1044253 );
			index = AddCraft( typeof( EggBomb ), 1044496, 1030249, 90.0, 120.0, typeof( Eggs ), 1044477, 1, 1044253 );
			AddRes( index, typeof( SackFlour ), 1044468, 3, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( ParrotWafer ), 1044496, 1032246, 37.5, 87.5, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 10, 1044253 );
			#region UO-The Expanse
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( PremiumDogFood ), 1044496, "premium dog biscuits", 58.5, 87.5, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( GroundBeef ), "Ground Beef", 5, 1044253 );
			AddRes( index, typeof( DriedHerbs ), "Dried Herbs", 2, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			index = AddCraft( typeof( PremiumParrotFood ), 1044496, "premium parrot wafer", 58.5, 87.5, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
			AddRes( index, typeof( RawFishSteak ), 1044476, 5, 1044253 );
			AddRes( index, typeof( DriedHerbs ), "Dried Herbs", 2, 1044253 );
			SetNeededExpansion( index, Expansion.ML );
			#endregion
			index = AddCraft( typeof( DriedOnions ), 1044496, "dried onions", 0.0, 100.0, typeof( Onion ), "Onions", 5, 1044253 );
			index = AddCraft( typeof( DriedHerbs ), 1044496, "dried herbs", 0.0, 100.0, typeof( Garlic ), "Garlic", 2, 1044253 );
			AddRes( index, typeof( Ginseng ), "Ginseng", 2, 1044253 );
			AddRes( index, typeof( TanGinger ), "Tan Ginger", 2, "You need more tan ginger" );
			index = AddCraft( typeof( BasketOfHerbs ), 1044496, "basket of herbs", 0.0, 100.0, typeof( DriedHerbs ), "Dried Herbs", 1, 1044253 );
			AddRes( index, typeof( DriedOnions ), "Dried Onions", 1, 1044253 );

			index = AddCraft( typeof( BarbecueSauce ), "Sauces", "barbecue sauce", 0.0, 100.0, typeof( Tomato ), "Tomato", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			index = AddCraft( typeof( CheeseSauce ), "Sauces", "cheese sauce", 0.0, 100.0, typeof( Butter ), "Butter", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), "Milk", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );
			index = AddCraft( typeof( EnchiladaSauce ), "Sauces", "enchilada sauce", 0.0, 100.0, typeof( Tomato ), "Tomato", 1, 1044253 );
			AddRes( index, typeof( ChiliPepper ), "Chili Pepper", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			index = AddCraft( typeof( Gravy ), "Sauces", "gravy", 0.0, 100.0, typeof( Dough ), 1044469, 2, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			index = AddCraft( typeof( HotSauce ), "Sauces", "hot sauce", 0.0, 100.0, typeof( Tomato ), "Tomato", 2, 1044253 );
			AddRes( index, typeof( ChiliPepper ), "Chili Pepper", 3, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			index = AddCraft( typeof( SoySauce ), "Sauces", "soy sauce", 0.0, 100.0, typeof( BagOfSoy ), "Bag of Soy", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			index = AddCraft( typeof( Teriyaki ), "Sauces", "teriyaki", 0.0, 100.0, typeof( SoySauce ), "Soy Sauce", 1, 1044253 );
			AddRes( index, typeof( BottleOfWine ), "Bottle of Wine", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			index = AddCraft( typeof( TomatoSauce ), "Sauces", "tomato sauce", 0.0, 100.0, typeof( Tomato ), "Tomato", 3, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );

			index = AddCraft( typeof( CakeMix ), "Mixes", "cake mix", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( CookingOil ), "Cooking Oil", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			index = AddCraft( typeof( CookieMix ), "Mixes", "cookie mix", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			index = AddCraft( typeof( AsianVegMix ), "Mixes", "asian vegetable mix", 0.0, 100.0, typeof( Cabbage ), "Cabbage", 1, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 1, 1044253 );
			AddRes( index, typeof( RedMushroom ), "Red Mushroom", 1, "You need a Red Mushroom" );
			AddRes( index, typeof( Carrot ), "Carrot", 1, 1044253 );
			index = AddCraft( typeof( ChocolateMix ), "Mixes", "chocolate mix", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( BagOfCocoa ), "Bag of Cocoa", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			index = AddCraft( typeof( MixedVegetables ), "Mixes", "mixed vegetables", 0.0, 100.0, typeof( Potato ), "Potato", 2, 1044253 );
			AddRes( index, typeof( Carrot ), "Carrot", 1, 1044253 );
			AddRes( index, typeof( Celery ), "Celery", 1, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 1, 1044253 );
			index = AddCraft( typeof( PieMix ), "Mixes", "pie mix", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			index = AddCraft( typeof( PizzaCrust ), "Mixes", "pizza crust", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			index = AddCraft( typeof( WaffleMix ), "Mixes", "waffle mix", 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 2, 1044253 );
			AddRes( index, typeof( CookingOil ), "cooking oil", 1, 1044253 );

			index = AddCraft( typeof( BowlCornFlakes ), 1044497, "bowl of corn flakes", 0.0, 100.0, typeof( BagOfCornmeal ), "Bag of Cornmeal", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			index = AddCraft( typeof( BowlRiceKrisps ), 1044497, "bowl of rice krisps", 0.0, 100.0, typeof( BagOfRicemeal ), "Bag of Ricemeal", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			index = AddCraft( typeof( FruitBasket ), 1044497, "fruit basket", 0.0, 100.0, typeof( Apple ), "Apple", 5, 1044253 );
			AddRes( index, typeof( Peach ), "Peach", 5, 1044253 );
			AddRes( index, typeof( Pear ), "Pear", 5, 1044253 );
			AddRes( index, typeof( Cherry ), "Cherries", 5, 1044253 );
			index = AddCraft( typeof( Tofu ), 1044497, "tofu", 0.0, 100.0, typeof( BagOfSoy ), "Bag of Soy", 1, 1044253 );

			index = AddCraft( typeof( BreadLoaf ), 1044497, 1024156, 0.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( Cookies ), 1044497, 1025643, 0.0, 100.0, typeof( CookieMix ), 1044474, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( Cake ), 1044497, 1022537, 0.0, 100.0, typeof( CakeMix ), 1044471, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( Muffins ), 1044497, 1022539, 0.0, 100.0, typeof( SweetDough ), 1044475, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( Quiche ), 1044497, 1041345, 0.0, 100.0, typeof( UnbakedQuiche ), 1044518, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( MeatPie ), 1044497, 1041347, 0.0, 100.0, typeof( UnbakedMeatPie ), 1044519, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( SausagePizza ), 1044497, 1044517, 0.0, 100.0, typeof( UncookedSausagePizza ), 1044520, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( CheesePizza ), 1044497, 1044516, 0.0, 100.0, typeof( UncookedCheesePizza ), 1044521, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( FruitPie ), 1044497, 1041346, 0.0, 100.0, typeof( UnbakedFruitPie ), 1044522, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( PeachCobbler ), 1044497, 1041344, 0.0, 100.0, typeof( UnbakedPeachCobbler ), 1044523, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( ApplePie ), 1044497, 1041343, 0.0, 100.0, typeof( UnbakedApplePie ), 1044524, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( PumpkinPie ), 1044497, 1041348, 0.0, 100.0, typeof( UnbakedPumpkinPie ), 1046461, 1, 1044253 );
			SetNeedOven( index, true );
			index = AddCraft( typeof( MisoSoup ), 1044497, 1030317, 60.0, 110.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			SetNeedOven( index, true );
			index = AddCraft( typeof( WhiteMisoSoup ), 1044497, 1030318, 60.0, 110.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			SetNeedOven( index, true );
			index = AddCraft( typeof( RedMisoSoup ), 1044497, 1030319, 60.0, 110.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			SetNeedOven( index, true );
			index = AddCraft( typeof( AwaseMisoSoup ), 1044497, 1030320, 60.0, 110.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeededExpansion( index, Expansion.SE );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CookedBird ), 1044498, 1022487, 50.0, 100.0, typeof( RawBird ), 1044470, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( ChickenLeg ), 1044498, 1025640, 0.0, 75.0, typeof( RawChickenLeg ), 1044473, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( FriedEggs ), 1044498, 1022486, 0.0, 50.0, typeof( Eggs ), 1044477, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( LambLeg ), 1044498, 1025642, 50.0, 100.0, typeof( RawLambLeg ), 1044478, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( Ribs ), 1044498, 1022546, 20.0, 100.0, typeof( RawRibs ), 1044485, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( CookedSteak ), 1044498, "steak", 50.0, 100.0, typeof( RawSteak ), "Raw Steak", 1, "You need more Raw Steak" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( HamSlices ), 1044498, "ham slices", 20.0, 100.0, typeof( RawHamSlices ), "Raw Ham Slice", 1, "You need more Raw Ham Slice" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( RoastHam ), 1044498, "roast ham", 50.0, 100.0, typeof( RawHam ), "Raw Ham", 1, "You need more Raw Ham" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( FishSteak ), 1044498, 1022427, 30.0, 100.0, typeof( RawFishSteak ), 1044476, 1, 1044253 );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( HalibutFishSteak ), 1044498, "halibut fish steak", 50.0, 120.0, typeof( RawHalibutSteak ), "Raw Halibut Fish Steak", 1, "You need more Raw Halibut Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( FlukeFishSteak ), 1044498, "fluke fish steak", 50.0, 120.0, typeof( RawFlukeSteak ), "Raw Fluke Fish Steak", 1, "You need more Raw Fluke Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( MahiFishSteak ), 1044498, "mahi fish steak", 50.0, 120.0, typeof( RawMahiSteak ), "Raw Mahi Fish Steak", 1, "You need more Raw Mahi Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( SalmonFishSteak ), 1044498, "salmon fish steak", 50.0, 120.0, typeof( RawSalmonSteak ), "Raw Salmon Fish Steak", 1, "You need more Raw Salmon Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( RedSnapperFishSteak ), 1044498, "red snapper fish steak", 50.0, 120.0, typeof( RawRedSnapperSteak ), "Raw Red Snapper Fish Steak", 1, "You need more Raw Red Snapper Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( ParrotFishSteak ), 1044498, "parrot fish steak", 50.0, 120.0, typeof( RawParrotFishSteak ), "Raw Parrot Fish Steak", 1, "You need more Raw Parrot Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( TroutFishSteak ), 1044498, "trout fish steak", 50.0, 120.0, typeof( RawTroutSteak ), "Raw Trout Fish Steak", 1, "You need more Raw Trout Fish Steaks" );
			SetNeedHeat( index, true );
			index = AddCraft( typeof( CookedShrimp ), 1044498, "cooked shrimp", 50.0, 120.0, typeof( RawShrimp ), "Raw Shrimp", 1, "You need more Raw Shrimp" );

            SetNeedHeat(index, true);
            SetUseAllRes(index, true);
            AddRecipe(index, (int)CookRecipes.RotWormStew );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( Pulp ), "Other", "100 pulp", 50.0, 100.0, typeof( Log ), "Log", 200, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			#region Mondain's Legacy
			/* Begin Enchanted */
			if ( Core.ML )
			{
				index = AddCraft( typeof( FoodEngraver ), 1073108, 1072951, 75.0, 100.0, typeof( Dough ), 1044469, 1, 1044253 );
				AddRes( index, typeof( JarHoney ), 1044472, 1, 1044253 );
				SetNeededExpansion( index, Expansion.ML );		
				
				index = AddCraft( typeof( EnchantedApple ), 1073108, 1072952, 60.0, 110.0, typeof( Apple ), 1044479, 1, 1044253 );
				AddRes( index, typeof( GreaterHealPotion ), 1073467, 1, 1044253 );
				SetNeededExpansion( index, Expansion.ML );		
				
				index = AddCraft( typeof( WrathGrapes ), 1073108, 1072953, 95.0, 145.0, typeof( Grapes ), 1073468, 1, 1044253 );
				AddRes( index, typeof( GreaterStrengthPotion ), 1073466, 1, 1044253 );
				SetNeededExpansion( index, Expansion.ML );			
				
				index = AddCraft( typeof( FruitBowl ), 1073108, 1072950, 55.0, 105.0, typeof( EmptyWoodenBowl ), 1073472, 1, 1044253 );
				AddRes( index, typeof( Pear ), 1044481, 3, 1044253 );
				AddRes( index, typeof( Apple ), 1044479, 3, 1044253 );
				AddRes( index, typeof( Banana ), 1073470, 3, 1044253 );
				SetNeededExpansion( index, Expansion.ML );		
			}
			/* End Enchanted */
			#endregion

			/* Begin Chocolatiering */
			if ( Core.ML )
			{
				index = AddCraft( typeof( DarkChocolate ), 1080001, 1079994, 15.0, 100.0, typeof( SackOfSugar ), 1079997, 1, 1044253 );
				AddRes( index, typeof( CocoaButter ), 1079998, 1, 1044253 );
				AddRes( index, typeof( CocoaLiquor ), 1079999, 1, 1044253 );
				SetItemHue( index, 0x465 );
				SetNeededExpansion( index, Expansion.ML );

				index = AddCraft( typeof( MilkChocolate ), 1080001, 1079995, 32.5, 107.5, typeof( SackOfSugar ), 1079997, 1, 1044253 );
				AddRes( index, typeof( CocoaButter ), 1079998, 1, 1044253 );
				AddRes( index, typeof( CocoaLiquor ), 1079999, 1, 1044253 );
				AddRes( index, typeof( BaseBeverage ), 1022544, 1, 1044253 );
				SetBeverageType( index, BeverageType.Milk );
				SetItemHue( index, 0x461 );
				SetNeededExpansion( index, Expansion.ML );

				index = AddCraft( typeof( WhiteChocolate ), 1080001, 1079996, 52.5, 127.5, typeof( SackOfSugar ), 1079997, 1, 1044253 );
				AddRes( index, typeof( CocoaButter ), 1079998, 1, 1044253 );
				AddRes( index, typeof( Vanilla ), 1080000, 1, 1044253 );
				AddRes( index, typeof( BaseBeverage ), 1022544, 1, 1044253 );
				SetBeverageType( index, BeverageType.Milk );
				SetItemHue( index, 0x47E );
				SetNeededExpansion( index, Expansion.ML );
			}
			/* End Chocolatiering */

            #region SA
            /* Plant Pigments*/
            
            /*if (Core.SA)
            {
            	index = AddCraft(typeof(PlantPigment), "Plant Pigments", 1112132, 33.0, 83.0, typeof(PlantClippings), 1112131, 1, 1044253);
                AddRes(index, typeof(Bottle), 1023854, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(NaturalDye), "Plant Pigments", 1112136, 75.0, 100.0, typeof(PlantPigment), 1112132, 1, 1044253);
                AddRes(index, typeof(ColorFixative), 1112135, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);


                index = AddCraft(typeof(ColorFixative), "Plant Pigments", 1112135, 75.0, 100.0, typeof(SilverSerpentVenom), 1112173, 1, 1044253);
                AddRes(index, typeof(BaseBeverage), 1044476, 1, 1044253);//TODO correct Consumption of BaseBeverage...
                SetNeededExpansion(index, Expansion.SA);
             }*/
            #endregion
			
			#region Seafood
			
			index = AddCraft( typeof( SteamedCrab ), "Seafood", "steamed crab", 60.0, 110.0, typeof( Crab ), "Crab", 1, "You don't have enough crab to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedBlueCrab ), "Seafood", "steamed blue crab", 60.0, 110.0, typeof( BlueCrab ), "Blue Crab", 1, "You don't have enough blue crab to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedSnowCrab ), "Seafood", "steamed snow crab", 60.0, 110.0, typeof( SnowCrab ), "Snow Crab", 1, "You don't have enough snow crab to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedRedRockCrab ), "Seafood", "steamed red rock crab", 60.0, 110.0, typeof( RedRockCrab ), "Crab", 1, "You don't have enough red rock crab to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedLobster ), "Seafood", "steamed lobster", 60.0, 110.0, typeof( Lobster ), "Lobster", 1, "You don't have enough lobster to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedRockLobster ), "Seafood", "steamed rock lobster", 60.0, 110.0, typeof( RockLobster ), "Rock Lobster", 1, "You don't have enough rock lobster to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );
			
			index = AddCraft( typeof( SteamedSpineyLobster ), "Seafood", "steamed spiney lobster", 60.0, 110.0, typeof( SpineyLobster ), "Spiney Lobster", 1, "You don't have enough spiney lobster to make that." );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			#endregion

		}
	}
}