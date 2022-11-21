using System;
using Server;

namespace Server.Items
{
	public class LeggingsOfAegis : PlateLegs
	{
		public override int LabelNumber{ get{ return 1061602; } } // Leggings of Ægis
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 18; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public LeggingsOfAegis()
		{
			Name = "Leggings of Aegis";
			Hue = 0x47E;
			ArmorAttributes.SelfRepair = 5;
			Attributes.ReflectPhysical = 18;
			Attributes.DefendChance = 18;
			Attributes.LowerManaCost = 14;
		}

		public LeggingsOfAegis( Serial serial ) : base( serial )
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