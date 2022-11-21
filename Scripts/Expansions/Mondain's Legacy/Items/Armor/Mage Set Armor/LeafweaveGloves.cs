using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2B75, 0x316C )]
	public class LeafweaveGloves : HideGloves
	{
		public override int LabelNumber{ get{ return 1074299; } } // Elven Leafweave
		
		public override SetItem SetID{ get{ return SetItem.Mage; } }
		public override int Pieces{ get{ return 4; } }
		
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public LeafweaveGloves() : base()
		{
			SetHue = 0x47E;
			
			Attributes.RegenMana = 1;
			ArmorAttributes.MageArmor = 1;
			
			SetAttributes.BonusInt = 10;
			SetAttributes.SpellDamage = 15;
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 4;
			SetFireBonus = 5;
			SetColdBonus = 3;
			SetPoisonBonus = 4;
			SetEnergyBonus = 4;
		}

		public LeafweaveGloves( Serial serial ) : base( serial )
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
