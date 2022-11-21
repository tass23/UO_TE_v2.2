using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfAgapiteIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "an Agapite Ingot Bag"; }
		}
		[Constructable] 
		public BagOfAgapiteIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfAgapiteIngots( int amount ) 
		{  
			DropItem( new AgapiteIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfAgapiteIngots( Serial serial ) : base( serial ) 
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
