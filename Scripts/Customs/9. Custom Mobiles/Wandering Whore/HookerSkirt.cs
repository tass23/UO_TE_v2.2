using System;
using Server;

namespace Server.Items
{
	public class HookerSkirt : Kilt
	{

		[Constructable] 
		public HookerSkirt()
		{      
			Name = "Cheap Hooker Mini Skirt"; 
			Hue = 31;
		}

		public HookerSkirt( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize( GenericWriter writer ) 
		{ 
			base.Serialize( writer ); 

			writer.Write( (int) 0 ); 
		} 
        
		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 

			int version = reader.ReadInt(); 
		} 
	} 
} 


