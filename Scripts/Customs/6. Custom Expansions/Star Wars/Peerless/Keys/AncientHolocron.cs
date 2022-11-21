using System;
using Server;

namespace Server.Items
{
	public class AncientHolocron : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public AncientHolocron() : base( 3834 )
		{
			Name = "Ancient Holocron";
			Weight = 2;
			Hue = 1479;
		}

		public AncientHolocron( Serial serial ) : base( serial )
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