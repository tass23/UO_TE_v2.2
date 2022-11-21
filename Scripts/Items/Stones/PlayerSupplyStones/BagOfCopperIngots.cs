using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfCopperIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "a Copper Ingot Bag"; }
		}
		[Constructable] 
		public BagOfCopperIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfCopperIngots( int amount ) 
		{  
			DropItem( new CopperIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfCopperIngots( Serial serial ) : base( serial ) 
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
