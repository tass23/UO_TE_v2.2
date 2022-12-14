using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item inherited from BaseResourceKey
	public class JewelryKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 1; } }		
		public override bool CanUseFromPack{ get{ return false; } }		
		public override bool CanUseFromHouse{ get{ return true; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				//NOTE: if you're looking to add new stash entry list types, make sure to include the appropriate "using" directives at the top of this file, or use the full class path				
				//entry.Add( new ResourceEntry( typeof( Amber ), "Amber", 0, 25, -11, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Amethyst ), "Amethyst", 0, 25, -11, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Citrine ), "Citrine", 0, 25, -13, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Diamond ), "Diamond", 0, 25, -5, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Emerald ), "Emerald", 0, 25, 7, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Ruby ), "Ruby", 0, 25, 7, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Sapphire ), "Sapphire", 0, 25, 6, 9 ) );
				//entry.Add( new ResourceEntry( typeof( StarSapphire ), "Star Sapphire", 0, 25, -7, 9 ) );
				//entry.Add( new ResourceEntry( typeof( Tourmaline ), "Tourmaline", 0, 25, 11, 8 ) );
				entry.Add
				(
				
					new StashEntry
					(
						typeof( BaseJewel ), "Jewelry", 100, new StashSortData
						(											//add the column information
							new StashSortEntry[]		//the first entry is what is displayed by default
							{
							},
							new StashSortEntry[]	//the second entry is what is also available to the user when they customize the gump
							{
								new StashSortEntry( "Physical Resist", new string[]{ "Resistances", "Physical" } ),
								new StashSortEntry( "Fire Resist", new string[]{ "Resistances", "Fire" } ),
								new StashSortEntry( "Cold Resist", new string[]{ "Resistances", "Cold" } ),
								new StashSortEntry( "Energy Resist", new string[]{ "Resistances", "Energy" } ),
								new StashSortEntry( "Poison Resist", new string[]{ "Resistances", "Poison" } ),
								new StashSortEntry( "Body Position", "Layer" ),
								new StashSortEntry( "Str Bonus", new string[]{ "Attributes", "BonusStr" } ),
								new StashSortEntry( "Dex Bonus", new string[]{ "Attributes", "BonusDex" } ),
								new StashSortEntry( "Int Bonus", new string[]{ "Attributes", "BonusInt" } ),
								new StashSortEntry( "HP Bonus", new string[]{ "Attributes", "BonusHits" } ),
								new StashSortEntry( "Stamina Bonus", new string[]{ "Attributes", "BonusStam" } ),
								new StashSortEntry( "Mana Bonus", new string[]{ "Attributes", "BonusMana" } ),
								new StashSortEntry( "FCR", new string[]{ "Attributes", "CastRecovery" } ),
								new StashSortEntry( "FC", new string[]{ "Attributes", "CastSpeed" } ),
								new StashSortEntry( "HCI", new string[]{ "Attributes", "AttackChance" } ),
								new StashSortEntry( "DCI", new string[]{ "Attributes", "DefendChance" } ),
								new StashSortEntry( "LMC", new string[]{ "Attributes", "LowerManaCost" } ),
								new StashSortEntry( "LRC", new string[]{ "Attributes", "LowerRegCost" } ),
								new StashSortEntry( "Luck", new string[]{ "Attributes", "Luck" } ),
								new StashSortEntry( "Enhance Potions", new string[]{ "Attributes", "EnhancePotions" } ),
								new StashSortEntry( "Night Sight", new string[]{ "Attributes", "NightSight" } ),
								new StashSortEntry( "Reflect Phys Damage", new string[]{ "Attributes", "Reflect Physical" } ),
								new StashSortEntry( "Hits Regen", new string[]{ "Attributes", "RegenHits" } ),
								new StashSortEntry( "Stam Regen", new string[]{ "Attributes", "RegenStam" } ),
								new StashSortEntry( "Mana Regen", new string[]{ "Attributes", "RegenMana" } ),
								new StashSortEntry( "Spell Channeling", new string[]{ "Attributes", "SpellChanneling" } ),
								new StashSortEntry( "Spell Damage", new string[]{ "Attributes", "SpellDamage" } ),
								new StashSortEntry( "Damage Increase", new string[]{ "Attributes", "WeaponDamage" } ),
								new StashSortEntry( "Swing Speed Increase", new string[]{ "Attributes", "WeaponSpeed" } ),
								new StashSortEntry( "Skill 1", new string[]{ "SkillBonuses", "Skill_1_Name" }, new string[]{ "SkillBonuses", "Skill_1_Value" } ),
								new StashSortEntry( "Skill 1 Name", new string[]{ "SkillBonuses", "Skill_1_Name" } ),
								new StashSortEntry( "Skill 1 Value", new string[]{ "SkillBonuses", "Skill_1_Value" } ),
								new StashSortEntry( "Skill 2", new string[]{ "SkillBonuses", "Skill_2_Name" }, new string[]{ "SkillBonuses", "Skill_2_Value" } ),
								new StashSortEntry( "Skill 2 Name", new string[]{ "SkillBonuses", "Skill_2_Name" } ),
								new StashSortEntry( "Skill 2 Value", new string[]{ "SkillBonuses", "Skill_2_Value" } ),
								new StashSortEntry( "Skill 3", new string[]{ "SkillBonuses", "Skill_3_Name" }, new string[]{ "SkillBonuses", "Skill_3_Value" } ),
								new StashSortEntry( "Skill 3 Name", new string[]{ "SkillBonuses", "Skill_3_Name" } ),
								new StashSortEntry( "Skill 3 Value", new string[]{ "SkillBonuses", "Skill_3_Value" } ),
								new StashSortEntry( "Skill 4", new string[]{ "SkillBonuses", "Skill_4_Name" }, new string[]{ "SkillBonuses", "Skill_4_Value" } ),
								new StashSortEntry( "Skill 4 Name", new string[]{ "SkillBonuses", "Skill_4_Name" } ),
								new StashSortEntry( "Skill 4 Value", new string[]{ "SkillBonuses", "Skill_4_Value" } ),
								new StashSortEntry( "Skill 5", new string[]{ "SkillBonuses", "Skill_5_Name" }, new string[]{ "SkillBonuses", "Skill_5_Value" } ),
								new StashSortEntry( "Skill 5 Name", new string[]{ "SkillBonuses", "Skill_5_Name" } ),
								new StashSortEntry( "Skill 5 Value", new string[]{ "SkillBonuses", "Skill_5_Value" } )
							} 
						)	 
					) 
				);				
				return entry;
			}
		}	
		
		[Constructable]
		public JewelryKey() : base( 2767 ) //hue 2767
		{
			ItemID = 2472;
			Name = "Jewelry Box";
			Weight = 100;
		}		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Jewelry Box";
			
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public JewelryKey( Serial serial ) : base( serial )
		{
		}
		
		//events		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );			
			int version = reader.ReadInt();
		}
	}
}