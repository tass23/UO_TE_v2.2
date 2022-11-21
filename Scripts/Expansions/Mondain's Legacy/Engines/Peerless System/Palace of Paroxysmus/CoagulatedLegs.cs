using System;
using Server;

namespace Server.Items
{
	public class CoagulatedLegs : PeerlessKey
	{
		public override int LabelNumber{ get{ return 1074327; } } // coagulated legs
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public CoagulatedLegs() : base( 0x1CDF )
		{
			Weight = 30.0;
		}

		public CoagulatedLegs( Serial serial ) : base( serial )
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

