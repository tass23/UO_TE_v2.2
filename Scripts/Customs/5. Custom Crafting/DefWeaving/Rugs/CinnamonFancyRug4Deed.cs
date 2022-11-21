using System;
using Server;

namespace Server.Items
{
	public class CinnamonFancyRug4Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new CinnamonFancyRug4Deed(); } }

		[Constructable]
		public CinnamonFancyRug4Addon()
		{
			AddComponent( new AddonComponent( 0x0AE4 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AE8 ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x0AE6 ),  1, -1, 0 );
			AddComponent( new AddonComponent( 0x0AE7 ), -1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AEB ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x0AE9 ),  1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AE5 ), -1,  1, 0 );
			AddComponent( new AddonComponent( 0x0AEA ),  0,  1, 0 );
			AddComponent( new AddonComponent( 0x0AE3 ),  1,  1, 0 );
		}

		public CinnamonFancyRug4Addon( Serial serial ) : base( serial )
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

	public class CinnamonFancyRug4Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new CinnamonFancyRug4Addon(); } }
		public override int LabelNumber{ get{ return 1076587; } } // Cinnamon Fancy Rug

		[Constructable]
		public CinnamonFancyRug4Deed()
		{
		}

		public CinnamonFancyRug4Deed( Serial serial ) : base( serial )
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
