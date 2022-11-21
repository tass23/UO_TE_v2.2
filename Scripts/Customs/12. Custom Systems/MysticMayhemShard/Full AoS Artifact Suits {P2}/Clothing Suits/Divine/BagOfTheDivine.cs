using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheDivine : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Divine"; }
		}

		[Constructable] 
		public BagOfTheDivine() 
		{  
			DropItem( new DivineTunic() );
			DropItem( new DivineLeggings() );
			DropItem( new DivineGorget() );
			DropItem( new DivineGloves() );
			DropItem( new DivineArms() );
		} 

		public BagOfTheDivine( Serial serial ) : base( serial ) 
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
