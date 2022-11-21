using System;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefMysticBowcraft : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.Fletching; }
		}

		public override int GumpTitleNumber
		{
			get { return 0; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>MYSTIC BOWCRAFT MENU</CENTER></basefont>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefMysticBowcraft();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefMysticBowcraft() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
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
			// no animation
			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 33, 5, 1, true, false, 0 );

			from.PlaySound( 0x55 );
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

		public override CraftECA ECA{ get{ return CraftECA.FiftyPercentChanceMinusTenPercent; } }

		public override void InitCraftList()
		{
			int index = -1;

			// Arrows and Bolts
			index = AddCraft( typeof( FireArrow ), "Arrows and Bolts", "Fire Arrow", 65.0, 90.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( FireFeather ), "Fire feather", 1, "You don't have enough Fire feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( BloodArrow ), "Arrows and Bolts", "Blood Arrow", 65.0, 90.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( BloodFeather ), "Blood feather", 1, "You don't have enough Blood feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( PoisonArrow ), "Arrows and Bolts", "Poison Arrow", 65.0, 90.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( PoisonFeather ), "Poison feather", 1, "You don't have enough Poison feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( MoonArrow ), "Arrows and Bolts", "Moon Arrow", 70.0, 95.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( AirFeather ), "Air feather", 1, "You don't have enough Air feathers." );
			AddRes( index, typeof( WaterFeather ), "Water feather", 1, "You don't have enough Water feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( FalconArrow ), "Arrows and Bolts", "Falcon Arrow", 70.0, 95.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( AirFeather ), "Air feather", 1, "You don't have enough Air feathers." );
			AddRes( index, typeof( BloodFeather ), "Blood feather", 1, "You don't have enough Blood feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( SunArrow ), "Arrows and Bolts", "Sun Arrow", 70.0, 95.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( AirFeather ), "Air feather", 1, "You don't have enough Air feathers." );
			AddRes( index, typeof( FireFeather ), "Fire feather", 1, "You don't have enough Fire feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( StarArrow ), "Arrows and Bolts", "Star Arrow", 75.0, 100.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( DragonFeather ), "Dragon feather", 1, "You don't have enough Dragon feathers." );
			AddRes( index, typeof( WaterFeather ), "Water feather", 1, "You don't have enough Water feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( DarkArrow ), "Arrows and Bolts", "Dark Arrow", 75.0, 100.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( DragonFeather ), "Dragon feather", 1, "You don't have enough Dragon feathers." );
			AddRes( index, typeof( PoisonFeather ), "Poison feather", 1, "You don't have enough Poison feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( ZodiacArrow ), "Arrows and Bolts", "Zodiac Arrow", 75.0, 100.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( DragonFeather ), "Dragon feather", 1, "You don't have enough Dragon feathers." );
			AddRes( index, typeof( FireFeather ), "Fire feather", 1, "You don't have enough Fire feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( AirBolt ), "Arrows and Bolts", "Air Bolt", 65.0, 90.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( AirFeather ), "Air feather", 1, "You don't have enough Air feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( WaterBolt ), "Arrows and Bolts", "Water Bolt", 65.0, 90.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( WaterFeather ), "Water feather", 1, "You don't have enough Water feathers." );
			SetUseAllRes( index, true );

			index = AddCraft( typeof( DeathBolt ), "Arrows and Bolts", "Death Bolt", 75.0, 100.0, typeof( Shaft ), 1027124, 1, "You don't have enough Shafts." );
			AddRes( index, typeof( DragonFeather ), "Dragon feather", 1, "You don't have enough Dragon feathers." );
			SetUseAllRes( index, true );

			// Bows
			index = AddCraft( typeof( BowBlaze ), "Bows", "Bow of Blaze" , 75.0, 100.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes( index, typeof( FireRuby ), 1026254, 1, 1053098 );
			AddRes( index, typeof( SilkThread ), "Silk Thread", 2, "You don't have enough Silk Thread." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
 
			index = AddCraft( typeof( BowLeech ), "Bows", "Bow of the Leech" , 75.0, 100.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(Amethyst), 1062605, 1, 1044240);
			AddRes( index, typeof( SilkThread ), "Silk Thread", 2, "You don't have enough Silk Thread." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( BowDecay ), "Bows", "Bow of Decay" , 75.0, 100.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(Emerald), 1062601, 1, 1044240);
			AddRes( index, typeof( SilkThread ), "Silk Thread", 2, "You don't have enough Silk Thread." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( LunaBow ), "Bows", "Luna Bow" , 85.0, 110.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(Diamond), 1062608, 1, 1044240);
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( FalconBow ), "Bows", "Falcon's Bow" , 85.0, 110.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes( index, typeof( AirFeather ), "Air feather", 1, "You don't have enough Air feathers." );
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( SunBow ), "Bows", "Bow of the Sun" , 85.0, 110.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(Amber), 1062607, 1, 1044240);
			AddRes( index, typeof( SasquatchHair ), "Sasquatch Hair", 2, "You don't have enough Sasquatch Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( BowStars ), "Bows", "Bow of the Stars" , 95.0, 120.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(StarSapphire), 1062600, 1, 1044253);
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( BowZodiac ), "Bows", "Bow of the Zodiac" , 95.0, 120.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(Tourmaline), 1062606, 1, 1044240);
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( BowDark ), "Bows", "Bow of the Dark" , 95.0, 120.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(DarkSapphire), 1032690, 1, 1044240);
			AddRes( index, typeof( SasquatchHair ), "Sasquatch Hair", 2, "You don't have enough Sasquatch Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			// Crossbows
			index = AddCraft( typeof( ScreechingCrossbow ), "Crossbows", "Screeching Crossbow", 75.0, 100.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes( index, typeof( DragonFeather ), "Dragon feather", 1, "You don't have enough Dragon feathers." );
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( TidalCrossbow ), "Crossbows", "Tidal Crossbow", 75.0, 100.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes( index, typeof( SasquatchHair ), "Sasquatch Hair", 2, "You don't have enough Sasquatch Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");

			index = AddCraft( typeof( ShadowCrossbow ), "Crossbows", "Crossbow of Shadow", 90.0, 115.0, typeof( Board ), 1044041, 10, 1044351 );
			AddRes(index, typeof(DarkSapphire), 1032690, 1, 1044240);
			AddRes( index, typeof( YetiHair ), "Yeti Hair", 2, "You don't have enough Yeti Hair." );
			AddRes(index, typeof(Dyes), "Dyes", 2, "You don't have enough Dyes.");
			
			// Set the overidable material
			SetSubRes( typeof( Board ), "Change Wood" );

			// Add every material you want the player to be able to chose from
			// This will overide the overidable material
			AddSubRes( typeof( Board ),			"Normal", 0.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( OakBoard ),	    		"Oak", 65.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( AshBoard ),	   	 	"Ash", 80.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( YewBoard ),			"Yew", 95.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( BloodwoodBoard ),		"Bloodwood", 100.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( HeartwoodBoard ),			"Heartwood", 100.0, "You can't work with the kind of wood." );
			AddSubRes( typeof( FrostwoodBoard ),			"Frostwood", 100.0, "You can't work with the kind of wood." );

			MarkOption = true;
			Repair = Core.AOS;
		}
	}
}