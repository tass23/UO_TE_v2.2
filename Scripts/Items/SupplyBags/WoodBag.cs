using System; 
using Server; 
using Server.Items;

namespace Server.Items
{ 
	public class WoodBag : Bag 
	{ 
		[Constructable] 
		public WoodBag() : this( 5000 ) 
		{
			Name = "A Carpentry Kit";
		} 

		[Constructable] 
		public WoodBag( int amount ) 
		{ 
			DropItem( new Log   ( amount ) ); 
			DropItem( new OakLog   ( amount ) ); 
			DropItem( new AshLog   ( amount ) ); 
			DropItem( new YewLog   ( amount ) ); 
			DropItem( new HeartwoodLog   ( amount ) ); 
			DropItem( new BloodwoodLog   ( amount ) ); 
			DropItem( new FrostwoodLog   ( amount ) ); 
			DropItem( new Scorp ( amount ) );
			DropItem( new FletcherTools ( amount ) );

		} 

		public WoodBag( Serial serial ) : base( serial ) 
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
