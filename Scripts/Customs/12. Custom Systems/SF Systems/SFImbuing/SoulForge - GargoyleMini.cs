using System;
using Server;

namespace Server.Items
{
	public class GargoyleMiniSoulForge : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GargoyleMiniSoulForgeDeed(); } }

		[Constructable]
		public GargoyleMiniSoulForge()
		{
			AddComponent( new AddonComponent( 17607 ), 0, 0, 0 );
		}

		public GargoyleMiniSoulForge( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
	public class GargoyleMiniSoulForgeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GargoyleMiniSoulForge(); } }
		
		[Constructable]
		public GargoyleMiniSoulForgeDeed()
		{
			Name = "Gargoyle SoulForge";
		}

		public GargoyleMiniSoulForgeDeed( Serial serial ) : base( serial )
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