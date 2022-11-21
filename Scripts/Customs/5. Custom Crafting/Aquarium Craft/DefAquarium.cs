using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefAquarium : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Fishing; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>AQUARIUM MENU</CENTER></basefont>"; } 
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefAquarium();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefAquarium() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
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
			from.PlaySound( 0x1C6 ); 
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x1C6 );
			}
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
				from.PlaySound( 0x1c6 );

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
			int index = AddCraft( typeof( BladePlantSmallFishtankAddonDeed ), "Deco Tanks", "Blade Plant", 60.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Nightshade ), 1044358, 1, 1044366 );

			index = AddCraft( typeof( CoralSmallFishtankAddonDeed ), "Deco Tanks", "Coral", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Granite ), 1044514, 2, 1044513 );

			index = AddCraft( typeof( DeadFishSmallFishtankAddonDeed ), "Deco Tanks", "Dead Fish", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Bone ), "Bones", 10, 1049063 );

			index = AddCraft( typeof( EmptyFishtankAddonDeed ), "Deco Tanks", "Empty Tank", 50.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );

			index = AddCraft( typeof( GhostShipAnchorSmallFishtankAddonDeed ), "Deco Tanks", "Ghost Anchor", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( IronIngot ), 1044036, 2, 1044037 );

			index = AddCraft( typeof( SunkenShipSmallFishtankAddonDeed ), "Deco Tanks", "Sunken Ship", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Log ), 1044041, 2, 1044351 );

			index = AddCraft( typeof( TreasureChestSmallFishtankAddonDeed ), "Deco Tanks", "Treasure Chest", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( GoldIngot ), "Gold Ingots", 4, 1044037 );

			index = AddCraft( typeof( WhitePearlSmallFishtankAddonDeed ), "Deco Tanks", "White Pearl", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( WhitePearl ), "White Pearl", 1, 1044240 );

			index = AddCraft( typeof( AlbinoAngelfishSmallFishtankAddonDeed ), "Fish Tanks", "Albino Angel", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( AngelfishSmallFishtankAddonDeed ), "Fish Tanks", "Angel Fish", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( BlueCorySmallFishtankAddonDeed ), "Fish Tanks", "Blue Cory", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( BrineShrimpSmallFishtankAddonDeed ), "Fish Tanks", "Brine Shrimp", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( BritainiaCrownFishSmallFishtankAddonDeed ), "Fish Tanks", "Britania Crown", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( ClownfishSmallFishtankAddonDeed ), "Fish Tanks", "Clown Fish", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( FandancerSmallFishtankAddonDeed ), "Fish Tanks", "Fandancer Fish", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( FishiesSmallFishtankAddonDeed ), "Fish Tanks", "Fishies", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( GoldenBroadtailSmallFishtankAddonDeed ), "Fish Tanks", "Golden Broad Tail", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( JellyfishSmallFishtankAddonDeed ), "Fish Tanks", "Jelly Fish", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( MinocBlueSmallFishtankAddonDeed ), "Fish Tanks", "Minoc Blue", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( NujelmHoneySmallFishtankAddonDeed ), "Fish Tanks", "Nujelm Honey", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( RainbowMollySmallFishtankAddonDeed ), "Fish Tanks", "Rainbow Molly", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( ShrimpSmallFishtankAddonDeed ), "Fish Tanks", "Shrimp", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( SmallMouthSuckerFishSmallFishtankAddonDeed ), "Fish Tanks", "Smallmouth Sucker", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( SpeckledCrabSmallFishtankAddonDeed ), "Fish Tanks", "Speckled Crab", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( SpottedBucaneerSmallFishtankAddonDeed ), "Fish Tanks", "Spotted Bucaneer", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( SpottedPufferSmallFishtankAddonDeed ), "Fish Tanks", "Spotted Puffer", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( StripedSosarianSwillSmallFishtankAddonDeed ), "Fish Tanks", "Striped Sosarian", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );

			index = AddCraft( typeof( VesperReefTigerSmallFishtankAddonDeed ), "Fish Tanks", "Vesper Reef Tiger", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 1, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaFishtankNAddonDeed ), "Specialty Tanks", "Deep Sea Tank North", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaFishtankNWcornerAddonDeed ), "Specialty Tanks", "Deep Sea Tank NW Corner", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );

			index = AddCraft( typeof( DeepSeaFishtankWAddonDeed ), "Specialty Tanks", "Deep Sea Tank West", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaFishtankNReplaceableAddonDeed ), "Specialty Tanks", "Deep Sea Tank North w/o Glass", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( OneReplaceableDeepSeaFishtankWAddonDeed ), "Specialty Tanks", "Deep Sea Tank West w/o Glass", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaNWCornerWithGlassAddonDeed ), "Specialty Tanks", "Deep Sea Tank NW Corner w/Glass", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaNWithGlassAddonDeed ), "Specialty Tanks", "Deep Sea Tank NW w/Glass", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( DeepSeaWWithGlassAddonDeed ), "Specialty Tanks", "Deep Sea Tank West w/Glass", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( EastWestCornerScaffoldAddonDeed ), "Specialty Tanks", "Tank w/ Scaffold North-South Corner", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( EastWestScaffoldAddonDeed ), "Specialty Tanks", "Tank w/ Scaffold North-South", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( NorthSouthCornerScaffoldAddonDeed ), "Specialty Tanks", "Tank w/ Scaffold East-West Corner", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( NorthSouthScaffoldAddonDeed ), "Specialty Tanks", "Tank w/ Scaffold East-West", 70.0, 95.0, typeof( Sand ), 1044625, 25, 1044627 );
			AddRes( index, typeof( BaseBeverage ), 1046458, 5, 1044253 );
			AddRes( index, typeof( Fish ), "Fish", 3, "You need a fish to make that" );
			
			index = AddCraft( typeof( FishtankLightbulb150 ), "Fish Tank Lights", "150-watt lightbulb", 75.0, 100.0, typeof( Sand ), 1044625, 5, 1044627 );
			
			index = AddCraft( typeof( FishtankLightbulb225 ), "Fish Tank Lights", "225-watt lightbulb", 75.0, 100.0, typeof( Sand ), 1044625, 8, 1044627 );
			
			index = AddCraft( typeof( FishtankLightbulb300 ), "Fish Tank Lights", "300-watt lightbulb", 75.0, 100.0, typeof( Sand ), 1044625, 11, 1044627 );


		}
	}
}