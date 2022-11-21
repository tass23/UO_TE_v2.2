using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheDragonWing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Dragon Wing"; }
		}

		[Constructable] 
		public BagOfTheDragonWing() 
		{  
			DropItem( new TunicOfTheDragonWing() );
			DropItem( new LegsOfTheDragonWing() );
			DropItem( new GlovesOfTheDragonWing() );
			DropItem( new CapOfTheDragonWing() );
			DropItem( new ArmsOfTheDragonWing() );
		} 

		public BagOfTheDragonWing( Serial serial ) : base( serial ) 
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
