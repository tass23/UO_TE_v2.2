using System;
using Server;

namespace Server.Items
{
	public class ElvenWashBasinSouthAddon : BaseAddonContainer
	{
		public override BaseAddonContainerDeed Deed{ get{ return new ElvenWashBasinSouthDeed(); } }
		public override bool RetainDeedHue{ get{ return true; } }
		public override int DefaultGumpID{ get{ return 0x104; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		[Constructable]
		public ElvenWashBasinSouthAddon() : base( 0x30E2 )
		{
			AddComponent( new AddonContainerComponent( 0x30E1 ), -1, 0, 0 );
		}

		public ElvenWashBasinSouthAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class ElvenWashBasinSouthDeed : BaseAddonContainerDeed
	{
		public override BaseAddonContainer Addon{ get{ return new ElvenWashBasinSouthAddon(); } }
		public override int LabelNumber{ get{ return 1072865; } } // elven wash basin (south)

		[Constructable]
		public ElvenWashBasinSouthDeed() : base()
		{
		}

		public ElvenWashBasinSouthDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
