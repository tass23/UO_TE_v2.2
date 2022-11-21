using System;
using Server;

namespace Server.Items
{
	public class QuagmireStatue : Item
	{
		public override int LabelNumber{ get{ return 1073195; } } // A Quagmire Contribution Statue from the Britannia Royal Zoo.
	
		[Constructable]
		public QuagmireStatue() : base( 0x2614 )
		{
			LootType = LootType.Blessed;
			Weight = 1.0;			
		}

		public QuagmireStatue( Serial serial ) : base( serial )
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

