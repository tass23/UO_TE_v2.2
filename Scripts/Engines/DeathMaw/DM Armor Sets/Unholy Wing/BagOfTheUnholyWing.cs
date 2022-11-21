using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheUnholyWing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Unholy Wing"; }
		}

		[Constructable] 
		public BagOfTheUnholyWing() 
		{  
			DropItem( new TunicOfTheUnholyWing() );
			DropItem( new LegsOfTheUnholyWing() );
			DropItem( new GlovesOfTheUnholyWing() );
			DropItem( new CapOfTheUnholyWing() );
			DropItem( new ArmsOfTheUnholyWing() );
		} 

		public BagOfTheUnholyWing( Serial serial ) : base( serial ) 
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
