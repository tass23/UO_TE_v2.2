using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefGrilling : CraftSystem
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
			get { return "<basefont color=#FFFFFF><CENTER>GRILLING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefGrilling();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0;
		}

		private DefGrilling() : base( 1, 1, 1.25 )
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

			index = AddCraft( typeof( Pancakes ), "Breakfast", "Pancakes", 0.0, 100.0, typeof( Batter ), "Batter", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Waffles ), "Breakfast", "Waffles", 0.0, 100.0, typeof( WaffleMix ), "Waffle Mix", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FriedEggs ), "Breakfast", "Fried Eggs", 0.0, 100.0, typeof( Eggs ), "Eggs", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Bacon ), "Breakfast", "Bacon", 0.0, 100.0, typeof( RawBacon ), "Raw Bacon", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Ribs ), "Barbecue", "Ribs", 0.0, 100.0, typeof( RawRibs ), "Raw Ribs", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( CookedBird ), "Barbecue", "Cooked Bird", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenLeg ), "Barbecue", "Chicken Leg", 0.0, 100.0, typeof( RawChickenLeg ), "Raw Chicken Leg", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( FishSteak ), "Barbecue", "Fish Steak", 0.0, 100.0, typeof( RawFishSteak ), "Raw Fish Steak", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( LambLeg ), "Barbecue", "Lamb Leg", 0.0, 100.0, typeof( RawLambLeg ), "Raw Lamb Leg", 1, 1044253 );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefBBQRibs ), "Dinners", "Beef Barbecue Ribs", 0.0, 100.0, typeof( RawRibs ), "Raw Ribs", 1, 1044253 );
			AddRes( index, typeof( BarbecueSauce ), "Barbecue Sauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefBroccoli ), "Dinners", "Beef and Broccoli", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( Broccoli ), "Broccoli", 4, 1044253 );
			AddRes( index, typeof( SoySauce ), "Soy Sauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChoChoBeef ), "Dinners", "Cho Cho Beef", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( Teriyaki ), "Teriyaki", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefSnowpeas ), "Dinners", "Beef and Snow Peas", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( SnowPeas ), "Snow Peas", 4, 1044253 );
			AddRes( index, typeof( SoySauce ), "Soy Sauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( Hamburger ), "Dinners", "Hamburger", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( BreadLoaf ), "Bread", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );


			index = AddCraft( typeof( BeefLoMein ), "Dinners", "Beef Lo Mein", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( PastaNoodles ), "Pasta Noodles", 2, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BeefStirfry ), "Dinners", "Beef Stirfry", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( ChickenStirfry ), "Dinners", "Chicken Stirfry", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MooShuPork ), "Dinners", "Moo Shu Pork", 0.0, 100.0, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( MoPoTofu ), "Dinners", "Mo Po Tofu", 0.0, 100.0, typeof( Tofu ), "Tofu", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( ChiliPepper ), "Chili Pepper", 3, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( PorkStirfry ), "Dinners", "Pork Stirfry", 0.0, 100.0, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( BowlCookedVeggies ), "Cooked Mixed Vegetables", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SweetSourChicken ), "Dinners", "Sweet and Sour Chicken", 0.0, 100.0, typeof( RawBird ), "Raw Bird", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "SoySauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( SweetSourPork ), "Dinners", "Sweet and Sour Pork", 0.0, 100.0, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			AddRes( index, typeof( SoySauce ), "SoySauce", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( BaconAndEgg ), "Dinners", "Bacon and Eggs", 0.0, 100.0, typeof( Eggs ), "Eggs", 2, 1044253 );
			AddRes( index, typeof ( RawBacon ), "Raw Bacon", 1, 1044253 );
			AddRes( index, typeof( FoodPlate ), "Plate", 1, "You need a plate!" );
			SetNeedOven( index, true );

			index = AddCraft( typeof( GarlicBread ), "Other Food", "Garlic Bread", 0.0, 100.0, typeof( BreadLoaf ), "Bread", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );
			AddRes( index, typeof( Garlic ), "Garlic", 2, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Herbs", 1, 1044253 );

			index = AddCraft( typeof( GrilledHam ), "Other Food", "Grilled Ham", 0.0, 100.0, typeof( RawHamSlices ), "Raw Sliced Ham", 1, 1044253 );

			index = AddCraft( typeof( Sausage ), "Other Food", "Sausage", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( GroundPork ), "Ground Pork", 1, 1044253 );
			AddRes( index, typeof( BasketOfHerbs ), "Ground Pork", 1, 1044253 );

			index = AddCraft( typeof( Hotwings ), "Other Food", "Hotwings", 0.0, 100.0, typeof ( RawChickenLeg ), "Raw Chicken Leg", 1, 1044253 );
			AddRes( index, typeof( JarHoney ), "Honey", 1, 1044253 );
			AddRes( index, typeof( HotSauce ), "Hot Sauce", 1, 1044253 );

			index = AddCraft( typeof( PotatoFries ), "Other Food", "Potato Fries", 0.0, 100.0, typeof( Potato ), "Potato", 3, 1044253 );
			AddRes( index, typeof( Onion ), "Onion", 1, 1044253 );
			AddRes( index, typeof( Butter ), "Butter", 1, 1044253 );

			index = AddCraft( typeof( Taco ), "Other Food", "Taco", 0.0, 100.0, typeof( GroundBeef ), "Ground Beef", 1, 1044253 );
			AddRes( index, typeof( Tortilla ), "Tortilla", 1, 1044253 );
			AddRes( index, typeof( CheeseWheel ), "Cheese Wheel", 1, 1044253 );

		}
	}
}