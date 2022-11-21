using System;
using Server.Items;

namespace Server.Items
{
	public class VampGorget0 : BaseArmor
	{
		public override int Hue{ get{ return 37; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampGorget0() : base( 0x2FC7 )
		{
	        Name = "Vampire Collar";
			Weight = 2.0;
		}

		public VampGorget0( Serial serial ) : base( serial )
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

	public class VampGorget1 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 8; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 60; } }
		public override int InitMaxHits{ get{ return 60; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override int ArmorBase{ get{ return 24; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampGorget1() : base( 0x2FC7 )
		{
	        Name = "Vampire Fledgling Collar";
			Weight = 2.0;
		}

		public VampGorget1( Serial serial ) : base( serial )
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

	public class VampGorget2 : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }
		public override int ArmorBase{ get{ return 30; } }
		public override int Hue{ get{ return 37; } }
		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 100; } }
		public override int AosStrReq{ get{ return 10; } }
		public override int OldStrReq{ get{ return 10; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampGorget2() : base( 0x2FC7 )
		{
	        Name = "Vampire Collar";
			Weight = 2.0;
		}

		public VampGorget2( Serial serial ) : base( serial )
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

	public class VampGorget3 : BaseArmor
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
		public VampGorget3() : base( 0x2FC7 )
		{
	        Name = "Vampire Elder Collar";
			Weight = 2.0;
        	Attributes.CastRecovery = 2;
		    Attributes.CastSpeed = 1;
			Attributes.RegenHits = 2;
			Attributes.SpellDamage = 5;
		}

		public VampGorget3( Serial serial ) : base( serial )
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

	public class VampGorget4 : BaseArmor
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
		public VampGorget4() : base( 0x2FC7 )
		{
	        Name = "Vampire Patrician Collar";
			Weight = 2.0;
        	Attributes.CastRecovery = 3;
		    Attributes.CastSpeed = 2;
			Attributes.RegenHits = 5;
			Attributes.SpellDamage = 10;
		}

		public VampGorget4( Serial serial ) : base( serial )
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

	public class VampGorget5 : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.Vampire; } }
		public override int Pieces{ get{ return 5; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 5; } }
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
		public VampGorget5() : base( 0x2FC7 )
		{
	        Name = "Ancient Vampire Collar";
			Weight = 2.0;
			Attributes.LowerManaCost = 15;
			Attributes.SpellDamage = 5;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 40 );
			SetAttributes.Luck = 100;
			SetAttributes.NightSight = 1;
			SetFireBonus = 8;
			SetEnergyBonus = 5;
		}

		public VampGorget5( Serial serial ) : base( serial )
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