using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class BardsKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 2; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new InstrumentEntry( typeof( Lute ), InstrumentQuality.Exceptional, "Ex. Lute" ) );
				entry.Add( new InstrumentEntry( typeof( Tambourine ), InstrumentQuality.Exceptional, "Ex. Tambourine" ) );
				entry.Add( new InstrumentEntry( typeof( TambourineTassel ), InstrumentQuality.Exceptional, "Ex. T. Tambourine" ) );
				entry.Add( new InstrumentEntry( typeof( Drums ), InstrumentQuality.Exceptional, "Ex. Drums" ) );
				entry.Add( new InstrumentEntry( typeof( LapHarp ), InstrumentQuality.Exceptional, "Ex. Lap Harp", 0, 30, -10, 0 ) );
				entry.Add( new InstrumentEntry( typeof( Harp ), InstrumentQuality.Exceptional, "Ex. Harp", 0, 60, -40, 0 ) );
				entry.Add( new InstrumentEntry( typeof( BambooFlute ), InstrumentQuality.Exceptional, "Ex. Bamboo Flute" ) );
				entry.Add( new ListEntry( typeof( BaseInstrument ), typeof( InstrumentListEntry ), "Instruments" ) );
				
				return entry;
			}
		}
		
		
		
		[Constructable]
		public BardsKey() : base( 1152 )		//hue 1152
		{
			ItemID = 0xEB6;			//music stand
			Name = "Bard's Stand";
		}
		
		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Bard's Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public BardsKey( Serial serial ) : base( serial )
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