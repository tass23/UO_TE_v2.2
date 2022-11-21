using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.BulkOrders;

namespace Server.Items
{	
	//item inherited from BaseResourceKey
	public class BODKey : BaseStoreKey
	{
		//public override int DisplayColumns{ get{ return 2; } }
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				entry.Add( new ListEntry( typeof( SmallSmithBOD ), typeof( SmallBODListEntry ), "Sm. Blacksmith", 0x2258, 0x44E  ) );
				entry.Add( new ListEntry( typeof( SmallTailorBOD ), typeof( SmallBODListEntry ), "Sm. Tailor", 0x2258, 0x483 ) );
				entry.Add(new ListEntry(typeof(SmallTamingBOD), typeof(SmallBODMobileListEntry), "Sm. Taming", 0x14EF, 0x1CA));
				
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ListEntry( typeof( LargeSmithBOD ), typeof( LargeBODListEntry ), "Lg. Blacksmith", 0x2258, 0x44E ) );
				entry.Add( new ListEntry( typeof( LargeTailorBOD ), typeof( LargeBODListEntry ), "Lg. Tailor", 0x2258, 0x483 ) );
				entry.Add(new ListEntry(typeof(LargeTamingBOD), typeof(LargeBODMobileListEntry), "Lg. Taming", 0x2258, 0x1CA));
		
				return entry;
			}
		}
		
		[Constructable]
		public BODKey() : base( 1161 )		//hue 1161 - blaze
		{
			ItemID = 8793;
			Name = "Bulk Order Deed Filing System";
			Weight = 100;
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Bulk Order Deed Filing System";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public BODKey( Serial serial ) : base( serial )
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