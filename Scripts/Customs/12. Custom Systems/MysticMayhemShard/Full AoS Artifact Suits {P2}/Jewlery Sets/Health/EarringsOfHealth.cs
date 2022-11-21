using System;
using Server;

namespace Server.Items
{
	public class EarringsOfHealth : GoldEarrings
	{
		public override int LabelNumber{ get{ return 1061103; } } // Earrings of Health
		public override int ArtifactRarity{ get{ return 11; } }

		[Constructable]
		public EarringsOfHealth()
		{
			Name = "Earrings of Health";
			Hue = 0x21;
			Attributes.BonusHits = 3;
			Attributes.RegenHits = 5;
		}

		public EarringsOfHealth( Serial serial ) : base( serial )
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