using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfShadowIronIngots : Bag 
	{
		public override string DefaultName
		{
			get { return "a Shadow Iron Ingot Bag"; }
		}
		[Constructable] 
		public BagOfShadowIronIngots() : this( 1000 ) 
		{ 
		} 

		[Constructable] 
		public BagOfShadowIronIngots( int amount ) 
		{  
			DropItem( new ShadowIronIngot   ( amount ) );
			DropItem( new Tongs() );
			DropItem( new TinkerTools() );

		} 

		public BagOfShadowIronIngots( Serial serial ) : base( serial ) 
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
