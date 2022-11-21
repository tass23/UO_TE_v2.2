using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x2641, 0x2642 )]
	public class SorrowOfMoldyovia : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 60; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.None; } }

		[Constructable]
		public SorrowOfMoldyovia() : base( 0x2641 )
		{
			Weight = 10.0;
			Name = "Sorrow of Moldyovia";
			Attributes.CastSpeed = 1;
			Attributes.RegenMana = 3;
			Attributes.SpellDamage = 10;
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 35;
			IntRequirement = 115;
			ArmorAttributes.SelfRepair = 10;
			ArmorAttributes.MageArmor = 1;
			StrRequirement = 60;
			Hue = 1772;
		}

		public SorrowOfMoldyovia( Serial serial ) : base( serial )
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

			if ( Weight == 1.0 )
				Weight = 15.0;
		}
	}
}