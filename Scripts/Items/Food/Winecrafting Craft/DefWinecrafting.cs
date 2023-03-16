using System;
using Server;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWinecrafting : CraftSystem
	{
		public override SkillName MainSkill { get { return SkillName.Alchemy; } }

		public override int GumpTitleNumber { get { return 0; } }

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>Winecrafting Menu</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null ) m_CraftSystem = new DefWinecrafting();
				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item ) { return 0.5; }

		private DefWinecrafting() : base( 1, 1, 1.25 ){}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 ) return 1044038;
			return 0;
		}

		public override void PlayCraftEffect( Mobile from ) { from.PlaySound( 0x241 ); }

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
			string skillNotice = "You have no idea how to craft wine with this type of grape.";

			index = AddCraft( typeof( WineKeg ), "Wines", "Keg of Wine", 80.0, 105.6, typeof( CabernetSauvignonGrapes ), "Cabernet Sauvignon Grapes", 50, "You need more grapes." );
			AddRes( index, typeof( Keg ), "Keg", 1, "You need a keg." );
			AddRes( index, typeof( BagOfSugar ), "Bag of Sugar", 1, "You need more sugar." );
			AddRes( index, typeof( BrewersYeast ), "Brewers Yeast", 1, "You need more Brewers Yeast." );

			SetSubRes( typeof( CabernetSauvignonGrapes ), "Cabernet Sauvignon Grapes" );

			AddSubRes(typeof(CabernetSauvignonGrapes), "Cabernet Sauvignon Grapes", 80.0, skillNotice);
			AddSubRes(typeof(ChardonnayGrapes), "Chardonnay Grapes", 80.0, skillNotice);
			AddSubRes(typeof(CheninBlancGrapes), "Chenin Blanc Grapes", 85.0, skillNotice);
			AddSubRes(typeof(MerlotGrapes), "Merlot Grapes", 80.0, skillNotice);
			AddSubRes(typeof(PinotNoirGrapes), "Pinot Noir Grapes", 80.0, skillNotice);
			AddSubRes(typeof(RieslingGrapes), "Riesling Grapes", 85.0, skillNotice);
			AddSubRes(typeof(SangioveseGrapes), "Sangiovese Grapes", 90.0, skillNotice);
			AddSubRes(typeof(SauvignonBlancGrapes), "Sauvignon Blanc Grapes", 90.0, skillNotice);
			AddSubRes(typeof(ShirazGrapes), "Shiraz Grapes", 90.0, skillNotice);
			AddSubRes(typeof(ViognierGrapes), "Viognier Grapes", 99.0, skillNotice);
			AddSubRes(typeof(ZinfandelGrapes), "Zinfandel Grapes", 80.0, skillNotice);
			AddSubRes(typeof(Apple), "Apples", 60.0, skillNotice);
			AddSubRes(typeof(Apricot), "Apricots", 70.0, skillNotice);
			AddSubRes(typeof(Cherry), "Cherries", 80.0, skillNotice);
			AddSubRes(typeof(Mango), "Mangos", 80.0, skillNotice);
			AddSubRes(typeof(Orange), "Oranges", 70.0, skillNotice);
			AddSubRes(typeof(Pear), "Pears", 60.0, skillNotice);
			AddSubRes(typeof(Peach), "Peaches", 60.0, skillNotice);
			AddSubRes(typeof(Blackberry), "Blackberries", 90.0, skillNotice);
			AddSubRes(typeof(BlackRaspberry), "Black Raspberries", 90.0, skillNotice);
			AddSubRes(typeof(Blueberry), "Blueberries", 90.0, skillNotice);
			AddSubRes(typeof(Cranberry), "Cranberries", 90.0, skillNotice);
			AddSubRes(typeof(RedRaspberry), "Red Raspberries", 90.0, skillNotice);
			AddSubRes(typeof(Strawberry), "Strawberries", 90.0, skillNotice);
			AddSubRes(typeof(Watermelon), "Watermelons", 90.0, skillNotice);
			AddSubRes(typeof(RiceSheath), "Rice Sheathes", 100.0, skillNotice);
			AddSubRes(typeof(Dandelion), "Dandelions", 125.0, skillNotice);

			MarkOption = true;
			Repair = false;
		}
	}
}