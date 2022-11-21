using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfDullCopperIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "a Dull Copper Ingot Bag"; }
		}
		[Constructable] 
		public BagOfDullCopperIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfDullCopperIngots( int amount ) 
		{  
			DropItem( new DullCopperIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfDullCopperIngots( Serial serial ) : base( serial ) 
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
