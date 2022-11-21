using System;
using Server;

namespace Server.Items
{
	public class AntiquatedSaberHilt : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public AntiquatedSaberHilt() : base( 6812 )
		{
			Name = "Antiquated Lightsaber Hilt";
			Weight = 2;
			Hue = 1478;
		}

		public AntiquatedSaberHilt( Serial serial ) : base( serial )
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