using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item inherited from BaseResourceKey
	public class MeatKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 3; } }

		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				entry.Add( new ResourceEntry( typeof( RawRibs ), "Raw Ribs" ) );
				entry.Add( new ResourceEntry( typeof( RawBird ), "Raw Bird" ) );
				entry.Add( new ResourceEntry( typeof( RawChickenLeg ), "Raw Chicken Leg" ) );
				entry.Add( new ResourceEntry( typeof( RawLambLeg ), "Raw Lamb Leg" ) );
				entry.Add( new ResourceEntry( typeof( RawFishSteak ), "Raw Fishsteak" ) );
				entry.Add( new ResourceEntry( typeof( Sausage ), "Sausage" ) );
				entry.Add( new ResourceEntry( typeof( Ribs ), "Ribs" ) );
				entry.Add( new ResourceEntry( typeof( CookedBird ), "Bird" ) );
				entry.Add( new ResourceEntry( typeof( ChickenLeg ), "Chicken Leg" ) );
				entry.Add( new ResourceEntry( typeof( LambLeg ), "Lamb Leg" ) );
				entry.Add( new ResourceEntry( typeof( FishSteak ), "Fishsteak" ) );				
				return entry;
			}
		}

		[Constructable]
		public MeatKey() : base( 2991 ) //hue 2991
		{
			ItemID = 9915; //bone harvester			
			Name = "Butcher's Hook";
			Weight = 100;
		}

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Butcher's Tools";
			
			store.OfferDeeds = false;
			return store;
		}

		//serial constructor
		public MeatKey( Serial serial ) : base( serial )
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