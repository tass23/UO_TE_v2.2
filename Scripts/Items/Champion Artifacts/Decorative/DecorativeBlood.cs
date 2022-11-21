using System;
using Server;

namespace Server.Items
{
    public class DecorativeBlood : Item
    {
		[Constructable]
		public DecorativeBlood() : base( 0x1D95 )
		{
			Weight = 5.0;			
		}

        public DecorativeBlood(Serial serial)
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

