using System;
using Server.Items;

namespace Server.Items
{
	public class GrizzleTunic : BoneChest
	{
		public override int LabelNumber{ get{ return 1074467; } } // Tunic of the Grizzle
		
		public override SetItem SetID{ get{ return SetItem.Grizzle; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		[Constructable]
		public GrizzleTunic() : base()
		{
			SetHue = 0x278;
			
			ArmorAttributes.MageArmor = 1;
			Attributes.BonusHits = 5;
			Attributes.NightSight = 1;
			
			SetAttributes.DefendChance = 10;
			SetAttributes.BonusStr = 12;
			
			SetSelfRepair = 3;
			
			SetPhysicalBonus = 3;
			SetFireBonus = 5;
			SetColdBonus = 3;
			SetPoisonBonus = 3;
			SetEnergyBonus = 5;
		}

		public GrizzleTunic( Serial serial ) : base( serial )
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