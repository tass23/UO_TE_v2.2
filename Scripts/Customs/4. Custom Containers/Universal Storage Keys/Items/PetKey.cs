using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							
using Server.Mobiles;

namespace Server.Items
{
	public class PetKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 1; } }		
		public override bool CanUseFromPack{ get{ return true; } }		
		public override bool CanUseFromHouse{ get{ return true; } }

		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				entry.Add
				(
					new StashEntry
					(
						typeof( ShrinkItemX ), "Pets", 500, new StashSortData
						(
							new StashSortEntry[]
							{
								new StashSortEntry( "Name", "PetName" ),
								new StashSortEntry( "Bonded", "PetBonded" ),
								new StashSortEntry( "Damage", "PetMaxDamage" ),
								new StashSortEntry( "Hit Points", "PetHitsNow" ),
								new StashSortEntry( "Magery", "SkillMagery" ),
								new StashSortEntry( "Strength", "StatStr" ),
								new StashSortEntry( "Dexterity", "StatDex" )
							}, 
							new StashSortEntry[]
							{
								new StashSortEntry( "Level", "Level" ),
								new StashSortEntry( "Max Level", "MaxLevel" ),
								new StashSortEntry( "Minimum Taming", "PetMinTame" ),								
								new StashSortEntry( "Resisting Spells", "SkillMagicResist" ), 
								new StashSortEntry( "Generation", "Generation" ),
								new StashSortEntry( "Able to Mate", "AllowMating" ),
								new StashSortEntry( "Poisoning", "SkillPoisoning" ),
								new StashSortEntry( "Mating Delay", "MatingDelay" ),
								new StashSortEntry( "Evaluating Intelligence", "SkillEvalInt" ),
								new StashSortEntry( "Tactics", "SkillTactics" ),
								new StashSortEntry( "Wrestling", "SkillWrestling" ),								
								new StashSortEntry( "Physical Resist", "ResistPhysical" ),
								new StashSortEntry( "Fire Resist", "ResistFire" ),
								new StashSortEntry( "Cold Resist", "ResistCold" ),
								new StashSortEntry( "Energy Resist", "ResistEnergy" ),
								new StashSortEntry( "Poison Resist", "ResistPoison" ),								
								new StashSortEntry( "Physical Damage", "DamageTypePhysical" ),
								new StashSortEntry( "Fire Damage", "DamageTypeFire" ),
								new StashSortEntry( "Cold Damage", "DamageTypeCold" ),
								new StashSortEntry( "Energy Damage", "DamageTypeEnergy" ),
								new StashSortEntry( "Poison Damage", "DamageTypePoison" ),								
								new StashSortEntry( "Fire Breath Attack", "FireBreathAttack" ),
								new StashSortEntry( "Poison Attack", "PetPoisonAttack" ),
								new StashSortEntry( "Roar Attack", "RoarAttack" ),
							} 
						)	 
					) 
				);				
				return entry;
			}
		}
		
		[Constructable]
		public PetKey() : base( 0 )
		{
			ItemID = 8928;
			Hue = 1365;
			Name = "Personal Bestiary Catalog";
			LootType = LootType.Blessed;
			Weight = 100;
		}
		
		protected override ItemStore GenerateItemStore()
		{
			ItemStore store = base.GenerateItemStore();
			store.Label = "Personal Bestiary Catalog";
			store.OfferDeeds = false;
			return store;
		}

		/*
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is ShrinkItemX ) 
			{
				new StashEntry( typeof( ShrinkItemX ), "Pets", 500 );	//Not quite right.
				return true;
			}
			else
			{
				from.SendMessage( "That is not a pet!" );
				return false;	
			}
		}
		*/
		
		public PetKey( Serial serial ) : base( serial )
		{
		}
		
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