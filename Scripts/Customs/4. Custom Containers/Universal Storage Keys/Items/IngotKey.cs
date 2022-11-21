using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class IngotKey : BaseStoreKey
	{
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				entry.Add( new ResourceEntry( typeof( IronIngot ), "Iron" ) );
				entry.Add( new ResourceEntry( typeof( DullCopperIngot ), "Dull" ) );
				entry.Add( new ResourceEntry( typeof( ShadowIronIngot ), "Shadow" ) );
				entry.Add( new ResourceEntry( typeof( CopperIngot ), "Copper" ) );
				entry.Add( new ResourceEntry( typeof( BronzeIngot ), "Bronze" ) );
				entry.Add( new ResourceEntry( typeof( GoldIngot ), "Gold" ) );
				entry.Add( new ResourceEntry( typeof( AgapiteIngot ), "Agapite" ) );
				entry.Add( new ResourceEntry( typeof( VeriteIngot ), "Verite" ) );
				entry.Add( new ResourceEntry( typeof( ValoriteIngot ), "Valorite" ) );
				entry.Add( new ResourceEntry( typeof( SmallFireRock ), "Sm. Fire Rock", 0, 25, -20, 0  ) );
				entry.Add( new ResourceEntry( typeof( LargeFireRock ), "Lg. Fire Rock") );
				entry.Add( new ResourceEntry( typeof( CrystalineFire ), "Crystalline Fire") );
				return entry;
			}
		}

		[Constructable]
		public IngotKey() : base( 2936 ) //hue 2936
		{
			ItemID = 0x1BE8; //pile of ingots
			Name = "Ingot Warehouse";
			Weight = 100;
		}

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Ingot Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}

		//serial constructor
		public IngotKey( Serial serial ) : base( serial )
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