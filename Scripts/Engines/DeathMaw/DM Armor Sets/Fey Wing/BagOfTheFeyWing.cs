using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheFeyWing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Fey Wing"; }
		}

		[Constructable] 
		public BagOfTheFeyWing() 
		{  
			DropItem( new TunicOfTheFeyWing() );
			DropItem( new LegsOfTheFeyWing() );
			DropItem( new GlovesOfTheFeyWing() );
			DropItem( new CapOfTheFeyWing() );
			DropItem( new ArmsOfTheFeyWing() );
		} 

		public BagOfTheFeyWing( Serial serial ) : base( serial ) 
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
