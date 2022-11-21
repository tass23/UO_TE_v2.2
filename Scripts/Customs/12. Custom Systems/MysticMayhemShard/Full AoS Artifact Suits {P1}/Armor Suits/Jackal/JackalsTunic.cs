using System;
using Server;

namespace Server.Items
{
	public class JackalsTunic : PlateChest
	{
		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Tunic
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseFireResistance{ get{ return 23; } }
		public override int BaseColdResistance{ get{ return 29; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public JackalsTunic()
		{
			Name = "Jackal's Tunic";
			Hue = 0x6D1;
			Attributes.BonusDex = 15;
			Attributes.RegenHits = 2;
		}

		public JackalsTunic( Serial serial ) : base( serial )
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