using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealBeetle : Item
	{
		public override int LabelNumber{ get{ return 1049748; } } // Ethereal Beetle Statuette

		[Constructable]
		public CraftableEtherealBeetle() : base( 0x260F )
		{
			Weight = 4;
			Name = "a crafted ethereal Beetle";
		}

		public CraftableEtherealBeetle( Serial serial ) : base( serial ) 
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