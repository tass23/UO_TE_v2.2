/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2641, 0x2642)]
	public class CrystalineFireChest : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 95; } }
		public override int OldStrReq{ get{ return 60; } }

		public override int OldDexBonus{ get{ return -8; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public CrystalineFireChest() : base( 0x2641 )
		{
            Name = "CrystalineFire Chest";
            Hue = 1357;
			Weight = 10.0;

            Attributes.BonusDex = Utility.RandomMinMax(2, 5);
            Attributes.BonusHits = Utility.RandomMinMax(3, 7);
            Attributes.DefendChance = Utility.RandomMinMax(3, 8);
            Attributes.LowerManaCost = Utility.RandomMinMax(8, 20);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 15);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);
            ArmorAttributes.SelfRepair = 3;

            EnergyBonus = Utility.RandomMinMax(3, 5);
            FireBonus = Utility.RandomMinMax(12, 20);
            PoisonBonus = Utility.RandomMinMax(3, 6);
            PhysicalBonus = Utility.RandomMinMax(5, 12);

            LootType = LootType.Regular;
		}

        public CrystalineFireChest(Serial serial)
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