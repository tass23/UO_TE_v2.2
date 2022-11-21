using Server;
using System;
using System.Collections;
using Server.Multis;
using Server.Targeting;
using Server.Items;

namespace Server.Engines.Craft
{
	public class DefDeedCrafting : CraftSystem
	{
		public override SkillName MainSkill{get	{ return SkillName.Inscribe;	}}

		public override string GumpTitleString
		{
			get{ return "Deed Crafting by Old School"; }
		}

		private static CraftSystem m_CraftSystem;

		public static CraftSystem CraftSystem
		{
			get
			{
				if ( m_CraftSystem == null )
					m_CraftSystem = new DefDeedCrafting();

				return m_CraftSystem;
			}
		}

		public override double GetChanceAtMin( CraftItem item )
		{
			return 0.5; // 50%
		}

		private DefDeedCrafting() : base( 1, 1, 1.25 )// base( 1, 1, 3.0 )
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

			from.PlaySound( 0x24A );
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
              //Materials
            	AddCraft( typeof( CraftedGoldBricks ), "Materials", "Crafted Gold Bricks", -25.0, 0.0, typeof( Gold ), "Gold", 10000 );
            	              
              //Deeds
              //AddCraft( typeof( CraftedBeardRestylingDeed ), "Deeds", "Crafted Beard Restyling Deed", 0.0, 20.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 1 );
              //AddCraft( typeof( CraftedHairRestylingDeed ), "Deeds", "Crafted Hair Restyling Deed", 20.0, 35.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 1 );
              AddCraft( typeof( CraftedNameChangeDeed ), "Deeds", "Crafted Name Change Deed", 35.0, 55.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 1 );
              AddCraft( typeof( CraftedSexChangeDeed ), "Deeds", "Crafted Sex Change Deed", 55.0, 60.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 1 );
              AddCraft( typeof( CraftedSpellChannelingDeed ), "Deeds", "Crafted Spell Channeling Deed", 60.0, 65.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 2 );
              AddCraft( typeof( CraftedContractOfEmployment ), "Deeds", "Crafted Contract Of Employment", 65.0, 70.3, typeof( Gold ), "Gold", 10 );
              AddCraft( typeof( CraftedPetResurrectionDeed ), "Deeds", "Crafted Pet Resurrection Deed", 70.0, 75.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 2 );
              AddCraft( typeof( CraftedPetBondingDeed ), "Deeds", "Crafted Pet Bonding Deed", 75.0, 80.4, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 2 );
              AddCraft( typeof( CraftedGuildDeed ), "Deeds", "Crafted Guild Deed", 80.0, 90.3, typeof( Gold ), "Gold", 100 );
              AddCraft( typeof( CraftedClothingBlessDeed ), "Deeds", "Crafted Clothing Bless Deed", 90.0, 100.3, typeof( CraftedGoldBricks ), "Crafted Gold Bricks", 3 );
                            
            //Tool
              AddCraft(typeof(DeedCraftingTool), "Tool", "Deed Crafting Tool", 0.0, 20.4, typeof(Gold), "Gold", 30);

                        MarkOption = true;
			Repair = Core.AOS;
		}
	}
}