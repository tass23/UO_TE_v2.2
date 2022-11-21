using System;
using Server;

namespace Server.Items
{
	public class TigerClawKey : PeerlessKey
	{
		public override int LabelNumber{ get{ return 1074342; } } // tiger claw key
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public TigerClawKey() : base( 0x1012 )
		{
			Weight = 1.0;
			Hue = 0x5D; // TODO check
		}

		public TigerClawKey( Serial serial ) : base( serial )
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

