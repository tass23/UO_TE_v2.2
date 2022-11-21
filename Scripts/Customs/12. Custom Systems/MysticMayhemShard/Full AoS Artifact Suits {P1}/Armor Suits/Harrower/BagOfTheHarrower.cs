using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheHarrower : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Harrower"; }
		}

		[Constructable] 
		public BagOfTheHarrower() 
		{  
			DropItem( new ArmsOfTheHarrower() );
			DropItem( new TunicOfTheHarrower() );
			DropItem( new LegsOfTheHarrower() );
			DropItem( new GlovesOfTheHarrower() );
		} 

		public BagOfTheHarrower( Serial serial ) : base( serial ) 
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
