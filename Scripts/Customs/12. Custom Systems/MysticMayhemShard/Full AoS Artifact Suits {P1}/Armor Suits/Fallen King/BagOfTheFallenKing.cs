using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheFallenKing : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Fallen King"; }
		}

		[Constructable] 
		public BagOfTheFallenKing() 
		{  
			DropItem( new TunicOfTheFallenKing() );
			DropItem( new LegsOfTheFallenKing() );
			DropItem( new GlovesOfTheFallenKing() );
			DropItem( new CapOfTheFallenKing() );
			DropItem( new ArmsOfTheFallenKing() );
			DropItem( new VoiceOfTheFallenKing() );
		} 

		public BagOfTheFallenKing( Serial serial ) : base( serial ) 
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
