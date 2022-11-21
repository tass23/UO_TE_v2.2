using System;
using Server;

namespace Server.Items
{
	public class HelmOfAegis : PlateHelm
	{
		public override int LabelNumber{ get{ return 1061602; } } // Helm of Ægis
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 10; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public HelmOfAegis()
		{
			Name = "Helm of Aegis";
			Hue = 0x47E;
			ArmorAttributes.SelfRepair = 5;
			Attributes.ReflectPhysical = 14;
			Attributes.DefendChance = 14;
			Attributes.LowerManaCost = 12;
		}

		public HelmOfAegis( Serial serial ) : base( serial )
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