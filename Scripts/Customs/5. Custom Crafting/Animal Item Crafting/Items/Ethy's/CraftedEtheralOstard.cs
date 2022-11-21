using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealOstard : Item
	{
		public override int LabelNumber{ get{ return 1041299; } } // Ethereal Ostard Statuette

		[Constructable]
		public CraftableEtherealOstard() : base( 0x2135 )
		{
			Weight = 4;
			Name = "a crafted ethereal Ostard";
		}

		public CraftableEtherealOstard( Serial serial ) : base( serial ) 
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