
using System;
using Server;

namespace Server.Items
{
	public class GreenTeaCauldronAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GreenTeaCauldronDeed(); } }

		[Constructable]
		public GreenTeaCauldronAddon()
		{
			AddComponent( new AddonComponent( 10315 ), 0, 0, 0 );
		}

		public GreenTeaCauldronAddon( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}

	public class GreenTeaCauldronDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GreenTeaCauldronAddon(); } }
		public override int LabelNumber{ get{ return 1030315; } }

		[Constructable]
		public GreenTeaCauldronDeed()
		{
			Name = "Green Tea Cauldron Deed";
		}

		public GreenTeaCauldronDeed( Serial serial ) : base( serial ) { }

		public override void Serialize( GenericWriter writer ) { base.Serialize( writer ); writer.Write( (int) 0 );}

		public override void Deserialize( GenericReader reader ) { base.Deserialize( reader ); int version = reader.ReadInt();}
	}
}