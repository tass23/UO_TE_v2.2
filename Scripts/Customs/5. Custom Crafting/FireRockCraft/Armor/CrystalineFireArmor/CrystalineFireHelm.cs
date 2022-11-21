/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
    [Flipable(0x2645, 0x2646)]
	public class CrystalineFireHelm : BaseArmor
	{
        public override int Hue { get { return 1357; } }
		public override int BaseFireResistance{ get{ return 6; } }

		public override int InitMinHits{ get{ return 100; } }
		public override int InitMaxHits{ get{ return 150; } }

		public override int AosStrReq{ get{ return 80; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 40; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Dragon; } }

		[Constructable]
		public CrystalineFireHelm() : base( 0x2645 )
		{
            Name = "CrystalineFire Helm";
            Hue = 1357;
			Weight = 5.0;

            Attributes.BonusDex = Utility.RandomMinMax(2, 8);
            Attributes.DefendChance = Utility.RandomMinMax(4, 10);
            Attributes.LowerRegCost = Utility.RandomMinMax(3, 12);
            Attributes.ReflectPhysical = Utility.RandomMinMax(4, 15);
            Attributes.RegenHits = Utility.RandomMinMax(2, 5);


            FireBonus = Utility.RandomMinMax(7, 25);
            PoisonBonus = Utility.RandomMinMax(3, 8);
            PhysicalBonus = Utility.RandomMinMax(5, 9);

            LootType = LootType.Regular;
		}

        public CrystalineFireHelm(Serial serial)
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