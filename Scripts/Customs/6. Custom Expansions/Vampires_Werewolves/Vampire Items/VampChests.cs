using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest0 : BaseArmor
	{
		public override int Hue{ get{ return 37; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest0() : base( 0x2FC5 )
		{
	        Name = "Vampire Tunic";
			Weight = 2.0;
		}

		public VampChest0( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest1 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 60; } }
		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 20; } }
		public override int ArmorBase{ get{ return 24; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest1() : base( 0x2FC5 )
		{
	        Name = "Vampire Fledgling Tunic";
			Weight = 2.0;
		}

		public VampChest1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest2 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int ArmorBase{ get{ return 30; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 80; } }
		public override int AosStrReq{ get{ return 20; } }
		public override int OldStrReq{ get{ return 20; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest2() : base( 0x2FC5 )
		{
      		Name = "Vampire Tunic";
			Weight = 2.0;
		}

		public VampChest2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest3 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 12; } }
		public override int BasePoisonResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 12; } }
		public override int ArmorBase{ get{ return 35; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 120; } }
		public override int InitMaxHits{ get{ return 120; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest3() : base( 0x2FC5 )
		{
	        Name = "Vampire Elder Tunic";
			Weight = 2.0;
        	Attributes.CastRecovery = 2;
		    Attributes.CastSpeed = 1;
			Attributes.LowerRegCost = 2;
			Attributes.RegenMana = 2;
			Attributes.SpellDamage = 5;
		}

		public VampChest3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest4 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 14; } }
		public override int BaseFireResistance{ get{ return 14; } }
		public override int BaseColdResistance{ get{ return 14; } }
		public override int BasePoisonResistance{ get{ return 14; } }
		public override int BaseEnergyResistance{ get{ return 14; } }
		public override int ArmorBase{ get{ return 40; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 140; } }
		public override int InitMaxHits{ get{ return 140; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest4() : base( 0x2FC5 )
		{
        	Name = "Vampire Patrician Tunic";
			Weight = 2.0;
			Attributes.CastRecovery = 3;
		    Attributes.CastSpeed = 2;
			Attributes.LowerRegCost = 7;
			Attributes.RegenMana = 5;
			Attributes.SpellDamage = 10;
		}

		public VampChest4( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}

	[FlipableAttribute( 0x2FC5, 0x317B )]
	public class VampChest5 : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.Vampire; } }
		public override int Pieces{ get{ return 5; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 20; } }
		public override int BasePoisonResistance{ get{ return 20; } }
		public override int ArmorBase{ get{ return 60; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 200; } }
		public override int InitMaxHits{ get{ return 200; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampChest5() : base( 0x2FC5 )
		{
	        Name = "Ancient Vampire Tunic";
			Weight = 2.0;
			Attributes.LowerRegCost = 12;
			Attributes.RegenMana = 4;
			Attributes.SpellDamage = 15;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 40 );
			SetAttributes.Luck = 100;
			SetAttributes.NightSight = 1;
			SetFireBonus = 8;
			SetEnergyBonus = 5;
		}

		public VampChest5( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}