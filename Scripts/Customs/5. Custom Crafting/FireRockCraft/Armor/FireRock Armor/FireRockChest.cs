/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1415, 0x1416 )]
	public class FireRockChest : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 95; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FireRockChest() : base( 0x1415 )
		{
            Name = "FireRock Chest";
            Hue = 1359;
			Weight = 10.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.BonusHits = Utility.RandomMinMax(2, 7);
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerManaCost = Utility.RandomMinMax(7, 15);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 12);
            Attributes.RegenStam = Utility.RandomMinMax(2, 6);
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            FireBonus = Utility.RandomMinMax(13, 15);
            PhysicalBonus = Utility.RandomMinMax(4, 12);

            LootType = LootType.Regular;
		}

        public FireRockChest(Serial serial)
            : base(serial)
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
				Weight = 10.0;
		}
	}
}