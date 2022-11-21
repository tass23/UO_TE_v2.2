using System;
using Server;

namespace Server.Items
{
	public class MidnightHelm : BoneHelm
	{
		public override int LabelNumber{ get{ return 1061093; } } // Midnight Helm
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 12; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public MidnightHelm()
		{
			Name = "Midnight Helm";
			Hue = 0x455;
			SkillBonuses.SetValues( 0, SkillName.Necromancy, 5.0 );
			Attributes.SpellDamage = 10;
			ArmorAttributes.MageArmor = 1;
		}

		public MidnightHelm( Serial serial ) : base( serial )
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