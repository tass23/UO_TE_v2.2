using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps0 : BaseArmor
	{
		public override int Hue{ get{ return 37; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public VampLegWraps0() : base( 0x2FCA )
		{
            Name = "Vampire Legwraps";
			Weight = 2.0;
		}

		public VampLegWraps0( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps1 : BaseArmor
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
		public VampLegWraps1() : base( 0x2FCA )
		{
            Name = "Vampire Fledgling Legwraps";
			Weight = 2.0;
		}

		public VampLegWraps1( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps2 : BaseArmor
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
		public VampLegWraps2() : base( 0x2FCA )
		{
	        Name = "Vampire Legwraps";
			Weight = 2.0;
		}

		public VampLegWraps2( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps3 : BaseArmor
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
		public VampLegWraps3() : base( 0x2FCA )
		{
	        Name = "Vampire Elder Legwraps";
			Weight = 2.0;
        	Attributes.CastRecovery = 2;
		    Attributes.CastSpeed = 1;
			Attributes.LowerManaCost = 3;
			Attributes.RegenMana = 2;
			Attributes.SpellDamage = 2;
		}

		public VampLegWraps3( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps4 : BaseArmor
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
		public VampLegWraps4() : base( 0x2FCA )
		{
	        Name = "Vampire Patrician Legwraps";
			Weight = 2.0;
        	Attributes.CastRecovery = 3;
		    Attributes.CastSpeed = 2;
			Attributes.LowerManaCost = 8;
			Attributes.RegenMana = 5;
			Attributes.SpellDamage = 2;
		}

		public VampLegWraps4( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2FCA, 0x3180 )]
	public class VampLegWraps5 : BaseArmor
	{
		public override SetItem SetID{ get{ return SetItem.Vampire; } }
		public override int Pieces{ get{ return 5; } }
		public override int BasePhysicalResistance{ get{ return 20; } }
		public override int BaseFireResistance{ get{ return 20; } }
		public override int BaseColdResistance{ get{ return 20; } }
		public override int BasePoisonResistance{ get{ return 20; } }
		public override int BaseEnergyResistance{ get{ return 20; } }
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
		public VampLegWraps5() : base( 0x2FCA )
		{
	        Name = "Ancient Vampire Legwraps";
			Weight = 2.0;
			Attributes.LowerManaCost = 15;
			Attributes.RegenMana = 5;
			SetAttributes.BonusInt = 10;
			SetSkillBonuses.SetValues( 0, SkillName.Necromancy, 40 );
			SetAttributes.Luck = 100;
			SetAttributes.NightSight = 1;
			SetFireBonus = 8;
			SetEnergyBonus = 5;
		}

		public VampLegWraps5( Serial serial ) : base( serial )
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