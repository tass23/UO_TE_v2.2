using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfIronIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "an Iron Ingot Bag"; }
		}
		[Constructable] 
		public BagOfIronIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfIronIngots( int amount ) 
		{  
			DropItem( new IronIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfIronIngots( Serial serial ) : base( serial ) 
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
