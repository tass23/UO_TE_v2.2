using System;
using Server;

namespace Server.Items
{
	public class DragonTorphySouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new DragonTorphySouthDeed(); } }

		[Constructable]
		public DragonTorphySouthAddon()
		{
			AddComponent( new AddonComponent( 0x2234 ), 0, 0, 10 );
		}

		public DragonTorphySouthAddon( Serial serial ) : base( serial )
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

	public class DragonTorphySouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new DragonTorphySouthAddon(); } }

		[Constructable]
		public DragonTorphySouthDeed()
		{
			Name = "dragon head trophy deed [south]";
		}

		public DragonTorphySouthDeed( Serial serial ) : base( serial )
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