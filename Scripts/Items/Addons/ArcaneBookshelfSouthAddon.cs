using System;
using Server;

namespace Server.Items
{
	public class ArcaneBookshelfSouthAddon : BaseAddonContainer
	{
		public override BaseAddonContainerDeed Deed{ get{ return new ArcaneBookshelfSouthDeed(); } }
		public override bool RetainDeedHue{ get{ return true; } }
		public override int DefaultGumpID{ get{ return 0x107; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		[Constructable]
		public ArcaneBookshelfSouthAddon() : base( 0x3084 )
		{
			AddComponent( new AddonContainerComponent( 0x3085 ), -1, 0, 0 );
		}

		public ArcaneBookshelfSouthAddon( Serial serial ) : base( serial )
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

	public class ArcaneBookshelfSouthDeed : BaseAddonContainerDeed
	{
		public override BaseAddonContainer Addon{ get{ return new ArcaneBookshelfSouthAddon(); } }
		public override int LabelNumber{ get{ return 1072871; } } // arcane bookshelf (south)

		[Constructable]
		public ArcaneBookshelfSouthDeed() : base()
		{
		}

		public ArcaneBookshelfSouthDeed( Serial serial ) : base( serial )
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