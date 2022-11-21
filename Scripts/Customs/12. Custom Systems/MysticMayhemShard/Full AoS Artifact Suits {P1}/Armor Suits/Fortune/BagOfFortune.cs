using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfFortune : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Fortune"; }
		}

		[Constructable] 
		public BagOfFortune() 
		{  
			DropItem( new GlovesOfFortune() );
			DropItem( new CapOfFortune() );
			DropItem( new ArmsOfFortune() );
			DropItem( new LegsOfFortune() );
			DropItem( new GorgetOfFortune() );
		} 

		public BagOfFortune( Serial serial ) : base( serial ) 
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
