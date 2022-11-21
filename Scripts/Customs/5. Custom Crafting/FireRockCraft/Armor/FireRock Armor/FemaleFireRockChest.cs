/* Created by Hammerhand */

using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1c04, 0x1c05 )]
	public class FemaleFireRockChest : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 95; } }
		public override int OldStrReq{ get{ return 45; } }

		public override int OldDexBonus{ get{ return -5; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FemaleFireRockChest() : base( 0x1C04 )
		{
            Name = "Female FireRock Chest";
            Hue = 1359;
			Weight = 4.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.BonusInt = Utility.RandomMinMax(2, 5);
            Attributes.DefendChance = Utility.RandomMinMax(8, 15);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(8, 20);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);
            Attributes.RegenStam = Utility.RandomMinMax(2, 5);
            ArmorAttributes.DurabilityBonus = 10;

            FireBonus = Utility.RandomMinMax(13, 25);
            PoisonBonus = Utility.RandomMinMax(3, 9);
            PhysicalBonus = Utility.RandomMinMax(4, 12);
            StrBonus = Utility.RandomMinMax(3, 5);

            LootType = LootType.Regular;
		}

        public FemaleFireRockChest(Serial serial)
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
				Weight = 4.0;
		}
	}
}