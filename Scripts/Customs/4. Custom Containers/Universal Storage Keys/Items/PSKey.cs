using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.BulkOrders;

namespace Server.Items
{
	//item inherited from BaseResourceKey
	public class PSKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 1; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				string[] skillnames = Enum.GetNames( typeof( SkillName ) );
			
				entry.Add( new ListEntry( typeof( PowerScroll ), typeof( PowerScrollListEntry ), "Power Scrolls" ) );
				//entry.Add( new ListEntry( typeof( StatCapScroll ), typeof( StatCapScrollListEntry ), "Stat Scrolls" ) );
				//entry.Add( new ListEntry( typeof( ScrollofTranscendence ), typeof( ScrollofTranscendenceListEntry ), "Transcendence" ) );
				
				return entry;
			}
		}

		[Constructable]
		public PSKey() : base( 1153 )
		{
			ItemID = 8793;
			Name = "Powerscroll Filing System";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Powerscroll Filing System";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public PSKey( Serial serial ) : base( serial )
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