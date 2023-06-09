using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheHunter : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of the Hunter"; }
		}

		[Constructable] 
		public BagOfTheHunter() 
		{  
			DropItem( new HuntersArms() );
			DropItem( new HuntersGloves() );
			DropItem( new HuntersGorget() );
			DropItem( new HuntersLeggings() );
			DropItem( new HuntersTunic() );
		} 

		public BagOfTheHunter( Serial serial ) : base( serial ) 
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
