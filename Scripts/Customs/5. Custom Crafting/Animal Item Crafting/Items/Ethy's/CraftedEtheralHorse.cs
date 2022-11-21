using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealHorse : Item
	{
		public override int LabelNumber{ get{ return 1041298; } } // Ethereal Horse Statuette

		[Constructable]
		public CraftableEtherealHorse() : base( 0x20DD )
		{
			Weight = 4;
			Name = "a crafted ethereal horse";
		}

		public CraftableEtherealHorse( Serial serial ) : base( serial ) 
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