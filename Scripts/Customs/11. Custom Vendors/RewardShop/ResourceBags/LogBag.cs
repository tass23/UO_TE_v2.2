using System; 
using Server; 
using Server.Items;

namespace Server.Items
{ 
	public class LogBag : Bag 
	{
		public override string DefaultName
		{
			get { return "a Log Supply bag"; }
		}
		[Constructable] 
		public LogBag() : this( 5000 ) 
		{ 
		} 

		[Constructable] 
		public LogBag( int amount ) 
		{ 
			DropItem( new Log   ( amount ) ); 
			DropItem( new OakLog   ( amount ) ); 
			DropItem( new AshLog   ( amount ) ); 
			DropItem( new YewLog   ( amount ) ); 
			DropItem( new HeartwoodLog   ( amount ) ); 
			DropItem( new BloodwoodLog   ( amount ) ); 
			DropItem( new FrostwoodLog   ( amount ) );
		} 

		public LogBag( Serial serial ) : base( serial ) 
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
