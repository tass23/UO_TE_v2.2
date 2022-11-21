using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Solaris.ItemStore;							//for connection to resource store data objects
using Server.Engines.CannedEvil;

namespace Server.Items
{
	//item inherited from BaseResourceKey
	public class ChampSkullKey : BaseStoreKey
	{
		public override int DisplayColumns{ get{ return 2; } }
		
		public override List<StoreEntry> EntryStructure
		{
			get
			{
				List<StoreEntry> entry = base.EntryStructure;
				
				string[] skullnames = Enum.GetNames( typeof( ChampionSkullType ) );
			
				foreach( int value in Enum.GetValues( typeof( ChampionSkullType ) ) )
				{
					entry.Add( new ChampionSkullEntry( (ChampionSkullType)value ) );
				}
				
				return entry;
			}
		}
		
		
		
		[Constructable]
		public ChampSkullKey() : base( 1547 )
		{
			ItemID = 0x2203;			//big skull
			Name = "Champion Skull Holder";
		}
		
		
		
		//this loads properties specific to the store, like the gump label, and whether it's a dynamic storage device
		protected override ItemStore GenerateItemStore()
		{
			//load the basic store info
			ItemStore store = base.GenerateItemStore();

			//properties of this storage device
			store.Label = "Champion Skull Storage";
			
			store.Dynamic = false;
			store.OfferDeeds = false;
			return store;
		}
		
		//serial constructor
		public ChampSkullKey( Serial serial ) : base( serial )
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