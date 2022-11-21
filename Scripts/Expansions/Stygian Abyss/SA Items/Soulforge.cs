using System;
using Server;

namespace Server.Items
{
	public class OneTileSoulForgeAddon : BaseAddon
	{
        public override BaseAddonDeed Deed { get { return new OneTileSoulForgeDeed(); } }

		#region Mondain's Legacy
		public override bool RetainDeedHue{ get{ return true; }	}
		#endregion

		[Constructable]
		public OneTileSoulForgeAddon()
		{
			AddComponent( new ForgeComponent( 0x44C7 ), 0, 0, 0 );
		}

        public OneTileSoulForgeAddon(Serial serial)
            : base(serial)
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

	public class OneTileSoulForgeDeed : BaseAddonDeed
	{
        public override BaseAddon Addon { get { return new OneTileSoulForgeAddon(); } }
		

		[Constructable]
        public OneTileSoulForgeDeed()
		{
            Name = "SoulForge";
		}

		public OneTileSoulForgeDeed( Serial serial ) : base( serial )
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