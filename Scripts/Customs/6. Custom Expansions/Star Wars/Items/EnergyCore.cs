using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{ 
	public class EnergyCore : Item 
	{ 
		[Constructable]
		public EnergyCore()
		{
			ItemID = 6245;
			Movable = true;
			Hue = 1772;
			Name = "Energy Core";
		}

		public EnergyCore( Serial serial ) : base( serial ) 
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