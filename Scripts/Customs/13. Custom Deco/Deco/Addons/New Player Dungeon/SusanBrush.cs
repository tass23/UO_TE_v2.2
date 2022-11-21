using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public class SusanBrush : Item
	{
		[Constructable]
		public SusanBrush( ) : base( 0x1373  )
		{
			Name = "Susan's Brush";
			Hue = 1266;
			Stackable = false;
			Weight = 1.0;
		}

		public SusanBrush( Serial serial ) : base( serial )
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
