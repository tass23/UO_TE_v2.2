using System;
using Server;

namespace Server.Items
{
	public class CharredRing : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public CharredRing() : base( 16914 )
		{
			ItemID = 16914;
			Name = "Charred Stabilizing Ring";
			Weight = 2;
			Hue = 1647;
		}

		public CharredRing( Serial serial ) : base( serial )
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