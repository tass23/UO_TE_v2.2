using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheShadowDancer : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag Of The Shadow Dancer"; }
		}

		[Constructable] 
		public BagOfTheShadowDancer() 
		{  
			DropItem( new ShadowDancerTunic() );
			DropItem( new ShadowDancerGorget() );
			DropItem( new ShadowDancerGloves() );
			DropItem( new ShadowDancerCap() );
			DropItem( new ShadowDancerArms() );
		} 

		public BagOfTheShadowDancer( Serial serial ) : base( serial ) 
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
