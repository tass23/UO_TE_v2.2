using System;
using Server;

namespace Server.Items
{
	public class DeathBoneHelm : BoneHelm
	{
		public override int LabelNumber{ get{ return 1074305; } } // Death's Essence
		
		public override SetItem SetID{ get{ return SetItem.Necromancer; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public DeathBoneHelm() : base()
		{
			SetHue = 0x455;			
			
			Attributes.RegenHits = 1;
			Attributes.RegenMana = 1;
			
			SetAttributes.LowerManaCost = 10;
			
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 10 );
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 4;
			SetFireBonus = 5;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;
		}

		public DeathBoneHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}