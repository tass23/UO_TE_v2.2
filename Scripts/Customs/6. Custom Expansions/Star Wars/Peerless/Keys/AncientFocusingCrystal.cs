using System;
using Server;

namespace Server.Items
{
	public class AncientFocusingCrystal : PeerlessKey
	{	
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public AncientFocusingCrystal() : base( 3982 )
		{
			Name = "Ancient Focusing Crystal";
			Weight = 10;
			Hue = 1479;
		}

		public AncientFocusingCrystal( Serial serial ) : base( serial )
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