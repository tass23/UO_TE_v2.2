using System;

namespace Server.Items
{
	public class MillieBracelet : Item
	{
		[Constructable]
		public MillieBracelet( ) : base( 0x1086 )
		{
			Weight = 1.0;
			Name = "Millie's Gold Bracelet";
			Hue = 1281;
		}
		
		public MillieBracelet( Serial serial ) : base( serial )
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