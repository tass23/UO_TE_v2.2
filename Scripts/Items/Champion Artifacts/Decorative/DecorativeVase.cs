using System;
using Server;

namespace Server.Items
{
    public class DecorationLargeVase : Item
    {
		[Constructable]
		public DecorationLargeVase() : base( 0xB47 )
		{		
		}

        public DecorationLargeVase(Serial serial)
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

    public class DecorationSmallVase : Item
    {
        [Constructable]
        public DecorationSmallVase()
            : base(0xB48)
        {
        }

        public DecorationSmallVase(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}

