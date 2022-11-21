using System;
using Server.Items;

namespace Server.Items
{
	public class AssassinArms : LeatherArms
	{
		public override int LabelNumber{ get{ return 1074304; } } // Assassin Armor
		
		public override SetItem SetID{ get{ return SetItem.Assassin; } }
		public override int Pieces{ get{ return 4; } }
	
		public override int BasePhysicalResistance{ get{ return 9; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public AssassinArms() : base()
		{
			SetHue = 0x455;
			
			Attributes.BonusStam = 2;
			Attributes.WeaponSpeed = 5;		
			
			SetSkillBonuses.SetValues( 0, SkillName.Stealth, 30 );
			
			SetSelfRepair = 3;
			
			SetAttributes.BonusDex = 12;
			
			SetPhysicalBonus = 5;
			SetFireBonus = 4;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;
		}

		public AssassinArms( Serial serial ) : base( serial )
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