using System;
using Server;


namespace Server.Items
{
	public class RareSCJewel : Item
	{
	[Constructable]
			public RareSCJewel () : base ( 0xF10 )
			{
				Name = " Rare Spell Craft Jewel ";
				Weight = 1.0;
				Hue = 18;
				
			}
			public RareSCJewel (Serial serial) : base ( serial )
			{
				
			}
			
			public override void Serialize ( GenericWriter writer)
			{
				base.Serialize (writer);
				writer.Write ((int)0); //vershion
				
			}
			public override void Deserialize (GenericReader reader)
			{
				base.Deserialize (reader);
				int version = reader.ReadInt ();
				
			}
          public override void OnDoubleClick (Mobile m)
		{
			m.SendMessage ( " A rare Spell Craft Gem") ;
			
		}
	}
}




