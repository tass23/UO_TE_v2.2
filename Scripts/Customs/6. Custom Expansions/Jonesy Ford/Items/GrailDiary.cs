using System;
using Server;

namespace Server.Items
{
	public class GrailDiary : Item
	{
		[Constructable]
		public GrailDiary() : base( 0x0FBD )
		{
			LootType = LootType.Blessed;
			Weight = 1;
			Name = "Grail Diary";
		}

		public GrailDiary( Serial serial ) : base( serial )
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