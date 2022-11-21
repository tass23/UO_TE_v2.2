using System;
using Server;

namespace Server.Items
{
	public class ShatteredCrystals : PeerlessKey
	{
		public override int LabelNumber{ get{ return 1074266; } } // shattered crystal
		public override int Lifespan{ get{ return 21600; } }
	
		[Constructable]
		public ShatteredCrystals() : base( 0x223F )
		{
			Weight = 1;
			Hue = 0x47E;
		}

		public ShatteredCrystals( Serial serial ) : base( serial )
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

