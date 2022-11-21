using System;
using Server.Items;

namespace Server.Items
{
	public class GreymistLegs : LeatherLegs
	{
		public override int LabelNumber{ get{ return 1074307; } } // Greymist Armor
		
		public override SetItem SetID{ get{ return SetItem.Acolyte; } }
		public override int Pieces{ get{ return 4; } }
	
		public override int BasePhysicalResistance{ get{ return 7; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public GreymistLegs() : base()
		{
			SetHue = 0xCB;		
			
			Attributes.BonusMana = 2;
			Attributes.SpellDamage = 2;
			
			SetAttributes.Luck = 100;
			SetAttributes.NightSight = 1;
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 3;
			SetFireBonus = 3;
			SetColdBonus = 3;
			SetPoisonBonus = 3;
			SetEnergyBonus = 3;
		}

		public GreymistLegs( Serial serial ) : base( serial )
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