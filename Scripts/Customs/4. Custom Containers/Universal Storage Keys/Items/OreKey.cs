using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class OreKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 2; } }

		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				entry.Add( new ResourceEntry( typeof( IronOre ), "Iron" ) );
				entry.Add( new ResourceEntry( typeof( DullCopperOre ), "Dull" ) );
				entry.Add( new ResourceEntry( typeof( ShadowIronOre ), "Shadow" ) );
				entry.Add( new ResourceEntry( typeof( CopperOre ), "Copper" ) );
				entry.Add( new ResourceEntry( typeof( BronzeOre ), "Bronze" ) );
				entry.Add( new ResourceEntry( typeof( GoldOre ), "Gold" ) );
				entry.Add( new ResourceEntry( typeof( AgapiteOre ), "Agapite" ) );
				entry.Add( new ResourceEntry( typeof( VeriteOre ), "Verite" ) );
				entry.Add( new ResourceEntry( typeof( ValoriteOre ), "Valorite" ) );				
				entry.Add( new ResourceEntry( typeof( SmallFireRock ), "Sm. Fire Rock", 0, 25, -20, 0  ) );
				entry.Add( new ResourceEntry( typeof( LargeFireRock ), "Lg. Fire Rock") );
				entry.Add( new ResourceEntry( typeof( CrystalineFire ), "Crystalline Fire") );
				return entry;
			}
		}
		
		[Constructable]
		public OreKey() : base( 2907 ) //hue 2907
		{
			ItemID = 0x19B9; //pile of ore
			Name = "Pocket Mine";
			Weight = 100;
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Ore Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}
		
		//serial constructor
		public OreKey( Serial serial ) : base( serial )
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