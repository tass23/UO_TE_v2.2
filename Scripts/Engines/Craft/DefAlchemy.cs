using System;
using Server.Items;
using Server.Engines.Plants;

namespace Server.Engines.Craft
{
	public class DefAlchemy : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044001; } // <CENTER>ALCHEMY MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefAlchemy();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefAlchemy() : base( 1, 1, 1.25 )// base( 1, 1, 3.1 )
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
		
		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x242 );
		}

		private static Type typeofPotion = typeof( BasePotion );

		public static bool IsPotion( Type type )
		{
			return typeofPotion.IsAssignableFrom( type );
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				if ( IsPotion( item.ItemType ) )
				{
					from.AddToBackpack( new Bottle() );
					return 500287; // You fail to create a useful potion.
				}
				else
				{
					return 1044043; // You failed to create the item, and some of your materials are lost.
				}
			}
			else
			{
				from.PlaySound( 0x240 ); // Sound of a filling bottle

				if ( IsPotion( item.ItemType ) )
				{
					if ( quality == -1 )
						return 1048136; // You create the potion and pour it into a keg.
					else
						return 500279; // You pour the potion into a bottle...
				}
				else
				{
					return 1044154; // You create the item.
				}
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			// Refresh Potion
			index = AddCraft( typeof( RefreshPotion ), 1044530, 1044538, -25, 25.0, typeof( BlackPearl ), 1044353, 1, 1044361 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( TotalRefreshPotion ), 1044530, 1044539, 25.0, 75.0, typeof( BlackPearl ), 1044353, 5, 1044361 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Agility Potion
			index = AddCraft( typeof( AgilityPotion ), 1044531, 1044540, 15.0, 65.0, typeof( Bloodmoss ), 1044354, 1, 1044362 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterAgilityPotion ), 1044531, 1044541, 35.0, 85.0, typeof( Bloodmoss ), 1044354, 3, 1044362 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Nightsight Potion
			index = AddCraft( typeof( NightSightPotion ), 1044532, 1044542, -25.0, 25.0, typeof( SpidersSilk ), 1044360, 1, 1044368 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Heal Potion
			index = AddCraft( typeof( LesserHealPotion ), 1044533, 1044543, -25.0, 25.0, typeof( Ginseng ), 1044356, 1, 1044364 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( HealPotion ), 1044533, 1044544, 15.0, 65.0, typeof( Ginseng ), 1044356, 3, 1044364 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterHealPotion ), 1044533, 1044545, 55.0, 105.0, typeof( Ginseng ), 1044356, 7, 1044364 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Strength Potion
			index = AddCraft( typeof( StrengthPotion ), 1044534, 1044546, 25.0, 75.0, typeof( MandrakeRoot ), 1044357, 2, 1044365 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterStrengthPotion ), 1044534, 1044547, 45.0, 95.0, typeof( MandrakeRoot ), 1044357, 5, 1044365 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Poison Potion
			index = AddCraft( typeof( LesserPoisonPotion ), 1044535, 1044548, -5.0, 45.0, typeof( Nightshade ), 1044358, 1, 1044366 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( PoisonPotion ), 1044535, 1044549, 15.0, 65.0, typeof( Nightshade ), 1044358, 2, 1044366 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterPoisonPotion ), 1044535, 1044550, 55.0, 105.0, typeof( Nightshade ), 1044358, 4, 1044366 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( DeadlyPoisonPotion ), 1044535, 1044551, 90.0, 140.0, typeof( Nightshade ), 1044358, 8, 1044366 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
            index = AddCraft(typeof(ScouringToxin), 1044535, 1112292, 75.0, 100.0, typeof(ToxicVenomSac), 1112291, 1, 1044366);
            AddRes(index, typeof(Bottle), 1044529, 1, 500315);


			// Cure Potion
			index = AddCraft( typeof( LesserCurePotion ), 1044536, 1044552, -10.0, 40.0, typeof( Garlic ), 1044355, 1, 1044363 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( CurePotion ), 1044536, 1044553, 25.0, 75.0, typeof( Garlic ), 1044355, 3, 1044363 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterCurePotion ), 1044536, 1044554, 65.0, 115.0, typeof( Garlic ), 1044355, 6, 1044363 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			// Explosion Potion
			index = AddCraft( typeof( LesserExplosionPotion ), 1044537, 1044555, 5.0, 55.0, typeof( SulfurousAsh ), 1044359, 3, 1044367 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( ExplosionPotion ), 1044537, 1044556, 35.0, 85.0, typeof( SulfurousAsh ), 1044359, 5, 1044367 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );
			index = AddCraft( typeof( GreaterExplosionPotion ), 1044537, 1044557, 65.0, 115.0, typeof( SulfurousAsh ), 1044359, 10, 1044367 );
			AddRes( index, typeof ( Bottle ), 1044529, 1, 500315 );

			if( Core.SE )
			{
				index = AddCraft( typeof( SmokeBomb ), 1044537, 1030248, 90.0, 120.0, typeof( Eggs ), 1044477, 1, 1044253 );
				AddRes( index, typeof ( Ginseng ), 1044356, 3, 1044364 );
				SetNeededExpansion( index, Expansion.SE );
			}
			#region UO-The Expanse
			index = AddCraft(typeof(HolyWater), 1044537, "holy water bomb", 70.0, 95.0, typeof(HolyWaterUnblessed), "Regular Holy Water", 1, "You do not have enough regular Holy Water.");
			AddRes(index, typeof(Bottle), "Bottle", 1, "You do not have enough Bottles.");
			AddRes(index, typeof(FreshGarlic), "Fresh Garlic", 1, "You do not have enough Fresh Garlic.");
			#endregion
			
			#region Mondain's Legacy
			// region Necromancy (Core.ML?)
			index = AddCraft( typeof( ConflagrationPotion ), 1044109, 1072096, 55.0, 105.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof ( GraveDust ), 1023983, 5, 1044253 );
			
			index = AddCraft( typeof( GreaterConflagrationPotion ), 1044109, 1072099, 70.0, 120.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof ( GraveDust ), 1023983, 10, 1044253 );
			
			index = AddCraft( typeof( ConfusionBlastPotion ), 1044109, 1072106, 55.0, 105.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof ( PigIron ), 1023978, 5, 1044253 );
			
			index = AddCraft( typeof( GreaterConfusionBlastPotion ), 1044109, 1072109, 70.0, 120.0, typeof( Bottle ), 1044529, 1, 500315 );
			AddRes( index, typeof ( PigIron ), 1023978, 10, 1044253 );
			
			// Earthen Mixtures
			if ( Core.ML )
			{
				index = AddCraft( typeof( InvisibilityPotion ), 1074832, 1074860, 65.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
				AddRes( index, typeof ( Bloodmoss ), 1044354, 4, 1044362 );
				AddRes( index, typeof ( Nightshade ), 1044358, 4, 1044366 );
				AddRecipe( index, (int) TinkerRecipes.InvisibilityPotion );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( ParasiticPotion ), 1074832, 1072942, 65.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
				AddRes( index, typeof ( ParasiticPlant ), 1073474, 5, 1044253 );
				AddRecipe( index, (int) TinkerRecipes.ParasiticPotion );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( DarkglowPotion ), 1074832, 1072943, 65.0, 115.0, typeof( Bottle ), 1044529, 1, 500315 );
				AddRes( index, typeof ( LuminescentFungi ), 1073475, 5, 1044253 );
				AddRecipe( index, (int) TinkerRecipes.DarkglowPotion );
				SetNeededExpansion( index, Expansion.ML );
				
				index = AddCraft( typeof( HoveringWisp ), 1074832, 1072881, 65.0, 115.0, typeof( CapturedEssence ), 1032686, 4, 1044253 );
				AddRecipe( index, (int) TinkerRecipes.HoveringWisp );
				SetNeededExpansion( index, Expansion.ML );
			}
			#endregion
            #region SA
            /* Plant Pigments/Natural Dyes*/

            if (Core.SA)
            {
                index = AddCraft(typeof(PlantPigment), "Plant Pigments", 1112132, 33.0, 83.0, typeof(PlantClippings), 1112131, 1, 1044253);
                AddRes(index, typeof(Bottle), 1023854, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(NaturalDye), "Plant Pigments", 1112136, 75.0, 100.0, typeof(PlantPigment), 1112132, 1, 1044253);
                AddRes(index, typeof(ColorFixative), 1112135, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(ColorFixative), "Plant Pigments", 1112135, 75.0, 100.0, typeof(SilverSerpentVenom), 1112173, 1, 1044253);
                AddRes(index, typeof(BottleOfWine), 1022503, 1, 1044253);
                SetNeededExpansion(index, Expansion.SA);

                index = AddCraft(typeof(SoftenedReeds), "Plant Pigments", 1112249, 75.0, 100.0, typeof(ScouringToxin), 1112292, 2, "You do not have enough scouring toxin to make this.");
                AddRes(index, typeof(DryReeds), 1112248, 1, 1112250); 
            }
            #endregion
			#region UO-The Expanse
			//Sparklers
			AddCraft( typeof( SpiderSilkSmallSparkler ), "Sparklers", "small spider's silk", 42.0, 50.3, typeof( SpidersSilk ), "Spider's Silk", 7 );
			AddCraft( typeof( SpiderSilkLargeSparkler ), "Sparklers", "large spider's silk", 53.0, 70.7, typeof( SpidersSilk ), "Spider's Silk", 12 );
			AddCraft( typeof( BlackPearlSmallSparkler ), "Sparklers", "small black pearl", 42.0, 50.3, typeof( BlackPearl ), "Black Pearl", 7 );
			AddCraft( typeof( BlackPearlLargeSparkler ), "Sparklers", "large black pearl", 53.0, 70.7, typeof( BlackPearl ), "Black Pearl", 12 );
			AddCraft( typeof( BloodMossSmallSparkler ), "Sparklers", "small blood moss", 42.0, 50.3, typeof( Bloodmoss ), "Blood Moss", 7 );
			AddCraft( typeof( BloodMossLargeSparkler ), "Sparklers", "large blood moss", 53.0, 70.7, typeof( Bloodmoss ), "Blood Moss", 12 );
			AddCraft( typeof( GinsengSmallSparkler ), "Sparklers", "small ginseng", 42.0, 50.3, typeof( Ginseng ), "Ginseng", 7 );
			AddCraft( typeof( GinsengLargeSparkler ), "Sparklers", "large ginseng", 53.0, 70.7, typeof( Ginseng ), "Ginseng", 12 );
			AddCraft( typeof( MandrakeRootSmallSparkler ), "Sparklers", "small mandrake root", 42.0, 50.3, typeof( MandrakeRoot ), "Mandrake Root", 7 );
			AddCraft( typeof( MandrakeRootLargeSparkler ), "Sparklers", "large mandrake root", 53.0, 70.7, typeof( MandrakeRoot ), "Mandrake Root", 12 );
			AddCraft( typeof( NightshadeSmallSparkler ), "Sparklers", "small nightshade", 42.0, 50.3, typeof( Nightshade ), "Nightshade", 7 );
			AddCraft( typeof( NightshadeLargeSparkler ), "Sparklers", "large nightshade", 53.0, 70.7, typeof( Nightshade ), "Nightshade", 12 );
			AddCraft( typeof( GarlicSmallSparkler ), "Sparklers", "small garlic", 42.0, 50.3, typeof( Garlic ), "Garlic", 7 );
			AddCraft( typeof( GarlicLargeSparkler ), "Sparklers", "large garlic", 53.0, 70.7, typeof( Garlic ), "Garlic", 12 );
			AddCraft( typeof( SulfurousAshSmallSparkler ), "Sparklers", "small sulfurous ash", 42.0, 50.3, typeof( SulfurousAsh ), "Sulfurous Ash", 7 );
			AddCraft( typeof( SulfurousAshLargeSparkler ), "Sparklers", "large sulfurous ash", 53.0, 70.7, typeof( SulfurousAsh ), "Sulfurous Ash", 12 );
			//Rockets
			AddCraft( typeof( SpiderSilkFirework ), "Rockets", "spider's silk", 78.5, 115.8, typeof( SpidersSilk ), "Spider's Silk", 30 );
			AddCraft( typeof( BlackPearlFirework ), "Rockets", "black pearl", 78.5, 115.8, typeof( BlackPearl ), "Black Pearl", 30 );
			AddCraft( typeof( BloodMossFirework ), "Rockets", "blood moss", 78.5, 115.8, typeof( Bloodmoss ), "Blood Moss", 30 );
			AddCraft( typeof( GinsengFirework ), "Rockets", "ginseng", 78.5, 115.8, typeof( Ginseng ), "Ginseng", 30 );
			AddCraft( typeof( MandrakeRootFirework ), "Rockets", "mandrake root", 78.5, 115.8, typeof( MandrakeRoot ), "Mandrake Root", 30 );
			AddCraft( typeof( NightshadeFirework ), "Rockets", "nightshade", 78.5, 115.8, typeof( Nightshade ), "Nightshade", 30 );
			AddCraft( typeof( GarlicFirework ), "Rockets", "garlic", 78.5, 115.8, typeof( Garlic ), "Garlic", 30 );
			AddCraft( typeof( SulfurousAshFirework ), "Rockets", "sulfurous ash", 78.5, 115.8, typeof( SulfurousAsh ), "Sulfurous Ash", 30 );
			//Fountains
			AddCraft( typeof( SpiderSilkFountain ), "Fountains", "spider's silkn", 90.0, 122.3, typeof( SpidersSilk ), "Spider's Silk", 42 );
			AddCraft( typeof( BlackPearlFountain ), "Fountains", "black pearl", 90.0, 122.3, typeof( BlackPearl ), "Black Pearl", 42 );
			AddCraft( typeof( BloodMossFountain ), "Fountains", "blood moss", 90.0, 122.3, typeof( Bloodmoss ), "Blood Moss", 42 );
			AddCraft( typeof( GinsengFountain ), "Fountains", "ginseng", 90.0, 122.3, typeof( Ginseng ), "Ginseng", 42 );
			AddCraft( typeof( MandrakeRootFountain ), "Fountains", "mandrake root", 90.0, 122.3, typeof( MandrakeRoot ), "Mandrake Root", 42 );
			AddCraft( typeof( NightshadeFountain ), "Fountains", "nightshade", 90.0, 122.3, typeof( Nightshade ), "Nightshade", 42 );
			AddCraft( typeof( GarlicFountain ), "Fountains", "garlic", 90.0, 122.3, typeof( Garlic ), "Garlic", 42 );
			AddCraft( typeof( SulfurousAshFountain ), "Fountains", "sulfurous ash", 90.0, 122.3, typeof( SulfurousAsh ), "Sulfurous Ash", 42 );
			#endregion
		}
	}
}