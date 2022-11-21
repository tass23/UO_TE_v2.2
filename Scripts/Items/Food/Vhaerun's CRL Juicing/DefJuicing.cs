using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefJuicing : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Cooking; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>JUICING MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null ) m_CraftSystem = new DefJuicing();
				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5;
		}

		private DefJuicing() : base( 1, 1, 1.25 ) { }

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x241 );
		}

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
			string skillNotice = "You have no idea how to work with this type of fruit.";

			index = AddCraft( typeof( JuiceKeg ), "Juices", "Keg of Juice", 20.0, 120.0, typeof( Apple ), "Apples", 25 );
			AddRes( index, typeof( BaseBeverage ), "Water", 5 );
			AddRes( index, typeof( Keg ), "Keg", 1 );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1 );

			SetSubRes( typeof( Apple ), "Apple" );

			AddSubRes( typeof( Apple ),		"Apples", 20.0, skillNotice );
			AddSubRes( typeof( Banana ),		"Bananas", 20.0, skillNotice );
			AddSubRes( typeof( Dates ),		"Dates", 20.0, skillNotice );
			AddSubRes( typeof( Grapes ),		"Grapes", 20.0, skillNotice );
			AddSubRes( typeof( Lemon ),		"Lemons", 20.0, skillNotice );
			AddSubRes( typeof( Lime ),		"Limes", 20.0, skillNotice );
			AddSubRes( typeof( Orange ),		"Oranges", 20.0, skillNotice );
			AddSubRes( typeof( Peach ),		"Peaches", 20.0, skillNotice );
			AddSubRes( typeof( Pear ),		"Pears", 20.0, skillNotice );
			AddSubRes( typeof( Pumpkin ),	"Pumpkins", 20.0, skillNotice );
			AddSubRes( typeof( Tomato ),		"Tomatos", 20.0, skillNotice );
			AddSubRes( typeof( Watermelon ),	"Watermelons", 20.0, skillNotice );
			AddSubRes( typeof( Apricot ),		"Apricots", 20.0, skillNotice );
			AddSubRes( typeof( Blackberry ),	"Blackberry", 20.0, skillNotice );
			AddSubRes( typeof( Blueberry ),	"Blueberry", 20.0, skillNotice );
			AddSubRes( typeof( Cherry ),		"Cherry", 20.0, skillNotice );
			AddSubRes( typeof( Cranberry ),	"Cranberry", 20.0, skillNotice );
			AddSubRes( typeof( Grapefruit ),	"Grapefruit", 20.0, skillNotice );
			AddSubRes( typeof( Kiwi ),		"Kiwis", 20.0, skillNotice );
			AddSubRes( typeof( Mango ),		"Mangos", 20.0, skillNotice );
			AddSubRes( typeof( Pineapple ),	"Pineapples", 20.0, skillNotice );
			AddSubRes( typeof( Pomegranate ),	"Pomegranates", 20.0, skillNotice );
			AddSubRes( typeof( Strawberry ),	"Strawberry", 20.0, skillNotice );
			AddSubRes( typeof( Almond ),		"Almond", 20.0, skillNotice );
			AddSubRes( typeof( Asparagus ),	"Asparagus", 20.0, skillNotice );
			AddSubRes( typeof( Avocado ),	"Avocado", 20.0, skillNotice );
			AddSubRes( typeof( Beet ),		"Beet", 20.0, skillNotice );
			AddSubRes( typeof( BlackRaspberry ),	"BlackRaspberry", 20.0, skillNotice );
			AddSubRes( typeof( Cantaloupe ),	"Cantaloupe", 20.0, skillNotice );
			AddSubRes( typeof( Carrot ),		"Carrot", 20.0, skillNotice );
			AddSubRes( typeof( Cauliflower ),	"Cauliflower", 20.0, skillNotice );
			AddSubRes( typeof( Celery ),		"Celery", 20.0, skillNotice );
			AddSubRes( typeof( Coconut ),	"Coconut", 20.0, skillNotice );
			AddSubRes( typeof( Corn ),		"Corn", 20.0, skillNotice );
			AddSubRes( typeof( Cucumber ),	"Cucumber", 20.0, skillNotice );
			AddSubRes( typeof( GreenSquash ),	"GreenSquash", 20.0, skillNotice );
			AddSubRes( typeof( HoneydewMelon ),	"HoneydewMelon", 20.0, skillNotice );
			AddSubRes( typeof( Onion ),		"Onion", 20.0, skillNotice );
			AddSubRes( typeof( Peanut ),		"Peanut", 20.0, skillNotice );
			AddSubRes( typeof( Pistacio ),		"Pistacio", 20.0, skillNotice );
			AddSubRes( typeof( Potato ),		"Potato", 20.0, skillNotice );
			AddSubRes( typeof( Radish ),		"Radish", 20.0, skillNotice );
			AddSubRes( typeof( RedRaspberry ),	"RedRaspberry", 20.0, skillNotice );
			AddSubRes( typeof( Spinach ),		"Spinach", 20.0, skillNotice );
			AddSubRes( typeof( Squash ),		"Squash", 20.0, skillNotice );
			AddSubRes( typeof( SweetPotato ),	"SweetPotato", 20.0, skillNotice );
			AddSubRes( typeof( Turnip ),		"Turnip", 20.0, skillNotice );

			MarkOption = true;
			Repair = false;
		}
	}
}