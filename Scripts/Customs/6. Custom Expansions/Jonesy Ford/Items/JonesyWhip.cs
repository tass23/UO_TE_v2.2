using System;
using Server;

namespace Server.Items
{
	public class JonesyWhip : Item
	{
		
		[Constructable]
		public JonesyWhip() : base( 0x166E )
		{
			LootType = LootType.Blessed;
			Weight = 1;
			Name = "Jonesy's Whip";
		}
		
		public JonesyWhip( Serial serial ) : base( serial )
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