using System;
using Server;
using Solaris.Addons;

namespace Server.Items
{
	public class MagicThread : Item
	{
		
		[Constructable]
		public MagicThread() : base( 4000 )
		{
			Name = "Magic Thread";
			Weight = 0.1;
			Stackable = true;
			Hue = 1152;
		}
		
		public MagicThread( Serial serial ) : base( serial )
		{
		}
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
			
	}
}