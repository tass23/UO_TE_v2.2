/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2647, 0x2648)]
	public class CrystalineFireLegs : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 8; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int OldStrReq{ get{ return 60; } }
		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public CrystalineFireLegs() : base( 0x2647 )
		{
            Name = "CrystalineFire Legs";
            Hue = 1357;
			Weight = 7.0;

            Attributes.BonusHits = Utility.RandomMinMax(3, 12);
            Attributes.DefendChance = Utility.RandomMinMax(4, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(2, 12);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 16);
            Attributes.RegenHits = Utility.RandomMinMax(3, 5);
            Attributes.RegenStam = Utility.RandomMinMax(3, 7);

            EnergyBonus = Utility.RandomMinMax(2, 5);
            FireBonus = Utility.RandomMinMax(8, 25);
            PhysicalBonus = Utility.RandomMinMax(7, 15);

            LootType = LootType.Regular;
		}

        public CrystalineFireLegs(Serial serial)
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
		}
	}
}