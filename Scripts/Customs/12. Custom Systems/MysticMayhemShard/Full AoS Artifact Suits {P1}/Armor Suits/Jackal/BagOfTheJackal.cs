using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheJackal : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Jackal"; }
		}

		[Constructable] 
		public BagOfTheJackal() 
		{  
			DropItem( new JackalsTunic() );
			DropItem( new JackalsLeggings() );
			DropItem( new JackalsHelm() );
			DropItem( new JackalsGloves() );
			DropItem( new JackalsArms() );
		} 

		public BagOfTheJackal( Serial serial ) : base( serial ) 
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
