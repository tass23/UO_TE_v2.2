using System;
using Server;

namespace Server.Items
{
	public class CapOfTheFeyWing : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.FeyWing; } }
		public override int Pieces{ get{ return 5; } }
		
		public override int ArtifactRarity{ get{ return 15; } }

		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }
		
		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }
		
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }
		
		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public CapOfTheFeyWing() : base ( 0x1547 )
		{
			Name = "Galadriel's Boon Visage";
			Hue = 2224;

			ArmorAttributes.MageArmor = 1;
			Attributes.BonusInt = Utility.RandomMinMax (5,8);
			
			SetSkillBonuses.SetValues( 0, SkillName.Magery, 10.0 );
			SetSkillBonuses.SetValues( 0, SkillName.Inscribe, 10.0 );
			SetAttributes.BonusInt = 5;
		}

		public CapOfTheFeyWing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}