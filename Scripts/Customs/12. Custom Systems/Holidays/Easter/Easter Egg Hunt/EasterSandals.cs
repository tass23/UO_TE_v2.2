using System;  
using Server; 

namespace Server.Items 
{ 
  [FlipableAttribute( 0x170d, 0x170e )]
   public class EasterSandals : Sandals 
    
{ 
  
     
      [Constructable] 
      public EasterSandals() : base( 0x170d ) 
      { 
                
              Name = "Easter Bunny Sandals";
      	      Hue = 1170;
      	      Weight = 1.0;
      	
      	      Resistances.Poison = 3;
                
      } 

      public EasterSandals( Serial serial ) : base( serial ) 
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
