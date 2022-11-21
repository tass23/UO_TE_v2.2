using System;
using Server;

namespace Server.Items
{
	public class ArcaneBookshelfEastAddon : BaseAddonContainer
	{
		public override BaseAddonContainerDeed Deed{ get{ return new ArcaneBookshelfEastDeed(); } }
		public override bool RetainDeedHue{ get{ return true; } }
		public override int DefaultGumpID{ get{ return 0x107; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		[Constructable]
		public ArcaneBookshelfEastAddon() : base( 0x3086 )
		{
			AddComponent( new AddonContainerComponent( 0x3087 ), 0, -1, 0 );
		}

		public ArcaneBookshelfEastAddon( Serial serial ) : base( serial )
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

	public class ArcaneBookshelfEastDeed : BaseAddonContainerDeed
	{
		public override BaseAddonContainer Addon{ get{ return new ArcaneBookshelfEastAddon(); } }
		public override int LabelNumber{ get{ return 1073371; } } // arcane bookshelf (east)

		[Constructable]
		public ArcaneBookshelfEastDeed() : base()
		{
		}

		public ArcaneBookshelfEastDeed( Serial serial ) : base( serial )
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