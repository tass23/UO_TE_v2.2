using System;
using Server;

namespace Server.Items
{
	public class JackalsLeggings : PlateLegs
	{
		public override int LabelNumber{ get{ return 1061594; } } // Jackal's Leggings
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BaseFireResistance{ get{ return 23; } }
		public override int BaseColdResistance{ get{ return 19; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public JackalsLeggings()
		{
			Name = "Jackal's Leggings";
			Hue = 0x6D1;
			Attributes.BonusDex = 15;
			Attributes.RegenHits = 2;
		}

		public JackalsLeggings( Serial serial ) : base( serial )
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