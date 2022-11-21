using System;
using Server;

namespace Server.Items
{
	public class RingOfHealth : GoldRing
	{
		public override int LabelNumber{ get{ return 1061103; } } // Ring of Health
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public RingOfHealth()
		{
			Name = "Ring of Health";
			Hue = 0x21;
			Attributes.BonusHits = 4;
			Attributes.RegenHits = 7;
		}

		public RingOfHealth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}