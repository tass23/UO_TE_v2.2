using System;
using Server;

namespace Server.Items
{
	public class PaladinHelm : PlateHelm
	{
		public override int LabelNumber{ get{ return 1074303; } } // Plate of Honor
		
		public override SetItem SetID{ get{ return SetItem.Paladin; } }
		public override int Pieces{ get{ return 6; } }
	
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public PaladinHelm() : base()
		{
			SetHue = 0x47E;					
			
			Attributes.RegenHits = 1;
			Attributes.AttackChance = 5;
			
			SetAttributes.ReflectPhysical = 25;
			SetAttributes.NightSight = 1;
			
			SetSkillBonuses.SetValues( 0, SkillName.Chivalry, 10 );
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 2;
			SetFireBonus = 5;
			SetColdBonus = 5;
			SetPoisonBonus = 3;
			SetEnergyBonus = 5;
		}

		public PaladinHelm( Serial serial ) : base( serial )
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