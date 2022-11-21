using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfNobility : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Nobility"; }
		}

		[Constructable] 
		public BagOfNobility() 
		{  
			DropItem( new ArmsOfNobility() );
			DropItem( new ArmorOfNobility() );
			DropItem( new LegsOfNobility() );
		} 

		public BagOfNobility( Serial serial ) : base( serial ) 
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
