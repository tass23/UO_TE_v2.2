using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.BulkOrders;

namespace Server.Items
{
	//item inherited from BaseResourceKey
	public class FishKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 2; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = new List<StoreEntry>();				
				entry.Add( new GenericDistinguishableEntry( typeof( Fish ), "ItemID", "0x9CC", "Amount" ) );
				entry.Add( new GenericDistinguishableEntry( typeof( Fish ), "ItemID", "0x9CD", "Amount" ) );
				entry.Add( new GenericDistinguishableEntry( typeof( Fish ), "ItemID", "0x9CE", "Amount" ) );
				entry.Add( new GenericDistinguishableEntry( typeof( Fish ), "ItemID", "0x9CF", "Amount" ) );				
				return entry;
			}
		}

		[Constructable]
		public FishKey() : base( 1929 )
		{
			ItemID = 0x1EB7;
			Hue = 2953;
			Name = "Fish Bucket";
		}	

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Fish Storage";

			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}

		//serial constructor
		public FishKey( Serial serial ) : base( serial )
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