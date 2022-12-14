using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects

namespace Server.Items
{
	//item derived from BaseResourceKey
	public class ReagentKey : BaseStoreKey
	{
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;				
				entry.Add( new ResourceEntry( typeof( BlackPearl ), "Black Pearl" ) );
				entry.Add( new ResourceEntry( typeof( Bloodmoss ), "Bloodmoss" ) );
				entry.Add( new ResourceEntry( typeof( MandrakeRoot ), "Mandrake Root" ) );
				entry.Add( new ResourceEntry( typeof( SpidersSilk ), "Spider's Silk" ) );
				entry.Add( new ResourceEntry( typeof( Nightshade ), "Nightshade" ) );
				entry.Add( new ResourceEntry( typeof( SulfurousAsh ), "Sulfurous Ash" ) );
				entry.Add( new ResourceEntry( typeof( Garlic ), "Garlic" ) );
				entry.Add( new ResourceEntry( typeof( Ginseng ), "Ginseng" ) );
				entry.Add( new ResourceEntry( typeof( NoxCrystal ), "Nox Crystal" ) );
				entry.Add( new ResourceEntry( typeof( PigIron ), "Pig Iron" ) );
				entry.Add( new ResourceEntry( typeof( GraveDust ), "Grave Dust" ) );
				entry.Add( new ResourceEntry( typeof( BatWing ), "Bat Wing" ) );
				entry.Add( new ResourceEntry( typeof( DaemonBlood ), "Daemon Blood" ) );
				entry.Add( new ResourceEntry( typeof( Bottle ), "Empty Bottle" ) );
				entry.Add( new ResourceEntry( typeof( PotionKeg ), "Potion Keg" ) );
				entry.Add( new ResourceEntry( typeof( FertileDirt ), "Fertile Dirt" ) );
				entry.Add( new ResourceEntry( typeof( KeyRing ), "Key Ring" ) );
				entry.Add( new ResourceEntry( typeof( Beeswax ), "Beeswax" ) );
				entry.Add( new ResourceEntry( typeof( DaemonBone ), "Daemon Bone" ) );
				entry.Add( new ResourceEntry( typeof( SpringWater ), "Spring Water" ) );
				entry.Add( new ResourceEntry( typeof( PetrifiedWood ), "Petrified Wood" ) );
				entry.Add( new ResourceEntry( typeof( DestroyingAngel ), "Destroying Angel" ) );
				entry.Add( new ResourceEntry( typeof( Bone ), "Bone" ) );
				entry.Add( new ResourceEntry( typeof( BlankScroll ), "Blank Scroll" ) );
				entry.Add( new ResourceEntry( typeof( Sand ), "Sand" ) );				
				return entry;
			}
		}

		[Constructable]
		public ReagentKey() : base( 33 )
		{
			ItemID = 0x18DE;			//fancy mandrake root
			Name = "Apothecary";
			Weight = 25;
		}

		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Reagent Storage";			
			store.Dynamic = false;
			store.OfferDeeds = true;
			return store;
		}

		//serial constructor
		public ReagentKey( Serial serial ) : base( serial )
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