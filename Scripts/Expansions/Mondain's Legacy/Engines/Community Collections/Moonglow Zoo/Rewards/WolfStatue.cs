using System;
using Server;

namespace Server.Items
{
	public class WolfStatue : Item
	{
		public override int LabelNumber{ get{ return 1073190; } } // A Wolf Contribution Statue from the Britannia Royal Zoo.
	
		[Constructable]
		public WolfStatue() : base( 0x25D3 )
		{
			LootType = LootType.Blessed;
			Weight = 1.0;			
		}

		public WolfStatue( Serial serial ) : base( serial )
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

