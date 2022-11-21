using System;
using Server;

namespace Server.Items
{
	public class JackalsGloves : PlateGloves
	{
		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Gloves
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseFireResistance{ get{ return 13; } }
		public override int BaseColdResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public JackalsGloves()
		{
			Name = "Jackal's Gloves";
			Hue = 0x6D1;
			Attributes.BonusDex = 15;
			Attributes.RegenHits = 2;
		}

		public JackalsGloves( Serial serial ) : base( serial )
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