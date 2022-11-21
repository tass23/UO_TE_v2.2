using Server;
using Server.Items;
using Server.Multis;
using System.Collections.Generic;
using Server.Network;
using System;

namespace Server.Items
{ 
	public class QuestArk : Item 
	{ 
		[Constructable] 
		public QuestArk() : base (10936)
		{
			Name = "The Ark of the Covenant";
			Hue = 1591;
			Light = LightType.Circle300;
			Weight = 15.0;
		} 

		public QuestArk( Serial serial ) : base( serial ) 
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