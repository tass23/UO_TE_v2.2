using System; 
using Server; 
using Server.Items;

namespace Server.Items 
{ 
	public class BagOfTheHolyKnight : Bag 
	{
		public override string DefaultName
		{
			get { return "a Bag of The Holy Knight"; }
		}

		[Constructable] 
		public BagOfTheHolyKnight() 
		{  
			DropItem( new HolyKnightsLegging() );
			DropItem( new HolyKnightsPlateHelm() );
			DropItem( new HolyKnightsGorget() );
			DropItem( new HolyKnightsGloves() );
			DropItem( new HolyKnightsArmPlates() );
		} 

		public BagOfTheHolyKnight( Serial serial ) : base( serial ) 
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
