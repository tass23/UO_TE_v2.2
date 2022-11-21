using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{ 
	public class ArmorweaveCladding : Item 
	{ 
		[Constructable]
		public ArmorweaveCladding()
		{
			ItemID = 16473;
			Movable = true;
			Hue = 1495;
			Name = "Armorweave Cladding";
		}

		public ArmorweaveCladding( Serial serial ) : base( serial ) 
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