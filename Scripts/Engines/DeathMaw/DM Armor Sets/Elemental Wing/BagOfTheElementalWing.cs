using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheElementalWing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Elemental Wing"; }
		}

		[Constructable] 
		public BagOfTheElementalWing() 
		{  
			DropItem( new TunicOfTheElementalWing() );
			DropItem( new LegsOfTheElementalWing() );
			DropItem( new GlovesOfTheElementalWing() );
			DropItem( new CapOfTheElementalWing() );
			DropItem( new ArmsOfTheElementalWing() );
		} 

		public BagOfTheElementalWing( Serial serial ) : base( serial ) 
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
