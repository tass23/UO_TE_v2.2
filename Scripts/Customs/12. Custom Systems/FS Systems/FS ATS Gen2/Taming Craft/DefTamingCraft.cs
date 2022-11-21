using System;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.Craft
{
	public class DefTamingCraft : CraftSystem
	{
		public override SkillName MainSkill
		{
			get	{ return SkillName.AnimalTaming; }
		}

		public override string GumpTitleString
		{
			get { return "<BASEFONT COLOR=#FFFFFF><CENTER>VETERINARY CRAFTING</CENTER></BASEFONT>"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefTamingCraft();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.50; // 50%
		}

		private DefTamingCraft() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type typeItem )
		{
			if ( tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
				
			return 0;
		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x249 );
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
				return 1044154; // You create the item.
			}
		}

		public override void InitCraftList()
		{
			int index = -1;

			//Potions

			if ( FSATS.EnableShrinkSystem == true )
			{
				index = AddCraft( typeof( PetShrinkPotion ), "Pet Potions", "Shrink Potion", 85.0, 100.0, typeof( Bottle ), "Bottle", 1 );
				AddSkill( index, SkillName.AnimalLore, 50.0, 60.0 );
				AddRes( index, typeof( SpringWater ), "Spring Water", 10 );
				AddRes( index, typeof( PetrifiedWood ), "Petrified Wood", 5 );
				//SetUseAllRes( index, true );
			}
			
			index = AddCraft( typeof( PetResurrectPotion ), "Pet Potions", "Pet Resurrection Potion", 100.0, 120.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Veterinary, 80.0, 100.0 );
			AddRes( index, typeof( DestroyingAngel ), "Destroying Angel", 25 );
			AddRes( index, typeof( Bloodmoss ), "Blood Moss", 15 );
			//SetUseAllRes( index, true );
			
			index = AddCraft( typeof( CurePotionPet ), "Pet Potions", "Pet Cure Potion", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Veterinary, 60.0, 80.0 );
			AddRes( index, typeof( SpringWater ), "Spring Water", 5 );
			AddRes( index, typeof( Ginseng ), "Ginseng", 3 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( GreaterCurePotionPet ), "Pet Potions", "Pet Greater Cure Potion", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Veterinary, 70.0, 90.0 );
			AddRes( index, typeof( SpringWater ), "Spring Water", 10 );
			AddRes( index, typeof( Ginseng ), "Ginseng", 7 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( HealPotionPet ), "Pet Potions", "Pet Heal Potion", 50.0, 70.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Veterinary, 50.0, 70.0 );
			AddRes( index, typeof( PetrifiedWood ), "Petrified Wood", 5 );
			AddRes( index, typeof( Garlic ), "Garlic", 3 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( GreaterHealPotionPet ), "Pet Potions", "Pet Greater Heal Potion", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.Veterinary, 60.0, 80.0 );
			AddRes( index, typeof( PetrifiedWood ), "Petrified Wood", 15 );
			AddRes( index, typeof( Garlic ), "Garlic", 7 );
			AddRes( index, typeof( SpidersSilk ), "Spider's Silk", 5 );
			//SetUseAllRes( index, true );

			//Dyes
			index = AddCraft( typeof( RedPetDye ), "Normal Pet Dyes", "Red Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( BluePetDye ), "Normal Pet Dyes", "Blue Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( GreenPetDye ), "Normal Pet Dyes", "Green Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( YellowPetDye ), "Normal Pet Dyes", "Yellow Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( PurplePetDye ), "Normal Pet Dyes", "Purple Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( OrangePetDye ), "Normal Pet Dyes", "Orange Pet Dye", 25.0, 35.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( WhitePetDye ), "Unique Pet Dyes", "White Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes(index, typeof(Diamond), 1062608, 5, 1044240);
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( BlackPetDye ), "Unique Pet Dyes", "Black Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes(index, typeof(PigIron), "Pig Iron", 10, "You do not have enough Pig Iron");
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( BlazePetDye ), "Unique Pet Dyes", "Blaze Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof(SmallFireRock), "Small Fire Rock", 5, "You do not have enough Fire Rock.");
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( BloodPetDye ), "Unique Pet Dyes", "Blood Red Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( IceBluePetDye ), "Unique Pet Dyes", "Ice Blue Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof( WaterFeather ), "Water Feather", 10, "You do not have enough Water Feathers.");			
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( IceGreenPetDye ), "Unique Pet Dyes", "Ice Green Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof( DragonFeather ), "Dragon Feather", 10, "You do not have enough Dragon Feathers." );
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( PinkPetDye ), "Unique Pet Dyes", "Pink Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof(BloodFeather), "Blood Feather", 10, "You do not have enough Blood Feathers.");
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( MossGreenPetDye ), "Unique Pet Dyes", "Mystic Green Pet Dye", 60.0, 80.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof(PoisonFeather), "Poison Feather", 10, "You do not have enough Poison Feathers.");
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( IceWhitePetDye ), "Rare Pet Dyes", "Ice White Pet Dye", 80.0, 100.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes( index, typeof( WhitePearl ), 1026253, 2, 1053098 );
			AddRes( index, typeof(AirFeather), "Air Feather", 20, "You do not have enough Air Feathers.");
			//SetUseAllRes( index, true );

			index = AddCraft( typeof( FrostBluePetDye ), "Rare Pet Dyes", "Frost Blue Pet Dye", 80.0, 100.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes(index, typeof(BlueDiamond), 1032696, 10, 1044253);
			AddRes( index, typeof(AirFeather), "Air Feather", 20, "You do not have enough Air Feathers.");
			//SetUseAllRes( index, true );
			// Misc

			index = AddCraft( typeof( ElectricBluePetDye ), "Rare Pet Dyes", "Electric Blue Pet Dye", 80.0, 100.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes(index, typeof(BlueDiamond), 1032696, 10, 1044253);
			AddRes( index, typeof(WaterFeather), "Water Feather", 20, "You do not have enough Water Feathers.");
			
			index = AddCraft( typeof( ElectricGreenPetDye ), "Rare Pet Dyes", "Electric Green Pet Dye", 80.0, 100.0, typeof( Bottle ), "Bottle", 1 );
			AddSkill( index, SkillName.AnimalLore, 10.0, 20.0 );
			AddRes( index, typeof( Dyes ), "Dyes", 1 );
			AddRes(index, typeof(PerfectEmerald), "Perfect Emerald", 10, "You do not have enough Perfect Emeralds.");
			AddRes( index, typeof(PoisonFeather), "Poison Feather", 20, "You do not have enough Poison Feathers.");

			if ( FSATS.EnableShrinkSystem == true )
			{
				index = AddCraft( typeof( NewHitchingPostEast3Deed ), "Misc.", "Hitching Post Addon (East)", 110.0, 120.0, typeof( Board ), "Boards", 15 );
				AddSkill( index, SkillName.AnimalLore, 80.0, 100.0 );
				AddRes( index, typeof( IronIngot ), "Iron Ingots", 25 );
				//SetUseAllRes( index, true );

				index = AddCraft( typeof( NewHitchingPostSouth3Deed ), "Misc.", "Hitching Post Addon (South)", 110.0, 120.0, typeof( Board ), "Boards", 15 );
				AddSkill( index, SkillName.AnimalLore, 80.0, 100.0 );
				AddRes( index, typeof( IronIngot ), "Iron Ingots", 25 );
				//SetUseAllRes( index, true );

				index = AddCraft( typeof( PetLeash3 ), "Misc.", "Pet Leash", 90.0, 120.0, typeof( Leather ), "Leather", 10 );
				AddSkill( index, SkillName.AnimalLore, 80.0, 100.0 );
				AddRes( index, typeof( IronIngot ), "Iron Ingots", 25 );
				//SetUseAllRes( index, true );
			}

			index = AddCraft( typeof( PetDyeTub ), "Misc.", "Pet Dye Tub", 80.0, 95.0, typeof( BarrelStaves ), "Barrel Staves", 10 );
			AddSkill( index, SkillName.AnimalLore, 80.0, 95.0 );
			AddRes( index, typeof( RedPetDye ), "Red Pet Dye", 1 );
			AddRes( index, typeof( BluePetDye ), "Blue Pet Dye", 1 );
			AddRes( index, typeof( GreenPetDye ), "Green Pet Dye", 1 );
			//SetUseAllRes( index, true );
			
		}
	}
}