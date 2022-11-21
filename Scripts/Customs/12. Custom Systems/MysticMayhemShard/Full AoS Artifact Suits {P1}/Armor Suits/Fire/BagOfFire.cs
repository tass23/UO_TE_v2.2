using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfFire : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Fire"; }
		}

		[Constructable] 
		public BagOfFire() 
		{  
			DropItem( new CoifOfFire() );
			DropItem( new LeggingsOfFire() );
			DropItem( new TunicOfFire() );
		} 

		public BagOfFire( Serial serial ) : base( serial ) 
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
