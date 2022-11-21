using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class HolyGrail : Item
	{
	
		[Constructable]
		public HolyGrail( ) : base( 0x099A  )
		{
			Name = "Holy Grail";
			Hue = 1711;
			Stackable = false;
			Weight = 1.0;
		}
		
		public HolyGrail( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
