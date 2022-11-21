using Server;
using Server.Items;
using Server.Multis;
using System.Collections.Generic;
using Server.Network;
using System;

namespace Server.Items
{ 
	public class SankaraStones : Item 
	{ 
		[Constructable] 
		public SankaraStones() : base (0x5737)
		{
			Name = "The Sacred Sankara Stones";
			Hue = 1261;
			Light = LightType.Circle150;
			Weight = 5.0;
		} 

		public SankaraStones( Serial serial ) : base( serial ) 
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