using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefWaxCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>WAX CRAFTING MENU</CENTER></basefont>"; } // <CENTER>COOKING MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefWaxCrafting();

				return m_CraftSystem;
			}
		}

		public override CraftECA ECA{ get{ return CraftECA.ChanceMinusSixtyToFourtyFive; } }

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefWaxCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 1.5 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x242 );
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
			int index = -1;

			/* Begin Resources */
			index = AddCraft( typeof( CandleWick ), "Resources", "Candle Wick", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 1, "You do not have enough Beeswax." );
			AddRes( index, typeof( Cloth ), "Cloth", 1, "You do not have enough cloth." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( BlankCandle ), "Resources", "Blank Candle", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 2, "You do not have enough Beeswax." );
			SetNeedHeat( index, true );
			
			index = AddCraft( typeof( SealingWax ), "Resources", "Sealing Wax", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 1, "You do not have enough Beeswax." );
			AddRes( index, typeof( CandleShort ), "Small Candle", 2, "You do not have enough Small Candles." );
			SetNeedHeat( index, true );
			/* End Resources */

			/* Begin Candles */
			index = AddCraft( typeof( CandleShort ), "Candles", "Small Candle", 80.0, 105.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a blank Candle." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleShortColor ), "Candles", "Small colored Candle", 80.0, 105.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a Blank Candle." );
			AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleLong ), "Candles", "Large Candle", 80.0, 110.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a blank Candle." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleLongColor ), "Candles", "Large colored Candle", 80.0, 110.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a blank Candle." );
			AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleSkull ), "Candles", "Skull Candle", 100.0, 100.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a blank Candle." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			AddRes( index, typeof( CandleFitSkull ), "Candle for a skull", 1, "You need a Candle for a skull." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( CandleOfLove ), "Candles", "Candle of love", 100.0, 100.0, typeof( BlankCandle ), "Blank Candle", 1, "You need a blank Candle." );
			AddRes( index, typeof( CandleWick ), "Candle Wick", 1, "You need a Candle Wick." );
			AddRes( index, typeof( EssenceOfLove ), "Essence Of Love", 1, "You need Essence Of Love." );
			SetNeedHeat( index, true );
			/* End Candles */

			/* Begin Decorative */
			index = AddCraft( typeof( DippingStick ), "Decorative", "Dipping Stick", 75.0, 115.0, typeof( BlankCandle ), "Blank Candle", 3, "You need more Blank Candles." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( PileOfBlankCandles ), "Decorative", "Pile of Blank Candles", 75.0, 115.0, typeof( BlankCandle ), "Blank Candle", 5, "You need more Blank Candles." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( SomeBlankCandles ), "Decorative", "Some Blank Candles", 75.0, 115.0, typeof( BlankCandle ), "Blank Candle", 3, "You need more Blank Candles." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof( RawWaxBust ), "Decorative", "Raw Wax Bust", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 4, "You do not have enough Raw Beeswax." );
			SetNeedHeat( index, true );
			
			index = AddCraft( typeof(  WaxCatStatue ), "Decorative", "Wax Cat Statue", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 4, "You do not have enough Raw Beeswax." );
            AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			SetNeedHeat( index, true );

            index = AddCraft( typeof(  WaxPumpkin ), "Decorative", "Wax Pumpkin", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 4, "You do not have enough Raw Beeswax." );
            AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			SetNeedHeat( index, true );
			
			index = AddCraft( typeof(  WaxCornucopia ), "Decorative", "Wax Cornucopia", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 6, "You do not have enough Raw Beeswax." );
            AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			SetNeedHeat( index, true );

			index = AddCraft( typeof(  WaxBrazier  ), "Decorative", "Wax Brazier", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 6, "You do not have enough Raw Beeswax." );
            AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			SetNeedHeat( index, true );

            index = AddCraft( typeof(  WaxCampfire  ), "Decorative", "Wax Campfire", 50.0, 80.0, typeof( Beeswax ), "Raw Beeswax", 5, "You do not have enough Raw Beeswax." );
            AddRes( index, typeof( Dyes ), "Dyes", 1, "You need dyes." );
			SetNeedHeat( index, true );

            index = AddCraft(typeof( WaxLargeMushroom1 ), "Decorative", "Wax Large Mushroom", 50.0, 80.0, typeof(Beeswax), "Raw Beeswax", 6, "You do not have enough Raw Beeswax.");
            AddRes(index, typeof(Dyes), "Dyes", 1, "You need dyes.");
            SetNeedHeat(index, true);

            index = AddCraft(typeof( WaxLargeMushroom2 ), "Decorative", "Wax Medium Mushroom", 50.0, 80.0, typeof(Beeswax), "Raw Beeswax", 6, "You do not have enough Raw Beeswax.");
            AddRes(index, typeof(Dyes), "Dyes", 1, "You need dyes.");
            SetNeedHeat(index, true);

            index = AddCraft(typeof( WaxLadderSouth ), "Decorative", "Wax Ladder S", 50.0, 80.0, typeof(Beeswax), "Raw Beeswax", 10, "You do not have enough Raw Beeswax.");
            AddRes(index, typeof(Dyes), "Dyes", 1, "You need dyes.");
            SetNeedHeat(index, true);

            index = AddCraft(typeof( WaxLadderEast ), "Decorative", "Wax Ladder E", 50.0, 80.0, typeof(Beeswax), "Raw Beeswax", 10, "You do not have enough Raw Beeswax.");
            AddRes(index, typeof(Dyes), "Dyes", 1, "You need dyes.");
            SetNeedHeat(index, true);

            index = AddCraft(typeof( WaxSpider ), "Decorative", "Wax Spider", 50.0, 80.0, typeof(Beeswax), "Raw Beeswax", 5, "You do not have enough Raw Beeswax.");
            AddRes(index, typeof(Dyes), "Dyes", 1, "You need dyes.");
            SetNeedHeat(index, true);
			/* End Decorative */

		}
	}
}