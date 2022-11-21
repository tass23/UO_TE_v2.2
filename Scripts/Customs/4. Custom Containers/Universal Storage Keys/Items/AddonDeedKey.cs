using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class AddonDeedKey : BaseStoreKey
	{
		//public override int DisplayColumns{ get{ return 1; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new ListEntry( typeof( BaseAddonDeed ), typeof( BaseAddonDeedListEntry ), "Addon Deeds" ) );

				return entry;
			}
		}
		
		
		
		[Constructable]
		public AddonDeedKey() : base( 1861 )		//hue 1861
		{
			ItemID = 0x1E81;				//wooden box
			Name = "Addon Deed Storage";
		}
		
		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Addon Deed Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public AddonDeedKey( Serial serial ) : base( serial )
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

