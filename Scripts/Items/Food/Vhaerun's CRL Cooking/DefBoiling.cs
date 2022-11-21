using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefBoiling : CraftSystem
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
			get { return "<basefont color=#FFFFFF><CENTER>BOILING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefBoiling();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0;
		}

		private DefBoiling() : base( 1, 1, 1.25 )
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

			index = AddCraft( typeof( ChickenNoodleSoup ), "Soups and Stews", "Chicken Noodle Soup", 0.0, 100.0, typeof( CookedBird ), "Cooked Bird", 1, 1044253 );
			AddRes( index, typeof( PastaNoodles ), "Pasta Noodles", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( TomatoRice ), "Soups and Stews", "Tomato and Rice", 0.0, 100.0, typeof( Tomato ), "Tomato", 3, 1044253 );
			AddRes( index, typeof( BowlRice ), "Bowl of Rice", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlOfStew ), "Soups and Stews", "Beef Stew", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( Gravy ), "Gravy", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Bowl of Vegetables", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( TomatoSoup ), "Soups and Stews", "Tomato Soup", 0.0, 100.0, typeof( Tomato ), "Tomato", 5, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlBeets ), "Vegetables", "Bowl of Beets", 0.0, 100.0, typeof( Beet ), "Beet", 4, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlBroccoli ), "Vegetables", "Bowl of Broccoli", 0.0, 100.0, typeof( Broccoli ), "Broccoli", 4, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlCauliflower ), "Vegetables", "Bowl of Cauliflower", 0.0, 100.0, typeof( Cauliflower ), "Cauliflower", 4, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlGreenBeans ), "Vegetables", "Bowl of Green Beans", 0.0, 100.0, typeof( GreenBean ), "Green Beans", 20, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( Bacon ), "Bacon", 3, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlRice ), "Vegetables", "Bowl of Rice", 0.0, 100.0, typeof( BagOfRicemeal ), "Bag of Rice", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlSpinach ), "Vegetables", "Bowl of Spinach", 0.0, 100.0, typeof( Spinach ), "Spinach", 8, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( Vinegar ), "Vinegar", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlTurnips ), "Vegetables", "Bowl of Turnips", 0.0, 100.0, typeof( Turnip ), "Turnip", 4, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlMashedPotatos ), "Vegetables", "Bowl of Mashed Potatos", 0.0, 100.0, typeof( Potato ), "Potato", 5, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), "Milk", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlCookedVeggies ), "Vegetables", "Cooked Bowl of Vegetables", 0.0, 100.0, typeof( MixedVegetables ), "Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "Soy Sauce", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WoodenBowlCabbage ), "Vegetables", "Bowl of Cabbage", 0.0, 100.0, typeof( Cabbage ), "Cabbage", 2, 1044253 );
			AddRes( index, typeof( Vinegar ), "Vinegar", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WoodenBowlCarrot ), "Vegetables", "Bowl of Carrots", 0.0, 100.0, typeof( Carrot ), "Carrot", 12, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WoodenBowlCorn ), "Vegetables", "Bowl of Corn", 0.0, 100.0, typeof( Corn ), "Corn", 3, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WoodenBowlLettuce ), "Vegetables", "Bowl of Lettuce", 0.0, 100.0, typeof( Lettuce ), "Lettuce", 2, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( WoodenBowlPea ), "Vegetables", "Bowl of Peas", 0.0, 100.0, typeof( Peas ), "Peas", 20, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PewterBowlOfPotatos ), "Vegetables", "Bowl of Potatos", 0.0, 100.0, typeof( Potato ), "Potato", 5, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CornOnCob ), "Vegetables", "Corn on the Cob", 0.0, 100.0, typeof( Corn ), "Corn", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Spaghetti ), "Dinners", "Spaghetti", 0.0, 100.0, typeof( PastaNoodles ), "Pasta Noodles", 3, 1044253 );
			AddRes( index, typeof( TomatoSauce ), "Tomato Sauce", 1, 1044253 );
			AddRes( index, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BowlOatmeal ), "Food", "Bowl of Oatmeal", 0.0, 100.0, typeof( BagOfOats ), "Bag of Oats", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Hotdog ), "Food", "Hotdog", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 1, 1044253 );
			AddRes( index, typeof( BreadLoaf ), "Bread", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MacaroniCheese ), "Food", "Macaroni and Cheese", 0.0, 100.0, typeof( PastaNoodles ), "Pasta Noodles", 3, 1044253 );
			AddRes( index, typeof( CheeseSauce ), "Cheese Sauce", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Popcorn ), "Food", "Popcorn", 0.0, 100.0, typeof( Corn ), "Corn", 2, 1044253 );
			AddRes( index, typeof( CookingOil ), "Cooking Oil", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FruitJam ), "Other", "Fruit Jam", 0.0, 100.0, typeof( FruitBasket ), "Fruit Basket", 1, 1044253 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, 1044253 );
			SetNeedOven( index, true );

		}
	}
}