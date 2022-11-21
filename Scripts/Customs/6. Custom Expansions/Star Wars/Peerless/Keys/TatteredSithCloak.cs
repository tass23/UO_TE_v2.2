using System;
using Server;

namespace Server.Items
{
	public class TatteredSithCloak : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public TatteredSithCloak() : base( 9859 )
		{
			Name = "Tattered Sith Grandmaster's Cloak";
			Weight = 5;
			Hue = 2051;
		}

		public TatteredSithCloak( Serial serial ) : base( serial )
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