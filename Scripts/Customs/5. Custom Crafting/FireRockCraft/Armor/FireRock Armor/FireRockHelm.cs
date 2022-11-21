/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class FireRockHelm : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 80; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FireRockHelm() : base( 0x1412 )
		{
            Name = "FireRock Helm";
            Hue = 1359;
			Weight = 5.0;

            Attributes.BonusDex = Utility.RandomMinMax(3, 5);
            Attributes.DefendChance = Utility.RandomMinMax(6, 15);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.Luck = Utility.RandomMinMax(100, 250);
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);


            FireBonus = Utility.RandomMinMax(12, 25);
            PoisonBonus = Utility.RandomMinMax(3, 5);
            PhysicalBonus = Utility.RandomMinMax(4, 15);
            StrBonus = Utility.RandomMinMax(2, 6);

            LootType = LootType.Regular;
		}

        public FireRockHelm(Serial serial)
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
				Weight = 5.0;
		}
	}
}