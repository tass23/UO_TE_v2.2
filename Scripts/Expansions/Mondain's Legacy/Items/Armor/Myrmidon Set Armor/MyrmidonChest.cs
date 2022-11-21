using System;
using Server.Items;

namespace Server.Items
{
	public class MyrmidonChest : StuddedChest
	{
		public override int LabelNumber{ get{ return 1074306; } } // Myrmidon Armor
		
		public override SetItem SetID{ get{ return SetItem.Myrmidon; } }
		public override int Pieces{ get{ return 6; } }
	
		public override int BasePhysicalResistance{ get{ return 7; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		[Constructable]
		public MyrmidonChest() : base()
		{
			SetHue = 0x331;
			
			Attributes.BonusStr = 1;
			Attributes.BonusHits = 2;
			
			SetAttributes.Luck = 500;
			SetAttributes.NightSight = 1;
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 3;
			SetFireBonus = 3;
			SetColdBonus = 3;
			SetPoisonBonus = 3;
			SetEnergyBonus = 3;
		}

		public MyrmidonChest( Serial serial ) : base( serial )
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