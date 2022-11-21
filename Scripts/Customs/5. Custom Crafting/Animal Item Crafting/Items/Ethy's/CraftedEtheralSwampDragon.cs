using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealSwampDragon : Item
	{
		public override int LabelNumber{ get{ return 1049749; } } // Ethereal SwampDragon Statuette

		[Constructable]
		public CraftableEtherealSwampDragon() : base( 0x2619 )
		{
			Weight = 4;
			Name = "a crafted ethereal Swamp Dragon";
		}

		public CraftableEtherealSwampDragon( Serial serial ) : base( serial ) 
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