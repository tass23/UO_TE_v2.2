using System;
using Server.Mobiles;
using Server.Items;
using Server.Spells;

namespace Server.Items
{

public class CraftableEtherealUnicorn : Item
	{
		public override int LabelNumber{ get{ return 1049745; } } // Ethereal Unicorn Statuette

		[Constructable]
		public CraftableEtherealUnicorn() : base( 0x25CE )
		{
			Weight = 4;
			Name = "a crafted ethereal Unicorn";
		}

		public CraftableEtherealUnicorn( Serial serial ) : base( serial ) 
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