using System;
using Server;

namespace Server.Items
{
	public class BluePlainRug2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BluePlainRug2Deed(); } }

		[Constructable]
		public BluePlainRug2Addon()
		{
			AddComponent( new AddonComponent( 0x0AC3 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AF7 ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x0AC5 ),  1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AF6 ), -1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AC1 ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x0AF8 ),  1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AC4 ), -1,  1, 0 );
			AddComponent( new AddonComponent( 0x0AF9 ),  0,  1, 0 );
			AddComponent( new AddonComponent( 0x0AC2 ),  1,  1, 0 );
		}

		public BluePlainRug2Addon( Serial serial ) : base( serial )
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

	public class BluePlainRug2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BluePlainRug2Addon(); } }
		public override int LabelNumber{ get{ return 1076585; } } // BluePlain Rug

		[Constructable]
		public BluePlainRug2Deed()
		{
		}

		public BluePlainRug2Deed( Serial serial ) : base( serial )
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
