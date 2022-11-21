using System;
using Server;

namespace Server.Items
{
	public class ShadowDancerTunic : LeatherChest
	{
		public override int LabelNumber{ get{ return 1061598; } } // Shadow Dancer Tunic
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 19; } }
		public override int BasePoisonResistance{ get{ return 21; } }
		public override int BaseEnergyResistance{ get{ return 21; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ShadowDancerTunic()
		{
			Name = "Shadow Dancer Tunic";
			Hue = 0x455;
			SkillBonuses.SetValues( 0, SkillName.Stealth, 15.0 );
			SkillBonuses.SetValues( 1, SkillName.Stealing, 15.0 );
		}

		public ShadowDancerTunic( Serial serial ) : base( serial )
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
				if ( ItemID == 0x13CB )
					ItemID = 0x13D2;

				PhysicalBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;
			}
		}
	}
}