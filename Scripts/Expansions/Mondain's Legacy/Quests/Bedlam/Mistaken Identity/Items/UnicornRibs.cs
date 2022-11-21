using System;
using Server;

namespace Server.Items
{
	public class UnicornRibs : Item
	{
		public override int LabelNumber{ get{ return 1074611; } } // Unicorn Ribs
				
		[Constructable]
		public UnicornRibs() : base( 0x9F1 )
		{
			LootType = LootType.Blessed;			
			Weight = 1;
			Hue = 0x14B;
		}
		
		public UnicornRibs( Serial serial ) : base( serial )
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