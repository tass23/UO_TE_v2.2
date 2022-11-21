using System; 
using Server; 
using Server.Items;
using Server.Gumps;
namespace Server.Items
{ 
	public class MilGoldDeed : Item 
	{ 
		


		[Constructable]
		public MilGoldDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 52;
			Name = " A Deed for a MILLION GOLD BankCheck";
					
		}
		
		 public override void OnDoubleClick( Mobile from )
      		{
       		  from.AddToBackpack( new BankCheck(1000000) ); 
       		  this.Delete();
        	 }

		[Constructable]
		public MilGoldDeed( int amount ) 
        {
		}
		
		

		public MilGoldDeed( Serial serial ) : base( serial ) 
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
