using System; 
using Server; 
using Server.Mobiles;
using Server.Items; 
using Server.Gumps; 
using Server.Network; 

namespace Server.Items 
{ 
   public class LifeStone : Item 
   { 
      [Constructable] 
      public LifeStone() : base( 8710 )
      { 
         Movable = false; 
         Name = "a lifestone";
//	 Hue = 96;
      } 

      public override void OnDoubleClick( Mobile from ) 
      { 
	  		if ( !from.InRange( GetWorldLocation(), 2 ) )
				from.SendLocalizedMessage( 500446 ); // That is too far away.
	  	 	
			else
			{
            	from.SendGump(	new LifeStoneGump( from ) );
				from.Frozen = true; 
			}
      } 

      public LifeStone( Serial serial ) : base( serial ) 
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