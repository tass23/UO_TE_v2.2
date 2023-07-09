
//Created by Ashlar, beloved of Morrigan

using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PaintbrushItemAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new PaintbrushItemAddonDeed();
			}
		}

		[ Constructable ]
		public PaintbrushItemAddon()
		{
			AddonComponent ac = null;
			ac = new AddonComponent( 16104 );
			ac.Name = "a paintbrush item";
			AddComponent( ac, 0, 0, 0 );

		}

		public PaintbrushItemAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

		}
	}

	public class PaintbrushItemAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new PaintbrushItemAddon();
			}
		}

		[Constructable]
		public PaintbrushItemAddonDeed()
		{
			Name = "a Paintbrush Item";
		}

		public PaintbrushItemAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}
