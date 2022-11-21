using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfBronzeIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bronze Ingot Bag"; }
		}
		[Constructable] 
		public BagOfBronzeIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfBronzeIngots( int amount ) 
		{  
			DropItem( new BronzeIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfBronzeIngots( Serial serial ) : base( serial ) 
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
