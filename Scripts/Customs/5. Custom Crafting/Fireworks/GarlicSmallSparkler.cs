using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   public class GarlicSmallSparkler : Item 
   { 
      [Constructable] 
      public GarlicSmallSparkler() : base( 0x10E7 ) 
      { 
		Hue = 1150;
		Name = "a garlic small sparkler";
      } 

      public GarlicSmallSparkler( Serial serial ) : base( serial ) 
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

	public override void OnDoubleClick( Mobile from )
	{
	    	if ( Parent != from ) 
           	from.FixedParticles( 0x3779, 200, 100, 5052, 1150, 0, EffectLayer.LeftHand );
		this.Delete();
	}
   } 
    
} 
