using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfGoldIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "a Gold Ingot Bag"; }
		}
		[Constructable] 
		public BagOfGoldIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfGoldIngots( int amount ) 
		{  
			DropItem( new GoldIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfGoldIngots( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); // version 
		} 

		public override void Deserialize( GenericReader reader ) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
} 
