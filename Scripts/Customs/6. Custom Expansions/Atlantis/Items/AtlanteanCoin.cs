using System;
using Server;

namespace Server.Items
{
	public class AtlanteanCoin : Item
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public AtlanteanCoin() : base( 4113 )
		{
			Name = "an Atlantean coin";
			Hue = 2983;
		}

		public AtlanteanCoin( Serial serial ) : base( serial )
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