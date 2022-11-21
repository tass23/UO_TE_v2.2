using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2FC8, 0x317E )]
	public class WerewolfArms1 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int Hue{ get{ return 1905; } }
		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 60; } }
		public override int AosStrReq{ get{ return 15; } }
		public override int OldStrReq{ get{ return 15; } }
		public override int ArmorBase{ get{ return 24; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public WerewolfArms1() : base( 0x2FC8 )
		{
			Weight = 2.0;
	        Name = "Werewolf Fledgling Armguards";
		}

		public WerewolfArms1( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FC8, 0x317E )]
	public class WerewolfArms2 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int ArmorBase{ get{ return 30; } }
		public override int Hue{ get{ return 1905; } }
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 80; } }
		public override int AosStrReq{ get{ return 15; } }
		public override int OldStrReq{ get{ return 15; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public WerewolfArms2() : base( 0x2FC8 )
		{
			Weight = 2.0;
       		Name = "Werewolf Armguards";
		}

		public WerewolfArms2( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FC8, 0x317E )]
	public class WerewolfArms3 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 12; } }
		public override int ArmorBase{ get{ return 35; } }
		public override int Hue{ get{ return 1905; } }
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public WerewolfArms3() : base( 0x2FC8 )
		{
	        Name = "Werewolf Elder Armguards";
			Weight = 2.0;
			Attributes.RegenStam = 1;
			Attributes.AttackChance = 5;
		}

		public WerewolfArms3( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FC8, 0x317E )]
	public class WerewolfArms4 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 14; } }
		public override int BaseFireResistance{ get{ return 14; } }
		public override int BaseEnergyResistance{ get{ return 14; } }
		public override int ArmorBase{ get{ return 40; } }
		public override int Hue{ get{ return 1905; } }
		public override int InitMinHits{ get{ return 140; } }
		public override int InitMaxHits{ get{ return 140; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public WerewolfArms4() : base( 0x2FC8 )
		{
	        Name = "Werewolf Patrician Armguards";
			Weight = 2.0;
			Attributes.RegenStam = 2;
		}

		public WerewolfArms4( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FC8, 0x317E )]
	public class WerewolfArms5 : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.Werewolf; } }
		public override int Pieces{ get{ return 5; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 20; } }
		public override int BaseEnergyResistance{ get{ return 20; } }
		public override int ArmorBase{ get{ return 60; } }
		public override int Hue{ get{ return 1905; } }
		public override int InitMinHits{ get{ return 200; } }
		public override int InitMaxHits{ get{ return 200; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public WerewolfArms5() : base( 0x2FC8 )
		{
	        Name = "Ancient Werewolf Armguards";
			Weight = 2.0;
			Attributes.LowerManaCost = 6;
			Attributes.ReflectPhysical = 8;
			SetAttributes.BonusStr = 10;
			SetSkillBonuses.SetValues( 0, SkillName.AnimalLore, 40 );
			SetAttributes.Luck = 100;
			SetAttributes.NightSight = 1;
			SetPoisonBonus = 8;
		}

		public WerewolfArms5( Serial serial ) : base( serial )
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