using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfMidnight : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Midnight"; }
		}

		[Constructable] 
		public BagOfMidnight() 
		{  
			DropItem( new MidnightTunic() );
			DropItem( new MidnightLegs() );
			DropItem( new MidnightHelm() );
			DropItem( new MidnightGloves() );
		} 

		public BagOfMidnight( Serial serial ) : base( serial ) 
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
