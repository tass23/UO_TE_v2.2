using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheInquisitor : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Inquisitor"; }
		}

		[Constructable] 
		public BagOfTheInquisitor() 
		{  
			DropItem( new InquisitorsTunic() );
			DropItem( new InquisitorsLeggings() );
			DropItem( new InquisitorsHelm() );
			DropItem( new InquisitorsGorget() );
			DropItem( new InquisitorsArms() );
		} 

		public BagOfTheInquisitor( Serial serial ) : base( serial ) 
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
