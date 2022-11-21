/* Created by Hammerhand*/

using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1414, 0x1418 )]
	public class FireRockGloves : BaseArmor
	{
        public override int Hue { get { return 1359; } }
		public override int BaseFireResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 250; } }
		public override int InitMaxHits{ get{ return 255; } }

		public override int AosStrReq{ get{ return 70; } }
		public override int OldStrReq{ get{ return 30; } }

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public FireRockGloves() : base( 0x1414 )
		{
            Name = "FireRock Gloves";
            Hue = 1359;
			Weight = 2.0;

            Attributes.BonusInt = Utility.RandomMinMax(3, 5);
            Attributes.DefendChance = Utility.RandomMinMax(2, 7);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = Utility.RandomMinMax(3, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);

            FireBonus = Utility.RandomMinMax(3, 15);
            PoisonBonus = Utility.RandomMinMax(2, 8);
            PhysicalBonus = Utility.RandomMinMax(2, 12);
            StrBonus = Utility.RandomMinMax(2, 5);

            LootType = LootType.Regular;
		}

        public FireRockGloves(Serial serial)
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