using System;
using Server;

namespace Server.Items
{
	public class GnawsFang : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
		public override int LabelNumber{ get{ return 1074332; } } // gnaw's fang
	
		[Constructable]
		public GnawsFang() : base( 0x10E8 )
		{
			Weight = 1;
			Hue = 0x174; // TODO check
		}

		public GnawsFang( Serial serial ) : base( serial )
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

