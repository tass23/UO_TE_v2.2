using System;
using Server;

namespace Server.Items
{
	public class SosariaSap : Item
	{	
		public override int LabelNumber{ get{ return 1074178; } } // Sap of Sosaria
	
		[Constructable]
		public SosariaSap() : base( 0x1848 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public SosariaSap( Serial serial ) : base( serial )
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

