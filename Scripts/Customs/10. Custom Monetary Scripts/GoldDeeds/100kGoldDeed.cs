using System; 
using Server; 
using Server.Items;
using Server.Gumps;
namespace Server.Items
{ 
	public class HunkGoldDeed : Item 
	{ 
		


		[Constructable]
		public HunkGoldDeed() : this( 1 )
		{
			ItemID = 5360;
			Movable = true;
			Hue = 52;
			Name = " A Deed for a 100k BankCheck";
					
		}
		
		 public override void OnDoubleClick( Mobile from )
      		{
       		  from.AddToBackpack( new BankCheck(100000) ); 
       		  this.Delete();
        	 }

		[Constructable]
		public HunkGoldDeed( int amount ) 
        {
		}
		
		

		public HunkGoldDeed( Serial serial ) : base( serial ) 
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
