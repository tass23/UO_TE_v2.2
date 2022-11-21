using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfBane : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Bane"; }
		}

		[Constructable] 
		public BagOfBane() 
		{  
			DropItem( new TunicOfBane() );
			DropItem( new CoifOfBane() );
			DropItem( new LeggingsOfBane() );
		} 

		public BagOfBane( Serial serial ) : base( serial ) 
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
