using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealLlama : Item
	{
		public override int LabelNumber{ get{ return 1041300; } } // Ethereal Llama Statuette

		[Constructable]
		public CraftableEtherealLlama() : base( 0x20F6 )
		{
			Weight = 4;
			Name = "a crafted ethereal Llama";
		}

		public CraftableEtherealLlama( Serial serial ) : base( serial ) 
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