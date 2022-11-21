/* Created by Hammerhand*/

using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
    public class CrashAndBurn : BaseBashing
	{
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.ConcussionBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.CrushingBlow; } }

        public override int Hue { get { return 1359; } }
		public override int AosStrengthReq{ get{ return 60; } }
		public override int AosMinDamage{ get{ return 18; } }
		public override int AosMaxDamage{ get{ return 20; } }
		public override int AosSpeed{ get{ return 38; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 45; } }
		public override int OldMinDamage{ get{ return 5; } }
		public override int OldMaxDamage{ get{ return 35; } }
		public override int OldSpeed{ get{ return 37; } }

		public override int InitMinHits{ get{ return 47; } }
		public override int InitMaxHits{ get{ return 115; } }

		[Constructable]
        public CrashAndBurn(): base(0x2D24)
		{
            Name = "Crash And Burn";
            Hue = 1359;
			Weight = 8.0;

            Attributes.AttackChance = Utility.RandomMinMax(8, 25);
            Attributes.WeaponSpeed = Utility.RandomMinMax(4, 17);
            Attributes.SpellChanneling = 1;
            Attributes.Luck = Utility.RandomMinMax(75, 100);
            Attributes.RegenStam = Utility.RandomMinMax(1, 3);
            WeaponAttributes.HitFireArea = Utility.RandomMinMax(5, 12);

            LootType = LootType.Regular;
		}

        public CrashAndBurn(Serial serial): base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}