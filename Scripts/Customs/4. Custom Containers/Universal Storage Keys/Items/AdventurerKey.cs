using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class AdventurerKey : BaseStoreKey
	{
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				entry.Add( new ResourceEntry( typeof( Bandage ), "Bandages" ) );
				entry.Add( new ResourceEntry( typeof( EnhancedBandage ), "Enhanced Bandages" ) );
				entry.Add( new ResourceEntry( typeof( Lockpick ), "Lockpicks" ) );
				entry.Add( new ResourceEntry( typeof( ZoogiFungus ), "Zoogi Fungus" ) );
				entry.Add( new ResourceEntry( typeof( PowderOfTranslocation ), "Powder of Trans." ) );
				entry.Add( new ResourceEntry( typeof( GreenThorns ), "Green Thorns" ) );
				entry.Add( new ResourceEntry( typeof( FertileDirt ), "Fertile Dirt" ) );
				entry.Add( new ResourceEntry( typeof( ArcaneGem ), "Arcane Gems" ) );
				entry.Add( new ResourceEntry( typeof( BolaBall ), "Bola Balls" ) );
				entry.Add( new ResourceEntry( typeof( Bola ), "Bola" ) );
				
				return entry;
			}
		}
		
		[Constructable]
		public AdventurerKey() : base( 1151 )		//hue 1151
		{
			ItemID = 0x170B;				//crate
			Name = "Adventurer's Boots";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();
			
			//properties of this storage device
			store.Label = "Adventurer's Boots";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			
			return store;
		}
		
		//serial constructor
		public AdventurerKey( Serial serial ) : base( serial )
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