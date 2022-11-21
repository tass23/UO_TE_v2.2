using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class JewelersKey : BaseStoreKey
	{
		//set the # of columns of entries to display on the gump.. default is 2
		public override int DisplayColumns{ get{ return 3; } }
		
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				entry.Add( new ResourceEntry( typeof( Amber ), "Amber", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( Amethyst ), "Amethyst", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( Citrine ), "Citrine", 0, 25, -13, 9 ) );
				entry.Add( new ResourceEntry( typeof( Diamond ), "Diamond", 0, 25, -5, 9 ) );
				entry.Add( new ResourceEntry( typeof( Emerald ), "Emerald", 0, 25, 7, 9 ) );
				entry.Add( new ResourceEntry( typeof( Ruby ), "Ruby", 0, 25, 7, 9 ) );
				entry.Add( new ResourceEntry( typeof( Sapphire ), "Sapphire", 0, 25, 6, 9 ) );
				entry.Add( new ResourceEntry( typeof( StarSapphire ), "Star Sapphire", 0, 25, -7, 9 ) );
				entry.Add( new ResourceEntry( typeof( Tourmaline ), "Tourmaline", 0, 25, 11, 8 ) );
				entry.Add( new ResourceEntry( typeof( PerfectEmerald ), "Perfect Emerald", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( DarkSapphire ), "Dark Sapphire", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( Turquoise ), "Turquoise", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( EcruCitrine ), "Ecru Citrine", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( WhitePearl ), "White Pearl", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( FireRuby ), "Fire Ruby", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( BlueDiamond ), "Blue Diamond", 0, 25, -11, 9 ) );
				entry.Add( new ResourceEntry( typeof( BrilliantAmber ), "Brilliant Amber", 0, 25, -11, 9 ) );
				entry.Add( new ColumnSeparationEntry() );
				entry.Add( new ResourceEntry( typeof( IronIngot ), "Iron" ) );
				entry.Add( new ResourceEntry( typeof( DullCopperIngot ), "Dull" ) );
				entry.Add( new ResourceEntry( typeof( ShadowIronIngot ), "Shadow" ) );
				entry.Add( new ResourceEntry( typeof( CopperIngot ), "Copper" ) );
				entry.Add( new ResourceEntry( typeof( BronzeIngot ), "Bronze" ) );
				entry.Add( new ResourceEntry( typeof( GoldIngot ), "Gold" ) );
				entry.Add( new ResourceEntry( typeof( AgapiteIngot ), "Agapite" ) );
				entry.Add( new ResourceEntry( typeof( VeriteIngot ), "Verite" ) );
				entry.Add( new ResourceEntry( typeof( ValoriteIngot ), "Valorite" ) );
				return entry;
			}
		}
		
		[Constructable]
		public JewelersKey() : base( 1154 )		//hue 1154
		{
			Name = "Lapidary Vault";
			Weight = 100;
		}
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Lapidary Vault";			
			store.Dynamic = false;
			store.OfferDeeds = false;			
			return store;
		}

		//serial constructor
		public JewelersKey( Serial serial ) : base( serial )
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