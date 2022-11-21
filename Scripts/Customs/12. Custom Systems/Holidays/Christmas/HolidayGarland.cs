using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x3BBB, 0x3BBC )]
	public class HolidayGarland : Item
	{		
		[Constructable]
		public HolidayGarland() : base( 0x3BBB )
		{
			Name = "Holiday Garland";
			Weight = 2.0;
			LootType = LootType.Blessed;
		}

		public HolidayGarland( Serial serial ) : base( serial )
		{
		}
		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Happy Holiday's" );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}