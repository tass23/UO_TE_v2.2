using System;
using Server;

namespace Server.Items
{
	public class ShadowDancerGloves : LeatherGloves
	{
		public override int LabelNumber{ get{ return 1061598; } } // Shadow Dancer Gloves
		public override SetItem SetID{ get{ return SetItem.Shadow; } }
		public override int Pieces{ get{ return 5; } }
		public override int ArtifactRarity{ get{ return 11; } }

		public override int BasePhysicalResistance{ get{ return 13; } }
		public override int BasePoisonResistance{ get{ return 15; } }
		public override int BaseEnergyResistance{ get{ return 15; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ShadowDancerGloves()
		{
			Name = "Shadow Dancer Gloves";
			Hue = 0x455;
			
			SetSkillBonuses.SetValues( 0, SkillName.Stealth, 5.0 );
			SetSkillBonuses.SetValues( 1, SkillName.Stealing, 5.0 );
		}

		public ShadowDancerGloves( Serial serial ) : base( serial )
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