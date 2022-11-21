using System;
using Server;

namespace Server.Items
{
	public class DisintegratingThesisNotes : PeerlessKey
	{
		public override int LabelNumber{ get{ return 1074440; } } // Disintegrating Thesis Notes
		public override int Lifespan{ get{ return 7200; } }
	
		[Constructable]
		public DisintegratingThesisNotes() : base( 0xE36 )
		{
			Weight = 1.0;
		}

		public DisintegratingThesisNotes( Serial serial ) : base( serial )
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

