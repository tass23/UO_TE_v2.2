using System;
using Server;

namespace Server.Items
{
	public class GoldenRug2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GoldenRug2Deed(); } }

		[Constructable]
		public GoldenRug2Addon()
		{
			AddComponent( new AddonComponent( 0x0AB8 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AB4 ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x0AB9 ),  1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AB7 ), -1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AB3 ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x0AB5 ),  1,  0, 0 );
			AddComponent( new AddonComponent( 0x0ABB ), -1,  1, 0 );
			AddComponent( new AddonComponent( 0x0AB6 ),  0,  1, 0 );
			AddComponent( new AddonComponent( 0x0ABA ),  1,  1, 0 );
		}

		public GoldenRug2Addon( Serial serial ) : base( serial )
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

	public class GoldenRug2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GoldenRug2Addon(); } }

		[Constructable]
		public GoldenRug2Deed()
		{
			Name = "Large golden rug";
		}

		public GoldenRug2Deed( Serial serial ) : base( serial )
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
