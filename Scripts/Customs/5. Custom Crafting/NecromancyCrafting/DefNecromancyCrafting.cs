using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.Craft
{
	public class DefNecromancyCrafting : CraftSystem
	{
		public override SkillName MainSkill
		{
			get{ return SkillName.Necromancy; }
		}

		public override string GumpTitleString
		{
			get { return "<basefont color=#FFFFFF><CENTER>NECROMANCY CRAFTING MENU</CENTER></basefont>"; } // <CENTER>INSCRIPTION MENU</CENTER>
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefNecromancyCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.0; // 0%
		}

		private DefNecromancyCrafting() : base( 1, 1, 1.25 )// base( 1, 2, 1.7 )
		{
		}

		public override int CanCraft( Mobile from, BaseTool tool, Type itemType )
		{
			if( tool == null || tool.Deleted || tool.UsesRemaining < 0 )
				return 1044038; // You have worn out your tool!
			else if ( !(from is PlayerMobile && from.Skills[SkillName.Necromancy].Base >= 20.0) )
				return 1044153; // You don't have the required skill
			else if ( !BaseTool.CheckAccessible( tool, from ) )
				return 1044263; // The tool must be on your person to use.

			return 0;

		}

		public override void PlayCraftEffect( Mobile from )
		{
			from.PlaySound( 0x1F5 ); // magic

			//if ( from.Body.Type == BodyType.Human && !from.Mounted )
			//	from.Animate( 9, 5, 1, true, false, 0 );

			//new InternalTimer( from ).Start();
		}

		// Delay to synchronize the sound with the hit on the anvil
		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_From = from;
			}

			protected override void OnTick()
			{
				m_From.PlaySound( 0x2A );
			}
		}

		public override int PlayEndingEffect( Mobile from, bool failed, bool lostMaterial, bool toolBroken, int quality, bool makersMark, CraftItem item )
		{
			if ( toolBroken )
				from.SendLocalizedMessage( 1044038 ); // You have worn out your tool

			if ( failed )
			{
				from.PlaySound( 65 ); // rune breaking
				if ( lostMaterial )
					return 1044043; // You failed to create the item, and some of your materials are lost.
				else
					return 1044157; // You failed to create the item, but no materials were lost.
			}
			else
			{
				from.PlaySound( 65 ); // rune breaking
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
			int index = AddCraft( typeof( VileCrystal ), "Crystals", "Vile Crystal", 20.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( AmberPowder ), "Amber Powder", 1, "You don't have enough Amber Powder to make that."  );

			index = AddCraft( typeof( deceptiveCrystal ), "Crystals", "Deceptive Crystal", 30.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( WhitePowder ), "White Powder", 1, "You don't have enough White Powder to make that."  );

			index = AddCraft( typeof( treacherousCrystal ), "Crystals", "Treacherous Crystal", 40.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( RedPowder ), "Red Powder", 1, "You don't have enough Red Powder to make that."  );

			index = AddCraft( typeof( wickedCrystal ), "Crystals", "Wicked Crystal", 50.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( OrangePowder ), "Orange Powder", 1, "You don't have enough Orange Powder to make that."  );

			index = AddCraft( typeof( taintedCrystal ), "Crystals", "Tainted Crystal", 20.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( PurplePowder ), "Purple Powder", 1, "You don't have enough Purple Powder to make that."  );

			index = AddCraft( typeof( SorrowCrystal ), "Crystals", "Sorrow Crystal", 70.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( AzulPowder ), "Blue Powder", 1, "You don't have enough Blue Powder to make that."  );

			index = AddCraft( typeof( perilousCrystal ), "Crystals", "Perilous Crystal", 80.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( TourmalinePowder ), "Tourmaline Powder", 1, "You don't have enough Tourmaline Powder to make that."  );

			index = AddCraft( typeof( ominousCrystal ), "Crystals", "Ominous Crystal", 90.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( ClearPowder ), "Clear Powder", 1, "You don't have enough Clear Powder to make that."  );

			index = AddCraft( typeof( MaliceCrystal ), "Crystals", "Malice Crystal", 100.0, 120.0, 
				typeof( CorruptCrystal ), "Corrupt Crystal", 1,"You don't have enough Corrupt Crystals." );
				AddRes( index, typeof ( GreenPowder ), "Green Powder", 1, "You don't have enough Green Powder to make that."  );

			//Skeleton Crafts

			index = AddCraft( typeof( SkelLegs ), "Skeletal", "Skeleton legs", 20.0, 120.0,
				typeof( Bones ), "Bones", 20,"You don't have enough Bones for the structure." );
				AddRes( index, typeof ( AnimateDeadScroll ), "Animate Dead Scroll", 1, "You don't have enough Animate Dead scrolls."  );
				//AddRes( index, typeof ( Spine ), "Spine", 1, "You don't have enough spine for the structure."  );
				AddSkill( index, SkillName.Anatomy, 20.0, 50.0 );

			index = AddCraft( typeof( SkelBod ), "Skeletal", "Skeleton Torso", 20.0, 120.0,
				typeof( Bones ), "Bones", 30,"You don't have enough Bones for the structure." );
				AddRes( index, typeof ( Skull1 ), "Skull", 1, "You don't have a Skull for the structure."  );
				AddRes( index, typeof ( RibCage ), "Rib Cage", 1, "You don't have a Rib Cage for the structure."  );
				AddRes( index, typeof ( Spine ), "Spine", 1, "You don't have a Spine for the structure."  );
				AddSkill( index, SkillName.Anatomy, 20.0, 50.0 );

			index = AddCraft( typeof( SkelMageBod ), "Skeletal", "Complete Skeletal Torso", 30.0, 130.0,
				typeof( SkelBod ), "Skeleton Torso", 1,"You don't have the Skeleton Torso needed for the structure." );
				AddRes( index, typeof ( Jawbone ), "Jawbone", 1, "You don't have a Jawbone for the structure."  );
				//AddRes( index, typeof ( Spine ), "Spine", 1, "You don't have enough spine for the structure."  );
				AddSkill( index, SkillName.Anatomy, 30.0, 60.0 );
			
			// Rotting Crafts

			index = AddCraft( typeof( RottingBod ), "Rotting", "Rotting Torso", 20.0, 120.0,
				typeof( Head ), "Head", 1,"You don't have a Head for the structure." );
				AddRes( index, typeof ( Torso ), "Torso", 1, "You don't have a Torso for the structure."  );
				AddRes( index, typeof ( RightArm ), "Right Arm", 1, "You don't have a Right Arm for the structure."  );
				AddRes( index, typeof ( LeftArm ), "Left Arm", 1, "You don't have a Left Arm for the structure."  );
				AddSkill( index, SkillName.Anatomy, 25.0, 50.0 );

			index = AddCraft( typeof( RottingLegs ), "Rotting", "Rotting Legs", 20.0, 120.0,
				typeof( LeftLeg ), "Left Leg", 1,"You don't have a Left Leg for the structure." );
				AddRes( index, typeof ( RightLeg ), "Right Leg", 1, "You don't have a Right Leg for the structure."  );
				AddRes( index, typeof ( AnimateDeadScroll ), "Animate Dead Scroll", 1, "You don't have enough Animate Dead scrolls."  );
				//AddRes( index, typeof ( LeftArm ), "left arm", 1, "You do not have the left arm for the structure."  );
				AddSkill( index, SkillName.Anatomy, 25.0, 50.0 );
			
			index = AddCraft( typeof( ToxicBod ), "Rotting", "Poisioned Rotting Torso", 20.0, 120.0,
				typeof( RottingBod ), "Rotting Torso", 1,"You don't have a Rotting Torso to poision." );
				AddRes( index, typeof ( GreaterPoisonPotion ), "Greater Poison Potion", 10, "You don't have enough Greater Poison Potions."  );
				AddRes( index, typeof ( CorpseSkinScroll ), "Corpse Skin Scroll", 1, "You don't have enough Corpse Skin scrolls."  );
				//AddRes( index, typeof ( LeftArm ), "left arm", 1, "You do not have the left arm for the structure."  );
				AddSkill( index, SkillName.Anatomy, 25.0, 50.0 );

			//Mummy Crafts


			index = AddCraft( typeof( WrappedLegs ), "Wrapped", "Mummified Legs", 40.0, 130.0,
				typeof( SkelLegs ), "Skeleton Legs", 1,"You don't have the Skeleton Legs for the structure." );
				AddRes( index, typeof ( Bandage ), "Bandages", 100, "You don't have enough Bandages for the wrapping."  );
				AddRes( index, typeof ( EvilOmenScroll ), "Evil Omen Scroll", 1, "You don't have enough Evil Omen scrolls."  );
				AddSkill( index, SkillName.Anatomy, 40.0, 80.0 );

			index = AddCraft( typeof( WrappedBod ), "Wrapped", "Mummified Torso", 40.0, 130.0,
				typeof( SkelBod ), "Skeleton Torso", 1,"You don't have a Skeleton Torso needed for the structure." );
				AddRes( index, typeof ( Bandage ), "Bandages", 100, "You don't have enough Bandages for the wrapping."  );
				AddSkill( index, SkillName.Anatomy, 40.0, 80.0 );

			index = AddCraft( typeof( WrappedMageBod ), "Wrapped", "Inscribed Mummified Torso", 50.0, 140.0,
				typeof( SkelMageBod ), "Complete Skeletal Torso", 1,"You don't have a Complete Skeletal Torso needed for the structure." );
				AddRes( index, typeof ( Bandage ), "Bandages", 100, "You don't have enough Bandages for the wrapping."  );
				AddRes( index, typeof ( BlankScroll ), "Blank Scroll", 10, "You don't have enough Blank Scrolls for the scripture."  );
				AddSkill( index, SkillName.Anatomy, 50.0, 80.0 );

			// Phylacery

			index = AddCraft( typeof( Phylacery ), "Phylacery", "Phylacery", 100.0, 130.0,
				typeof( Soul ), "Soul", 1,"You don't have a Soul needed to bind in the Phylacery." );
				AddRes( index, typeof ( ArcaneGem ), "Arcane Gem", 6, "You don't have enough Arcane Gems to entrap the soul."  );
				AddRes( index, typeof ( ExorcismScroll ), "Exorcism Scroll", 1, "You don't have enough Exorcism scrolls."  );
				AddRes( index, typeof ( WoodenBox ), "Wooden Box", 1, "You don't have a Box needed to craft the Phylacery."  );
				AddSkill( index, SkillName.Inscribe, 100.0, 120.0 );
				AddSkill( index, SkillName.Alchemy, 100.0, 120.0 );
				AddSkill( index, SkillName.Anatomy, 100.0, 120.0 );
		}
	}
}