using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class PotionKey : BaseStoreKey
	{
		//set the # of columns of entries to display on the gump.. default is 2
		public override int DisplayColumns{ get{ return 3; } }
		
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new ResourceEntry( typeof( Bottle ), "Empty Bottle" ) );
				entry.Add( new ResourceEntry( typeof( PotionKeg ), "Empty Keg" ) );
				entry.Add( new PotionEntry( typeof( AgilityPotion ), "Agility", 0, 20, 3, 0 ) );
				entry.Add( new PotionEntry( typeof( GreaterAgilityPotion ), "Greater Agility", 0, 20, 3, 0  ) );
				entry.Add( new PotionEntry( typeof( LesserCurePotion ), "Lesser Cure", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( CurePotion ), "Cure Poison", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterCurePotion ), "Greater Cure", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( LesserExplosionPotion ), "Lesser Explosion", 0, 20, 7, 0  ) );
				entry.Add( new PotionEntry( typeof( ExplosionPotion ), "Explosion", 0, 20, 7, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterExplosionPotion ), "Greater Explosion", 0, 20, 7, 0  ) );
				entry.Add( new PotionEntry( typeof( LesserHealPotion ), "Lesser Heal", 0, 20, -7, 0  ) );
				entry.Add( new PotionEntry( typeof( HealPotion ), "Heal", 0, 20, -7, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterHealPotion ), "Greater Heal", 0, 20, -7, 0  ) );
				entry.Add( new PotionEntry( typeof( LesserPoisonPotion ), "Lesser Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( PoisonPotion ), "Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterPoisonPotion ), "Greater Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( DeadlyPoisonPotion ), "Deadly Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( RefreshPotion ), "Refresh", 0, 20, -10, 0  ) );
				entry.Add( new PotionEntry( typeof( TotalRefreshPotion ), "Total Refresh", 0, 20, -10, 0  ) );
				entry.Add( new PotionEntry( typeof( StrengthPotion ), "Strength", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterStrengthPotion ), "Greater Strength", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( NightSightPotion ), "Night Sight", 0, 20, 2, 0  ) );
				
				return entry;
			}
		}

		
		[Constructable]
		public PotionKey() : base( 48 )		//hue 48
		{
			ItemID = 0x185E;			//full vials
			
			Name = "Potion Storage";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Potion Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			
			return store;
		}
		
		//serial constructor
		public PotionKey( Serial serial ) : base( serial )
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