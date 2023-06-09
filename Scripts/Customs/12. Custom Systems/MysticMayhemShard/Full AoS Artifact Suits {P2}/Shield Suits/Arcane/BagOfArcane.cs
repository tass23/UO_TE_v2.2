using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfArcane : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of Arcane"; }
		}

		[Constructable] 
		public BagOfArcane() 
		{  
			DropItem( new ArmsOfAegis() );
			DropItem( new GlovesOfAegis() );
			DropItem( new GorgetOfAegis() );
			DropItem( new HelmOfAegis() );
			DropItem( new LeggingsOfAegis() );
			DropItem( new TunicOfAegis() );
		} 

		public BagOfArcane( Serial serial ) : base( serial ) 
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
