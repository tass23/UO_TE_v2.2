using System; 
using Server; 
using Server.Items;
using Server.Gumps;

namespace Server.Items
{
	public class TornScroll : Item
	{
		[Constructable]
		public TornScroll()
		{
			ItemID = 0x1F23;
			Movable = true;
			Hue = 2101;
			Name = "Torn scroll for Tassarine the Hutt";
		}

		public TornScroll( Serial serial ) : base( serial ) 
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