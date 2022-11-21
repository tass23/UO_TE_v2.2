using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class SmithyKey : BaseStoreKey
	{
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				//this item really shows off the flexibility you have in creating cool customized combo items
				entry.Add( new ToolEntry( typeof( SmithHammer ), "Smith Hammer", 0, 30, -5, 0 ) );
				entry.Add( new ToolEntry( typeof( Tongs ), "Tongs", 0, 30, -5, 0 ) );
				entry.Add( new ToolEntry( typeof( SledgeHammer ), "Sledge Hammer", 0, 30, -5, 0 ) );
				//entry.Add( new ToolEntry( typeof( PowderOfTemperament ), "Powder of Fort." ) );
				entry.Add( new GenericEntry( typeof( PowderOfTemperament ), "UsesRemaining", "Powder of Fort." ) );
				
				entry.Add( new AncientSmithyHammerToolEntry( 10, "+10 ASH", 0, 30, 0, 0 ) );
				entry.Add( new AncientSmithyHammerToolEntry( 15, "+15 ASH", 0, 30, 0, 0 ) );
				entry.Add( new AncientSmithyHammerToolEntry( 30, "+30 ASH", 0, 30, 0, 0 ) );
				entry.Add( new AncientSmithyHammerToolEntry( 60, "+60 ASH", 0, 30, 0, 0 ) );
				entry.Add( new ListEntry( typeof( RepairDeed ), typeof( RepairDeedListEntry ), "Repair Deeds" ) );
				return entry;
			}
		}

		[Constructable]
		public SmithyKey() : base( 1049 )		//hue 1049
		{
			ItemID = 0xFB8;				//forged metal
			Name = "Blacksmith Shoppe";
			Weight = 100;
		}

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Blacksmith Shoppe";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}
		
		//serial constructor
		public SmithyKey( Serial serial ) : base( serial )
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