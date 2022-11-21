using System;
using Server.Items;

namespace Server.Items
{
	public class HunterGloves : LeafGloves
	{
		public override int LabelNumber{ get{ return 1074301; } } // Hunter's Garb
		
		public override SetItem SetID{ get{ return SetItem.Hunter; } }
		public override int Pieces{ get{ return 4; } }
		
		public override int BasePhysicalResistance{ get{ return 9; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public HunterGloves() : base()
		{
			SetHue = 0x483;
			
			Attributes.RegenHits = 1;
			Attributes.Luck = 50;
			
			SetAttributes.BonusDex = 10;
			
			SetSkillBonuses.SetValues( 0, SkillName.Stealth, 40 );
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 5;
			SetFireBonus = 4;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;
		}

		public HunterGloves( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}
