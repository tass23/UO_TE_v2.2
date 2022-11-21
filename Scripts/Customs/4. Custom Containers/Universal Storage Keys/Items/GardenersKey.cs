using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.Plants;
using Server.Items.Crops;

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class GardenersKey : BaseStoreKey
	{
		//set the # of columns of entries to display on the gump.. default is 2
		public override int DisplayColumns{ get{ return 2; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new BeverageEntry( typeof( Pitcher ), BeverageType.Water, "Water", 0, 20, -3, 0 ) );
				entry.Add( new PotionEntry( typeof( GreaterCurePotion ), "Greater Cure", 0, 20, -5, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterHealPotion ), "Greater Heal", 0, 20, -7, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterPoisonPotion ), "Greater Poison", 0, 20, 0, 0  ) );
				entry.Add( new PotionEntry( typeof( GreaterStrengthPotion ), "Greater Strength", 0, 20, -5, 0  ) );
				//the filled plant item is a new class, called PlantItem, so no special handling required to block filled plant bowls
				//being added
				entry.Add( new ResourceEntry( typeof( PlantBowl ), "Plant Bowl", 0, 25, 0, 0  ) );
				entry.Add( new ResourceEntry( typeof( FertileDirt ), "Fertile Dirt", 0, 25, 0, 0  ) );
				entry.Add( new ListEntry( typeof( Seed ), typeof( SeedListEntry ), "Seeds" ) );
				
				//note: need harvest system for this
				
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( BlackberrySeed ), "Blackberry" ) );
				entry.Add( new ResourceEntry( typeof( BlackRaspberrySeed ), "Black Raspberry" ) );
				entry.Add( new ResourceEntry( typeof( BlueberrySeed ), "Blueberry" ) );
				entry.Add( new ResourceEntry( typeof( CranberrySeed ), "Cranberry" ) );
				entry.Add( new ResourceEntry( typeof( PineappleSeed ), "Pineapple" ) );
				entry.Add( new ResourceEntry( typeof( RedRaspberrySeed ), "Red Raspberry" ) );
				
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( BlackRoseSeed ), "Black Rose" ) );
				entry.Add( new ResourceEntry( typeof( DandelionSeed ), "Dandilion" ) );
				entry.Add( new ResourceEntry( typeof( IrishRoseSeed ), "Irish Rose" ) );
				entry.Add( new ResourceEntry( typeof( PansySeed ), "Pansy" ) );
				entry.Add( new ResourceEntry( typeof( PinkCarnationSeed ), "Pink Carnation" ) );
				entry.Add( new ResourceEntry( typeof( PoppySeed ), "Poppy" ) );
				entry.Add( new ResourceEntry( typeof( RedRoseSeed ), "Red Rose" ) );
				entry.Add( new ResourceEntry( typeof( SnapdragonSeed ), "Snapdragon" ) );
				entry.Add( new ResourceEntry( typeof( SpiritRoseSeed ), "Spirit Rose" ) );
				entry.Add( new ResourceEntry( typeof( WhiteRoseSeed ), "White Rose" ) );
				entry.Add( new ResourceEntry( typeof( YellowRoseSeed ), "Yellow Rose" ) );
				entry.Add( new ResourceEntry( typeof( IrishRoseSeed ), "Irish Rose" ) );

				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( CottonSeed ), "Cotton" ) );
				entry.Add( new ResourceEntry( typeof( FlaxSeed ), "Flax" ) );
				entry.Add( new ResourceEntry( typeof( HaySeed ), "Hay" ) );
				entry.Add( new ResourceEntry( typeof( OatsSeed ), "Oats" ) );
				entry.Add( new ResourceEntry( typeof( RiceSeed ), "Rice" ) );
				entry.Add( new ResourceEntry( typeof( SugarcaneSeed ), "Sugarcane" ) );
				entry.Add( new ResourceEntry( typeof( WheatSeed ), "Wheat" ) );
				
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( GarlicSeed ), "Garlic" ) );
				entry.Add( new ResourceEntry( typeof( TanGingerSeed ), "Ginger" ) );
				entry.Add( new ResourceEntry( typeof( GinsengSeed ), "Ginseng" ) );
				entry.Add( new ResourceEntry( typeof( MandrakeSeed ), "Mandrake" ) );
				entry.Add( new ResourceEntry( typeof( NightshadeSeed ), "Nightshade" ) );
				entry.Add( new ResourceEntry( typeof( RedMushroomSeed ), "Red Mushroom" ) );
				entry.Add( new ResourceEntry( typeof( TanMushroomSeed ), "Tan Mushroom" ) );

				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( BitterHopsSeed ), "Bitter Hops" ) );
				entry.Add( new ResourceEntry( typeof( ElvenHopsSeed ), "Elven Hops" ) );
				entry.Add( new ResourceEntry( typeof( SnowHopsSeed ), "Snow Hops" ) );
				entry.Add( new ResourceEntry( typeof( SweetHopsSeed ), "Sweet Hops" ) );

				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( CornSeed ), "Corn" ) );
				entry.Add( new ResourceEntry( typeof( FieldCornSeed ), "Field Corn" ) );
				entry.Add( new ResourceEntry( typeof( SunFlowerSeed ), "Sunflower" ) );
				entry.Add( new ResourceEntry( typeof( TeaSeed ), "Tea" ) );
				entry.Add( new ResourceEntry( typeof( CucumberSeed ), "Cucumber" ) );
				entry.Add( new ResourceEntry( typeof( DateSeed ), "Date Palm" ) );
				entry.Add( new ResourceEntry( typeof( GreenSquashSeed ), "Green Squash" ) );
				
				return entry;
			}
		}

		[Constructable]
		public GardenersKey() : base( 62 )		//hue 62
		{
			ItemID = 0xFB7;				//forged metal
			Name = "Gardener's Trowel";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Gardener's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			
			return store;
		}
		
		//serial constructor
		public GardenersKey( Serial serial ) : base( serial )
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