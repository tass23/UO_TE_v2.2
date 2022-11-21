/* Made by Sommy for Splitterwelt.com
 * Das Script darf frei benutzt werden solange der Header erhalten bleibt.
 * Bei Feedback bitte mail an webmaster@sommys.com oder
 * PM auf www.run-uo.de
 * Date: 02.03.2005
 * Version 1.0
 * Honigwaben Schleuder
 */

using System;
using Server; 
using Server.Items;
using Server.Network;
using Server.Scripts; 


namespace Server.Items 
{ 

   
   public class HoneycombProcessingKettle : Item 
   { 

      [Constructable] 
      public HoneycombProcessingKettle() : base( 0x9ED ) 
      { 
         Name = "Honeycomb Processing Kettle"; 
         this.Weight = 10.0;             
      } 

      
      public override void OnDoubleClick( Mobile from ) 
      { 
         Container pack = from.Backpack; 

	if( from.InRange( this.GetWorldLocation(), 1 ) )
	{

         if (pack != null && pack.ConsumeTotal( typeof( HoneyComb ), 1 ) ) 
         { 
            from.SendMessage( "*You centrifuge the honeycomb and separate honey and wax*" ); 
                
               { 
					
                from.AddToBackpack( new RawBeeswax() ); 
                from.AddToBackpack( new JarHoney() );
               } 
         } 

         else 
         { 
            from.SendMessage( "You need a honeycomb to use in this kettle" ); 
            return; 
         } 
      } 



         else 
         { 
            from.SendMessage( "You are too far away from this" ); 
            return; 
         } 

	}
      public HoneycombProcessingKettle( Serial serial ) : base( serial )
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

