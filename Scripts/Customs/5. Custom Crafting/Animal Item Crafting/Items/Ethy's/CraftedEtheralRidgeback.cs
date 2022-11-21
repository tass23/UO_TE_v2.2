using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealRidgeback : Item
	{
		public override int LabelNumber{ get{ return 1049747; } } // Ethereal Ridgeback Statuette

		[Constructable]
		public CraftableEtherealRidgeback() : base( 0x2615 )
		{
			Weight = 4;
			Name = "a crafted ethereal Ridgeback";
		}

		public CraftableEtherealRidgeback( Serial serial ) : base( serial ) 
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