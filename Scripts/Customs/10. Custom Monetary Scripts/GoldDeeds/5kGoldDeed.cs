using System; 
using Server; 
using Server.Items;
using Server.Gumps;
namespace Server.Items
{ 
	public class FivekGoldDeed : Item 
	{ 
		


		[Constructable]
		public FivekGoldDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 52;
			Name = " A Deed for a 5k BankCheck";
					
		}
		
		 public override void OnDoubleClick( Mobile from )
      		{
       		  from.AddToBackpack( new BankCheck(5000) ); 
       		  this.Delete();
        	 }

		[Constructable]
		public FivekGoldDeed( int amount ) 
        {
		}
		
		

		public FivekGoldDeed( Serial serial ) : base( serial ) 
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
