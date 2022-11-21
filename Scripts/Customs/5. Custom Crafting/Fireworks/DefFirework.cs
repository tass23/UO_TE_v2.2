using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefFirework : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Alchemy;	}
		}

		public override int GumpTitleNumber
		{
			get { return 1044004; } // <CENTER>TOY CRAFT MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefFirework();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefFirework() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!

			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			from.PlaySound( 0x58 );
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

			//Sparklers
                        AddCraft( typeof( SpiderSilkSmallSparkler ), "Sparklers", "spider silk small sparkler", 42.0, 50.3, typeof( SpidersSilk ), "Spiders Silk", 7 );
                        AddCraft( typeof( SpiderSilkLargeSparkler ), "Sparklers", "spider silk large sparkler", 53.0, 70.7, typeof( SpidersSilk ), "Spiders Silk", 12 );
                        AddCraft( typeof( BlackPearlSmallSparkler ), "Sparklers", "black pearl small sparkler", 42.0, 50.3, typeof( BlackPearl ), "Black Pearl", 7 );
                        AddCraft( typeof( BlackPearlLargeSparkler ), "Sparklers", "black pearl large sparkler", 53.0, 70.7, typeof( BlackPearl ), "Black Pearl", 12 );
                        AddCraft( typeof( BloodMossSmallSparkler ), "Sparklers", "blood moss small sparkler", 42.0, 50.3, typeof( Bloodmoss ), "Blood Moss", 7 );
                        AddCraft( typeof( BloodMossLargeSparkler ), "Sparklers", "blood moss large sparkler", 53.0, 70.7, typeof( Bloodmoss ), "Blood Moss", 12 );
                        AddCraft( typeof( GinsengSmallSparkler ), "Sparklers", "ginseng small sparkler", 42.0, 50.3, typeof( Ginseng ), "Ginseng", 7 );
                        AddCraft( typeof( GinsengLargeSparkler ), "Sparklers", "ginseng large sparkler", 53.0, 70.7, typeof( Ginseng ), "Ginseng", 12 );
                        AddCraft( typeof( MandrakeRootSmallSparkler ), "Sparklers", "mandrake root small sparkler", 42.0, 50.3, typeof( MandrakeRoot ), "Mandrake Root", 7 );
                        AddCraft( typeof( MandrakeRootLargeSparkler ), "Sparklers", "mandrake root large sparkler", 53.0, 70.7, typeof( MandrakeRoot ), "Mandrake Root", 12 );
                        AddCraft( typeof( NightshadeSmallSparkler ), "Sparklers", "nightshade small sparkler", 42.0, 50.3, typeof( Nightshade ), "Nightshade", 7 );
                        AddCraft( typeof( NightshadeLargeSparkler ), "Sparklers", "nightshade large sparkler", 53.0, 70.7, typeof( Nightshade ), "Nightshade", 12 );
                        AddCraft( typeof( GarlicSmallSparkler ), "Sparklers", "garlic small sparkler", 42.0, 50.3, typeof( Garlic ), "Garlic", 7 );
                        AddCraft( typeof( GarlicLargeSparkler ), "Sparklers", "garlic large sparkler", 53.0, 70.7, typeof( Garlic ), "Garlic", 12 );
                        AddCraft( typeof( SulfurousAshSmallSparkler ), "Sparklers", "sulfurous ash small sparkler", 42.0, 50.3, typeof( SulfurousAsh ), "Sulfurous Ash", 7 );
                        AddCraft( typeof( SulfurousAshLargeSparkler ), "Sparklers", "sulfurous ash large sparkler", 53.0, 70.7, typeof( SulfurousAsh ), "Sulfurous Ash", 12 );

			//Rockets
                        AddCraft( typeof( BlackPearlFirework ), "Rockets", "black pearl firework", 78.5, 115.8, typeof( BlackPearl ), "Black Pearl", 30 );
                        AddCraft( typeof( BloodMossFirework ), "Rockets", "blood moss firework", 78.5, 115.8, typeof( Bloodmoss ), "Blood Moss", 30 );
                        AddCraft( typeof( GinsengFirework ), "Rockets", "ginseng firework", 78.5, 115.8, typeof( Ginseng ), "Ginseng", 30 );
                        AddCraft( typeof( MandrakeRootFirework ), "Rockets", "mandrake root firework", 78.5, 115.8, typeof( MandrakeRoot ), "Mandrake Root", 30 );
                        AddCraft( typeof( NightshadeFirework ), "Rockets", "nightshade firework", 78.5, 115.8, typeof( Nightshade ), "Nightshade", 30 );
                        AddCraft( typeof( GarlicFirework ), "Rockets", "garlic firework", 78.5, 115.8, typeof( Garlic ), "Garlic", 30 );
                        AddCraft( typeof( SulfurousAshFirework ), "Rockets", "sulfurous ash firework", 78.5, 115.8, typeof( SulfurousAsh ), "Sulfurous Ash", 30 );
                        AddCraft( typeof( SpiderSilkFirework ), "Rockets", "spider silk firework", 78.5, 115.8, typeof( SpidersSilk ), "Spiders Silk", 30 );

			//Fountains
                        AddCraft( typeof( BlackPearlFountain ), "Fountains", "black pearl fountain", 90.0, 122.3, typeof( BlackPearl ), "Black Pearl", 42 );
                        AddCraft( typeof( BloodMossFountain ), "Fountains", "blood moss fountain", 90.0, 122.3, typeof( Bloodmoss ), "Blood Moss", 42 );
                        AddCraft( typeof( GinsengFountain ), "Fountains", "ginseng fountain", 90.0, 122.3, typeof( Ginseng ), "Ginseng", 42 );
                        AddCraft( typeof( MandrakeRootFountain ), "Fountains", "mandrake root fountain", 90.0, 122.3, typeof( MandrakeRoot ), "Mandrake Root", 42 );
                        AddCraft( typeof( NightshadeFountain ), "Fountains", "nightshade fountain", 90.0, 122.3, typeof( Nightshade ), "Nightshade", 42 );
                        AddCraft( typeof( GarlicFountain ), "Fountains", "garlic fountain", 90.0, 122.3, typeof( Garlic ), "Garlic", 42 );
                        AddCraft( typeof( SulfurousAshFountain ), "Fountains", "sulfurous ash fountain", 90.0, 122.3, typeof( SulfurousAsh ), "Sulfurous Ash", 42 );
                        AddCraft( typeof( SpiderSilkFountain ), "Fountains", "spider silk fountain", 90.0, 122.3, typeof( SpidersSilk ), "Spiders Silk", 42 );

			MarkOption = true;
			Repair = Core.AOS;
		}
	}
}