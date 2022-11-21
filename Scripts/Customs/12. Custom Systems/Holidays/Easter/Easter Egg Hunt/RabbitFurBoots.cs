using System;  
using Server; 

namespace Server.Items 
{ 
	[Flipable( 0x2307, 0x2308 )]
 
   public class RabbitFurBoots : FurBoots 
    
{ 
      [Constructable] 
      public RabbitFurBoots() : base( 0x2307 ) 
      { 
               
            Name = "Rabbit Fur Boots";
      	    Hue = 1165;
      	    Weight = 3.0;
      	
      	    Resistances.Cold = 5;
			Resistances.Poison = 5;
      }

      public RabbitFurBoots( Serial serial ) : base( serial ) 
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
