using System;
using Server;

namespace Server.Items
{
	public class JackalsArms : PlateArms
	{
		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Arms
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public JackalsArms()
		{
			Name = "Jackal's Arms";
			Hue = 0x6D1;
			Attributes.BonusDex = 15;
			Attributes.RegenHits = 2;
		}

		public JackalsArms( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
			{
				if ( Hue == 0x54B )
					Hue = 0x6D1;

				FireBonus = 0;
				ColdBonus = 0;
			}
		}
	}
}