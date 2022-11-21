/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
	public class FireRockGorget : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 45; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FireRockGorget() : base( 0x1413 )
		{
            Name = "FireRock Gorget";
            Hue = 1359;
			Weight = 2.0;

            Attributes.BonusDex = Utility.RandomMinMax(2, 5);
            Attributes.DefendChance = Utility.RandomMinMax(3, 12);
            Attributes.LowerManaCost = Utility.RandomMinMax(3, 14);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(5, 12);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);


            FireBonus = Utility.RandomMinMax(9, 15);
            PoisonBonus = Utility.RandomMinMax(3, 8);
            PhysicalBonus = Utility.RandomMinMax(4, 10);
            StrBonus = Utility.RandomMinMax(2, 5);

            LootType = LootType.Regular;
		}

        public FireRockGorget(Serial serial)
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