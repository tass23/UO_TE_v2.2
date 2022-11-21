using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{ 
	public class MandalorianCrushgaunts : Item 
	{ 

		[Constructable]
		public MandalorianCrushgaunts()
		{
			ItemID = 11020;
			Movable = true;
			Hue = 2051;
			Name = "Mandalorian Crushgaunts";

		}

		public MandalorianCrushgaunts( Serial serial ) : base( serial ) 
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