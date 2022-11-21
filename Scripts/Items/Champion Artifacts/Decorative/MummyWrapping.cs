using System;
using Server;

namespace Server.Items
{
    public class MummyWrapping : Item
    {
        public override int LabelNumber { get { return 1094912; } } // Tattered Ancient Mummy Wrapping [Replica]

		[Constructable]
		public MummyWrapping() : base( 0xE21 )
		{		
		}

        public MummyWrapping(Serial serial)
            : base(serial)
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

