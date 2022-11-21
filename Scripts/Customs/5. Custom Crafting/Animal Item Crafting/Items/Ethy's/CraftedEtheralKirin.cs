using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealKirin : Item
	{
		public override int LabelNumber{ get{ return 1049746; } } // Ethereal Kirin Statuette

		[Constructable]
		public CraftableEtherealKirin() : base( 0x25A0 )
		{
			Weight = 4;
			Name = "a crafted ethereal Kirin";
		}

		public CraftableEtherealKirin( Serial serial ) : base( serial ) 
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