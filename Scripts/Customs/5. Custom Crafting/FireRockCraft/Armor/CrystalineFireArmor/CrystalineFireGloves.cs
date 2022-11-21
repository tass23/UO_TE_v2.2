/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x2643, 0x2644)]
	public class CrystalineFireGloves : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 70; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public CrystalineFireGloves() : base( 0x2643 )
		{
            Name = "CrystalineFire Gloves";
            Hue = 1357;
			Weight = 2.0;

            Attributes.BonusInt = Utility.RandomMinMax(2, 6);
            Attributes.DefendChance = Utility.RandomMinMax(5, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 10);
            Attributes.Luck = Utility.RandomMinMax(75, 150);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(7, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);

            EnergyBonus = Utility.RandomMinMax(2, 6);
            FireBonus = Utility.RandomMinMax(3, 15);
            PoisonBonus = Utility.RandomMinMax(4, 9);
            PhysicalBonus = Utility.RandomMinMax(4, 12);

            LootType = LootType.Regular;
		}

        public CrystalineFireGloves(Serial serial)
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
				Weight = 2.0;
		}
	}
}