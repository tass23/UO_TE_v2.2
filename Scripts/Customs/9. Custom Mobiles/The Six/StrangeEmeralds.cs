using System;
using Server;

namespace Server.Items
{
   	public class StrangeEmerald1 : Item
   	{
		[Constructable]
		public StrangeEmerald1()
		{
			Name = "emerald";
			ItemID = 3887;
			Weight = 1.0;
		}

		public StrangeEmerald1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
   	}
}