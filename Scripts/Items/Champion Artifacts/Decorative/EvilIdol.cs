using System;
using Server;

namespace Server.Items
{
    public class EvilIdol : Item
    {
        public override int LabelNumber { get { return 1095237; } } // Evil Idol
	
		[Constructable]
		public EvilIdol() : base( 0x1F18 )
		{
			Weight = 5.0;			
		}

        public EvilIdol(Serial serial)
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

