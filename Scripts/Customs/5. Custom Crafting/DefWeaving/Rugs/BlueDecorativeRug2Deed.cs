using System;
using Server;

namespace Server.Items
{
	public class BlueDecorativeRug2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new BlueDecorativeRug2Deed(); } }

		[Constructable]
		public BlueDecorativeRug2Addon()
		{
			AddComponent( new AddonComponent( 0x0AEF ), -1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AF3 ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x0AF1 ),  1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AF2 ), -1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AFA ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x0AF4 ),  1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AF0 ), -1,  1, 0 );
			AddComponent( new AddonComponent( 0x0AF5 ),  0,  1, 0 );
			AddComponent( new AddonComponent( 0x0AEE ),  1,  1, 0 );
		}

		public BlueDecorativeRug2Addon( Serial serial ) : base( serial )
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

	public class BlueDecorativeRug2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new BlueDecorativeRug2Addon(); } }
		public override int LabelNumber{ get{ return 1076589; } } // BlueDecorative Rug

		[Constructable]
		public BlueDecorativeRug2Deed()
		{
		}

		public BlueDecorativeRug2Deed( Serial serial ) : base( serial )
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
