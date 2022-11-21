using System;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.SkillHandlers;

namespace Server.Items 
{ 

   public class MandrakeRootSmallSparkler : Item 
   { 
      [Constructable] 
      public MandrakeRootSmallSparkler() : base( 0x10E7 ) 
      { 
		Hue = 1269;
		Name = "a mandrake root small sparkler";
      } 

      public MandrakeRootSmallSparkler( Serial serial ) : base( serial ) 
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
           	from.FixedParticles( 0x3779, 200, 100, 5052, 1268, 0, EffectLayer.LeftHand );
		this.Delete();
	}
   } 
    
} 
