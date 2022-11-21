using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.Addons;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class TailorKey : BaseStoreKey
	{
		//public override int DisplayColumns{ get{ return 4; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				entry.Add( new ResourceEntry( typeof( Leather ), new Type[]{ typeof( Hides ) }, "Leather", 0, 30, 0, 0 ) );
				entry.Add( new ResourceEntry( typeof( SpinedLeather ), new Type[]{ typeof( SpinedHides ) }, "Spined Leather", 0, 30, 0, 0 ) );
				entry.Add( new ResourceEntry( typeof( HornedLeather ), new Type[]{ typeof( HornedHides ) }, "Horned Leather", 0, 30, 0, 0 ) );
				entry.Add( new ResourceEntry( typeof( BarbedLeather ), new Type[]{ typeof( BarbedHides ) }, "Barbed Leather", 0, 30, 0, 0 ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( RedScales ), "Red Scales" ) );
				entry.Add( new ResourceEntry( typeof( YellowScales ), "Yellow Scales" ) );
				entry.Add( new ResourceEntry( typeof( BlackScales ), "Black Scales" ) );
				entry.Add( new ResourceEntry( typeof( GreenScales ), "Green Scales" ) );
				entry.Add( new ResourceEntry( typeof( WhiteScales ), "White Scales" ) );
				entry.Add( new ResourceEntry( typeof( BlueScales ), "Blue Scales" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( LightYarn ), new Type[]{ typeof( DarkYarn ), typeof( LightYarnUnraveled ) }, "Yarn" ) );
				entry.Add( new ResourceEntry( typeof( SpoolOfThread ), "Spool of Thread" ) );
				entry.Add( new ResourceEntry( typeof( Wool ), "Wool" ) );
				entry.Add( new ResourceEntry( typeof( Cotton ), "Cotton" ) );
				entry.Add( new ResourceEntry( typeof( Flax ), "Flax" ) );
				entry.Add( new ResourceEntry( typeof( MagicThread ), "Magic Thread" ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( Dyes ), "Dyes" , 0, 30, 0, 0 ) );
				entry.Add( new ResourceEntry( typeof( UncutCloth ), new Type[]{ typeof( Cloth ) }, "Cloth", 0, 30, 0, 0 ) );
				entry.Add( new ResourceEntry( typeof( BoltOfCloth ), "Bolt of Cloth" , 0, 50, 0, 0 ) );
				entry.Add( new ColumnSeparationEntry() );
				return entry;
			}
		}

		[Constructable]
		public TailorKey() : base( 68 )
		{
			ItemID = 3997;			//sewingkit
			Name = "Tailor Supplies";
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Tailor Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}
		
		//serial constructor
		public TailorKey( Serial serial ) : base( serial )
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