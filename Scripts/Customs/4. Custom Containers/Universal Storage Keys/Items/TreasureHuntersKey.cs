using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class TreasureHuntersKey : BaseStoreKey
	{
		//public override int DisplayColumns{ get{ return 1; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				entry.Add( new ResourceEntry( typeof( Lockpick ), "Lockpicks" ) );
				entry.Add( new ToolEntry( typeof( Shovel ), new Type[]{ typeof( SturdyShovel ) }, "Shovel", 0, 35, -10, -10 ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ListEntry( typeof( TreasureMap ), typeof( TreasureMapListEntry ), "Treasure Maps" ) );
				entry.Add( new ListEntry( typeof( ArtifactMap ), typeof( ArtifactMapListEntry ), "Artifact Maps" ) );
				entry.Add( new ListEntry( typeof( SOS ), typeof( SOSListEntry ), "SOS's" ) );
				entry.Add( new ListEntry( typeof( SpecialFishingNet ), typeof( SpecialFishingNetListEntry ), "Fishing Nets" ) );
				return entry;
			}
		}

		[Constructable]
		public TreasureHuntersKey() : base( 1154 ) //hue 1154
		{
			ItemID = 0x1E74; //table leg
			Name = "Treasure Hunter's Cache";
		}

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Treasure Hunter's Cache";			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}

		//serial constructor
		public TreasureHuntersKey( Serial serial ) : base( serial )
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