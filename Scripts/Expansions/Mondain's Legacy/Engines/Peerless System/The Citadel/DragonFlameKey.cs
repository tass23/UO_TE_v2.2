using System;
using Server;

namespace Server.Items
{
	public class DragonFlameKey : PeerlessKey
	{
		public override int LabelNumber{ get{ return 1074343; } } // dragon flame key
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public DragonFlameKey() : base( 0x1012 )
		{
			Weight = 1.0;
			Hue = 0x8F; // TODO check
		}

		public DragonFlameKey( Serial serial ) : base( serial )
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

