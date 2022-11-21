using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBaking : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Cooking;	}
		}

		public override int GumpTitleNumber
		{
			get { return 0; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>BAKING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBaking();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0;
		}

		private DefBaking() : base( 1, 1, 1.25 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038;
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263;

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 );

			if ( failed )
			{
				if ( lostMaterial )
					return 1044043;
				else
					return 1044157;
			}
			else
			{
				if ( quality == 0 )
					return 502785;
				else if ( makersMark && quality == 2 )
					return 1044156;
				else if ( quality == 2 )
					return 1044155;
				else
					return 1044154;
			}
		}





		public override void InitCraftList()
		{
			int index = -1;

			index = AddCraft( typeof( BreadLoaf ), "Breads", "Bread", 0.0, 100.0, typeof( Dough ), "Dough", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Muffins ), "Breads", "Muffins", 0.0, 100.0, typeof( Batter ), "Batter", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BananaBread ), "Breads", "Banana Bread", 0.0, 100.0, typeof( SweetDough ), "Sweet Dough", 1, 1044253 );
			AddRes( index, typeof( Banana ), "Banana", 6, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BlueberryMuffins), "Breads", "Blueberry Muffins", 0.0, 100.0, typeof( SweetDough ), "Sweet Dough", 1, 1044253 );
			AddRes( index, typeof( Blueberry ), "Blueberry", 6, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CornBread ), "Breads", "Corn Bread", 0.0, 100.0, typeof( BagOfCornmeal ), "Bag of Cornmeal", 1, 1044253 );
			AddRes( index, typeof( Batter ), "Batter", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Donuts ), "Breads", "Donuts", 0.0, 100.0, typeof( SweetDough ), "Sweet Dough", 2, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinBread ), "Breads", "Pumpkin Bread", 0.0, 100.0, typeof( SweetDough ), "Sweet Dough", 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), "Pumpkin", 3, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinMuffins ), "Breads", "Pumpkin Muffins", 0.0, 100.0, typeof( SweetDough ), "Sweet Dough", 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), "Pumpkin", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( AlmondCookies ), "Cookies", "Almond Cookies", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( Almond ), "Almond", 12, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChocChipCookies ), "Cookies", "Chocolate Chip Cookies", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( BagOfCocoa ), "Bag of Cocoa", 1, "YUou need a bag of cocoa" );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( GingerSnaps ), "Cookies", "Ginger Snaps", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( TanGinger ), "Tan Ginger", 12, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( OatmealCookies ), "Cookies", "Oatmeal Cookies", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( BagOfOats ), "Bag of Oats", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PeanutButterCookies ), "Cookies", "Peanut Butter Cookies", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( PeanutButter ), "Peanut Butter", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinCookies ), "Cookies", "Pumpkin Cookies", 0.0, 100.0, typeof( CookieMix ), "Cookie Mix", 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), "Pumpkin", 6, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ApplePie ), "Desserts", "Apple Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Apple ), "Apple", 8, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BlueberryPie ), "Desserts", "Blueberry Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Blueberry ), "Blueberry", 8, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CherryPie ), "Desserts", "Cherry Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Cherry ), "Cherry", 8, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FruitPie ), "Desserts", "Fruit Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( FruitBasket ), "Fruit Basket", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( KeyLimePie ), "Desserts", "Key Lime Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Lime ), "Lime", 12, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( LemonMerenguePie ), "Desserts", "Lemon Merengue Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Lemon ), "Lemon", 12, 1044253 );
			AddRes( index, typeof( Cream ), "Cream", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PumpkinPie ), "Desserts", "Pumpkin Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Pumpkin ), "Pumpkin", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BlackberryCobbler ), "Desserts", "Blackberry Cobbler", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Blackberry ), "Blackberry", 10, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PeachCobbler ), "Desserts", "Peach Cobbler", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Peach ), "Peach", 10, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Brownies ), "Desserts", "Brownies", 0.0, 100.0, typeof( ChocolateMix ), "Chocolate Mix", 1, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 2, 1044253 );
			AddRes( index, typeof( CookingOil ), "Cooking Oil", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChocSunflowerSeeds ), "Desserts", "Chocolate Sunflower Seeds", 0.0, 100.0, typeof( EdibleSun ), "Sunflower Seeds", 1, 1044253 );
			AddRes( index, typeof( BagOfCocoa ), "Bag of Cocoa", 1, "you need a bag oc cocoa" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( RiceKrispTreat ), "Desserts", "RiceKrispTreat", 0.0, 100.0, typeof( BowlRiceKrisps ), "Bowl Of Rice Krips", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BananaCake ), "Cakes", "Banana Cake", 0.0, 100.0, typeof( CakeMix ), "Cake Mix", 1, 1044253 );
			AddRes( index, typeof( Banana ), "Banana", 4, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CarrotCake ), "Cakes", "Carrot Cake", 0.0, 100.0, typeof( CakeMix ), "Cake Mix", 1, 1044253 );
			AddRes( index, typeof( Carrot ), "Carrot", 6, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChocolateCake ), "Cakes", "Chocolate Cake", 0.0, 100.0, typeof( CakeMix ), "Cake Mix", 1, 1044253 );
			AddRes( index, typeof( BagOfCocoa ), "Bag of Cocoa", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CoconutCake ), "Cakes", "Coconut Cake", 0.0, 100.0, typeof( CakeMix ), "Cake Mix", 1, 1044253 );
			AddRes( index, typeof( Coconut ), "Coconut", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( LemonCake ), "Cakes", "Lemon Cake", 0.0, 100.0, typeof( CakeMix ), "Cake Mix", 1, 1044253 );
			AddRes( index, typeof( Lemon ), "Lemon", 4, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenParmesian ), "Dinners", "Chicken Parmesian", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( TomatoSauce ), "Tomato Sauce", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheeseEnchilada ), "Dinners", "Cheese Enchilada", 0.0, 100.0, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );
			AddRes( index, typeof( Tortilla ),"Tortilla", 1, 1044253 );
			AddRes( index, typeof( EnchiladaSauce ),"Enchilada Sauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenEnchilada ), "Dinners", "Chicken Enchilada", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( Tortilla ), "Tortilla",1, 1044253 );
			AddRes( index, typeof( EnchiladaSauce ),"Enchilada Sauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Lasagna ), "Dinners", "Lasagna", 0.0, 100.0, typeof( PastaNoodles ), "Pasta Noodles", 3, 1044253 );
			AddRes( index, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( LemonChicken ), "Dinners", "Lemon Chicken", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( Lemon ), "Lemon", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( OrangeChicken ), "Dinners", "Orange Chicken", 0.0, 100.0, typeof( RawBird), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( Orange ), "Orange", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( VealParmesian ), "Dinners", "Veal Parmesian", 0.0, 100.0, typeof( RawLambLeg ), "Raw Lamb Leg", 2, 1044253 );
			AddRes( index, typeof( TomatoSauce ), "Tomato Sauce", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BroccoliCheese ), "Food", "Broccoli and Cheese", 0.0, 100.0, typeof( Broccoli ), "Broccoli", 5, 1044253 );
			AddRes( index, typeof( CheeseSauce ), "Cheese Sauce", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BroccoliCaulCheese ), "Food", "Broccoli, Cauliflower and Cheese", 0.0, 100.0, typeof( Broccoli ), "Broccoli", 5, 1044253 );
			AddRes( index, typeof( Cauliflower ), "Cauliflower", 2, 1044253 );
			AddRes( index, typeof( CheeseSauce ), "Cheese Sauce", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CauliflowerCheese ), "Food", "Cauliflower and Cheese", 0.0, 100.0, typeof( Cauliflower ), "Cauliflower", 5, 1044253 );
			AddRes( index, typeof( CheeseSauce ), "Cheese Sauce", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenPie ), "Food", "Chicken Pie", 0.0, 100.0, typeof( RawBird ),"Raw Bird", 1, 1044253 );
			AddRes( index, typeof( PieMix ),"Pie Mix", 1, 1044253 );
			AddRes( index, typeof( MixedVegetables ), "Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( Gravy ), "Gravy", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefPie ), "Food", "Beef Pie", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( PieMix ),"Pie Mix", 1, 1044253 );
			AddRes( index, typeof( MixedVegetables ),"Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( Gravy ), "Gravy",1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Meatballs ), "Food", "Meatballs", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( BreadLoaf ), "Bread", 1, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Meatloaf ), "Food", "Meatloaf", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 2, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 2, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PotatoStrings ), "Food", "Potato Strings", 0.0, 100.0, typeof( Potato ), "Potato", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Quiche ), "Food", "Quiche", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( Eggs ), "Eggs", 1, 1044253 );
			AddRes( index, typeof( RawHamSlices ), "Raw Ham Slices", 3, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ShepherdsPie ), "Food", "Shepherds Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( BowlMashedPotatos ), "Bowl of Mashed Potatos", 1, 1044253 );
			AddRes( index, typeof( Corn ), "Corn", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( TurkeyPie ), "Food", "Turkey Pie", 0.0, 100.0, typeof( PieMix ), "Pie Mix", 1, 1044253 );
			AddRes( index, typeof( SlicedTurkey ), "Sliced Turkey", 2, 1044253 );
			AddRes( index, typeof( MixedVegetables ), "Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( Gravy ), "Gravy", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CheesePizza ), "Pizzas", "Cheese Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( HamPineapplePizza ), "Pizzas", "Ham and Pineapple Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			AddRes( index, typeof( RawHamSlices ), "Raw Ham Slices", 1, 1044253 );
			AddRes( index, typeof( Pineapple), "Pineapple", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MushroomOnionPizza ), "Pizzas", "Mushroom and Onion Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			AddRes( index, typeof( TanMushroom ), "Tan Mushrooms", 3, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 3, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SausOnionMushPizza ), "Pizzas", "Sausage Onion and Mushroom Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			AddRes( index, typeof( Sausage ), "Sausage", 2, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 2, 1044253 );
			AddRes( index, typeof( RedMushroom ), "Red Mushrooms", 2, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( TacoPizza ), "Pizzas", "Taco Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			AddRes( index, typeof( GroundBeef ),"Ground Beef", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ),"CheeseWheel", 1, 1044253 );
			AddRes( index, typeof( EnchiladaSauce ), "Enchilada Sauce", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( VeggiePizza ), "Pizzas", "Vegetable Pizza", 0.0, 100.0, typeof( UncookedPizza ), "Uncooked Pizza", 1, 1044253 );
			AddRes( index, typeof( MixedVegetables ),"Mixed Vegetables", 1, 1044523 );
			SetNeedOven( index, true );

		}
	}
}