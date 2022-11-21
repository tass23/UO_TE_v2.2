using System;
using Server;

namespace Server.Items
{
	public class RedRug2Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new RedRug2Deed(); } }

		[Constructable]
		public RedRug2Addon() 
		{
			AddComponent( new AddonComponent( 0x0ACA ), -1, -1, 0 );
			AddComponent( new AddonComponent( 0x0ACE ),  0, -1, 0 );
			AddComponent( new AddonComponent( 0x0ACC ),  1, -1, 0 );
			AddComponent( new AddonComponent( 0x0ACD ), -1,  0, 0 );
			AddComponent( new AddonComponent( 0x0AC8 ),  0,  0, 0 );
			AddComponent( new AddonComponent( 0x0ACF ),  1,  0, 0 );
			AddComponent( new AddonComponent( 0x0ACB ), -1,  1, 0 );
			AddComponent( new AddonComponent( 0x0AD0 ),  0,  1, 0 );
			AddComponent( new AddonComponent( 0x0AC9 ),  1,  1, 0 );
		}

		public RedRug2Addon( Serial serial ) : base( serial )
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

	public class RedRug2Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new RedRug2Addon(); } }

		[Constructable]
		public RedRug2Deed()
		{
			Name = "Red rug";
		}

		public RedRug2Deed( Serial serial ) : base( serial )
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
