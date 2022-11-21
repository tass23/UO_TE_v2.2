using System;
using Server;

namespace Server.Items
{
	public class TunicOfTheHarrower : BoneChest
	{
		public override int LabelNumber{ get{ return 1061095; } } // Tunic of the Harrower
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePoisonResistance{ get{ return 25; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public TunicOfTheHarrower()
		{
			Name = "Tunic of the Harrower";
			Hue = 0x4F6;
			Attributes.RegenHits = 7;
			Attributes.RegenStam = 7;
			Attributes.WeaponDamage = 35;
		}

		public TunicOfTheHarrower( Serial serial ) : base( serial )
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
				if ( Hue == 0x55A )
					Hue = 0x4F6;

				PoisonBonus = 0;
			}
		}
	}
}