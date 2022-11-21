using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{ 
	public class DroidActuators : Item 
	{ 
		[Constructable]
		public DroidActuators()
		{
			ItemID = 9439;
			Movable = true;
			Hue = 1459;
			Name = "Droid Actuators";
		}

		public DroidActuators( Serial serial ) : base( serial ) 
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