using System;
using Server;

namespace Server.Items
{
	public class AriellesBauble : Item
	{	
		public override int LabelNumber{ get{ return 1073137; } } // A bauble
	
		[Constructable]
		public AriellesBauble() : base( 0x23B )
		{
			Weight = 2.0;
			LootType = LootType.Blessed;
		}

		public AriellesBauble( Serial serial ) : base( serial )
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

