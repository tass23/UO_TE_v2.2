using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheGargDMSet : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Garg DM Set"; }
		}

		[Constructable] 
		public BagOfTheGargDMSet() 
		{  
			DropItem( new TunicOfTheGargDMSet() );
			DropItem( new LegsOfTheGargDMSet() );
			DropItem( new PlateKiltOfTheGargDMSet() );
			DropItem( new WingArmorOfTheGargDMSet() );
			DropItem( new ArmsOfTheGargDMSet() );
		} 

		public BagOfTheGargDMSet( Serial serial ) : base( serial ) 
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
