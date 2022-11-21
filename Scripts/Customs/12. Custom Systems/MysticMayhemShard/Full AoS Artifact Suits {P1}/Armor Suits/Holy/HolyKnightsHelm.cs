using System;
using Server;

namespace Server.Items
{
	public class HolyKnightsPlateHelm : PlateHelm
	{
		public override int LabelNumber{ get{ return 1061097; } } // Holy Knight's Plate Helm
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 12; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HolyKnightsPlateHelm()
		{
			Name = "Holy Knight's Plate Helm";
			Hue = 0x47E;
			Attributes.BonusHits = 10;
			Attributes.ReflectPhysical = 15;
		}

		public HolyKnightsPlateHelm( Serial serial ) : base( serial )
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
				PhysicalBonus = 0;
		}
	}
}